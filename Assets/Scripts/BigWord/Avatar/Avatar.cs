using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Avatar : MonoBehaviour
{
    /// <summary>
    /// 部件枚举
    /// </summary>
    public enum AvatarPartType
    { 
        body,//身体
        shirt,//上衣
        weapon,//武器
        weaponEx,//武器额外
        hair,//头发
        pants,//裤子
        pantsEx,//裤子额外
        shoes,//鞋子
        shoesEx,//鞋子额外
    }

    private List<AvatarPart> m_waitLoadPartList;

    public bool loadCompleted { get; set; }
    public Dictionary<int, AvatarPart> avatarPartDic { get; private set; }

    public Action onAvatarLoadComplete;
    public Coroutine loadCoroutine { get; private set; }


    private void OnEnable()
    {
        ActivedLoadCoroutine();
    }

    private void ActivedLoadCoroutine()
    {
        if (m_waitLoadPartList == null || m_waitLoadPartList.Count < 1)
            return;

        if (loadCoroutine == null)
            loadCoroutine = StartCoroutine(LoadCoroutine());
    }

    private IEnumerator LoadCoroutine()
    {
        //Debug.Log(string.Format("开始异步加载avatar：开始时间：{0}", Time.realtimeSinceStartup));
        loadCompleted = false;

        yield return null;
        AvatarPart part = null;
        for (int i = 0; i < m_waitLoadPartList.Count; i++)
        {
            part = m_waitLoadPartList[i];
            StartCoroutine(part.LoadAsset());
        }
        yield return new WaitUntil(() => IsAllComplete(m_waitLoadPartList));
        m_waitLoadPartList.Clear();
        //Debug.Log(string.Format("异步加载avatar完成：结束时间：{0}", Time.realtimeSinceStartup));
        onAvatarLoadComplete?.Invoke();
        loadCoroutine = null;
        loadCompleted = true;
    }

    private bool IsAllComplete(List<AvatarPart> partList)
    {
        foreach (AvatarPart part in partList)
        {
            if (!part.loadComplete)
                return false;
        }
        return true;
    }

    public void RefreshPart(int partType)
    {
        if (m_waitLoadPartList == null)
            m_waitLoadPartList = new List<AvatarPart>();
        AvatarPart part = GetPart(partType);
        if (!m_waitLoadPartList.Contains(part))
            m_waitLoadPartList.Add(part);

        ActivedLoadCoroutine();
    }

    public AvatarPart AddPart(int partType)
    {
        if (avatarPartDic == null)
            avatarPartDic = new Dictionary<int, AvatarPart>();
        AvatarPart part;
        if (!avatarPartDic.TryGetValue(partType, out part))
        {
            part = new AvatarPart(this, partType);
            avatarPartDic.Add(partType, part);
        }
        return part;
    }

    public AvatarPart GetPart(int partType)
    {
        return avatarPartDic != null && avatarPartDic.ContainsKey(partType) ? avatarPartDic[partType] : null;
    }

    public void Clear()
    {
        //停止异步加载
        StopAllCoroutines();
        loadCoroutine = null;
        if (m_waitLoadPartList != null)
            m_waitLoadPartList.Clear();

        if (avatarPartDic == null || avatarPartDic.Count <= 0) return;
        foreach (AvatarPart part in avatarPartDic.Values)
        {
            part.Clear();
        }
    }


}
