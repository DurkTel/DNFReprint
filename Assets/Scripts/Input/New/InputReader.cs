using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

[CreateAssetMenu(menuName = "ScriptableObject/Input/InputReader", fileName = "InputReader")]
public class InputReader : ScriptableObject, InputControls.IGameplayActions
{
    [SerializeField]
    private InputControls m_inputs;
    /// <summary>
    /// 移动输入事件
    /// </summary>
    public event UnityAction<Vector2> moveInputEvent = delegate { };
    /// <summary>
    /// 按键松开事件
    /// </summary>
    public event UnityAction<string> buttonPressEvent = delegate { };
    /// <summary>
    /// 按键松开事件
    /// </summary>
    public event UnityAction<string> buttonReleaseEvent = delegate { };
    /// <summary>
    /// 按键双击事件
    /// </summary>
    public event UnityAction<string> buttonMultiEvent = delegate { };

    private void OnEnable()
    {
        if (m_inputs == null)
        {
            m_inputs = new InputControls();
            m_inputs.Gameplay.SetCallbacks(this);
        }
    }

    public void EnableGameplayInput()
    {
        m_inputs.Gameplay.Enable();
    }

    public void DisableAllInPut()
    {
        m_inputs.Disable();
    }

    private void ButtonHandle(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (context.interaction is MultiTapInteraction)
            {
                buttonMultiEvent.Invoke(context.action.name);
            }
            else if (context.interaction is PressInteraction)
            {
                buttonReleaseEvent.Invoke(context.action.name);
            }
        }
        else if(context.phase == InputActionPhase.Canceled && context.interaction is MultiTapInteraction)
        {
            buttonPressEvent.Invoke(context.action.name);
        }
    }

    public void OnDown(InputAction.CallbackContext context)
    {
        ButtonHandle(context);
    }

    public void OnLeft(InputAction.CallbackContext context)
    {
        ButtonHandle(context);

    }


    public void OnRight(InputAction.CallbackContext context)
    {
        ButtonHandle(context);

    }

    public void OnUp(InputAction.CallbackContext context)
    {
        ButtonHandle(context);

    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInputEvent.Invoke(context.ReadValue<Vector2>());
    }
}
