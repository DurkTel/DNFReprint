using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Entity
{
    /// <summary>
    /// 实体id
    /// </summary>
    public int entityId { get; private set; }
    /// <summary>
    /// 实体类型
    /// </summary>
    public EntityType entityType { get; private set; }
    /// <summary>
    /// 皮肤是否已经初始化
    /// </summary>
    public bool skinInitialized { get; private set; }
    /// <summary>
    /// 记录开始初始化的帧数
    /// </summary>
    public int skinInitFrameCount { get; private set; }

    public GameObject gameObject;

    public Transform transform;

    public void Init(int uid, EntityType type, GameObject go)
    {
        entityId = uid;
        entityType = type;
        gameObject = go;
        transform = go.transform;

        switch (type)
        {
            case EntityType.LocalPlayer:
                gameObject.name = "local_Player";
                break;
            case EntityType.OtherPlayer:
                break;
            case EntityType.Monster:
                break;
            case EntityType.Robot:
                break;
            case EntityType.Npc:
                break;
            default:
                break;
        }
    }

    public void Update()
    {
        
    }

    public void LateUpdate()
    { 
        
    }

    public void WaitCreate()
    {
        if (!skinInitialized)
            Skin_CreateAvatar();
    }


    public enum EntityType
    { 
        LocalPlayer = 1,
        OtherPlayer = 2,
        Monster = 3,
        Robot = 4,
        Npc = 5,
    }
}
