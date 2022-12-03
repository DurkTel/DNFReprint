using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;

public abstract class AssetLoader : IEnumerator
{

    protected string m_assetName;
    public string assetName { get { return m_assetName; } }

    protected System.Type m_assetType;
    public System.Type assetType { get { return m_assetType; } }

    protected bool m_isDone;
    public bool isDone { get { return m_isDone; } }

    protected float m_progress; 
    public float progress { get { return m_progress; } }

    protected int m_priority;
    public int priority { get { return m_priority; } }

    protected bool m_async;
    public bool async { get { return m_async; } }

    protected bool m_error;
    public bool error { get { return m_error; } }

    protected Object m_rawObject;
    public Object rawObject { get { return m_rawObject; } }

    public object Current { get; }

    public UnityAction<float> onProgress;

    public UnityAction<AssetLoader> onComplete;

    public AssetLoader(string assetName)
    {
        this.m_assetName = assetName;
        this.m_assetType = typeof(Object);
    }
    public AssetLoader(string assetName, System.Type assetType, bool async)
    {
        this.m_assetName = assetName;
        this.m_assetType = assetType;
        this.m_async = async;
    }


    public abstract void Update();

    public abstract string GetAssetPath(string assetName);

    public abstract void Dispose();

    public bool MoveNext()
    {
        return !m_isDone;
    }

    public void Reset()
    {
        
    }
}
