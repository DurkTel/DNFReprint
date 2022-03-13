using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class JsonManager
{
    public enum JsonType
    {
        LitJson,
        JsonUtlity
    }

    public static void SaveData(Object obj,string name, JsonType type = JsonType.LitJson)
    {
        string path = Application.persistentDataPath + "/" + name + ".json";
        
        string jsonStr = "";
        switch (type)
        {
            case JsonType.LitJson:
                jsonStr = JsonMapper.ToJson(obj);
                break;
            case JsonType.JsonUtlity:
                jsonStr = JsonUtility.ToJson(obj);
                break;
        }

        File.WriteAllText(path, jsonStr);
    }

    public static T LoadData<T>(string name, JsonType type = JsonType.LitJson) where T : new()
    {
        string path = Application.persistentDataPath + "/" + name + ".json";
        if (!File.Exists(path))
            return new T();

        string jsonStr = File.ReadAllText(path);

        T data = default(T);
        switch (type)
        {
            case JsonType.LitJson:
                data = JsonMapper.ToObject<T>(jsonStr);
                break;
            case JsonType.JsonUtlity:
                data = JsonUtility.FromJson<T>(jsonStr);
                break;
        }

        return data;
    }

    public static T LoadConfig<T>(string name)
    {
        string path = Application.streamingAssetsPath + "/Config/" + name + ".json";
        string jsonStr = File.ReadAllText(path);
        if (!File.Exists(path))
            return default(T);

        T cfg = JsonMapper.ToObject<T>(jsonStr);

        return cfg;
    }

}
