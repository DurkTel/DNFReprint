// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/BigWord/Input/New/InputControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""67dedef3-8040-4b18-8e2d-f9891d3cd18f"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""5beb4aea-975b-4e7f-941b-7c9f54f9862a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""62835c92-37cf-466a-a9e3-dfb56f3958e2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""c28a45c5-736c-4c83-9fd1-293d38987524"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""75f56647-db3e-4819-b2ea-54aecf7ae345"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""830c7845-9e0d-4f12-90bd-939281d696a5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""f3bd0495-1a84-48a7-878f-d259caf2ccb5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""Attack_1"",
                    ""type"": ""Button"",
                    ""id"": ""739e924d-e5dc-4ff1-ad43-7d4b53f936f4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""Attack_2"",
                    ""type"": ""Button"",
                    ""id"": ""dc26c7c7-7221-472b-b29d-4c89e481a439"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""Skill_1"",
                    ""type"": ""Button"",
                    ""id"": ""e82fd099-8750-493d-9381-5b467deda157"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""Skill_2"",
                    ""type"": ""Button"",
                    ""id"": ""7b2c4176-4109-41f2-9d0a-7f7a368baecd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""Skill_3"",
                    ""type"": ""Button"",
                    ""id"": ""7f9ce7be-27ff-41f3-b1f7-a5c33bbb1fac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""Skill_4"",
                    ""type"": ""Button"",
                    ""id"": ""707d9ee8-5c15-4a52-81be-92918ea0409b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""EnableDebug"",
                    ""type"": ""Button"",
                    ""id"": ""f2cc5641-6000-4b55-ba60-25b174126181"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""278049df-2d1b-4069-80e5-465cfe48dac3"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""25e37341-15f2-49a4-8229-aa163fa1d69a"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""50dfbe18-6648-4a31-9dbf-d42520f88d7b"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ab3ecebd-01f3-4fd8-8b80-c044c950b5b9"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""957ef54d-2754-4c92-a4ee-55461bebfd1f"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""6acd24ea-c533-44a5-b045-60badb5e7003"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""0cbf0746-0124-491b-9eaf-ffcf8e842dd0"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""824e896c-93f0-4964-b7f8-7f96058d647d"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8409b88b-1b8d-4ada-8aca-2eaa5386ad13"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ce1719b2-8333-4458-9a1b-1c76e31a6762"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""411f0968-65e2-4718-8d8e-b0e7dc7b2488"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack_1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea4f839f-c98f-48b6-a31b-2cc39ae534e4"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack_2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d97164ae-7a39-4835-a968-93e6386809c9"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill_1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""568273ff-11fe-4b9a-b990-6a2588e45cec"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill_2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""88806b4c-ecb7-43d5-92cf-227a14101834"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill_3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6344f65f-0041-4202-a1ee-cc0ea460f648"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill_4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Button With One Modifier"",
                    ""id"": ""24b38a30-9a70-4d19-8c8c-8ff634d56d65"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EnableDebug"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""button"",
                    ""id"": ""cc4edf5e-6f5e-4b35-ac7a-9b2457d84bd9"",
                    ""path"": ""<Keyboard>/f1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EnableDebug"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""3ae99e4c-9020-4087-b0e5-1cca570574ce"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EnableDebug"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Debug"",
            ""id"": ""37ffc21c-851a-43d6-8c44-4aa53fd8cb8f"",
            ""actions"": [
                {
                    ""name"": ""F1"",
                    ""type"": ""Button"",
                    ""id"": ""ad1923dc-9e75-4068-97bb-7b5827d44d54"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""F2"",
                    ""type"": ""Button"",
                    ""id"": ""0f47a871-98e5-4dbc-875b-c3eaed5c11e9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""F3"",
                    ""type"": ""Button"",
                    ""id"": ""ce4b5301-79bc-469a-8d86-a744a3dcb8d7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""F4"",
                    ""type"": ""Button"",
                    ""id"": ""9e249dc8-a893-4ecd-b5a7-a0478968e9b9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""F5"",
                    ""type"": ""Button"",
                    ""id"": ""3c6e4669-adfc-4b5d-b04a-417b66dfd156"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""F6"",
                    ""type"": ""Button"",
                    ""id"": ""c3cfb6d1-c14c-4069-9dd2-32b95ae287de"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""F7"",
                    ""type"": ""Button"",
                    ""id"": ""8e00b6a2-32dd-479a-9b82-5ecccae36eed"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""F8"",
                    ""type"": ""Button"",
                    ""id"": ""1fe9d49b-7563-414d-b37c-cdd00e1b4644"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""F9"",
                    ""type"": ""Button"",
                    ""id"": ""226dba30-f91b-496b-85cf-5f094aa4a860"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""EnableGameplay"",
                    ""type"": ""Button"",
                    ""id"": ""17f59aeb-8492-45e2-be0d-8d6dd485b7fe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f3a4815f-91fd-4ed2-8b6e-fd6fc5926e3d"",
                    ""path"": ""<Keyboard>/f1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""F1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5b4653e5-7497-48aa-972d-b7f62b2dbf4b"",
                    ""path"": ""<Keyboard>/f2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""F2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cca939d9-987b-4ecb-b0a6-e3e9c3e4cfa4"",
                    ""path"": ""<Keyboard>/f3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""F3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""af0d9702-5f77-45cc-9ac4-32003f783e1a"",
                    ""path"": ""<Keyboard>/f4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""F4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cc51147a-d837-4d21-aed4-dfd4207a2157"",
                    ""path"": ""<Keyboard>/f5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""F5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f5334a1e-3971-4ab6-9626-beea6b81a39f"",
                    ""path"": ""<Keyboard>/f6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""F6"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""be3ee35b-7be0-46d7-80d9-ee9e19fc373c"",
                    ""path"": ""<Keyboard>/f7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""F7"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""370aaebd-2750-46ea-8328-94969845ab54"",
                    ""path"": ""<Keyboard>/f8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""F8"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5d5856ef-e98b-4fdd-8dd2-234eab49869d"",
                    ""path"": ""<Keyboard>/f9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""F9"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Button With One Modifier"",
                    ""id"": ""ac135592-f2e6-4713-aef5-2c63f014e9f2"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EnableGameplay"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""button"",
                    ""id"": ""cc834fac-2ace-416e-a6ef-fa25e7ac6f01"",
                    ""path"": ""<Keyboard>/f1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EnableGameplay"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""5814c0ef-40a9-4a2f-be04-a7a013032fcc"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EnableGameplay"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
        m_Gameplay_Up = m_Gameplay.FindAction("Up", throwIfNotFound: true);
        m_Gameplay_Down = m_Gameplay.FindAction("Down", throwIfNotFound: true);
        m_Gameplay_Left = m_Gameplay.FindAction("Left", throwIfNotFound: true);
        m_Gameplay_Right = m_Gameplay.FindAction("Right", throwIfNotFound: true);
        m_Gameplay_Jump = m_Gameplay.FindAction("Jump", throwIfNotFound: true);
        m_Gameplay_Attack_1 = m_Gameplay.FindAction("Attack_1", throwIfNotFound: true);
        m_Gameplay_Attack_2 = m_Gameplay.FindAction("Attack_2", throwIfNotFound: true);
        m_Gameplay_Skill_1 = m_Gameplay.FindAction("Skill_1", throwIfNotFound: true);
        m_Gameplay_Skill_2 = m_Gameplay.FindAction("Skill_2", throwIfNotFound: true);
        m_Gameplay_Skill_3 = m_Gameplay.FindAction("Skill_3", throwIfNotFound: true);
        m_Gameplay_Skill_4 = m_Gameplay.FindAction("Skill_4", throwIfNotFound: true);
        m_Gameplay_EnableDebug = m_Gameplay.FindAction("EnableDebug", throwIfNotFound: true);
        // Debug
        m_Debug = asset.FindActionMap("Debug", throwIfNotFound: true);
        m_Debug_F1 = m_Debug.FindAction("F1", throwIfNotFound: true);
        m_Debug_F2 = m_Debug.FindAction("F2", throwIfNotFound: true);
        m_Debug_F3 = m_Debug.FindAction("F3", throwIfNotFound: true);
        m_Debug_F4 = m_Debug.FindAction("F4", throwIfNotFound: true);
        m_Debug_F5 = m_Debug.FindAction("F5", throwIfNotFound: true);
        m_Debug_F6 = m_Debug.FindAction("F6", throwIfNotFound: true);
        m_Debug_F7 = m_Debug.FindAction("F7", throwIfNotFound: true);
        m_Debug_F8 = m_Debug.FindAction("F8", throwIfNotFound: true);
        m_Debug_F9 = m_Debug.FindAction("F9", throwIfNotFound: true);
        m_Debug_EnableGameplay = m_Debug.FindAction("EnableGameplay", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Move;
    private readonly InputAction m_Gameplay_Up;
    private readonly InputAction m_Gameplay_Down;
    private readonly InputAction m_Gameplay_Left;
    private readonly InputAction m_Gameplay_Right;
    private readonly InputAction m_Gameplay_Jump;
    private readonly InputAction m_Gameplay_Attack_1;
    private readonly InputAction m_Gameplay_Attack_2;
    private readonly InputAction m_Gameplay_Skill_1;
    private readonly InputAction m_Gameplay_Skill_2;
    private readonly InputAction m_Gameplay_Skill_3;
    private readonly InputAction m_Gameplay_Skill_4;
    private readonly InputAction m_Gameplay_EnableDebug;
    public struct GameplayActions
    {
        private @InputControls m_Wrapper;
        public GameplayActions(@InputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @Up => m_Wrapper.m_Gameplay_Up;
        public InputAction @Down => m_Wrapper.m_Gameplay_Down;
        public InputAction @Left => m_Wrapper.m_Gameplay_Left;
        public InputAction @Right => m_Wrapper.m_Gameplay_Right;
        public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
        public InputAction @Attack_1 => m_Wrapper.m_Gameplay_Attack_1;
        public InputAction @Attack_2 => m_Wrapper.m_Gameplay_Attack_2;
        public InputAction @Skill_1 => m_Wrapper.m_Gameplay_Skill_1;
        public InputAction @Skill_2 => m_Wrapper.m_Gameplay_Skill_2;
        public InputAction @Skill_3 => m_Wrapper.m_Gameplay_Skill_3;
        public InputAction @Skill_4 => m_Wrapper.m_Gameplay_Skill_4;
        public InputAction @EnableDebug => m_Wrapper.m_Gameplay_EnableDebug;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Up.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnUp;
                @Up.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnUp;
                @Up.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnUp;
                @Down.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDown;
                @Down.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDown;
                @Down.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDown;
                @Left.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeft;
                @Left.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeft;
                @Left.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeft;
                @Right.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRight;
                @Right.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRight;
                @Right.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRight;
                @Jump.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Attack_1.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack_1;
                @Attack_1.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack_1;
                @Attack_1.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack_1;
                @Attack_2.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack_2;
                @Attack_2.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack_2;
                @Attack_2.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack_2;
                @Skill_1.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill_1;
                @Skill_1.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill_1;
                @Skill_1.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill_1;
                @Skill_2.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill_2;
                @Skill_2.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill_2;
                @Skill_2.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill_2;
                @Skill_3.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill_3;
                @Skill_3.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill_3;
                @Skill_3.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill_3;
                @Skill_4.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill_4;
                @Skill_4.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill_4;
                @Skill_4.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkill_4;
                @EnableDebug.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEnableDebug;
                @EnableDebug.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEnableDebug;
                @EnableDebug.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEnableDebug;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Up.started += instance.OnUp;
                @Up.performed += instance.OnUp;
                @Up.canceled += instance.OnUp;
                @Down.started += instance.OnDown;
                @Down.performed += instance.OnDown;
                @Down.canceled += instance.OnDown;
                @Left.started += instance.OnLeft;
                @Left.performed += instance.OnLeft;
                @Left.canceled += instance.OnLeft;
                @Right.started += instance.OnRight;
                @Right.performed += instance.OnRight;
                @Right.canceled += instance.OnRight;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Attack_1.started += instance.OnAttack_1;
                @Attack_1.performed += instance.OnAttack_1;
                @Attack_1.canceled += instance.OnAttack_1;
                @Attack_2.started += instance.OnAttack_2;
                @Attack_2.performed += instance.OnAttack_2;
                @Attack_2.canceled += instance.OnAttack_2;
                @Skill_1.started += instance.OnSkill_1;
                @Skill_1.performed += instance.OnSkill_1;
                @Skill_1.canceled += instance.OnSkill_1;
                @Skill_2.started += instance.OnSkill_2;
                @Skill_2.performed += instance.OnSkill_2;
                @Skill_2.canceled += instance.OnSkill_2;
                @Skill_3.started += instance.OnSkill_3;
                @Skill_3.performed += instance.OnSkill_3;
                @Skill_3.canceled += instance.OnSkill_3;
                @Skill_4.started += instance.OnSkill_4;
                @Skill_4.performed += instance.OnSkill_4;
                @Skill_4.canceled += instance.OnSkill_4;
                @EnableDebug.started += instance.OnEnableDebug;
                @EnableDebug.performed += instance.OnEnableDebug;
                @EnableDebug.canceled += instance.OnEnableDebug;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);

    // Debug
    private readonly InputActionMap m_Debug;
    private IDebugActions m_DebugActionsCallbackInterface;
    private readonly InputAction m_Debug_F1;
    private readonly InputAction m_Debug_F2;
    private readonly InputAction m_Debug_F3;
    private readonly InputAction m_Debug_F4;
    private readonly InputAction m_Debug_F5;
    private readonly InputAction m_Debug_F6;
    private readonly InputAction m_Debug_F7;
    private readonly InputAction m_Debug_F8;
    private readonly InputAction m_Debug_F9;
    private readonly InputAction m_Debug_EnableGameplay;
    public struct DebugActions
    {
        private @InputControls m_Wrapper;
        public DebugActions(@InputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @F1 => m_Wrapper.m_Debug_F1;
        public InputAction @F2 => m_Wrapper.m_Debug_F2;
        public InputAction @F3 => m_Wrapper.m_Debug_F3;
        public InputAction @F4 => m_Wrapper.m_Debug_F4;
        public InputAction @F5 => m_Wrapper.m_Debug_F5;
        public InputAction @F6 => m_Wrapper.m_Debug_F6;
        public InputAction @F7 => m_Wrapper.m_Debug_F7;
        public InputAction @F8 => m_Wrapper.m_Debug_F8;
        public InputAction @F9 => m_Wrapper.m_Debug_F9;
        public InputAction @EnableGameplay => m_Wrapper.m_Debug_EnableGameplay;
        public InputActionMap Get() { return m_Wrapper.m_Debug; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DebugActions set) { return set.Get(); }
        public void SetCallbacks(IDebugActions instance)
        {
            if (m_Wrapper.m_DebugActionsCallbackInterface != null)
            {
                @F1.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnF1;
                @F1.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnF1;
                @F1.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnF1;
                @F2.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnF2;
                @F2.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnF2;
                @F2.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnF2;
                @F3.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnF3;
                @F3.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnF3;
                @F3.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnF3;
                @F4.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnF4;
                @F4.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnF4;
                @F4.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnF4;
                @F5.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnF5;
                @F5.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnF5;
                @F5.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnF5;
                @F6.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnF6;
                @F6.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnF6;
                @F6.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnF6;
                @F7.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnF7;
                @F7.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnF7;
                @F7.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnF7;
                @F8.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnF8;
                @F8.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnF8;
                @F8.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnF8;
                @F9.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnF9;
                @F9.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnF9;
                @F9.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnF9;
                @EnableGameplay.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnEnableGameplay;
                @EnableGameplay.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnEnableGameplay;
                @EnableGameplay.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnEnableGameplay;
            }
            m_Wrapper.m_DebugActionsCallbackInterface = instance;
            if (instance != null)
            {
                @F1.started += instance.OnF1;
                @F1.performed += instance.OnF1;
                @F1.canceled += instance.OnF1;
                @F2.started += instance.OnF2;
                @F2.performed += instance.OnF2;
                @F2.canceled += instance.OnF2;
                @F3.started += instance.OnF3;
                @F3.performed += instance.OnF3;
                @F3.canceled += instance.OnF3;
                @F4.started += instance.OnF4;
                @F4.performed += instance.OnF4;
                @F4.canceled += instance.OnF4;
                @F5.started += instance.OnF5;
                @F5.performed += instance.OnF5;
                @F5.canceled += instance.OnF5;
                @F6.started += instance.OnF6;
                @F6.performed += instance.OnF6;
                @F6.canceled += instance.OnF6;
                @F7.started += instance.OnF7;
                @F7.performed += instance.OnF7;
                @F7.canceled += instance.OnF7;
                @F8.started += instance.OnF8;
                @F8.performed += instance.OnF8;
                @F8.canceled += instance.OnF8;
                @F9.started += instance.OnF9;
                @F9.performed += instance.OnF9;
                @F9.canceled += instance.OnF9;
                @EnableGameplay.started += instance.OnEnableGameplay;
                @EnableGameplay.performed += instance.OnEnableGameplay;
                @EnableGameplay.canceled += instance.OnEnableGameplay;
            }
        }
    }
    public DebugActions @Debug => new DebugActions(this);
    public interface IGameplayActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnUp(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
        void OnLeft(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnAttack_1(InputAction.CallbackContext context);
        void OnAttack_2(InputAction.CallbackContext context);
        void OnSkill_1(InputAction.CallbackContext context);
        void OnSkill_2(InputAction.CallbackContext context);
        void OnSkill_3(InputAction.CallbackContext context);
        void OnSkill_4(InputAction.CallbackContext context);
        void OnEnableDebug(InputAction.CallbackContext context);
    }
    public interface IDebugActions
    {
        void OnF1(InputAction.CallbackContext context);
        void OnF2(InputAction.CallbackContext context);
        void OnF3(InputAction.CallbackContext context);
        void OnF4(InputAction.CallbackContext context);
        void OnF5(InputAction.CallbackContext context);
        void OnF6(InputAction.CallbackContext context);
        void OnF7(InputAction.CallbackContext context);
        void OnF8(InputAction.CallbackContext context);
        void OnF9(InputAction.CallbackContext context);
        void OnEnableGameplay(InputAction.CallbackContext context);
    }
}
