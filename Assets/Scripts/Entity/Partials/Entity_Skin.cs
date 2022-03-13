using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Entity
{
    public Transform skinNode { get; private set; }

    public BoxCollider2D boxCollider { get; private set; }

    public Rigidbody2D rigidbody { get; private set; }

    public Avatar mainAvatar { get; private set; }

    public Dictionary<Avatar.AvatarPartType, ModelInfo> models = new Dictionary<Avatar.AvatarPartType, ModelInfo>();

    public struct ModelInfo
    {
        public int modelCode;

        public string des;

        public string modelPath;

        public float modelScale;

        public float modelPositionX;

        public float modelPositionY;

        public float modelPositionZ;
    }


    /// <summary>
    /// 创建载体
    /// </summary>
    private void Skin_CreateAvatar()
    {
        skinInitialized = true;

        if(skinNode == null)
        {
            skinNode = new GameObject("Skins").transform;
            skinNode.SetParent(transform);
        }

        if (boxCollider == null)
        {
            boxCollider = gameObject.AddComponent<BoxCollider2D>();
            boxCollider.offset = new Vector2(0, 0.08f);
            boxCollider.size = new Vector2(0.5f, 0.1f);
        }

        if (rigidbody == null)
        {
            rigidbody = gameObject.AddComponent<Rigidbody2D>();
            rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            rigidbody.gravityScale = 0;
        }

        if (mainAvatar == null)
        {
            GameObject go = new GameObject("MainAvatar");
            mainAvatar = go.AddComponent<Avatar>();
            go.transform.SetParent(skinNode);
            //加载完成后添加组件
            mainAvatar.onAvatarLoadComplete += AssembleComponent;
        }

        //初始化完载体加载各个皮肤部件
        Init_Skin();
    }

    /// <summary>
    /// 初始化皮肤部件
    /// </summary>
    private void Init_Skin()
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

    private void Skin_SetAvatarPart(Avatar.AvatarPartType partType, Dictionary<Avatar.AvatarPartType, ModelInfo> modelInfo)
    {
        if (mainAvatar == null || !modelInfo.ContainsKey(partType)) return;
        AvatarPart part = mainAvatar.AddPart(partType);
        part.assetName = modelInfo[partType].modelPath;
        part.fashionCode = modelInfo[partType].modelCode;
        part.position = new Vector3(modelInfo[partType].modelPositionX, modelInfo[partType].modelPositionY, -modelInfo[partType].modelPositionZ);
        part.scale = Vector3.one * modelInfo[partType].modelScale;
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
        if (mainAvatar == null) return;
        mainAvatar.transform.localPosition = position;
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
    }
}
