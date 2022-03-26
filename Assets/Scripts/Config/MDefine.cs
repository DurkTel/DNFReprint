using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class MDefine<T>
{
    private static T[] m_db;

    public static T[] GetCfg(string cfgName)
    {
        if (m_db == null)
            m_db = JsonManager.LoadConfig<T[]>(cfgName);
        return m_db;
    }
    
}
