using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class RenenderSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    private Sprite m_singSprite;

    private Texture2D m_singTexture;

    private Vector2 m_newPivot;

    private Vector2 m_anchorVector;

    private int m_spriteCount;

    private string[] m_coordinate;

    public bool loadComplete { get; private set; }

    [HideInInspector]
    public List<Sprite> part_Sprite = new List<Sprite>();

    private void Awake()
    {
        //InitSprite();
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

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
            m_singSprite = AssetLoader.Load<Sprite>(path + '/' + i);

            if (m_singSprite == null)
            {
                Debug.LogError("资源里图片和中心配置表数量不对，开始超出范围是" + i);
                return;
            }

            part_Sprite.Add(m_singSprite);
        }
    }

    public void InitSprite(string assetName)
    {
        string path = assetName;
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
        string tempName = path.Substring(path.LastIndexOf('/') + 1);

        for (int i = 0; i < m_spriteCount; i++)
        {
            m_singSprite = AssetLoader.Load<Sprite>(path + '/' + tempName + '_' + i);

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
    public IEnumerator InitSpriteAsync(string assetName, UnityAction<Avatar.AvatarPartType> callback)
    {
        loadComplete = false;
        string path = assetName;
        string tempName = path.Substring(path.LastIndexOf('/') + 1);

        part_Sprite.Clear();
        if (path == null)
        {
            Debug.LogError("请添加Sprite资源路径");
            yield break;
        }
        TextAsset tempTA = AssetLoader.Load<TextAsset>(path + '/' + tempName + '_' + "pointoffsize");
        
        if (tempTA == null)
        {
            Debug.LogError("位置偏移点获取不到!!!!!!!!!!!"+ path + '/' + tempName + '_' + "pointoffsize");
            yield break;
        }

        string str = tempTA.ToString();
        m_coordinate = str.Split(' ', '\n');

        m_spriteCount = m_coordinate.Length / 2;

        int i = 0;
        while (i < m_spriteCount)
        {
            AsyncOperation r = AssetLoader.LoadAsyncAO<Sprite>(path + '/' + tempName + '_' + i);

            yield return r;

            switch (AssetLoader.loadMode)
            {
                case AssetLoader.LoadMode.Resources:
                    m_singSprite = (r as ResourceRequest).asset as Sprite;
                    break;
                case AssetLoader.LoadMode.AssetBundle:
                    m_singSprite = (r as AssetBundleRequest).asset as Sprite;
                    break;
            }

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
            spriteRenderer.sprite = part_Sprite[index];
        }
    }

    public void SetSpriteFilp(bool isLeft)
    {
        spriteRenderer.flipX = isLeft;
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
        return spriteRenderer.flipX ? -1 : 1;
    }

}
