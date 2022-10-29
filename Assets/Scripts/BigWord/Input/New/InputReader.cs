using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.Utilities;

[CreateAssetMenu(menuName = "ScriptableObject/Input/InputReader", fileName = "InputReader")]
public class InputReader : ScriptableObject, InputControls.IGameplayActions, InputControls.IDebugActions
{
    public static string path = "Assets/ScriptableObjects/Input/InputReader.asset";

    public static InputReader inputReader;

    [SerializeField]
    private InputControls m_inputs;
    /// <summary>
    /// 双击的间隔时间
    /// </summary>
    private double m_multiTime;
    /// <summary>
    /// 记录按钮行为
    /// </summary>
    public Dictionary<string, ButtonBehaviour> buttonBehaviour = new Dictionary<string, ButtonBehaviour>();
    /// <summary>
    /// 移动输入事件
    /// </summary>
    public event UnityAction<Vector2> moveInputEvent = delegate { };
    /// <summary>
    /// 按键按下事件
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
    /// <summary>
    /// 按键长按事件
    /// </summary>
    public event UnityAction<string> buttonHoldEvent = delegate { };

    private void OnEnable()
    {
        if (m_inputs == null)
        {
            m_inputs = new InputControls();
            m_inputs.Gameplay.SetCallbacks(this);
            m_inputs.Debug.SetCallbacks(this);
            m_multiTime = 0.2;
            
            InitGameplayButton();
        }
    }

    public static InputReader GetInputAsset()
    {
        if (inputReader != null)
            return inputReader;

        inputReader = AssetUtility.LoadAsset<InputReader>("InputReader.asset");
        return inputReader;
    }

    private void InitGameplayButton()
    {

        ReadOnlyArray<InputActionMap> inputActions = m_inputs.asset.actionMaps;
        foreach (var actionMap in inputActions)
        {
            foreach (var action in actionMap)
            {
                if (!buttonBehaviour.ContainsKey(action.name))
                {
                    buttonBehaviour.Add(action.name, new ButtonBehaviour(action.name, action));
                }
            }
        }
    }

    public bool GetGamePlayEnabled()
    {
        return m_inputs.Gameplay.enabled;
    }

    public void EnableGameplayInput()
    {
        m_inputs.Gameplay.Enable();
    }

    public void EnableDebugInput()
    {
        m_inputs.Debug.Enable();
    }

    public void OnlyEnableGameplayInput()
    {
        DisableAllInPut();
        m_inputs.Gameplay.Enable();
        Debug.Log("切换到游戏输入模式");
    }

    public void OnlyEnableDebugInput()
    {
        DisableAllInPut();
        m_inputs.Debug.Enable();
        Debug.Log("切换到Debug输入模式");
    }

    public void DisableAllInPut()
    {
        m_inputs.Disable();
    }

    private void ButtonHandle(InputAction.CallbackContext context)
    {
        buttonBehaviour[context.action.name].onMulti = false;
        
        if (context.phase == InputActionPhase.Started)
        {
            buttonPressEvent.Invoke(context.action.name);
            //需求要按下算一次点击，自带的双击判断是松开算一次点击，自己模拟一下
            if (context.startTime - buttonBehaviour[context.action.name].startTime <= m_multiTime)
            {
                buttonBehaviour[context.action.name].onMulti = true;
                buttonMultiEvent.Invoke(context.action.name);
            }
            buttonBehaviour[context.action.name].startTime = context.startTime;
        }
        else if (context.phase == InputActionPhase.Performed)
        {
            if (context.interaction is HoldInteraction)
            {
                buttonHoldEvent.Invoke(context.action.name);

            }
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            buttonReleaseEvent.Invoke(context.action.name);
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        ButtonHandle(context);
        moveInputEvent.Invoke(context.ReadValue<Vector2>());
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
   
    public void OnJump(InputAction.CallbackContext context)
    {
        ButtonHandle(context);
    }

    public void OnAttack_1(InputAction.CallbackContext context)
    {
        ButtonHandle(context);

    }

    public void OnAttack_2(InputAction.CallbackContext context)
    {
        ButtonHandle(context);

    }

    public void OnSkill_1(InputAction.CallbackContext context)
    {
        ButtonHandle(context);

    }

    public void OnSkill_2(InputAction.CallbackContext context)
    {
        ButtonHandle(context);

    }

    public void OnSkill_3(InputAction.CallbackContext context)
    {
        ButtonHandle(context);

    }

    public void OnSkill_4(InputAction.CallbackContext context)
    {
        ButtonHandle(context);

    }

    public void OnSkill_5(InputAction.CallbackContext context)
    {
        ButtonHandle(context);
    }

    public void OnF1(InputAction.CallbackContext context)
    {
        ButtonHandle(context);
    }

    public void OnF2(InputAction.CallbackContext context)
    {
        ButtonHandle(context);
    }

    public void OnF3(InputAction.CallbackContext context)
    {
        ButtonHandle(context);
    }

    public void OnF4(InputAction.CallbackContext context)
    {
        ButtonHandle(context);
    }

    public void OnF5(InputAction.CallbackContext context)
    {
        ButtonHandle(context);
    }

    public void OnF6(InputAction.CallbackContext context)
    {
        ButtonHandle(context);
    }

    public void OnF7(InputAction.CallbackContext context)
    {
        ButtonHandle(context);
    }

    public void OnF8(InputAction.CallbackContext context)
    {
        ButtonHandle(context);
    }

    public void OnF9(InputAction.CallbackContext context)
    {
        ButtonHandle(context);
    }

    public void OnEnableDebug(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            OnlyEnableDebugInput();
        }
    }

    public void OnEnableGameplay(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            OnlyEnableGameplayInput();
        }
    }

}

/// <summary>
/// 按键行为
/// </summary>
public class ButtonBehaviour
{
    public string actionName;
    public double startTime;
    public bool onPressed { get { return inputAction.phase == InputActionPhase.Started; } }
    public bool onRelease { get { return inputAction.phase == InputActionPhase.Performed; } }
    public bool onMulti { get; set; }
    public bool onHold { get; set; }
    public InputAction inputAction;
    public ButtonBehaviour(string actionName, InputAction inputAction)
    {
        this.actionName = actionName;
        this.inputAction = inputAction;
    }
}
