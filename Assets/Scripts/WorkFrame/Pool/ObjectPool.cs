using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 池接口链接单个对象池和总对象池列表
/// </summary>
public interface IPool
{
    Type type { get; }

    ICollection collection { get; }
}

/// <summary>
/// 存放所有的对象池
/// </summary>
public static class ObjectPoolList
{
    public static List<IPool> poolList = new List<IPool>();
}


/// <summary>
/// 对象池基类
/// </summary>
/// <typeparam name="T"></typeparam>
public class ObjectPool<T> : IPool where T : new()
{
    private readonly Stack<T> m_stack = new Stack<T>();

    private readonly Action<T> m_onGet;

    private readonly Action<T> m_onRelease;
    public Type type => typeof(T);
    public ICollection collection => m_stack;

    /// <summary>
    /// 无参构造
    /// </summary>
    public ObjectPool()
    {

    }

    /// <summary>
    /// 构造函数 工厂模式
    /// </summary>
    /// <param name="onGet"></param>
    /// <param name="onRelease"></param>
    public ObjectPool(Action<T> onGet, Action<T> onRelease)
    {
        m_onGet = onGet;
        m_onRelease = onRelease;
        ObjectPoolList.poolList.Add(this);
    }

    public T Get()
    {
        if (m_stack.Count > 0)
        {
            return m_stack.Pop();
        }

        T newObj = new T();
        m_onGet?.Invoke(newObj);

        return newObj;
    }

    public void Release(T item)
    {
        if (m_stack.Count > 0 && m_stack.Contains(item))
        {
            Debug.LogErrorFormat("{0}该对象池以存在此对象{1}", typeof(T).Name, item.ToString());
            return;
        }
        m_stack.Push(item);
        m_onRelease?.Invoke(item);
    }

    public void Clear()
    {
        m_stack.Clear();
    }

}

/// <summary>
/// 通用对象静态池 无工厂构造和回收析构
/// </summary>
/// <typeparam name="T"></typeparam>
public static class Pool<T> where T : new()
{
    private static readonly ObjectPool<T> objectPool = new ObjectPool<T>();

    public static T Get()
    {
        return objectPool.Get();
    }

    public static void Release(T item)
    {
        objectPool.Release(item);
    }

    public static void Clear()
    {
        objectPool.Clear();
    }
}

/// <summary>
/// 通用列表静态池
/// </summary>
/// <typeparam name="T"></typeparam>
public static class ListPool<T>
{
    private static readonly ObjectPool<List<T>> s_ListPool = new ObjectPool<List<T>>(null, Clear);
    static void Clear(List<T> l) { l.Clear(); }
    public static List<T> Get()
    {
        return s_ListPool.Get();
    }

    public static void Release(List<T> toRelease)
    {
        s_ListPool.Release(toRelease);
    }
}

public static class QueuePool<T>
{
    private static readonly ObjectPool<Queue<T>> s_QueuePool = new ObjectPool<Queue<T>>(null, Clear);
    static void Clear(Queue<T> l) { l.Clear(); }
    public static Queue<T> Get()
    {
        return s_QueuePool.Get();
    }

    public static void Release(Queue<T> toRelease)
    {
        s_QueuePool.Release(toRelease);
    }
}

/// <summary>
/// 通用字典对象池
/// </summary>
/// <typeparam name="K"></typeparam>
/// <typeparam name="V"></typeparam>
public static class DictionaryPool<K, V>
{
    private static readonly ObjectPool<DictionaryEx<K, V>> s_DicPool = new ObjectPool<DictionaryEx<K, V>>(null, Clear);
    static void Clear(Dictionary<K, V> l) { l.Clear(); }

    public static DictionaryEx<K, V> Get()
    {
        return s_DicPool.Get();
    }

    public static void Release(DictionaryEx<K, V> toRelease)
    {
        s_DicPool.Release(toRelease);
    }
}



