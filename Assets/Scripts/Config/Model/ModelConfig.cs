using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelConfig
{
    private static Dictionary<int, Entity.ModelInfo> m_modelDic = new Dictionary<int, Entity.ModelInfo>();

    private static void GetInfo()
    {
        Entity.ModelInfo[] modelInfos = JsonManager.LoadConfig<Entity.ModelInfo[]>("t_model_info");

        foreach (var item in modelInfos)
        {
            m_modelDic.Add(item.modelCode, item);
        }
    }


    public static Entity.ModelInfo GetInfoByCode(int modelCode)
    {
        if (!m_modelDic.ContainsKey(modelCode))
            GetInfo();

        if (!m_modelDic.ContainsKey(modelCode))
        { 
            Debug.LogError("t_model_info配置表中没有该code" + modelCode);
            return new Entity.ModelInfo();
        }

        return m_modelDic[modelCode];
    }
}
