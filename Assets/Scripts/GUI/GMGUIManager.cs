using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class GMGUIManager : MonoBehaviour
{
    private static GMGUIManager m_instance;

    public static GMGUIManager Instance
    {
        get 
        { 
            if(m_instance == null)
            {
                GameObject go = new GameObject("GUI");
                m_instance = go.AddComponent<GMGUIManager>();   
                DontDestroyOnLoad(go);
            }
            return m_instance; 
        }  
    }

    public Camera uiCamera;

    public Transform canvasRoot;

    private List<Canvas> m_layers = new List<Canvas>();

    public void Initialize()
    {
        transform.localPosition = new Vector3(5000, 5000, 0);
        int layer = LayerMask.NameToLayer("UI");
        gameObject.layer = layer;
        GameObject cameraGO = new GameObject("UI_Camera");
        cameraGO.layer = layer;
        cameraGO.transform.SetParentZero(transform);
        cameraGO.tag = "UICamera";
        uiCamera = cameraGO.AddComponent<Camera>();
        uiCamera.clearFlags = CameraClearFlags.Depth;
        uiCamera.cullingMask = LayerMask.GetMask("UI");
        uiCamera.orthographic = true;
        uiCamera.depth = 2;
        uiCamera.farClipPlane = 50;

        GameObject canvasGO = new GameObject("UI_Canvas");
        canvasGO.layer = layer;
        canvasGO.transform.SetParentZero(transform);
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvasGO.AddComponent<GraphicRaycaster>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = uiCamera;
        canvas.planeDistance = 10;
        CanvasScaler canvasScaler = canvasGO.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(1280, 720);
        canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        canvasScaler.matchWidthOrHeight = 1f;
        canvasRoot = canvasGO.transform;

        GameObject eventGO = new GameObject("EventSystem", typeof(InputSystemUIInputModule));
        eventGO.layer = layer;
        eventGO.transform.SetParentZero(transform);
    }

    public Canvas InitLayer(string layerName, int order)
    {
        Canvas canvas = SetGUILayer(layerName, order);
        m_layers.Add(canvas);
        return canvas;
    }

    private Canvas SetGUILayer(string layerName, int order)
    {
        GameObject layerGO = new GameObject(layerName);
        RectTransform rect = layerGO.AddComponent<RectTransform>();
        rect.SetParentZero(canvasRoot);
        rect.gameObject.layer = LayerMask.NameToLayer("UI");

        rect.offsetMax = rect.offsetMin = rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;

        Canvas canvas = layerGO.AddComponent<Canvas>();
        canvas.overrideSorting = true;
        canvas.sortingOrder = order;
        canvas.sortingLayerName = "UI";
        layerGO.AddComponent<GraphicRaycaster>();

        return canvas;
    }
}
