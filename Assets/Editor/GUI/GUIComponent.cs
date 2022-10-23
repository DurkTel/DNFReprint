using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine.UI;

public class GUIComponent : Editor
{
    [MenuItem("GameObject/GUIComponent/ListView")]
    static void CreateListView(MenuCommand menuCommand)
    {
        GameObject parent = (GameObject)menuCommand.context;

        GameObject listViewObj = new GameObject("ListView", new System.Type[2] { typeof(ListView), typeof(ScrollRect) });
        GameObject viewPortObj = new GameObject("Viewport", typeof(RectMask2D));
        GameObject contentObj = new GameObject("Content", typeof(RectTransform));
        listViewObj.transform.SetParentZero(parent.transform);
        viewPortObj.transform.SetParent(listViewObj.transform);
        contentObj.transform.SetParent(viewPortObj.transform);

        ListView listView = listViewObj.GetComponent<ListView>();
        ScrollRect scrollRect = listViewObj.GetComponent<ScrollRect>();

        RectTransform viewPortRect = (RectTransform)viewPortObj.transform;
        RectTransform contentRect = (RectTransform)contentObj.transform;
        viewPortRect.anchorMax = Vector2.one;
        viewPortRect.anchorMin = Vector2.zero;
        viewPortRect.offsetMin = viewPortRect.offsetMax = Vector2.zero;
        contentRect.anchorMin = contentRect.anchorMax = contentRect.pivot = new Vector2(0, 1);

        listView.scrollRect = scrollRect;
        listView.viewPort = viewPortRect;
        listView.content = contentRect;

        scrollRect.viewport = viewPortRect;
        scrollRect.content = contentRect;
        
    }
}
