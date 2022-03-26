using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class Test111 : MonoBehaviour
{
    // Start is called before the first frame update

    Dictionary<Avatar.AvatarPartType, Entity.ModelInfo> modelInfo = new Dictionary<Avatar.AvatarPartType, Entity.ModelInfo>();
    void Start()
    {

        //Entity entity = GMEntityManager.Instance.CreateEntity(Entity.EntityType.LocalPlayer,CommonDefine.Career.Swordsman);
        //modelInfo.Add(Avatar.AvatarPartType.body, ModelConfig.GetInfoByCode(10000));
        //modelInfo.Add(Avatar.AvatarPartType.hair, ModelConfig.GetInfoByCode(10001));
        //modelInfo.Add(Avatar.AvatarPartType.pants, ModelConfig.GetInfoByCode(10002));
        //modelInfo.Add(Avatar.AvatarPartType.pantsEx, ModelConfig.GetInfoByCode(10003));
        //modelInfo.Add(Avatar.AvatarPartType.shirt, ModelConfig.GetInfoByCode(10004));
        //modelInfo.Add(Avatar.AvatarPartType.shoes, ModelConfig.GetInfoByCode(10005));
        //modelInfo.Add(Avatar.AvatarPartType.shoesEx, ModelConfig.GetInfoByCode(10006));
        //modelInfo.Add(Avatar.AvatarPartType.weapon, ModelConfig.GetInfoByCode(10007));
        //modelInfo.Add(Avatar.AvatarPartType.weaponEx, ModelConfig.GetInfoByCode(10008));

        //entity.models = modelInfo;
        //entity.Skin_SetVisible(true);


        //EntitySkill entitySkill = SkillConfig.GetInfoByCode(10000);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
