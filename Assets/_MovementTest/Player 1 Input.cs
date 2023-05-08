//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/_MovementTest/Player 1 Input.inputactions
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

public partial class @Player1Input : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Player1Input()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player 1 Input"",
    ""maps"": [
        {
            ""name"": ""Controller"",
            ""id"": ""bd037af0-d9ab-413e-b888-d49551695f89"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""f857231c-dadb-4ec3-9f0c-791121b8edd4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""8f9635a6-f246-4fb1-b24c-8443224405b7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""d697f9e8-bfed-4896-b1a3-f3c16f4126f1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Block"",
                    ""type"": ""Button"",
                    ""id"": ""93c375e5-6d12-4055-860c-537f50a88a2a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Strafe"",
                    ""type"": ""Button"",
                    ""id"": ""09c0ae10-dab0-4f34-a9fe-fb81ee7f952a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""309776cf-e07b-41f2-babf-81aa0595c9f2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Exit"",
                    ""type"": ""Button"",
                    ""id"": ""0f34a647-5be5-4643-acd5-a41944fa56d5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dismount"",
                    ""type"": ""Button"",
                    ""id"": ""1d093ba1-9e9b-4518-bc45-c93fc47a82ea"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""AnyButton"",
                    ""type"": ""Button"",
                    ""id"": ""8ac99054-c09c-4d06-9b87-7b229af24833"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c09a088f-604b-4e71-b5fa-baf0275d2259"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cfaa5a19-7d17-47d4-aae9-a822429e0ad9"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""833a5671-4c79-4344-95b3-705d05599879"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6b6728f1-e1da-4360-8a08-7089a992a671"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5b9efc12-7b19-4a2a-a3f9-07d8d77607d6"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Strafe"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""805b4975-4241-4f57-837f-663e5005d617"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d64b4834-cb40-4dbc-ac5a-e926ca46049f"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1ef7ced3-6ed2-40d4-8daa-f5fd51fd8d4d"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a972023e-6475-46c8-b871-fccad04bcadf"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""df1a0dc3-68d4-4b0e-a785-8417267daf1d"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""041dd881-4511-46dd-9fc6-49c07d052b06"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""02637cfa-e284-40b2-8aa6-9ff3910f8f0a"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9224aa89-3855-4007-885f-ed87d9e44e88"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c570833b-c28f-47a0-8672-eb300085774a"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""787ae8b7-5a5f-444b-a717-fdfbd85f9d90"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""854003ff-2c91-42f6-af11-435509e25ab5"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""821e02f9-e18a-49d3-84a0-7e33e341bf12"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c08de211-b506-4bc9-b9a9-159cd7c9911c"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9ea94504-d318-457e-beba-227d410236c4"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7f783656-6820-40c7-a167-b372135bf786"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""079b93f2-1c46-4e63-b7b9-d937380245b2"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dismount"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""612d965a-5532-4b1c-919b-5696c90df13a"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Exit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""d1ff1f0a-cbc7-4ce6-8d44-047c74c92b80"",
            ""actions"": [
                {
                    ""name"": ""MoveDown"",
                    ""type"": ""Button"",
                    ""id"": ""1b29710b-1b02-4801-8e23-9a092f801de3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveUp"",
                    ""type"": ""Button"",
                    ""id"": ""18206392-b90f-4ba8-a4d8-97e5f0e79209"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""4fb8366a-a5e1-42ab-bbaf-85065358f410"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1784468f-bc09-4e67-8a07-7669a949ab4a"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea777ff8-3704-4467-a23f-8d3882692fd4"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f61e93c6-9593-4e48-a113-791de33cd2dd"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""88f387a8-72f1-4d2f-a931-40f833235a0c"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ccaae2b7-4333-4c2b-ad00-8c09fee21a11"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Controller
        m_Controller = asset.FindActionMap("Controller", throwIfNotFound: true);
        m_Controller_Move = m_Controller.FindAction("Move", throwIfNotFound: true);
        m_Controller_Interact = m_Controller.FindAction("Interact", throwIfNotFound: true);
        m_Controller_Dash = m_Controller.FindAction("Dash", throwIfNotFound: true);
        m_Controller_Block = m_Controller.FindAction("Block", throwIfNotFound: true);
        m_Controller_Strafe = m_Controller.FindAction("Strafe", throwIfNotFound: true);
        m_Controller_Pause = m_Controller.FindAction("Pause", throwIfNotFound: true);
        m_Controller_Exit = m_Controller.FindAction("Exit", throwIfNotFound: true);
        m_Controller_Dismount = m_Controller.FindAction("Dismount", throwIfNotFound: true);
        m_Controller_AnyButton = m_Controller.FindAction("AnyButton", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_MoveDown = m_Menu.FindAction("MoveDown", throwIfNotFound: true);
        m_Menu_MoveUp = m_Menu.FindAction("MoveUp", throwIfNotFound: true);
        m_Menu_Select = m_Menu.FindAction("Select", throwIfNotFound: true);
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

    // Controller
    private readonly InputActionMap m_Controller;
    private IControllerActions m_ControllerActionsCallbackInterface;
    private readonly InputAction m_Controller_Move;
    private readonly InputAction m_Controller_Interact;
    private readonly InputAction m_Controller_Dash;
    private readonly InputAction m_Controller_Block;
    private readonly InputAction m_Controller_Strafe;
    private readonly InputAction m_Controller_Pause;
    private readonly InputAction m_Controller_Exit;
    private readonly InputAction m_Controller_Dismount;
    private readonly InputAction m_Controller_AnyButton;
    public struct ControllerActions
    {
        private @Player1Input m_Wrapper;
        public ControllerActions(@Player1Input wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Controller_Move;
        public InputAction @Interact => m_Wrapper.m_Controller_Interact;
        public InputAction @Dash => m_Wrapper.m_Controller_Dash;
        public InputAction @Block => m_Wrapper.m_Controller_Block;
        public InputAction @Strafe => m_Wrapper.m_Controller_Strafe;
        public InputAction @Pause => m_Wrapper.m_Controller_Pause;
        public InputAction @Exit => m_Wrapper.m_Controller_Exit;
        public InputAction @Dismount => m_Wrapper.m_Controller_Dismount;
        public InputAction @AnyButton => m_Wrapper.m_Controller_AnyButton;
        public InputActionMap Get() { return m_Wrapper.m_Controller; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ControllerActions set) { return set.Get(); }
        public void SetCallbacks(IControllerActions instance)
        {
            if (m_Wrapper.m_ControllerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnMove;
                @Interact.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnInteract;
                @Dash.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnDash;
                @Block.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnBlock;
                @Block.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnBlock;
                @Block.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnBlock;
                @Strafe.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnStrafe;
                @Strafe.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnStrafe;
                @Strafe.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnStrafe;
                @Pause.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnPause;
                @Exit.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnExit;
                @Exit.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnExit;
                @Exit.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnExit;
                @Dismount.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnDismount;
                @Dismount.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnDismount;
                @Dismount.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnDismount;
                @AnyButton.started -= m_Wrapper.m_ControllerActionsCallbackInterface.OnAnyButton;
                @AnyButton.performed -= m_Wrapper.m_ControllerActionsCallbackInterface.OnAnyButton;
                @AnyButton.canceled -= m_Wrapper.m_ControllerActionsCallbackInterface.OnAnyButton;
            }
            m_Wrapper.m_ControllerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @Block.started += instance.OnBlock;
                @Block.performed += instance.OnBlock;
                @Block.canceled += instance.OnBlock;
                @Strafe.started += instance.OnStrafe;
                @Strafe.performed += instance.OnStrafe;
                @Strafe.canceled += instance.OnStrafe;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Exit.started += instance.OnExit;
                @Exit.performed += instance.OnExit;
                @Exit.canceled += instance.OnExit;
                @Dismount.started += instance.OnDismount;
                @Dismount.performed += instance.OnDismount;
                @Dismount.canceled += instance.OnDismount;
                @AnyButton.started += instance.OnAnyButton;
                @AnyButton.performed += instance.OnAnyButton;
                @AnyButton.canceled += instance.OnAnyButton;
            }
        }
    }
    public ControllerActions @Controller => new ControllerActions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private readonly InputAction m_Menu_MoveDown;
    private readonly InputAction m_Menu_MoveUp;
    private readonly InputAction m_Menu_Select;
    public struct MenuActions
    {
        private @Player1Input m_Wrapper;
        public MenuActions(@Player1Input wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveDown => m_Wrapper.m_Menu_MoveDown;
        public InputAction @MoveUp => m_Wrapper.m_Menu_MoveUp;
        public InputAction @Select => m_Wrapper.m_Menu_Select;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                @MoveDown.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnMoveDown;
                @MoveDown.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnMoveDown;
                @MoveDown.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnMoveDown;
                @MoveUp.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnMoveUp;
                @MoveUp.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnMoveUp;
                @MoveUp.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnMoveUp;
                @Select.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnSelect;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveDown.started += instance.OnMoveDown;
                @MoveDown.performed += instance.OnMoveDown;
                @MoveDown.canceled += instance.OnMoveDown;
                @MoveUp.started += instance.OnMoveUp;
                @MoveUp.performed += instance.OnMoveUp;
                @MoveUp.canceled += instance.OnMoveUp;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
            }
        }
    }
    public MenuActions @Menu => new MenuActions(this);
    public interface IControllerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnBlock(InputAction.CallbackContext context);
        void OnStrafe(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnExit(InputAction.CallbackContext context);
        void OnDismount(InputAction.CallbackContext context);
        void OnAnyButton(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnMoveDown(InputAction.CallbackContext context);
        void OnMoveUp(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
    }
}
