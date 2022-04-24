using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageConfig
{
    private static Dictionary<int, DamageData> m_damageDic = new Dictionary<int, DamageData>();

    private static void GetInfo()
    {
        DamageData[] damageInfos = JsonManager.LoadConfig<DamageData[]>("t_damage");

        foreach (var item in damageInfos)
        {
            m_damageDic.Add(item.code, item);
        }
    }


    public static DamageData GetInfoByCode(int code)
    {
        if (code == 0) return null;
        if (!m_damageDic.ContainsKey(code))
            GetInfo();

        if (!m_damageDic.ContainsKey(code))
        {
            Debug.LogError("t_damage配置表中没有该code" + code);
            return null;
        }

        return m_damageDic[code];
    }
}
