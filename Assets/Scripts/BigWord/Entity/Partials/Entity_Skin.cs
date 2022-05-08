using cfg.db;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Entity
{
    public Transform skinNode { get; protected set; }

    public Transform rootBone { get; protected set; }

    public BoxCollider2D boxCollider { get; protected set; }

    public Rigidbody2D rigidbody { get; protected set; }

    public Avatar mainAvatar { get; protected set; }

    private Dictionary<string, Transform> m_allBones;
    public Dictionary<string,Transform> allBones
    {
        get
        {
            if (m_allBones == null)
                m_allBones = new Dictionary<string, Transform>();
            if (m_allBones.Count < 1 && rootBone != null)
            {
                Transform[] bones = rootBone.GetComponentsInChildren<Transform>();
                foreach (Transform bone in bones)
                {
                    m_allBones.Add(bone.name, bone);
                }
            }


            return m_allBones;
        }
    }

    public Dictionary<Avatar.AvatarPartType, ModelInfoCfg> models = new Dictionary<Avatar.AvatarPartType, ModelInfoCfg>();


    /// <summary>
    /// 创建载体
    /// </summary>
    protected virtual void Skin_CreateAvatar()
    {
        skinInitialized = true;

        gameObject.layer = 8;

        if(skinNode == null)
        {
            skinNode = new GameObject("Skins").transform;
            skinNode.SetParent(transform);
            skinNode.transform.localPosition = Vector3.zero;
        }
        m_collidersXY_parent.SetParent(skinNode);
        m_collidersZ_parent.SetParent(transform);



        if (mainAvatar == null)
        {
            GameObject go = new GameObject("MainAvatar");
            mainAvatar = go.AddComponent<Avatar>();
            go.transform.SetParent(skinNode);
            go.transform.localPosition = Vector3.zero;
            //加载完成后添加组件
            mainAvatar.onAvatarLoadComplete += AssembleComponent;
        }

    }

    /// <summary>
    /// 初始化皮肤部件
    /// </summary>
    protected void Init_Skin()
    {
        UpdateSkinAll();
    }

    public void UpdateSkinAll()
    {
        Update_Skin_Body();
        Update_Skin_Shirt();
        Update_Skin_Weapon();
        Update_Skin_Hair();
        Update_Skin_Pants();
        Update_Skin_Shoes();
    }

    public void Update_Skin_Body()
    {
        Skin_SetAvatarPart(Avatar.AvatarPartType.body, models);
    }

    public void Update_Skin_Shirt()
    {
        Skin_SetAvatarPart(Avatar.AvatarPartType.shirt, models);

    }

    public void Update_Skin_Weapon()
    {
        Skin_SetAvatarPart(Avatar.AvatarPartType.weapon, models);
        Skin_SetAvatarPart(Avatar.AvatarPartType.weaponEx, models);

    }

    public void Update_Skin_Hair()
    {
        Skin_SetAvatarPart(Avatar.AvatarPartType.hair, models);

    }

    public void Update_Skin_Pants()
    {
        Skin_SetAvatarPart(Avatar.AvatarPartType.pants, models);
        Skin_SetAvatarPart(Avatar.AvatarPartType.pantsEx, models);

    }

    public void Update_Skin_Shoes()
    {
        Skin_SetAvatarPart(Avatar.AvatarPartType.shoes, models);
        Skin_SetAvatarPart(Avatar.AvatarPartType.shoesEx, models);
    }

    private void Skin_SetAvatarPart(Avatar.AvatarPartType partType, Dictionary<Avatar.AvatarPartType, ModelInfoCfg> modelInfo)
    {
        if (mainAvatar == null || !modelInfo.ContainsKey(partType)) return;
        AvatarPart part = mainAvatar.AddPart(partType);
        ModelInfoCfg info = modelInfo[partType];
        part.assetName = info.ModelPath;
        part.fashionCode = info.Id;
        part.position = new Vector3(info.ModelPositionX, info.ModelPositionY, info.ModelPositionZ);
        part.scale = Vector3.one * info.ModelScale;
        part.sort = info.Sort;
        part.boneTransform = allBones.ContainsKey(info.BoneName) ? allBones[info.BoneName] : mainAvatar.transform;
    }

    public void Skin_SetAvatarPartScale(Avatar.AvatarPartType partType, Vector3 scale)
    {
        if (mainAvatar == null) return;
        AvatarPart part = mainAvatar.GetPart(partType);
        part.partNode.localScale = scale;
    }

    public void Skin_SetAvatarPartPosition(Avatar.AvatarPartType partType, Vector3 position)
    {
        if (mainAvatar == null) return;
        AvatarPart part = mainAvatar.GetPart(partType);
        part.partNode.localPosition = position;
    }

    public void Skin_SetAvatarPosition(Vector3 position)
    {
        transform.localPosition = position;
    }

    public void Skin_SetVisible(bool visible)
    {
        if (!skinInitialized)
            skinInitFrameCount = visible ? Time.frameCount : 0;

        if (skinNode == null) return;

        if (visible)
            skinNode.localPosition = new Vector3(0, 0, 0);
        else
            skinNode.position = new Vector3(0, -99999, 0);

        PauseAni(!visible);
        updateColliderEnabled = visible;

    }

    private void ReleaseSkin()
    {
        skinInitialized = false;
        m_allBones = null;
        if (mainAvatar) mainAvatar.Clear();
        if (rootBone != null)
            Object.Destroy(rootBone.gameObject);//先销毁 后面做成对象池回收
        current_animationData = null;
        m_renenderSprites.Clear();
    }
}
