using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

[CreateAssetMenu(menuName = "ScriptableObject/Input/InputReader", fileName = "InputReader")]
public class InputReader : ScriptableObject, InputControls.IGameplayActions
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

    private void OnEnable()
    {
        if (m_inputs == null)
        {
            m_inputs = new InputControls();
            m_inputs.Gameplay.SetCallbacks(this);
            m_multiTime = 0.2;
            
            InitGameplayButton();
        }
    }

    public static InputReader GetInputAsset()
    {
        if (inputReader != null)
            return inputReader;

        //if (File.Exists(path))
        //{
        //InputReader input = AssetDatabase.LoadAssetAtPath(path, typeof(InputReader)) as InputReader;
        //inputReader = input;

        //}
        //else
        //{
        //Directory.CreateDirectory(path);
        //var input = ScriptableObject.CreateInstance<InputReader>();
        //AssetDatabase.CreateAsset(input, path);
        //inputReader = input;
        //}
        inputReader = AssetLoader.Load<InputReader>("so/InputReader");
        return inputReader;
    }

    private void InitGameplayButton()
    {
        InputActionMap gamePlayActionMap = m_inputs.asset.FindActionMap("Gameplay");
        
        foreach (var action in gamePlayActionMap)
        {
            if(!buttonBehaviour.ContainsKey(action.name))
            {
                buttonBehaviour.Add(action.name, new ButtonBehaviour(action.name, action));
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
            //需求要按下算一次点击，自带的双击判断是松开算一次点击，自己封装一下
            if (context.startTime - buttonBehaviour[context.action.name].startTime <= m_multiTime)
            {
                buttonBehaviour[context.action.name].onMulti = true;
                buttonMultiEvent.Invoke(context.action.name);
            }
            buttonBehaviour[context.action.name].startTime = context.startTime;
        }
        if (context.phase == InputActionPhase.Performed)
        {
            if (context.interaction is PressInteraction)
            {
                buttonReleaseEvent.Invoke(context.action.name);

            }
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
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
