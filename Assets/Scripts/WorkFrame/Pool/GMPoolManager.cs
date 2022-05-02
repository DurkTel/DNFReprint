using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMPoolManager : MonoBehaviour
{
    private static GMPoolManager m_instance;

    public static GMPoolManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                GameObject go = new GameObject("GMPoolManager");
                m_instance = go.AddComponent<GMPoolManager>();
                DontDestroyOnLoad(go);
            }
            return m_instance;
        }
    }

    private Dictionary<string, GameObjectPool> m_allGOPool = new Dictionary<string, GameObjectPool>();

    public GameObjectPool Create(string poolName)
    {
        if (string.IsNullOrEmpty(poolName) || m_allGOPool.ContainsKey(poolName))
            return null;

        GameObject go = new GameObject(poolName);
        go.transform.SetParent(transform);
        GameObjectPool pool = go.AddComponent<GameObjectPool>();
        m_allGOPool.Add(poolName, pool);
        return pool;
    }

    public GameObjectPool Get(string poolName)
    {
        if (string.IsNullOrEmpty(poolName) || !m_allGOPool.ContainsKey(poolName))
            return null;

        return m_allGOPool[poolName];
    }

    public GameObjectPool TryGet(string poolName)
    {
        GameObjectPool pool = Get(poolName);
        if (pool == null)
            pool = Create(poolName);

        return pool;
    }
}
