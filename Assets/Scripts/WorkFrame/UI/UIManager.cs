using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// UI层级
/// </summary>
public enum UI_Layer
{
    Bot,
    Mid,
    Top,
    System,
}

/// <summary>
/// UI管理器
/// 1.管理所有显示的面板
/// 2.提供给外部 显示和隐藏等等接口
/// </summary>
public class UIManager : SingletonBase<UIManager>
{
    public Dictionary<string, GUIView> panelDic = new Dictionary<string, GUIView>();

    private Transform bot;
    private Transform mid;
    private Transform top;
    private Transform system;

    //记录我们UI的Canvas父对象 方便以后外部可能会使用它
    public RectTransform canvas;

    public UIManager()
    {
        //创建Canvas 让其过场景的时候 不被移除
        GameObject obj = ResManager.Instance.Load<GameObject>("UI/Canvas");
        canvas = obj.transform as RectTransform;
        GameObject.DontDestroyOnLoad(obj);

        //找到各层
        bot = canvas.Find("Bot");
        mid = canvas.Find("Mid");
        top = canvas.Find("Top");
        system = canvas.Find("System");

        //创建EventSystem 让其过场景的时候 不被移除
        obj = ResManager.Instance.Load<GameObject>("UI/EventSystem");
        GameObject.DontDestroyOnLoad(obj);
    }

    /// <summary>
    /// 通过层级枚举 得到对应层级的父对象
    /// </summary>
    /// <param name="layer"></param>
    /// <returns></returns>
    public Transform GetLayerFather(UI_Layer layer)
    {
        switch(layer)
        {
            case UI_Layer.Bot:
                return this.bot;
            case UI_Layer.Mid:
                return this.mid;
            case UI_Layer.Top:
                return this.top;
            case UI_Layer.System:
                return this.system;
        }
        return null;
    }

    /// <summary>
    /// 显示面板
    /// </summary>
    /// <typeparam name="T">面板脚本类型</typeparam>
    /// <param name="panelName">面板名</param>
    /// <param name="layer">显示在哪一层</param>
    /// <param name="callBack">当面板预设体创建成功后 你想做的事</param>
    public void OpenView<T>(string panelName, UI_Layer layer = UI_Layer.Mid, UnityAction<T> callBack = null) where T: GUIView
    {
        if (panelDic.ContainsKey(panelName))
        {
            panelDic[panelName].Enable();
            // 处理面板创建完成后的逻辑
            if (callBack != null)
                callBack(panelDic[panelName] as T);
            //避免面板重复加载 如果存在该面板 即直接显示 调用回调函数后  直接return 不再处理后面的异步加载逻辑
            return;
        }

        ResManager.Instance.LoadAsync<GameObject>("UI/" + panelName, (obj) =>
        {
            //把他作为 Canvas的子对象
            //并且 要设置它的相对位置
            //找到父对象 你到底显示在哪一层
            Transform father = bot;
            switch(layer)
            {
                case UI_Layer.Mid:
                    father = mid;
                    break;
                case UI_Layer.Top:
                    father = top;
                    break;
                case UI_Layer.System:
                    father = system;
                    break;
            }
            //设置父对象  设置相对位置和大小
            obj.transform.SetParent(father);
            
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;

            (obj.transform as RectTransform).offsetMax = Vector2.zero;
            (obj.transform as RectTransform).offsetMin = Vector2.zero;

            //得到预设体身上的面板脚本
            T panel = obj.GetComponent<T>();
            
            // 处理面板创建完成后的逻辑
            if (callBack != null)
                callBack(panel);
            panel.Enable();
            panel.viewName = panelName;

            //把面板存起来
            panelDic.Add(panelName, panel);
        });
    }

    /// <summary>
    /// 隐藏面板
    /// </summary>
    /// <param name="panelName"></param>
    public void CloseView(string panelName)
    {
        if (panelDic.ContainsKey(panelName))
        {
            panelDic[panelName].Disable();
            GameObject.Destroy(panelDic[panelName].gameObject);
            panelDic.Remove(panelName);
        }
    }

    /// <summary>
    /// 关闭所有视图
    /// </summary>
    public void CloseAllView()
    {
        foreach (var view in panelDic.Values)
        {
            view.Disable();
            GameObject.Destroy(view.gameObject);
        }
        panelDic.Clear();
    }

    /// <summary>
    /// 关闭所有视图 形参中的除外
    /// </summary>
    /// <param name="panelNames"></param>
    public void CloseAllView(string[] panelNames)
    {
        for (int i = 0; i < panelDic.Count;)
        {
            var view = panelDic.ElementAt(i);
            for (int j = 0; j < panelNames.Length; j++)
            {
                if (view.Key != panelNames[j])
                {
                    view.Value.Disable();
                    GameObject.Destroy(view.Value.gameObject);
                    panelDic.Remove(view.Key);
                }
                else
                    i++;
            }
        }
    }

    /// <summary>
    /// 此视图是否正在开启
    /// </summary>
    /// <param name="panelName"></param>
    /// <returns></returns>
    public bool IsShowing(string panelName)
    {
        return panelDic.ContainsKey(panelName);
    }

    /// <summary>
    /// 得到某一个已经显示的面板 方便外部使用
    /// </summary>
    public T GetView<T>(string name) where T: GUIView
    {
        if (panelDic.ContainsKey(name))
            return panelDic[name] as T;
        return null;
    }

    /// <summary>
    /// 给控件添加自定义事件监听
    /// </summary>
    /// <param name="control">控件对象</param>
    /// <param name="type">事件类型</param>
    /// <param name="callBack">事件的响应函数</param>
    public static void AddCustomEventListener(UIBehaviour control, EventTriggerType type, UnityAction<BaseEventData> callBack)
    {
        EventTrigger trigger = control.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = control.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = type;
        entry.callback.AddListener(callBack);

        trigger.triggers.Add(entry);
    }

}
