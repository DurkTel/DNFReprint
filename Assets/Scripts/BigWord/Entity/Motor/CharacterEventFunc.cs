using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;


public static class CharacterEventFunc
{
    private static Dictionary<InputActionDefine, string> inputEnumDic = new Dictionary<InputActionDefine, string>();
    /// <summary>
    /// 获得转换成字符串的输入枚举
    /// </summary>
    /// <param name="inputAction"></param>
    /// <returns></returns>
    public static string GetInputEnumToString(InputActionDefine inputAction)
    {
        string str;
        if (inputEnumDic.TryGetValue(inputAction, out str))
            return str;
        else
        {
            str = inputAction.ToString();
            inputEnumDic.Add(inputAction, str);
        }
        return str;
    }

    /// <summary>
    /// 通过字符串获得输入枚举
    /// </summary>
    /// <param name="inputAction"></param>
    /// <returns></returns>
    public static InputActionDefine GetInputStringToEnum(string actionName)
    {
        foreach (var item in inputEnumDic)
        {
            if (item.Value.Equals(actionName))
                return item.Key;
        }
        InputActionDefine re;
        if (System.Enum.TryParse(actionName, false, out re))
        {
            inputEnumDic.Add(re, actionName);
        }

        return re;
    }
}
