using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputUtility
{
    public static InputReader inputReader { get { return InputReader.GetInputAsset(); } }

    public static UnityEvent<string> onInputPressEvent = new UnityEvent<string>();

    public static UnityEvent<string> onInputReleaseEvent = new UnityEvent<string>();

    public static UnityEvent<string> onInputHoldEvent = new UnityEvent<string>();

    public static UnityEvent<string> onInputMultiEvent = new UnityEvent<string>();

    public static void Init()
    {
        inputReader.buttonPressEvent += InputPressAction;
        inputReader.buttonReleaseEvent += InputReleaseAction;
        inputReader.buttonHoldEvent += InputHolpAction;
        inputReader.buttonMultiEvent += InputMultiAction;
    }

    public static void InputPressAction(string actionName)
    {
        onInputPressEvent?.Invoke(actionName);
    }

    public static void InputReleaseAction(string actionName)
    {
        onInputReleaseEvent?.Invoke(actionName);
    }

    public static void InputHolpAction(string actionName)
    {
        onInputHoldEvent?.Invoke(actionName);
    }

    public static void InputMultiAction(string actionName)
    {
        onInputMultiEvent?.Invoke(actionName);
    }
}
