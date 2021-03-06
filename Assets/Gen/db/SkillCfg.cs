//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;
using SimpleJSON;



namespace cfg.db
{

public sealed partial class SkillCfg :  Bright.Config.BeanBase 
{
    public SkillCfg(JSONNode _json) 
    {
        { if(!_json["id"].IsNumber) { throw new SerializationException(); }  Id = _json["id"]; }
        { if(!_json["SameID"].IsNumber) { throw new SerializationException(); }  SameID = _json["SameID"]; }
        { if(!_json["SkillName"].IsString) { throw new SerializationException(); }  SkillName = _json["SkillName"]; }
        { if(!_json["SkillType"].IsNumber) { throw new SerializationException(); }  SkillType = _json["SkillType"]; }
        { if(!_json["SkillLevel"].IsNumber) { throw new SerializationException(); }  SkillLevel = _json["SkillLevel"]; }
        { if(!_json["NumbeOfAttacks"].IsNumber) { throw new SerializationException(); }  NumbeOfAttacks = _json["NumbeOfAttacks"]; }
        { if(!_json["NumbeOfInterval"].IsNumber) { throw new SerializationException(); }  NumbeOfInterval = _json["NumbeOfInterval"]; }
        { if(!_json["Damage"].IsNumber) { throw new SerializationException(); }  Damage = _json["Damage"]; }
        { if(!_json["CD"].IsNumber) { throw new SerializationException(); }  CD = _json["CD"]; }
        { if(!_json["Condition"].IsNumber) { throw new SerializationException(); }  Condition = _json["Condition"]; }
        { if(!_json["OpenLevel"].IsNumber) { throw new SerializationException(); }  OpenLevel = _json["OpenLevel"]; }
        { if(!_json["NeedMP"].IsNumber) { throw new SerializationException(); }  NeedMP = _json["NeedMP"]; }
        { if(!_json["NeedHP"].IsNumber) { throw new SerializationException(); }  NeedHP = _json["NeedHP"]; }
        { if(!_json["AirBorneForce"].IsNumber) { throw new SerializationException(); }  AirBorneForce = _json["AirBorneForce"]; }
        { if(!_json["CanAirBorne"].IsBoolean) { throw new SerializationException(); }  CanAirBorne = _json["CanAirBorne"]; }
        { if(!_json["AnimationDataName"].IsString) { throw new SerializationException(); }  AnimationDataName = _json["AnimationDataName"]; }
        { if(!_json["Des"].IsString) { throw new SerializationException(); }  Des = _json["Des"]; }
        { if(!_json["DamageCode"].IsNumber) { throw new SerializationException(); }  DamageCode = _json["DamageCode"]; }
        PostInit();
    }

    public SkillCfg(int id, int SameID, string SkillName, int SkillType, int SkillLevel, int NumbeOfAttacks, float NumbeOfInterval, float Damage, float CD, int Condition, int OpenLevel, float NeedMP, float NeedHP, float AirBorneForce, bool CanAirBorne, string AnimationDataName, string Des, int DamageCode ) 
    {
        this.Id = id;
        this.SameID = SameID;
        this.SkillName = SkillName;
        this.SkillType = SkillType;
        this.SkillLevel = SkillLevel;
        this.NumbeOfAttacks = NumbeOfAttacks;
        this.NumbeOfInterval = NumbeOfInterval;
        this.Damage = Damage;
        this.CD = CD;
        this.Condition = Condition;
        this.OpenLevel = OpenLevel;
        this.NeedMP = NeedMP;
        this.NeedHP = NeedHP;
        this.AirBorneForce = AirBorneForce;
        this.CanAirBorne = CanAirBorne;
        this.AnimationDataName = AnimationDataName;
        this.Des = Des;
        this.DamageCode = DamageCode;
        PostInit();
    }

    public static SkillCfg DeserializeSkillCfg(JSONNode _json)
    {
        return new db.SkillCfg(_json);
    }

    /// <summary>
    /// ??????id
    /// </summary>
    public int Id { get; private set; }
    /// <summary>
    /// ??????????????????
    /// </summary>
    public int SameID { get; private set; }
    /// <summary>
    /// ????????????
    /// </summary>
    public string SkillName { get; private set; }
    /// <summary>
    /// ????????????
    /// </summary>
    public int SkillType { get; private set; }
    /// <summary>
    /// ????????????
    /// </summary>
    public int SkillLevel { get; private set; }
    /// <summary>
    /// ????????????
    /// </summary>
    public int NumbeOfAttacks { get; private set; }
    /// <summary>
    /// ????????????
    /// </summary>
    public float NumbeOfInterval { get; private set; }
    /// <summary>
    /// ??????
    /// </summary>
    public float Damage { get; private set; }
    /// <summary>
    /// ????????????
    /// </summary>
    public float CD { get; private set; }
    /// <summary>
    /// ????????????
    /// </summary>
    public int Condition { get; private set; }
    /// <summary>
    /// ????????????
    /// </summary>
    public int OpenLevel { get; private set; }
    /// <summary>
    /// ??????
    /// </summary>
    public float NeedMP { get; private set; }
    /// <summary>
    /// ??????
    /// </summary>
    public float NeedHP { get; private set; }
    /// <summary>
    /// ?????????
    /// </summary>
    public float AirBorneForce { get; private set; }
    /// <summary>
    /// ???????????????
    /// </summary>
    public bool CanAirBorne { get; private set; }
    /// <summary>
    /// ????????????
    /// </summary>
    public string AnimationDataName { get; private set; }
    /// <summary>
    /// ??????
    /// </summary>
    public string Des { get; private set; }
    /// <summary>
    /// ??????id
    /// </summary>
    public int DamageCode { get; private set; }

    public const int __ID__ = -716421149;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "SameID:" + SameID + ","
        + "SkillName:" + SkillName + ","
        + "SkillType:" + SkillType + ","
        + "SkillLevel:" + SkillLevel + ","
        + "NumbeOfAttacks:" + NumbeOfAttacks + ","
        + "NumbeOfInterval:" + NumbeOfInterval + ","
        + "Damage:" + Damage + ","
        + "CD:" + CD + ","
        + "Condition:" + Condition + ","
        + "OpenLevel:" + OpenLevel + ","
        + "NeedMP:" + NeedMP + ","
        + "NeedHP:" + NeedHP + ","
        + "AirBorneForce:" + AirBorneForce + ","
        + "CanAirBorne:" + CanAirBorne + ","
        + "AnimationDataName:" + AnimationDataName + ","
        + "Des:" + Des + ","
        + "DamageCode:" + DamageCode + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}
