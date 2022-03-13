using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class RenenderSprite : MonoBehaviour
{
    private SpriteRenderer m_spriteRenderer;

    private Sprite m_singSprite;

    private Texture2D m_singTexture;

    private Vector2 m_newPivot;

    private Vector2 m_anchorVector;

    private int m_spriteCount;

    private string[] m_coordinate;

    public string path;

    public int offsetX;

    public int offsetY;

    public bool loadComplete { get; private set; }

    [HideInInspector]
    public List<Sprite> part_Sprite = new List<Sprite>();

    private void Awake()
    {
        //InitSprite();
    }

    private void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();

    }

    //protected void OnEnable()
    //{
        //InitSprite(10000);

    //}

    //private void InitSprite()
    //{
    //    part_Sprite.Clear();
    //    if (path == null)
    //    { 
    //        Debug.LogError("请添加Sprite资源路径");
    //        return;        
    //    }
    //    TextAsset tempTA = Resources.Load<TextAsset>(path + "/pointOffsize");

    //    string str = tempTA.ToString();
    //    m_coordinate = str.Split(' ', '\n');

    //    m_spriteCount = m_coordinate.Length / 2;

    //    for (int i = 0; i < m_spriteCount; i++)
    //    {
    //        m_singSprite = Resources.Load<Sprite>(path + '/' + i);
    //        m_singTexture = Resources.Load<Texture2D>(path + '/' + i);

    //        if(m_singSprite == null || m_singTexture == null)
    //        {
    //            Debug.LogError("资源里图片和中心配置表数量不对，开始超出范围是" + i);
    //            return;
    //        }

    //        m_anchorVector.x = int.Parse(m_coordinate[i * 2]);
    //        m_anchorVector.y = int.Parse(m_coordinate[i * 2 + 1]);
            
    //        m_newPivot = new Vector2(0.5f - ((m_anchorVector.x - offsetX + m_singSprite.rect.width / 2) / m_singSprite.rect.width),
    //            0.5f + ((m_anchorVector.y - offsetY + m_singSprite.rect.height / 2) / m_singSprite.rect.height));
    //        part_Sprite.Add(Sprite.Create(m_singTexture, m_singSprite.rect, m_newPivot, m_singSprite.pixelsPerUnit));
    //    }
    //}

    /// <summary>
    /// 同步加载
    /// </summary>
    /// <param name="assetName"></param>
    /// <param name="fashionCode"></param>
    public void InitSprite(string assetName, int fashionCode)
    {
        string path = string.Format("{0}/{1}/{2}", assetName, fashionCode, name);
        part_Sprite.Clear();
        if (path == null)
        {
            Debug.LogError("请添加Sprite资源路径");
            return;
        }
        TextAsset tempTA = Resources.Load<TextAsset>(path + "/pointOffsize");

        if (tempTA == null)
        { 
            Debug.LogError("位置偏移点获取不到!!!!!!!!!!!");
            return;
        }

        string str = tempTA.ToString();
        m_coordinate = str.Split(' ', '\n');

        m_spriteCount = m_coordinate.Length / 2;

        for (int i = 0; i < m_spriteCount; i++)
        {
            m_singSprite = Resources.Load<Sprite>(path + '/' + i);

            if (m_singSprite == null)
            {
                Debug.LogError("资源里图片和中心配置表数量不对，开始超出范围是" + i);
                return;
            }

            part_Sprite.Add(m_singSprite);
        }
    }

    /// <summary>
    /// 异步加载
    /// </summary>
    /// <param name="index"></param>
    public IEnumerator InitSpriteAsync(string assetName, int fashionCode, UnityAction<Avatar.AvatarPartType> callback)
    {
        loadComplete = false;
        string path = string.Format("{0}/{1}", assetName, fashionCode);
        part_Sprite.Clear();
        if (path == null)
        {
            Debug.LogError("请添加Sprite资源路径");
            yield break;
        }
        TextAsset tempTA = Resources.Load<TextAsset>(path + "/pointOffsize");
        
        if (tempTA == null)
        {
            Debug.LogError("位置偏移点获取不到!!!!!!!!!!!");
            yield break;
        }

        string str = tempTA.ToString();
        m_coordinate = str.Split(' ', '\n');

        m_spriteCount = m_coordinate.Length / 2;

        int i = 0;
        while (i < m_spriteCount)
        {
            ResourceRequest r = Resources.LoadAsync<Sprite>(path + '/' + i);

            yield return r;

            m_singSprite = r.asset as Sprite;
            if (m_singSprite == null)
            {
                Debug.LogError("资源里图片和中心配置表数量不对，开始超出范围是" + i);
                yield break;
            }
            part_Sprite.Add(m_singSprite);
            i++;
        }
        loadComplete = true;
        SetSprite(0);

        //加载完成回调
        callback?.Invoke((Avatar.AvatarPartType)Enum.Parse(typeof(Avatar.AvatarPartType), name));
    }

    public void SetSprite(int index)
    { 
        if(part_Sprite.Count - 1 >= index)
        {
            m_spriteRenderer.sprite = part_Sprite[index];
        }
    }

    public void SetSpriteFilp(bool isLeft)
    {
        m_spriteRenderer.flipX = isLeft;
    }

    public void Clear()
    { 
        part_Sprite.Clear();
    }

    /// <summary>
    /// 返回面朝方向
    /// </summary>
    /// <returns>-1为右</returns>
    public int GetCurFlip()
    {
        return m_spriteRenderer.flipX ? -1 : 1;
    }


    //绘制着地点/跳跃点
    public virtual void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        if (transform.parent != null)
        {
            Gizmos.DrawWireSphere(transform.parent.position, 0.1f);
        }
        if (transform.localPosition.y > 0)
        {
            Gizmos.DrawWireSphere(transform.position, 0.1f);
            Gizmos.DrawLine(transform.position, transform.parent.position);
        }

    }
}
