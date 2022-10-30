using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameObjectPool : MonoBehaviour
{
    public class GameObjectInfo
    {
        public GameObject gameObject;

        public float releaseTime;
    }

    private Dictionary<string, Queue<GameObjectInfo>> m_poolMap = new Dictionary<string, Queue<GameObjectInfo>>();

    private List<string> m_releaseKeyList = new List<string>();

    private int m_frameCount;
    
    private float m_lifeDuration = 300;

    private Vector3 m_releasePos = new Vector3(9999, 9999, 9999);

    public UnityAction<GameObject> constructor;

    public UnityAction<GameObject> destructor;

    public void Get(string assetName, UnityAction<GameObject> callBack)
    {
        if (m_poolMap.TryGetValue(assetName, out Queue<GameObjectInfo> queue) && queue.Count > 0)
        {
            GameObjectInfo info = queue.Dequeue();
            if (queue.Count < 1)
            {
                QueuePool<GameObjectInfo>.Release(m_poolMap[assetName]);
                m_poolMap.Remove(assetName);
            }
            callBack?.Invoke(info.gameObject);
            constructor?.Invoke(info.gameObject);
        }
        else
        {
            AssetLoader loader = AssetUtility.LoadAssetAsync<GameObject>(assetName);
            loader.onComplete = (p) =>
            {
                GameObject go = Instantiate(p.rawObject as GameObject);
                callBack?.Invoke(go);
                constructor?.Invoke(go);
            };
        }
    }

    public void Release(string assetName, GameObject item)
    {
        item.transform.SetParent(transform);
        item.transform.position = m_releasePos;
        if (m_poolMap.ContainsKey(assetName))
        {
            GameObjectInfo info = Pool<GameObjectInfo>.Get();
            info.gameObject = item;
            info.releaseTime = Time.realtimeSinceStartup;
            destructor?.Invoke(item);
            m_poolMap[assetName].Enqueue(info);
        }
        else
        {
            Queue<GameObjectInfo> queue = QueuePool<GameObjectInfo>.Get();
            GameObjectInfo info = Pool<GameObjectInfo>.Get();
            info.gameObject = item;
            info.releaseTime = Time.realtimeSinceStartup;
            destructor?.Invoke(item);
            queue.Enqueue(info);
            m_poolMap.Add(assetName, queue);
        }
    }


    void Update()
    {
        if (++m_frameCount % 60 == 0 && m_poolMap.Count > 0)
        {
            float nowTime = Time.realtimeSinceStartup;

            foreach (var pool in m_poolMap)
            {
                if (pool.Value.Count > 0)
                {
                    GameObjectInfo info = pool.Value.Peek();
                    while (nowTime - info.releaseTime >= m_lifeDuration)
                    {
                        pool.Value.Dequeue();

                        Destroy(info.gameObject);
                        info.gameObject = null;
                        info.releaseTime = -1;
                        Pool<GameObjectInfo>.Release(info);

                        if (pool.Value.Count > 0)
                            info = pool.Value.Peek();
                        else
                            break;

                        if (pool.Value.Count < 1)
                            m_releaseKeyList.Add(pool.Key);
                    }

                }
            }

            if (m_releaseKeyList.Count > 0)
            {
                foreach (string key in m_releaseKeyList)
                {
                    QueuePool<GameObjectInfo>.Release(m_poolMap[key]);
                    m_poolMap.Remove(key);
                }
                m_releaseKeyList.Clear();
            }
        }
    }
}
