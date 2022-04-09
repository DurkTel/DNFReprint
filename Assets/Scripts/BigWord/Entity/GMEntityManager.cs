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

    private static GMCullingGroup m_entityCullingGroup;

    private Dictionary<int, Entity> m_entityMap = new Dictionary<int, Entity>();

    private List<Entity> m_waitCreateList = new List<Entity>();
    public Dictionary<int ,Entity> entityMap { get { return m_entityMap; } }
    public Entity localPlayer { get; private set; }
    public static void Initialize()
    {
        m_transform = new GameObject("GMEntityManager").transform;
        m_transform.gameObject.AddComponent<GMEntityManager>();
        m_actives = new GameObject("GMEntity_Actives").transform;
        m_actives.SetParent(m_transform);
        m_pool = new GameObject("GMEntity_Pool").transform;
        m_pool.SetParent(m_transform);

        m_entityCullingGroup = m_transform.gameObject.AddComponent<GMCullingGroup>();
        m_entityCullingGroup.targetCamera = OrbitCamera.regularCamera;

        DontDestroyOnLoad(m_transform.gameObject);
    }

    private void FixedUpdate()
    {
        float fixedDeltaTime = Time.fixedDeltaTime;
        foreach (Entity entity in m_entityMap.Values)
        {
            //fixedUpdate跑移动等逻辑 未加载完不执行
            if (entity.mainAvatar != null && entity.mainAvatar.loadCompleted)
                entity.FixedUpdate(fixedDeltaTime);
        }
    }

    private void Update()
    {
        Entity entity = null;
        float deltaTime = Time.deltaTime;
        foreach (var item in m_entityMap)
        {
            entity = item.Value;
            entity.Update(deltaTime);
            //已经初始化皮肤 直接跳出
            if (entity.skinInitialized)
                continue;

            if (entity.skinInitFrameCount > 0 && !m_waitCreateList.Contains(entity))
            {
                m_waitCreateList.Add(entity);
            }
            else if (entity.skinInitFrameCount <= 0 && m_waitCreateList.Contains(entity))
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
    }

    public Entity CreateEntity(Entity.EntityType etype, CommonDefine.Career career)
    {
        if (etype == Entity.EntityType.LocalPlayer && localPlayer != null)
        {
            Debug.LogError("正在尝试创建多个LocalPlayer！！！");
            return null;
        }
        Entity entity = Pool<Entity>.Get();
        GameObject go = Pool<GameObject>.Get();
        int eid = GUID;
        entity.Init(eid, etype, career, go);
        entity.transform.SetParent(m_actives);
        entity.transform.localPosition = Vector3.zero;
        m_entityMap.Add(eid, entity);
        if (etype == Entity.EntityType.LocalPlayer)
            localPlayer = entity;
        else
            m_entityCullingGroup.AddCullingObject(entity);
        return entity;
    }

    public bool ReleaseEntity(int entityId)
    {

        if (m_entityMap.ContainsKey(entityId))
        {
            Entity entity = m_entityMap[entityId];
            m_entityMap.Remove(entityId);
            if (m_waitCreateList.Contains(entity))
                m_waitCreateList.Remove(entity);

            m_entityCullingGroup.RemoveCullingObject(entity);
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
