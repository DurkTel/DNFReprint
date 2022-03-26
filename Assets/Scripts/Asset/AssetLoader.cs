using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//目前只支持load Resource文件夹
public class AssetLoader
{
    //同步加载资源
    public static T Load<T>(string name) where T : Object
    {
        T res = Resources.Load<T>(name);
        return res;
    }

    //异步加载资源
    public static ResourceRequest LoadAsync<T>(string name) where T : Object
    {
        ResourceRequest re = Resources.LoadAsync<T>(name);

        return re;
    }

}
