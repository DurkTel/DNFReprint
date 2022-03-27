using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Launcher : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        print("游戏启动！进入加载流程");

        OrbitCamera.Initialize();

        GMScenesManager.Initialize();

        GMEntityManager.Initialize();

        //ResourceRequest sence = AssetLoader.LoadAsync<GameObject>("Map/1");
        //sence.completed += (p) => { Instantiate(sence.asset as GameObject); };


        Dictionary<Avatar.AvatarPartType, Entity.ModelInfo> modelInfo = new Dictionary<Avatar.AvatarPartType, Entity.ModelInfo>();
        modelInfo.Add(Avatar.AvatarPartType.body, ModelConfig.GetInfoByCode(10000));
        modelInfo.Add(Avatar.AvatarPartType.hair, ModelConfig.GetInfoByCode(10001));
        modelInfo.Add(Avatar.AvatarPartType.pants, ModelConfig.GetInfoByCode(10002));
        modelInfo.Add(Avatar.AvatarPartType.pantsEx, ModelConfig.GetInfoByCode(10003));
        modelInfo.Add(Avatar.AvatarPartType.shirt, ModelConfig.GetInfoByCode(10004));
        modelInfo.Add(Avatar.AvatarPartType.shoes, ModelConfig.GetInfoByCode(10005));
        modelInfo.Add(Avatar.AvatarPartType.shoesEx, ModelConfig.GetInfoByCode(10006));
        modelInfo.Add(Avatar.AvatarPartType.weapon, ModelConfig.GetInfoByCode(10007));
        modelInfo.Add(Avatar.AvatarPartType.weaponEx, ModelConfig.GetInfoByCode(10008));

        Entity entity = GMEntityManager.Instance.CreateEntity(Entity.EntityType.LocalPlayer, CommonDefine.Career.Swordsman);
        entity.models = modelInfo;
        entity.Skin_SetVisible(true);


        Entity entity2 = GMEntityManager.Instance.CreateEntity(Entity.EntityType.OtherPlayer, CommonDefine.Career.Swordsman);
        entity2.models = modelInfo;
        //entity2.Skin_SetVisible(true);

    }


    void Update()
    {
        
    }
}
