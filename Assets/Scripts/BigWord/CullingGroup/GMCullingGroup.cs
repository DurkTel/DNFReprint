using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GMCullingGroup : MonoBehaviour, IDisposable
{
    public interface ICulling
    {
        GMCullingGroup cullingGroup { get; set; }
        void OnGMCullingVisible(bool visible);
        void OnGMCullingDistance(int lod, int lodMax);
    }

    [SerializeField]
    private int m_capacitySize = 512;

    public CullingGroup cullingGroup { get; private set; }

    private BoundingSphere[] m_boundingSpheres;

    private ICulling[] m_ICullings;

    private int m_cullingIndex;

    [SerializeField]
    private float[] m_distances = new float[] { 2, 5, 10 };

    [SerializeField]
    private Camera m_targetCamera = null;

    [SerializeField]
    private Transform m_referenceTransform = null;

    private Dictionary<ICulling, int> m_cullingObjectDic;
    public float[] distances { get { return m_distances; } set { m_distances = value; cullingGroup.SetBoundingDistances(value); } }
    public UnityAction<ICulling, bool> onVisibleEvent { get; set; }
    public UnityAction<ICulling, int, int> onDistanceEvent { get; set; }

    public Camera targetCamera
    {
        get { return m_targetCamera; }
        set
        {
            m_targetCamera = value;
            cullingGroup.targetCamera = value;
            if (m_targetCamera && !m_referenceTransform)
                m_referenceTransform = m_targetCamera.transform;
        }
    }

    public Transform referenceTransform
    {
        get { return m_referenceTransform; }
        set
        {
            m_referenceTransform = value;
            cullingGroup.SetDistanceReferencePoint(m_referenceTransform);
        }
    }

    private void Awake()
    {
        cullingGroup = new CullingGroup();
        cullingGroup.enabled = true;
        cullingGroup.onStateChanged += StateChanged;

        m_cullingObjectDic = new Dictionary<ICulling, int>();

        //按最大数值初始化
        m_boundingSpheres = new BoundingSphere[m_capacitySize];
        Vector3 pos = new Vector3(0, -99999, 0);
        for (int i = 0; i < m_boundingSpheres.Length; i++)
            m_boundingSpheres[i].position = pos; //放在一边备用
        m_ICullings = new ICulling[m_capacitySize];
        cullingGroup.SetBoundingSpheres(m_boundingSpheres);
        cullingGroup.SetBoundingSphereCount(m_capacitySize);
        cullingGroup.SetBoundingDistances(m_distances);
        cullingGroup.targetCamera = m_targetCamera;
        if (m_targetCamera && !m_referenceTransform)
            m_referenceTransform = m_targetCamera.transform;
        cullingGroup.SetDistanceReferencePoint(m_referenceTransform);
    }

    private void Update()
    {
        //测试代码
        cullingGroup.SetDistanceReferencePoint(m_referenceTransform);
    }


    private void StateChanged(CullingGroupEvent sphere)
    {
        if (sphere.isVisible && sphere.hasBecomeVisible)
        {
            ICulling culling = m_ICullings[sphere.index];
            if (culling != null)
            {
                culling.OnGMCullingVisible(true);
                onVisibleEvent?.Invoke(culling, true);
            }
        }
        else if (sphere.hasBecomeInvisible)
        {
            ICulling culling = m_ICullings[sphere.index];
            if (culling != null)
            {
                culling.OnGMCullingVisible(false);
                onVisibleEvent?.Invoke(culling, false);
            }
        }

        if (sphere.previousDistance != sphere.currentDistance && m_distances != null && m_distances.Length > 0)
        {
            ICulling culling = m_ICullings[sphere.index];
            if (culling != null)
            {
                culling.OnGMCullingDistance(sphere.currentDistance, m_distances.Length);
                onDistanceEvent?.Invoke(culling, sphere.currentDistance, m_distances.Length);
            }
        }
    }

    public void AddCullingObject(ICulling cullingObject)
    {
        if (m_cullingObjectDic.ContainsKey(cullingObject))
        {
            Debug.LogErrorFormat("剔除组以添加过该物体 {0}", cullingObject);
            return;
        }

        if (m_ICullings[m_cullingIndex] != null)
        {
            for (int i = 0; i < m_ICullings.Length; i++)
            {
                if (m_ICullings[i] == null)
                {
                    m_cullingIndex = i;
                    break;
                }
            }

            if (m_ICullings[m_cullingIndex] != null)
            {
                Debug.LogErrorFormat("剔除组空间不足 {0}", m_cullingIndex);
                return;
            }
        }
        m_ICullings[m_cullingIndex] = cullingObject;
        m_cullingObjectDic.Add(cullingObject, m_cullingIndex);
        cullingObject.cullingGroup = this;
    }

    public void RemoveCullingObject(ICulling cullingObject)
    {
        int index = GetCullingObjectIndex(cullingObject);
        if (index < 0 || index > m_boundingSpheres.Length)
        {
            return;
        }
        m_boundingSpheres[index].position = new Vector3(0, -999999, 0);
        m_ICullings[index] = null;
        m_cullingObjectDic.Remove(cullingObject);
        cullingObject.cullingGroup = null;
    }

    public void UpdateBoundingSphere(ICulling cullingObject, Vector3 pos, float radius)
    {
        int index = GetCullingObjectIndex(cullingObject);
        if (index < 0 || index > m_boundingSpheres.Length)
        {
            Debug.LogErrorFormat("尝试刷新一个超出索引的剔除{0}", index);
            return;
        }
        m_boundingSpheres[index].position = pos;
        m_boundingSpheres[index].radius = radius;

    }

    public int GetDistance(ICulling cullingObject)
    {
        int index = GetCullingObjectIndex(cullingObject);
        if (index < 0 || index > m_boundingSpheres.Length)
        {
            return -1;
        }
        return cullingGroup.GetDistance(index);

    }

    public bool IsVisible(ICulling cullingObject)
    {
        int index = -1;
        if (m_cullingObjectDic.TryGetValue(cullingObject, out index))
            return cullingGroup.IsVisible(index);

        return false;
    }

    private int GetCullingObjectIndex(ICulling cullingObject)
    {
        int index = -1;
        m_cullingObjectDic.TryGetValue(cullingObject, out index);
        return index;
    }

    public void Dispose()
    {
        if (cullingGroup != null)
        {
            cullingGroup.onStateChanged -= StateChanged;
            cullingGroup.Dispose();
            cullingGroup = null;
        }

    }

    private void OnDestroy()
    {
        Dispose();
    }

    #region 辅助线

    private void OnDrawGizmos()
    {
        foreach (var item in m_boundingSpheres)
        {
            if (item.radius != 0f)
                Gizmos.DrawWireSphere(item.position, item.radius);
        }
    }

    #endregion
}
