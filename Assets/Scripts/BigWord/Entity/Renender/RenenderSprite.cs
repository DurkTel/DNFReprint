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

    public void InitSprite(string assetName)
    {
        part_Sprite.Clear();
        if (assetName == null)
        {
            Debug.LogError("请添加Sprite资源路径");
            return;
        }

        TextAsset tempTA = AssetUtility.LoadAsset<TextAsset>(assetName + "_pointOffsize.txt");

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
            m_singSprite = AssetUtility.LoadAsset<Sprite>(assetName + "_" + i + ".png");

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

        part_Sprite.Clear();
        if (assetName == null)
        {
            Debug.LogError("请添加Sprite资源路径");
            yield break;
        }
        TextAsset tempTA = AssetUtility.LoadAsset<TextAsset>(assetName + "_pointOffsize.txt");

        if (tempTA == null)
        {
            Debug.LogError("位置偏移点获取不到!!!!!!!!!!!"+ assetName + "_pointOffsize.txt");
            yield break;
        }

        string str = tempTA.ToString();
        m_coordinate = str.Split(' ', '\n');

        m_spriteCount = m_coordinate.Length / 2;

        int i = 0;
        while (i < m_spriteCount)
        {
            AssetLoader loader = AssetUtility.LoadAssetAsync<Sprite>(assetName + '_' + i + ".png");

            yield return loader;

            m_singSprite = loader.rawObject as Sprite;

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
            spriteRenderer.sprite = index == -1 ? null : part_Sprite[index];
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
    /// <returns>-1向左</returns>
    public int GetCurFlip()
    {
        return spriteRenderer.flipX ? -1 : 1;
    }

}
