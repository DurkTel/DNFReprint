using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarPart
{
    private string m_assetName;

    public string assetName
    {
        get
        {
            return m_assetName;
        }
        set
        {
            if (m_assetName == value)
                return;

            m_assetName = value;

            if (string.IsNullOrEmpty(m_assetName))
                Refresh();
        }
    }

    private int m_fashionCode;
    public int fashionCode
    {
        get
        {
            return m_fashionCode;
        }
        set
        {
            if (m_fashionCode == value)
                return;

            m_fashionCode = value;

            Refresh();
        }
    }

    public Transform boneTransform { get; set; }

    public RenenderSprite renender { get; private set; }

    public Avatar avatar { get; private set; }

    public Avatar.AvatarPartType partType { get; private set; }

    public bool loadComplete { get { return renender != null && renender.loadComplete; } }

    public Transform partNode { get; private set; }

    public Vector3 position { get; set; }

    public Vector3 scale { get; set; }

    public AvatarPart(Avatar avatar, Avatar.AvatarPartType partType)
    {
        this.avatar = avatar;
        this.partType = partType;
    }

    public void Refresh()
    {
        //Debug.Log(string.Format("开始异步加载{0}：开始帧数：{1}", partName, Time.frameCount));

        avatar.RefreshPart(partType);
    }

    private void OnPartLoadComplete(Avatar.AvatarPartType partType)
    {
        partNode.localPosition = position;
        partNode.localScale = scale == Vector3.zero ? Vector3.one : scale;
    }

    //外部主avatar同一调用
    public IEnumerator LoadAsset()
    {
        if (partNode == null)
        {
            GameObject go = new GameObject(partType.ToString());
            renender = go.AddComponent<RenenderSprite>();
            partNode = go.transform;
            partNode.SetParent(boneTransform);
        }

        return renender.InitSpriteAsync(assetName, fashionCode, OnPartLoadComplete);
    }

    private void ReleaseModel()
    {
        if (boneTransform)
        {
            //先销毁 后面做成对象池回收
            Object.Destroy(boneTransform.gameObject);
        }
    }

    public void Clear()
    {
        m_assetName = null;
        m_fashionCode = 0;
        partNode = null;
        renender = null;
        ReleaseModel();
    }
}
