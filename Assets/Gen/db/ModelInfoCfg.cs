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

public sealed partial class ModelInfoCfg :  Bright.Config.BeanBase 
{
    public ModelInfoCfg(JSONNode _json) 
    {
        { if(!_json["id"].IsNumber) { throw new SerializationException(); }  Id = _json["id"]; }
        { if(!_json["des"].IsString) { throw new SerializationException(); }  Des = _json["des"]; }
        { if(!_json["modelPath"].IsString) { throw new SerializationException(); }  ModelPath = _json["modelPath"]; }
        { if(!_json["modelScale"].IsNumber) { throw new SerializationException(); }  ModelScale = _json["modelScale"]; }
        { if(!_json["modelPositionX"].IsNumber) { throw new SerializationException(); }  ModelPositionX = _json["modelPositionX"]; }
        { if(!_json["modelPositionY"].IsNumber) { throw new SerializationException(); }  ModelPositionY = _json["modelPositionY"]; }
        { if(!_json["modelPositionZ"].IsNumber) { throw new SerializationException(); }  ModelPositionZ = _json["modelPositionZ"]; }
        { if(!_json["boneName"].IsString) { throw new SerializationException(); }  BoneName = _json["boneName"]; }
        { if(!_json["sort"].IsNumber) { throw new SerializationException(); }  Sort = _json["sort"]; }
        PostInit();
    }

    public ModelInfoCfg(int id, string des, string modelPath, float modelScale, float modelPositionX, float modelPositionY, float modelPositionZ, string boneName, int sort ) 
    {
        this.Id = id;
        this.Des = des;
        this.ModelPath = modelPath;
        this.ModelScale = modelScale;
        this.ModelPositionX = modelPositionX;
        this.ModelPositionY = modelPositionY;
        this.ModelPositionZ = modelPositionZ;
        this.BoneName = boneName;
        this.Sort = sort;
        PostInit();
    }

    public static ModelInfoCfg DeserializeModelInfoCfg(JSONNode _json)
    {
        return new db.ModelInfoCfg(_json);
    }

    /// <summary>
    /// 模型id
    /// </summary>
    public int Id { get; private set; }
    /// <summary>
    /// 描述
    /// </summary>
    public string Des { get; private set; }
    /// <summary>
    /// 资源路径
    /// </summary>
    public string ModelPath { get; private set; }
    /// <summary>
    /// 缩放
    /// </summary>
    public float ModelScale { get; private set; }
    /// <summary>
    /// 位置X
    /// </summary>
    public float ModelPositionX { get; private set; }
    /// <summary>
    /// 位置Y
    /// </summary>
    public float ModelPositionY { get; private set; }
    /// <summary>
    /// 位置Z
    /// </summary>
    public float ModelPositionZ { get; private set; }
    /// <summary>
    /// 绑定的骨骼
    /// </summary>
    public string BoneName { get; private set; }
    /// <summary>
    /// 排序层级
    /// </summary>
    public int Sort { get; private set; }

    public const int __ID__ = -509880067;
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
        + "Des:" + Des + ","
        + "ModelPath:" + ModelPath + ","
        + "ModelScale:" + ModelScale + ","
        + "ModelPositionX:" + ModelPositionX + ","
        + "ModelPositionY:" + ModelPositionY + ","
        + "ModelPositionZ:" + ModelPositionZ + ","
        + "BoneName:" + BoneName + ","
        + "Sort:" + Sort + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}
