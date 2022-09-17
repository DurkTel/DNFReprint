using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMEntityManager : SingletonMono<GMEntityManager>
{
    private static int m_GUID = 0;
    private static int GUID { get { return ++m_GUID; } }

    private static Transform m_transform;

    private static Transform m_actives;

    private static Transform m_pool;

    public static GMCullingGroup entityCullingGroup;

    public static GMUpdateCollider entityUpdateCollider;

    public static GMEntityHotRadius entityHotRadius;

    private static DictionaryEx<int, Entity> m_entityMap = new DictionaryEx<int, Entity>(); //foreach遍历添加删除会异常

    private static List<Entity> m_waitCreateList = new List<Entity>();

    public DictionaryEx<int ,Entity> entityMap { get { return m_entityMap; } }
    public static Entity localPlayer { get; set; }
    public static void Initialize()
    {
        m_transform = new GameObject("GMEntityManager").transform;
        m_transform.gameObject.AddComponent<GMEntityManager>();
        m_actives = new GameObject("GMEntity_Actives").transform;
        m_actives.SetParent(m_transform);
        m_pool = new GameObject("GMEntity_Pool").transform;
        m_pool.SetParent(m_transform);

        entityCullingGroup = m_transform.gameObject.AddComponent<GMCullingGroup>();
        entityCullingGroup.targetCamera = OrbitCamera.regularCamera;

        entityUpdateCollider = m_transform.gameObject.AddComponent<GMUpdateCollider>();

        entityHotRadius = m_transform.gameObject.AddComponent<GMEntityHotRadius>();

        DontDestroyOnLoad(m_transform.gameObject);
    }

    private void FixedUpdate()
    {
        float fixedDeltaTime = Time.fixedDeltaTime;
        Entity entity = null;
        for (int i = 0; i < m_entityMap.keyList.Count; i++)
        {
            int entityId = m_entityMap.keyList[i];
            entity = m_entityMap[entityId];
            //fixedUpdate跑移动等逻辑 未加载完不执行
            if (entity.mainAvatar != null && entity.mainAvatar.loadCompleted)
                entity.FixedUpdate(fixedDeltaTime);
        }
    }

    private void Update()
    {
        Entity entity = null;
        float deltaTime = Time.deltaTime;
        for (int i = 0; i < m_entityMap.keyList.Count; i++)
        {
            int entityId = m_entityMap.keyList[i];
            entity = m_entityMap[entityId];
            entity.Update(deltaTime);
            entityHotRadius.UpdateByEntity(entity);
            //已经初始化皮肤 直接跳出
            if (entity.skinInitialized)
                continue;

            if (entity.skinIniting && !m_waitCreateList.Contains(entity))
            {
                m_waitCreateList.Add(entity);
            }
            else if (!entity.skinIniting && m_waitCreateList.Contains(entity))
            {
                m_waitCreateList.Remove(entity);
            }
        }

        //一帧调一次生成
        if (m_waitCreateList.Count > 0 && Time.frameCount % 2 == 0)
        {
            entity = m_waitCreateList[0];
            entity.WaitCreate();
            m_waitCreateList.RemoveAt(0);
        }

        entityUpdateCollider.UpdateColliderContent();

    }

    private void LateUpdate()
    {

    }

    private void OnDestroy()
    {
        foreach (var item in m_entityMap.Values)
        {
            item.Dispose();
        }
    }

    public static Entity CreateEntity(int etype)
    {
        if (etype == EntityUnitily.LOCALPLAYER && localPlayer != null)
        {
            Debug.LogError("正在尝试创建多个LocalPlayer！！！");
            return null;
        }
        Entity entity = Pool<Entity>.Get();
        GameObject go = Pool<GameObject>.Get();
        int eid = GUID;
        entity.Init(eid, etype, CommonUtility.Career.Fighter, go);
        entity.transform.SetParent(m_actives);
        entity.transform.localPosition = Vector3.zero;
        m_entityMap.Add(eid, entity);
        entityCullingGroup.AddCullingObject(entity);

        entityUpdateCollider.AddColliderObject(eid, entity);
        return entity;
    }

    public static bool ReleaseEntity(int entityId)
    {

        if (m_entityMap.ContainsKey(entityId))
        {
            Entity entity = m_entityMap[entityId];
            m_entityMap.Remove(entityId);
            if (m_waitCreateList.Contains(entity))
                m_waitCreateList.Remove(entity);

            entityCullingGroup.RemoveCullingObject(entity);
            entityUpdateCollider.RemoveColliderObject(entityId, entity);

            entity.transform.SetParent(m_pool);
            entity.transform.localPosition = new Vector3(0, -99999, 0);
            entity.Release();

            Pool<Entity>.Release(entity);
            Pool<GameObject>.Release(entity.gameObject);

            return true;
        }


        return false;
    }

    public Entity GetEntityById(int entityId)
    {
        if (m_entityMap.ContainsKey(entityId))
        {
            return m_entityMap[entityId];
        }

        return null;
    }
}
