using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public partial class Entity
{
    private DictionaryEx<int, GameObject> m_effectMap = new DictionaryEx<int, GameObject>();

    private GameObjectPool m_effectPool;

    private void UpdateEffect()
    {
        SpiteAnimator animator = null;
        for (int i = 0; i < m_effectMap.keyList.Count; i++)
        {
            int id = m_effectMap.keyList[i];
            GameObject go = m_effectMap[id];
            if (go.TryGetComponent(out animator))
            {
                animator.spriteRenderer.flipX = curFlip == -1;
            }

        }
    }

    public void Add_BoneEffect(int boneName, string effectName, Vector3 pos)
    {
        AvatarPart part = null;
        if (mainAvatar.avatarPartDic.TryGetValue(boneName, out part) && !m_effectMap.ContainsKey(boneName))
        {
            Transform bone = part.boneTransform;
            if (m_effectPool == null)
                m_effectPool = GMPoolManager.Instance.TryGet("effect");
            m_effectPool.Get(effectName, (p) =>
            {
                p.transform.SetParentNew(bone, pos);
                m_effectMap.Add(boneName, p);
            });
        }
    }

    public void Remove_BoneEffect(int boneName, string effectName)
    {
        GameObject effect = null;
        if (m_effectMap.TryGetValue(boneName, out effect))
        {
            if (m_effectPool == null)
                m_effectPool = GMPoolManager.Instance.TryGet("effect");
            m_effectPool.Release(effectName, effect);
            m_effectMap.Remove(boneName);
        }
    }

    public GameObject Get_BoneEffect(int boneName)
    {
        if (m_effectMap.ContainsKey(boneName))
            return m_effectMap[boneName];

        return null;
    }
}
