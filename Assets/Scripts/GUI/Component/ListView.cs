using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.ScrollRect;

public class ListView : MonoBehaviour
{
    private ScrollRect m_scrollRect;

    private RectTransform m_viewPort;

    private RectTransform m_content;

    private GameObject m_template;

    private string m_templateAsset;

    private void Start()
    {
        if (m_template != null)
        {
            m_template.SetActive(false);
        }

        Init();
    }

    private void Init()
    {
        if (m_viewPort == null)
            m_viewPort = GetComponent<RectTransform>();

        if (m_content == null)
            m_content = m_viewPort;

        if(m_scrollRect != null)
        {
            m_scrollRect.onValueChanged.AddListener(OnScroll);
        }
    }

    private void OnScroll(Vector2 vector)
    { 
        
    }
}
