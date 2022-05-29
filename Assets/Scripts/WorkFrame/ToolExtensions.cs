using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class ToolExtensions
{
    public static bool TryUniqueAdd<T>(this List<T> list, T item, Action<T> callBack = null)
    {
        if (!list.Contains(item))
        {
            list.Add(item);
            callBack?.Invoke(item);
            return true;
        }

        return false;
    }

    public static bool TryRemove<T>(this List<T> list, T item, Action<T> callBack = null)
    {
        if (list.Contains(item))
        {
            list.Remove(item);
            callBack?.Invoke(item);
            return true;
        }

        return false;
    }

    public static T TryAddComponent<T>(this GameObject gameObject) where T : MonoBehaviour
    {
        if (gameObject.TryGetComponent(out T component))
        {
            return component;
        }

        return gameObject.AddComponent<T>();
    }

    public static Component TryAddComponent(this GameObject gameObject, string type)
    {
        Component component = gameObject.GetComponent(type);
        if (component != null)
            return component;

        component = gameObject.AddComponent(Type.GetType(type));

        return component;
    }

    public static Component TryAddComponent(this Transform transform, string type)
    {
        return TryAddComponent(transform.gameObject, type);
    }

    public static void SetActive(this Component component, bool value)
    {
        if (component != null && component.gameObject.activeSelf != value)
        {
            component.gameObject.SetActive(value);
        }
    }

    public static void SetParentIgnore(this Transform transform, Transform parent)
    {
        Vector3 oriPos = transform.position;
        Vector3 oriRote = transform.localEulerAngles;
        Vector3 oriScale = transform.localScale;
        transform.SetParent(parent);
        transform.position = oriPos;
        transform.localEulerAngles = oriRote;
        transform.localScale = oriScale;
    }

    public static void SetParentZero(this Transform transform, Transform parent)
    {
        transform.SetParent(parent);
        transform.position = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
        transform.localScale = Vector3.zero;
    }

    public static bool IsNull(this UnityEngine.Object obj)
    {
        return obj == null || obj.Equals(null);
    }
}
