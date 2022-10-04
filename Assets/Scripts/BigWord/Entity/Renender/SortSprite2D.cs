using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortSprite2D : MonoBehaviour
{
    
    public float floorHeight = 0.5f;
    
    private bool isStatic;

    private float m_spriteLowerBound;

    private float m_spriteHalfWidth;

    private Transform m_root;

    private readonly float m_tan30 = Mathf.Tan(Mathf.PI / 5);

    private void Start()
    {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        m_spriteLowerBound = spriteRenderer.bounds.size.y * 0.5f;
        m_spriteHalfWidth = 0.5f;
        m_root = transform.parent;
        RefreshFloor();

    }

    private void LateUpdate()
    {
        if (!isStatic)
        {
            RefreshFloor();
        }
    }

    private void RefreshFloor()
    {
        if (m_root != null)
        {
            m_root.position = new Vector3(m_root.position.x, m_root.position.y, (m_root.position.y - m_spriteLowerBound + floorHeight * m_tan30));
        }
    }

    private void OnDrawGizmos()
    {
        if (m_root != null)
        {
            Vector3 floorHeightPos = new Vector3(m_root.position.x, m_root.position.y - m_spriteLowerBound + floorHeight, m_root.position.z);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(floorHeightPos + Vector3.left * m_spriteHalfWidth, floorHeightPos + Vector3.right * m_spriteHalfWidth);

        }
    }
}
