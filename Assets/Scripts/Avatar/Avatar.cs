using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public Dictionary<AvatarPartType, AvatarPart> avatarPartDic { get; private set; }

    private List<AvatarPart> m_waitLoadPartList;

    public UnityAction onAvatarLoadComplete;
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
        Debug.Log(string.Format("开始异步加载：开始时间：{0}", Time.realtimeSinceStartup));

        yield return null;
        while (m_waitLoadPartList.Count > 0)
        {
            AvatarPart part = m_waitLoadPartList[0];
            yield return part.LoadAsset();
            if (part.loadComplete)
                m_waitLoadPartList.RemoveAt(0);
            //Debug.Log(string.Format("异步加载完成{0}：结束帧数：{1}", part.partName, Time.frameCount));
        }
        Debug.Log(string.Format("异步加载完成：结束时间：{0}", Time.realtimeSinceStartup));
        onAvatarLoadComplete?.Invoke();
        loadCoroutine = null;
    }
    public void RefreshPart(AvatarPartType partType)
    {
        if (m_waitLoadPartList == null)
            m_waitLoadPartList = new List<AvatarPart>();
        AvatarPart part = GetPart(partType);
        if (!m_waitLoadPartList.Contains(part))
            m_waitLoadPartList.Add(part);

        ActivedLoadCoroutine();
    }

    public AvatarPart AddPart(AvatarPartType partType)
    {
        if (avatarPartDic == null)
            avatarPartDic = new Dictionary<AvatarPartType, AvatarPart>();
        AvatarPart part;
        if (!avatarPartDic.TryGetValue(partType, out part))
        {
            part = new AvatarPart(this, partType);
            avatarPartDic.Add(partType, part);
        }
        return part;
    }

    public AvatarPart GetPart(AvatarPartType partType)
    {
        return avatarPartDic != null && avatarPartDic.ContainsKey(partType) ? avatarPartDic[partType] : null;
    }
}
