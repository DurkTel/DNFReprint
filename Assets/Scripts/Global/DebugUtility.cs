using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DebugUtility
{
    public static InputReader inputReader { get { return InputReader.GetInputAsset(); } }

    public static UnityAction<string> onInputEvent;
    public static void Init()
    {
        inputReader.buttonPressEvent += InputAction;
    }

    public static void InputAction(string actionName)
    {
        onInputEvent?.Invoke(actionName);
    }
}
