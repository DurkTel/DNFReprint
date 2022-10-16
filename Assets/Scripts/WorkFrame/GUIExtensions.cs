using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GUIExtensions
{
    public static void AddCanvas(this GameObject go, int sortingOrder)
    { 
        Canvas canvas = go.TryAddComponent<Canvas>();
        canvas.overrideSorting = true;
        canvas.sortingOrder = sortingOrder;
    }
}
