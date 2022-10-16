using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class Injection : MonoBehaviour
{
    private LuaTable m_injectTable;

    public List<InjectionObject> injectionObjects = new List<InjectionObject>();

    public void InitInject(LuaTable table)
    {
        foreach (var item in injectionObjects)
        {
            table.Set<string, Component>(item.name, item.component);
        }
    }

    public Component Get(string name)
    {
        foreach (var item in injectionObjects)
            if (item.name == name)
                return item.component;
        return null;
    }
}

[System.Serializable]
public class InjectionObject
{
    [SerializeField]
    private string m_name;
    public string name { get { return m_name; } }
    [SerializeField]
    private Object m_target;
    public Object target { get { return m_target; } }
    [SerializeField]
    private Component m_component;
    public Component component { get { return m_component; } }
}
