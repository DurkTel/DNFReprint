using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;


public class Test111 : MonoBehaviour
{
    GameObject go;

    void Start()
    {
        //AssetManifest.RefreshAssetsManifest();
        AssetManager.Initialize();
        AssetLoader loader = AssetUtility.LoadAssetAsync<GameObject>("10003.prefab");
        loader.onComplete = (p) =>
        {
            go = p.rawObject as GameObject;
            Instantiate(go);
            //Invoke("Destroy", 2);
        };


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Destroy()
    {
        AssetUtility.Destroy(go);
    }

}
