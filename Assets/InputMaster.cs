//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/InputMaster.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InputMaster : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""9ae2cb9c-92b8-4fbb-9ae9-7959fb8f64a1"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""2f687c53-477a-4a8e-b43f-8a4154a69ea7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""a855a299-208c-41f8-8204-754b9723cec8"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Immortal"",
                    ""type"": ""Button"",
                    ""id"": ""82cd9502-f0be-43fa-9eb7-f85945d16128"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Blink"",
                    ""type"": ""Button"",
                    ""id"": ""1a041d1f-5ba1-4d60-be06-61482aefd323"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Castling"",
                    ""type"": ""Button"",
                    ""id"": ""dc6e3314-88cf-49e6-a256-9234852e5d9e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""2575044e-85a3-48a8-b410-90dac7398135"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""d19c3f28-515e-4910-97d3-055779f88805"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""806fe9d9-e5d3-47cf-ad67-7a5ce69f7994"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Climb"",
                    ""type"": ""Value"",
                    ""id"": ""56131bf1-d04a-4769-bd29-f5afc8e67ad9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Map"",
                    ""type"": ""Button"",
                    ""id"": ""d2450ded-594b-4845-8b3f-c2b5b2d63dd3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""NextLevel"",
                    ""type"": ""Button"",
                    ""id"": ""63640a35-e15a-4781-bb44-6028dbcb990a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pin create"",
                    ""type"": ""Button"",
                    ""id"": ""a4462c73-91ac-4b57-984b-0906a37be236"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Zoom Map"",
                    ""type"": ""Value"",
                    ""id"": ""6f7cb8af-1389-4805-85c9-8165625ad2a5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Pan Map"",
                    ""type"": ""Value"",
                    ""id"": ""91514da1-8ac4-4f95-a987-76411cd90047"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ed96deb6-9969-4345-bd2e-1dc7b0e75170"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse_keyborad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""221a7525-c5d6-4439-9575-2ced074524a3"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8a3dfaa1-8cca-42da-a5f3-4ab10faa4efd"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse_keyborad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3e9602c5-b5a9-4e53-9361-c9286dc78e84"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse_keyborad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrow"",
                    ""id"": ""5e68f9ef-2986-4f05-ab03-c05dc7b074a5"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""93a7c5a2-6014-46e4-b7ec-7dfefb98f0b0"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse_keyborad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6c611625-9569-48b9-8d67-7b1b3026366c"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse_keyborad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e5d7b2bb-a33e-423d-903f-58793bd4a6bd"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse_keyborad"",
                    ""action"": ""Immortal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c79b8f6d-41d7-48af-9327-5adb8dfa849c"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse_keyborad"",
                    ""action"": ""Blink"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b2d895a4-d353-48c8-804d-07ed25b6368c"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse_keyborad"",
                    ""action"": ""Castling"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""306021fe-4e04-4e7d-b8a9-32f38812e301"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse_keyborad"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3db15316-e7ad-4005-80db-fabe2371be6c"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse_keyborad"",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cc6c2a25-8951-4aa5-8d86-2f23ed27f591"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse_keyborad"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5e4e969d-d152-46e5-baac-a688cfb02fbd"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse_keyborad"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""34e3f52f-faa0-4fcd-99ec-7f6d942069e2"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Climb"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f4d3e951-33f2-45e6-914d-7b374ba11066"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse_keyborad"",
                    ""action"": ""Climb"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c5e08723-5f4b-40bf-a2ad-b8fb2db43c5b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse_keyborad"",
                    ""action"": ""Climb"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrorw"",
                    ""id"": ""a453477e-3c88-4f55-a4fd-9e825a9671a3"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Climb"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""b1a45bf0-cf04-47fe-8333-d6ceb39133b3"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse_keyborad"",
                    ""action"": ""Climb"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""cb6acb9b-d8c6-4a51-92aa-e9e0b9552eb4"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse_keyborad"",
                    ""action"": ""Climb"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""03448b40-8aae-4753-96d0-286dcff2099c"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse_keyborad"",
                    ""action"": ""Map"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1163ae28-dd59-47c7-86d0-fcd2037f2839"",
                    ""path"": ""<Keyboard>/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse_keyborad"",
                    ""action"": ""NextLevel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a7ca7e33-7233-43b8-a99e-d788261d4a6c"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse_keyborad"",
                    ""action"": ""Pin create"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e1f99e0f-5499-40b1-83dd-fb540c04ad17"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse_keyborad"",
                    ""action"": ""Zoom Map"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a41b3e19-3034-4bd6-9ec7-c5191adf93b8"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""mouse_keyborad"",
                    ""action"": ""Pan Map"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""mouse_keyborad"",
            ""bindingGroup"": ""mouse_keyborad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Immortal = m_Player.FindAction("Immortal", throwIfNotFound: true);
        m_Player_Blink = m_Player.FindAction("Blink", throwIfNotFound: true);
        m_Player_Castling = m_Player.FindAction("Castling", throwIfNotFound: true);
        m_Player_Attack = m_Player.FindAction("Attack", throwIfNotFound: true);
        m_Player_MousePosition = m_Player.FindAction("MousePosition", throwIfNotFound: true);
        m_Player_Dash = m_Player.FindAction("Dash", throwIfNotFound: true);
        m_Player_Climb = m_Player.FindAction("Climb", throwIfNotFound: true);
        m_Player_Map = m_Player.FindAction("Map", throwIfNotFound: true);
        m_Player_NextLevel = m_Player.FindAction("NextLevel", throwIfNotFound: true);
        m_Player_Pincreate = m_Player.FindAction("Pin create", throwIfNotFound: true);
        m_Player_ZoomMap = m_Player.FindAction("Zoom Map", throwIfNotFound: true);
        m_Player_PanMap = m_Player.FindAction("Pan Map", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Immortal;
    private readonly InputAction m_Player_Blink;
    private readonly InputAction m_Player_Castling;
    private readonly InputAction m_Player_Attack;
    private readonly InputAction m_Player_MousePosition;
    private readonly InputAction m_Player_Dash;
    private readonly InputAction m_Player_Climb;
    private readonly InputAction m_Player_Map;
    private readonly InputAction m_Player_NextLevel;
    private readonly InputAction m_Player_Pincreate;
    private readonly InputAction m_Player_ZoomMap;
    private readonly InputAction m_Player_PanMap;
    public struct PlayerActions
    {
        private @InputMaster m_Wrapper;
        public PlayerActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Immortal => m_Wrapper.m_Player_Immortal;
        public InputAction @Blink => m_Wrapper.m_Player_Blink;
        public InputAction @Castling => m_Wrapper.m_Player_Castling;
        public InputAction @Attack => m_Wrapper.m_Player_Attack;
        public InputAction @MousePosition => m_Wrapper.m_Player_MousePosition;
        public InputAction @Dash => m_Wrapper.m_Player_Dash;
        public InputAction @Climb => m_Wrapper.m_Player_Climb;
        public InputAction @Map => m_Wrapper.m_Player_Map;
        public InputAction @NextLevel => m_Wrapper.m_Player_NextLevel;
        public InputAction @Pincreate => m_Wrapper.m_Player_Pincreate;
        public InputAction @ZoomMap => m_Wrapper.m_Player_ZoomMap;
        public InputAction @PanMap => m_Wrapper.m_Player_PanMap;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Immortal.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnImmortal;
                @Immortal.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnImmortal;
                @Immortal.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnImmortal;
                @Blink.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBlink;
                @Blink.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBlink;
                @Blink.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBlink;
                @Castling.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCastling;
                @Castling.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCastling;
                @Castling.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCastling;
                @Attack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @MousePosition.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @Dash.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Climb.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnClimb;
                @Climb.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnClimb;
                @Climb.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnClimb;
                @Map.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMap;
                @Map.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMap;
                @Map.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMap;
                @NextLevel.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNextLevel;
                @NextLevel.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNextLevel;
                @NextLevel.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNextLevel;
                @Pincreate.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPincreate;
                @Pincreate.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPincreate;
                @Pincreate.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPincreate;
                @ZoomMap.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnZoomMap;
                @ZoomMap.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnZoomMap;
                @ZoomMap.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnZoomMap;
                @PanMap.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPanMap;
                @PanMap.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPanMap;
                @PanMap.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPanMap;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Immortal.started += instance.OnImmortal;
                @Immortal.performed += instance.OnImmortal;
                @Immortal.canceled += instance.OnImmortal;
                @Blink.started += instance.OnBlink;
                @Blink.performed += instance.OnBlink;
                @Blink.canceled += instance.OnBlink;
                @Castling.started += instance.OnCastling;
                @Castling.performed += instance.OnCastling;
                @Castling.canceled += instance.OnCastling;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @Climb.started += instance.OnClimb;
                @Climb.performed += instance.OnClimb;
                @Climb.canceled += instance.OnClimb;
                @Map.started += instance.OnMap;
                @Map.performed += instance.OnMap;
                @Map.canceled += instance.OnMap;
                @NextLevel.started += instance.OnNextLevel;
                @NextLevel.performed += instance.OnNextLevel;
                @NextLevel.canceled += instance.OnNextLevel;
                @Pincreate.started += instance.OnPincreate;
                @Pincreate.performed += instance.OnPincreate;
                @Pincreate.canceled += instance.OnPincreate;
                @ZoomMap.started += instance.OnZoomMap;
                @ZoomMap.performed += instance.OnZoomMap;
                @ZoomMap.canceled += instance.OnZoomMap;
                @PanMap.started += instance.OnPanMap;
                @PanMap.performed += instance.OnPanMap;
                @PanMap.canceled += instance.OnPanMap;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_mouse_keyboradSchemeIndex = -1;
    public InputControlScheme mouse_keyboradScheme
    {
        get
        {
            if (m_mouse_keyboradSchemeIndex == -1) m_mouse_keyboradSchemeIndex = asset.FindControlSchemeIndex("mouse_keyborad");
            return asset.controlSchemes[m_mouse_keyboradSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnImmortal(InputAction.CallbackContext context);
        void OnBlink(InputAction.CallbackContext context);
        void OnCastling(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnClimb(InputAction.CallbackContext context);
        void OnMap(InputAction.CallbackContext context);
        void OnNextLevel(InputAction.CallbackContext context);
        void OnPincreate(InputAction.CallbackContext context);
        void OnZoomMap(InputAction.CallbackContext context);
        void OnPanMap(InputAction.CallbackContext context);
    }
}
