using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using cfg;
using SimpleJSON;

public class MDefine
{
    private static Tables m_tables;
    public static Tables tables { get { return m_tables; } }
    public static void Initialize()
    {
        m_tables = new Tables(Loader);
    }

    private static JSONNode Loader(string fileName)
    {
        return JSON.Parse(File.ReadAllText(Application.dataPath + "/../GenerateDatas/json/" + fileName + ".json"));
    }

}
