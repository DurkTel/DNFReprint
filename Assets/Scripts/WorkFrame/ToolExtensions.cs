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
}
