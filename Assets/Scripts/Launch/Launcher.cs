using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Launcher : MonoBehaviour
{
    Dictionary<Avatar.AvatarPartType, Entity.ModelInfo> modelInfo;
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        print("游戏启动！进入加载流程");


        StartCoroutine(LaunchGame(() => {
            modelInfo = new Dictionary<Avatar.AvatarPartType, Entity.ModelInfo>();
            modelInfo.Add(Avatar.AvatarPartType.body, ModelConfig.GetInfoByCode(10000));
            modelInfo.Add(Avatar.AvatarPartType.hair, ModelConfig.GetInfoByCode(10001));
            modelInfo.Add(Avatar.AvatarPartType.pants, ModelConfig.GetInfoByCode(10002));
            modelInfo.Add(Avatar.AvatarPartType.pantsEx, ModelConfig.GetInfoByCode(10003));
            modelInfo.Add(Avatar.AvatarPartType.shirt, ModelConfig.GetInfoByCode(10004));
            modelInfo.Add(Avatar.AvatarPartType.shoes, ModelConfig.GetInfoByCode(10005));
            modelInfo.Add(Avatar.AvatarPartType.shoesEx, ModelConfig.GetInfoByCode(10006));
            modelInfo.Add(Avatar.AvatarPartType.weapon, ModelConfig.GetInfoByCode(10007));
            modelInfo.Add(Avatar.AvatarPartType.weaponEx, ModelConfig.GetInfoByCode(10008));

            Entity entity = GMEntityManager.Instance.CreateEntity(Entity.EntityType.LocalPlayer, CommonUtility.Career.Swordsman);
            entity.models = modelInfo;
            entity.Skin_SetVisible(true);
            Entity entity2 = GMEntityManager.Instance.CreateEntity(Entity.EntityType.OtherPlayer, CommonUtility.Career.Swordsman);
            entity2.models = modelInfo;
            //id = entity2.entityId;
            //entity2.Skin_SetAvatarPosition(new Vector3(30, 0, 0));
            //entity2.Skin_SetVisible(true);
            //entity2.gameObject.AddComponent<Test111>().entity = entity2;

        }));
    }

    int id;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {

            Entity entity2 = GMEntityManager.Instance.CreateEntity(Entity.EntityType.OtherPlayer, CommonUtility.Career.Swordsman);
            entity2.models = modelInfo;
            id = entity2.entityId;
            //entity2.Skin_SetVisible(true);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            GMEntityManager.Instance.ReleaseEntity(id);
        }
    }

    /// <summary>
    /// 异步加载运行游戏所需的组件
    /// </summary>
    /// <returns></returns>
    private IEnumerator LaunchGame(UnityAction callback = null)
    {
        OrbitCamera.Initialize();
        yield return null;

        GMScenesManager.Initialize();
        yield return null;

        GMEntityManager.Initialize();
        yield return null;


        callback?.Invoke();

    }
}
