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
            mainAvatar.onAvatarLoadComplete = onAvatarLoadComplete;
        }

        if (rigidbody == null)
        {
            rigidbody = gameObject.AddComponent<Rigidbody2D>();
            rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            rigidbody.sleepMode = RigidbodySleepMode2D.NeverSleep;
            rigidbody.gravityScale = 0;
            rigidbody.drag = 10f;
            rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }

        if (entityType == EntityUnitily.LOCALPLAYER || entityType == EntityUnitily.OTHERPLAYER)
        {
            if (boxCollider == null)
            {
                boxCollider = gameObject.AddComponent<BoxCollider2D>();
                boxCollider.offset = new Vector2(0, 0.08f);
                boxCollider.size = new Vector2(0.5f, 0.1f);
            }


        }

        onCreateEvent?.Invoke(entityId);
    }

    /// <summary>
    /// avatar加载完成
    /// </summary>
    private void onAvatarLoadComplete()
    {
        onLuaAvatarLoadComplete?.Invoke(entityId);
    }

    public void EnableSortSprite(float floorHeight)
    {
        SortSprite2D sortSprite2D = skinNode.gameObject.TryAddComponent<SortSprite2D>();
        sortSprite2D.floorHeight = floorHeight;
    }

    public void Skin_SetAvatarSkeleton(string boneAssetName)
    {
        if (mainAvatar == null || string.IsNullOrEmpty(boneAssetName)) return;
        //初始化完载体加载各个皮肤部件
        AssetLoader loader = AssetUtility.LoadAssetAsync<GameObject>(boneAssetName);
        loader.onComplete = (p) =>
        {
            rootBone = Object.Instantiate(p.rawObject as GameObject).transform;
            rootBone.SetParent(mainAvatar.gameObject.transform);
            rootBone.localPosition = Vector3.zero;

            //加载完骨骼重新刷一下部件跟随(骨骼和部件都为异步)
            foreach (AvatarPart part in mainAvatar.avatarPartDic.Values)
            {
                Transform transform = allBones.ContainsKey(part.boneName) ? allBones[part.boneName] : mainAvatar.transform;
                part.boneTransform = transform;
                part.RefreshBoneBinding();
            }
        };
    }

    public void Skin_SetAvatarPart(int partType, string modelAssetName, string boneName)
    {
        if (mainAvatar == null || string.IsNullOrEmpty(modelAssetName)) return;
        AvatarPart part = mainAvatar.AddPart(partType);
        part.assetName = modelAssetName;
        part.boneName = boneName;
        part.boneTransform = allBones.ContainsKey(boneName) ? allBones[boneName] : mainAvatar.transform;
    }

    public void Skin_SetAvatarPartScale(int partType, float scale)
    {
        if (mainAvatar == null) return;
        AvatarPart part = mainAvatar.GetPart(partType);
        part.scale = Vector3.one * scale;
    }

    public void Skin_SetAvatarPartPosition(int partType, Vector3 position)
    {
        if (mainAvatar == null) return;
        AvatarPart part = mainAvatar.GetPart(partType);
        part.partNode.localPosition = position;
    }

    public void Skin_SetAvatarPartSort(int partType, int sort)
    {
        if (mainAvatar == null) return;
        AvatarPart part = mainAvatar.GetPart(partType);
        part.sort = sort;
    }

    public void Skin_SetAvatarPosition(Vector3 position)
    {
        transform.localPosition = position;
    }

    public void Skin_SetVisible(bool visible)
    {
        if (!skinInitialized)
            skinIniting = visible;

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
