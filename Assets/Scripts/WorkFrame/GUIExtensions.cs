using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class GUIExtensions
{
    public static void AddCanvas(this GameObject go, int sortingOrder)
    { 
        Canvas canvas = go.TryAddComponent<Canvas>();
        canvas.overrideSorting = true;
        canvas.sortingOrder = sortingOrder;

        go.TryAddComponent<GraphicRaycaster>();
    }

    public static void SetSprite(this Image image, string assetName)
    {
        image.sprite = AssetLoader.Load<Sprite>(assetName);
    }

    public static void SetSpriteAsync(this Image image, string assetName)
    {
        AssetLoader.LoadAsync<Sprite>(assetName, (p) =>
        {
            image.sprite = p;
        });
    }
}
