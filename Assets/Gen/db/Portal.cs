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

/// <summary>
/// 传送门
/// </summary>
public sealed partial class Portal :  Bright.Config.BeanBase 
{
    public Portal(JSONNode _json) 
    {
        { if(!_json["mapId"].IsNumber) { throw new SerializationException(); }  MapId = _json["mapId"]; }
        { if(!_json["x"].IsNumber) { throw new SerializationException(); }  X = _json["x"]; }
        { if(!_json["y"].IsNumber) { throw new SerializationException(); }  Y = _json["y"]; }
        { if(!_json["z"].IsNumber) { throw new SerializationException(); }  Z = _json["z"]; }
        { if(!_json["radius"].IsNumber) { throw new SerializationException(); }  Radius = _json["radius"]; }
        PostInit();
    }

    public Portal(int mapId, float x, float y, float z, float radius ) 
    {
        this.MapId = mapId;
        this.X = x;
        this.Y = y;
        this.Z = z;
        this.Radius = radius;
        PostInit();
    }

    public static Portal DeserializePortal(JSONNode _json)
    {
        return new db.Portal(_json);
    }

    /// <summary>
    /// 要传送的地图id
    /// </summary>
    public int MapId { get; private set; }
    /// <summary>
    /// 位置X
    /// </summary>
    public float X { get; private set; }
    /// <summary>
    /// 位置Y
    /// </summary>
    public float Y { get; private set; }
    /// <summary>
    /// 位置Z
    /// </summary>
    public float Z { get; private set; }
    /// <summary>
    /// 半径
    /// </summary>
    public float Radius { get; private set; }

    public const int __ID__ = -1088248836;
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
        + "MapId:" + MapId + ","
        + "X:" + X + ","
        + "Y:" + Y + ","
        + "Z:" + Z + ","
        + "Radius:" + Radius + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}
