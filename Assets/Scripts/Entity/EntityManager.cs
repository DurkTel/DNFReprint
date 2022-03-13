using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : SingletonMono<EntityManager>
{
    private static int m_GUID = 0;
    private static int GUID { get { return ++m_GUID; } }

    private static Transform m_transform;

    private static Transform m_actives;

    private Dictionary<int, Entity> m_entityMap = new Dictionary<int, Entity>();

    public Dictionary<int ,Entity> entityMap { get { return m_entityMap; } }

    private List<Entity> m_waitCreateList = new List<Entity>();

    public static void Initialize()
    {
        m_transform = new GameObject("EntityManager").transform;
        m_transform.gameObject.AddComponent<EntityManager>();
        m_actives = new GameObject("Entity_Actives").transform;
        m_actives.SetParent(m_transform);
    }

    private void Update()
    {
        Entity entity = null;
        foreach (var item in m_entityMap)
        {
            entity = item.Value;
            entity.Update();
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
        Entity entity = new Entity();
        GameObject go = new GameObject();
        int eid = GUID;
        entity.Init(eid, etype, career, go);
        entity.transform.SetParent(m_actives);
        m_entityMap.Add(eid, entity);
        return entity;
    }
}
