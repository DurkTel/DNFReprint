using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuaCoroutineRunner : MonoBehaviour
{

    private void Start()
    {
        gameObject.hideFlags = HideFlags.HideInHierarchy;
    }
    public void YieldAndCallback(object to_yield, System.Action callback)
    {
        StartCoroutine(CoBody(to_yield, callback));
    }

    private IEnumerator CoBody(object to_yield, System.Action callback)
    {
        if (to_yield is IEnumerator)
            yield return StartCoroutine((IEnumerator)to_yield);
        else
            yield return to_yield;
        callback();
    }
}
