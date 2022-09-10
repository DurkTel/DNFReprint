using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMEntityHotRadius : MonoBehaviour
{
    public class HotRadiusGroup
    {
        public bool change;
        public DictionaryEx<int, bool> enterIds = new DictionaryEx<int, bool>();
        public DictionaryEx<int, bool> exitIds = new DictionaryEx<int, bool>();
    }

    public bool pause;
    public Action onHotGroupChange { get; set; }

    private bool m_isChange;

    private float m_resetTime = 20f;

    private float m_lastResetTitme = 0f;

    private DictionaryEx<int, HotRadiusGroup> m_hotRadiusMap = new DictionaryEx<int, HotRadiusGroup>();
    public DictionaryEx<int,HotRadiusGroup> hotRadiusMap { get { return m_hotRadiusMap; } }

    public void UpdateByEntity(Entity entity)
    {
        Entity localPlayer = GMEntityManager.localPlayer;
        if (localPlayer == null || entity.hotRadius == null || entity.hotRadius[0] <= 0f && entity.hotRadius[1] <= 0f && entity.hotRadius[2] <= 0f || entity == localPlayer)
            return;

        for (int i = 0; i < entity.hotRadius.Length; i++)
        {
            if (entity.hotRadius[i] > 0f)
            {
                HotRadiusGroup hotRadius;
                if (!m_hotRadiusMap.TryGetValue(i, out hotRadius))
                {
                    hotRadius = new HotRadiusGroup();
                    m_hotRadiusMap.Add(i, hotRadius);
                }

                float dis = Vector2.Distance(entity.transform.position, localPlayer.transform.position);
                if (dis <= entity.hotRadius[i])
                {
                    //在热半径范围内，但不在进入列表
                    //代表新进入
                    int entityId = entity.entityId;
                    if (!hotRadius.enterIds.ContainsKey(entityId))
                    {
                        m_isChange = true;
                        hotRadius.enterIds.Add(entityId,true);
                        hotRadius.change = true;

                        //新增入进入范围 代表不在离开的列表
                        if (hotRadius.exitIds.ContainsKey(entityId))
                        {
                            hotRadius.exitIds.Remove(entityId);
                        }
                    }
                }
                else
                {
                    //不在热半径内，但也不在离开列表
                    int entityId = entity.entityId;
                    if (!hotRadius.exitIds.ContainsKey(entityId))
                    {
                        m_isChange = true;
                        hotRadius.exitIds.Add(entityId, true);
                        hotRadius.change = true;

                        //同理
                        if (hotRadius.enterIds.ContainsKey(entityId))
                        {
                            hotRadius.enterIds.Remove(entityId);
                        }
                    }
                }
            }
        }
    }


    private void Update()
    {
        if (pause) return;
        //热半径更新不需要太频繁 5帧抛一次事件
        if (Time.frameCount % 5 == 0 && m_isChange)
        {
            m_isChange = false;

            onHotGroupChange?.Invoke();

            for (int i = 0; i < m_hotRadiusMap.keyList.Count; i++)
            {
                HotRadiusGroup group = m_hotRadiusMap[m_hotRadiusMap.keyList[i]];
                for (int a = 0; a < group.enterIds.keyList.Count; a++)
                {
                    group.enterIds[group.enterIds.keyList[a]] = false;
                }

                for (int a = 0; a < group.exitIds.keyList.Count; a++)
                {
                    group.exitIds[group.exitIds.keyList[a]] = false;
                }
            }
        }

        if (Time.realtimeSinceStartup - m_lastResetTitme  > m_resetTime && m_hotRadiusMap.Count > 0)
        {
            m_lastResetTitme = Time.realtimeSinceStartup;
            for (int i = 0; i < m_hotRadiusMap.keyList.Count; i++)
            {
                HotRadiusGroup group = m_hotRadiusMap[m_hotRadiusMap.keyList[i]];
                if (group.exitIds.Count > 0)
                    group.exitIds.Clear();
            }
        }

    }

    private void OnDestroy()
    {
        onHotGroupChange = null;
    }

}
