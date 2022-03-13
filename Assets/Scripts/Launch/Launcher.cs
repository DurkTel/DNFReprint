using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        print("游戏启动！进入加载流程");

        new GameObject("TempCamera", typeof(Camera));
    }

    void Update()
    {
        
    }
}
