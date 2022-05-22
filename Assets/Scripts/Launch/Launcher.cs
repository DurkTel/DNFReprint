using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using cfg.db;

public class Launcher : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        print("游戏启动！进入加载流程");


        StartCoroutine(LaunchGame(() =>
        {
            //modelInfo = new Dictionary<Avatar.AvatarPartType, ModelInfoCfg>();

            //modelInfo.Add(Avatar.AvatarPartType.body, MDefine.tables.TbModelInfo.Get(10000));
            //modelInfo.Add(Avatar.AvatarPartType.hair, MDefine.tables.TbModelInfo.Get(10001));
            //modelInfo.Add(Avatar.AvatarPartType.pants, MDefine.tables.TbModelInfo.Get(10002));
            //modelInfo.Add(Avatar.AvatarPartType.pantsEx, MDefine.tables.TbModelInfo.Get(10003));
            //modelInfo.Add(Avatar.AvatarPartType.shirt, MDefine.tables.TbModelInfo.Get(10004));
            //modelInfo.Add(Avatar.AvatarPartType.shoes, MDefine.tables.TbModelInfo.Get(10005));
            //modelInfo.Add(Avatar.AvatarPartType.shoesEx, MDefine.tables.TbModelInfo.Get(10006));
            //modelInfo.Add(Avatar.AvatarPartType.weapon, MDefine.tables.TbModelInfo.Get(10007));
            //modelInfo.Add(Avatar.AvatarPartType.weaponEx, MDefine.tables.TbModelInfo.Get(10008));

            //Entity entity = GMEntityManager.Instance.CreateEntity(Entity.EntityType.LocalPlayer, CommonUtility.Career.Swordsman);
            //entity.models = modelInfo;
            //entity.Skin_SetVisible(true);
            ////Entity entity2 = GMEntityManager.Instance.CreateEntity(Entity.EntityType.OtherPlayer, CommonUtility.Career.Swordsman);
            ////entity2.models = modelInfo;
            ////id = entity2.entityId;
            ////entity2.Skin_SetAvatarPosition(new Vector3(30, 0, 0));
            ////entity2.Skin_SetVisible(true);
            ////entity2.gameObject.AddComponent<Test111>().entity = entity2;
            //GMScenesManager.Instance.SwitchScene(10000);
        }));

    }

    private void Update()
    {
        
    }

    /// <summary>
    /// 异步加载运行游戏所需的组件
    /// </summary>
    /// <returns></returns>
    private IEnumerator LaunchGame(UnityAction callback = null)
    {
        MDefine.Initialize();
        yield return null;

        OrbitCamera.Initialize();
        yield return null;

        GMScenesManager.Initialize();
        yield return null;

        yield return null;
        LuaEnvironment luaEnv = gameObject.AddComponent<LuaEnvironment>();

        luaEnv.LuaMain();

        callback?.Invoke();

    }
}
