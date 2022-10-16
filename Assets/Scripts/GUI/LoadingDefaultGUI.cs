using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingDefaultGUI : MonoBehaviour
{

    private static LoadingDefaultGUI m_instance;

    private static GameObject defaultUI;

    private static Image bg1;

    private static Image bg2;

    private static Image loading1;

    private static Image loading2;

    public static void Open(Transform transform)
    {
        GameObject go = AssetLoader.Load<GameObject>("prefabs/gui/Default_Loading_GUI");
        defaultUI = Instantiate(go);
        defaultUI.transform.SetParent(transform);

        Init();
    }

    public static void Close()
    {
        defaultUI.SetActive(false);
    }

    private static void Init()
    {
        bg1 = defaultUI.transform.Find("bgImage1").GetComponent<Image>();
        bg2 = defaultUI.transform.Find("bgImage2").GetComponent<Image>();
        loading1 = defaultUI.transform.Find("loading1Image").GetComponent<Image>();
        loading2 = defaultUI.transform.Find("loading1Image").GetComponent<Image>();
        loading2.gameObject.SetActive(false);
    }

    public static void LoadComplete()
    { 
        loading2.gameObject.SetActive(true);
        Close();
    }

}
