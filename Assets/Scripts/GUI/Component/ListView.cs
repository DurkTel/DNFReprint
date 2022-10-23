using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.UI.ScrollRect;

public class ListView : Injection
{
    public enum ListLayout
    {
        Horizontal,
        Vertical,
        Grid,
        Custom
    }

    public enum GridConstraint
    {
        ColumnCount,
        RowCount
    }

    [SerializeField]
    private ListLayout m_listLayout = ListLayout.Horizontal;

    [SerializeField]
    private GridConstraint m_gridConstraint = GridConstraint.ColumnCount;

    [SerializeField]
    private int m_gridConstraintCount;

    [SerializeField]
    private ScrollRect m_scrollRect;
    public ScrollRect scrollRect { get { return m_scrollRect; } set { m_scrollRect = value; } }

    [SerializeField]
    private RectTransform m_viewPort;
    public RectTransform viewPort { get { return m_viewPort; } set { m_viewPort = value; } }

    [SerializeField]
    private RectTransform m_content;
    public RectTransform content { get { return m_content; } set { m_content = value; } }

    [SerializeField]
    private GameObject m_template;

    [SerializeField]
    private string m_templateAsset;

    [SerializeField]
    private int m_dataCount;
    public int dataCount { get { return m_dataCount; } set { m_dataCount = value; ForceRefresh(); } }

    [SerializeField]
    private int m_waitCreateCount;

    [SerializeField]
    private Vector2 m_spacing;
    public Vector2 spacing { get { return m_spacing; } set { m_spacing = value; ForceRefresh(); } }

    private int m_waitIndex;

    private Dictionary<int, ListViewItemRender> m_visibleList;

    private List<ListViewItemRender> m_releaseList;

    private List<ListViewItemRender> m_temps;

    private bool m_forceRefresh;

    private Vector2Int m_lastRage;

    private Vector2 m_itemSize;
    public Vector2 itemSize 
    { 
        get 
        {
            if (m_itemSize == Vector2.zero && m_template != null)
            { 
                RectTransform rect = m_template.GetComponent<RectTransform>();
                m_itemSize = rect.rect.size;
                return m_itemSize;
            }
            return m_itemSize;
        }
    }
    public UnityAction<ListViewItemRender> onItemCreate { get; set; }
    public UnityAction<ListViewItemRender> onItemUpdate { get; set; }
    public UnityAction<ListViewItemRender> onItemRelease { get; set; }
    public UnityAction onUpdateComplete { get; set; }

    private void Start()
    {
        if (m_template != null)
            m_template.SetActive(false);

        Init();
        RefreshContent();
        ForceRefresh();
    }

    private void Update()
    {
        if (m_forceRefresh)
        {
            RefreshItems();
        }
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
        ForceRefresh();
    }

    public void ForceRefresh()
    {
        m_forceRefresh = true;
    }

    private void RefreshItems()
    {
        RefreshItemsState();
        RefreshItemsPos();
    }

    private void RefreshItemsState()
    {
        if (m_visibleList == null)
            m_visibleList = new Dictionary<int, ListViewItemRender>();

        //������ʾ��Χ
        int firstIndex = 0;
        int lastIndex = 0;
        switch (m_listLayout)
        {
            case ListLayout.Horizontal:
                GetVisibleRange(0, 1, out firstIndex, out lastIndex);
                break;
            case ListLayout.Vertical:
                GetVisibleRange(1, 1, out firstIndex, out lastIndex);
                break;
            case ListLayout.Grid:
                int axis = m_gridConstraint == GridConstraint.ColumnCount ? 1 : 0;
                GetVisibleRange(axis, m_gridConstraintCount, out firstIndex, out lastIndex);
                break;
        }
        
        //ɸѡ������ʾ��Χ
        foreach (var render in m_visibleList.Values)
        {
            if (render.index < firstIndex || render.index > lastIndex)
            {
                if (m_temps == null)
                    m_temps = new List<ListViewItemRender>();

                m_temps.TryUniqueAdd(render);
            }
        }

        ListViewItemRender item;
        //���ճ�����ʾ��Χ��Item
        if (m_temps != null && m_temps.Count > 0)
        {
            for (int i = 0; i < m_temps.Count; i++)
            {
                if (m_releaseList == null)
                    m_releaseList = new List<ListViewItemRender>();

                int index = m_temps[i].index;
                if (m_visibleList.TryGetValue(index, out item))
                {
                    m_visibleList.Remove(index);
                    m_releaseList.TryUniqueAdd(item);
                    item.Release();
                    onItemRelease?.Invoke(item);
                }
            }

            m_temps.Clear();
        }

        //���㻬��ʱ���ڷ�֡���µ���� ����index
        int offset = firstIndex - m_lastRage[0];
        m_waitIndex = offset >= 0 ? m_waitIndex - offset : 0;

        int waitFlag = 0;
        //ˢ��/��������ʾ��Χ��Item
        for (int i = firstIndex + m_waitIndex; i <= lastIndex; i++)
        {
            if (m_waitCreateCount != 0 && waitFlag++ >= m_waitCreateCount) //��֡ˢ��/����
                break;

            m_waitIndex++;
            if (!m_visibleList.TryGetValue(i, out item))
            {
                if (m_releaseList == null || m_releaseList.Count < 1)
                {
                    item = new ListViewItemRender();
                    GameObject go = Instantiate(m_template);
                    go.transform.SetParentZero(m_content);

                    if (!go.activeSelf)
                        go.SetActive(true);

                    item.SetData(i);
                    item.Create(go);
                    onItemCreate?.Invoke(item);
                }
                else
                {
                    item = m_releaseList[0];
                    m_releaseList.RemoveAt(0);
                }

                m_visibleList.TryAdd(i, item);
            }
            item.SetData(i);
            item.Refresh();
            onItemUpdate?.Invoke(item);
        }

        if (m_visibleList.Count > lastIndex - firstIndex)
        {
            m_forceRefresh = false;
            m_waitIndex = 0;
            onUpdateComplete?.Invoke();
        }

        m_lastRage.Set(firstIndex, lastIndex);
    }

    private void RefreshItemsPos()
    {
        switch (m_listLayout)
        {
            case ListLayout.Horizontal:
                RefreshHorizontalORVerticalPos(0);
                break;
            case ListLayout.Vertical:
                RefreshHorizontalORVerticalPos(1);
                break;
            case ListLayout.Grid:
                RefreshGridPos();
                break;
            case ListLayout.Custom:
                break;
            default:
                break;
        }

        if (m_releaseList != null && m_releaseList.Count > 0)
        {
            foreach (var item in m_releaseList)
            {
                item.rectTransform.anchoredPosition = new Vector2(9999, 9999);
            }

        }
    }

    private void RefreshHorizontalORVerticalPos(int axis)
    {
        Vector3 vector = Vector3.zero;
        int symbol = axis == 0 ? 1 : -1;
        foreach (var item in m_visibleList.Values)
        {
            vector[axis] = item.index * (itemSize[axis] + m_spacing[axis]) * symbol;
            item.rectTransform.anchoredPosition = vector;
        }
    }

    private void RefreshGridPos()   
    {
        Vector3 vector = Vector3.zero;
        int fixedAxis = m_gridConstraint == GridConstraint.ColumnCount ? 0 : 1;
        int variableAxis = fixedAxis == 0 ? 1 : 0;
        int symbol = m_gridConstraint == GridConstraint.ColumnCount ? 1 : -1;

        foreach (var item in m_visibleList.Values)
        {
            vector[fixedAxis] = item.index % m_gridConstraintCount * (itemSize[0] + m_spacing[0]) * symbol;
            vector[variableAxis] = item.index / m_gridConstraintCount * (itemSize[1] + m_spacing[1]) * -symbol;
            item.rectTransform.anchoredPosition = vector;
        }
    }

    private void RefreshContent()
    {

        if (m_dataCount == 0)
            m_content.sizeDelta = Vector2.zero;

        switch (m_listLayout)
        {
            case ListLayout.Horizontal:
                m_content.sizeDelta = new Vector2(m_dataCount * (itemSize[0] + m_spacing[0]), itemSize[1]);
                break;
            case ListLayout.Vertical:
                m_content.sizeDelta = new Vector2(itemSize[0], m_dataCount * (itemSize[1] + m_spacing[1]));
                break;
            case ListLayout.Grid:
                Vector2 size = Vector2.zero;
                int fixedAxis = m_gridConstraint == GridConstraint.ColumnCount ? 0 : 1;
                int variableAxis = fixedAxis == 0 ? 1 : 0;
                size[fixedAxis] = m_gridConstraintCount * (itemSize[fixedAxis] + m_spacing[fixedAxis]);
                size[variableAxis] = Mathf.Ceil(m_dataCount / (float)m_gridConstraintCount) * (itemSize[1] + m_spacing[variableAxis]);
                m_content.sizeDelta = size;
                break;
            case ListLayout.Custom:
                break;
            default:
                break;
        }

        m_content.anchorMax = m_content.anchorMin = m_content.pivot = new Vector2(0, 1);
    }

    private void GetVisibleRange(int axis, int maxShowNum, out int firstIndex, out int lastIndex)
    {
        int symbol = axis == 1 ? 1 : -1;

        float offsetStart = m_content.anchoredPosition[axis] * symbol;
        firstIndex = (int)(offsetStart / (itemSize[axis] + m_spacing[axis])) * maxShowNum;

        float offsetEnd =  m_content.anchoredPosition[axis] * symbol + m_viewPort.rect.size[axis];
        lastIndex = (int)(offsetEnd / (itemSize[axis] + m_spacing[axis])) * maxShowNum + maxShowNum - 1;

        firstIndex = Mathf.Max(firstIndex, 0);

        lastIndex = Mathf.Min(lastIndex, m_dataCount - 1);
    }
}


public class ListViewItemRender
{
    public int index { get; set; }
    public bool isVisible { get; set; }
    public GameObject gameObject { get; set; }
    public RectTransform rectTransform { get; set; }
    public Vector2 size { get; set; }

    private TextMeshProUGUI text;

    public void SetData(int index)
    {
        this.index = index;
    }

    public void Create(GameObject gameObject)
    {
        this.gameObject = gameObject;
        rectTransform = gameObject.GetComponent<RectTransform>();
        size = rectTransform.rect.size;
        rectTransform.anchorMin = rectTransform.anchorMax = rectTransform.pivot = new Vector2(0, 1);
    }

    public void Refresh()
    {
        isVisible = true;
        Image image;
    }

    public void Release()
    {
        isVisible = false;
    }
}
