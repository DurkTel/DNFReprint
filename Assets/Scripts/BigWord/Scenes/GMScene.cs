using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cfg.db;
using System;

public class GMScene
{
    private List<GMScenePart> sceneParts = new List<GMScenePart>();

    private GameObject m_gameObject;
    public GameObject gameObject { get { return m_gameObject; } set { m_gameObject = value; } }
    public Transform transform { get { return gameObject.transform; } }

    private float m_releaseTime;
    public float releaseTime { get { return m_releaseTime; } }

    public void Release()
    {
        m_gameObject = null;
    }

    public void Activate()
    {
        
    }

    public void Unactivation()
    {
        m_releaseTime = Time.realtimeSinceStartup;
    }


}
public enum MapType
{
    /// <summary>
    /// 主城
    /// </summary>
    Unique,
}
