// GENERATED AUTOMATICALLY FROM 'Assets/Inputs/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Default"",
            ""id"": ""8892db01-883f-43a1-aa3b-d53608546d87"",
            ""actions"": [
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""9db7c69a-6137-419b-b8a3-d16b4dfbbea1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cfbd88a2-188c-417a-ac32-1fd69f48103c"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""90c48a9f-a687-419a-894a-f37af1b4bfa4"",
                    ""path"": ""<Touchscreen>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""NextTurn"",
            ""id"": ""2dbe82be-2ebb-408f-8253-ce748b7b8b2a"",
            ""actions"": [
                {
                    ""name"": ""SkipTurn"",
                    ""type"": ""Button"",
                    ""id"": ""4c4125d9-b662-4385-91e5-9c9e7d901772"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""287584c8-e294-4e36-bc1e-76aa29720b0d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkipTurn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c2d4c015-c558-4aff-9a81-f8e21de4dbec"",
                    ""path"": ""<Touchscreen>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkipTurn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Init"",
            ""id"": ""b34ed7bb-e459-4f59-8008-4bd7b31562c3"",
            ""actions"": [
                {
                    ""name"": ""PlaceCharacter"",
                    ""type"": ""Button"",
                    ""id"": ""a8ccfe16-5a08-481e-80c0-1175b08ba8ff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e7442e76-2ef4-4679-a683-603b9020a0fe"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlaceCharacter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""329aa2c9-a92a-411c-9629-a6ec0dfeb9fe"",
                    ""path"": ""<Touchscreen>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlaceCharacter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""New control scheme"",
            ""bindingGroup"": ""New control scheme"",
            ""devices"": []
        }
    ]
}");
        // Default
        m_Default = asset.FindActionMap("Default", throwIfNotFound: true);
        m_Default_Click = m_Default.FindAction("Click", throwIfNotFound: true);
        // NextTurn
        m_NextTurn = asset.FindActionMap("NextTurn", throwIfNotFound: true);
        m_NextTurn_SkipTurn = m_NextTurn.FindAction("SkipTurn", throwIfNotFound: true);
        // Init
        m_Init = asset.FindActionMap("Init", throwIfNotFound: true);
        m_Init_PlaceCharacter = m_Init.FindAction("PlaceCharacter", throwIfNotFound: true);
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

    // Default
    private readonly InputActionMap m_Default;
    private IDefaultActions m_DefaultActionsCallbackInterface;
    private readonly InputAction m_Default_Click;
    public struct DefaultActions
    {
        private @Controls m_Wrapper;
        public DefaultActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Click => m_Wrapper.m_Default_Click;
        public InputActionMap Get() { return m_Wrapper.m_Default; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DefaultActions set) { return set.Get(); }
        public void SetCallbacks(IDefaultActions instance)
        {
            if (m_Wrapper.m_DefaultActionsCallbackInterface != null)
            {
                @Click.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnClick;
            }
            m_Wrapper.m_DefaultActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
            }
        }
    }
    public DefaultActions @Default => new DefaultActions(this);

    // NextTurn
    private readonly InputActionMap m_NextTurn;
    private INextTurnActions m_NextTurnActionsCallbackInterface;
    private readonly InputAction m_NextTurn_SkipTurn;
    public struct NextTurnActions
    {
        private @Controls m_Wrapper;
        public NextTurnActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @SkipTurn => m_Wrapper.m_NextTurn_SkipTurn;
        public InputActionMap Get() { return m_Wrapper.m_NextTurn; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(NextTurnActions set) { return set.Get(); }
        public void SetCallbacks(INextTurnActions instance)
        {
            if (m_Wrapper.m_NextTurnActionsCallbackInterface != null)
            {
                @SkipTurn.started -= m_Wrapper.m_NextTurnActionsCallbackInterface.OnSkipTurn;
                @SkipTurn.performed -= m_Wrapper.m_NextTurnActionsCallbackInterface.OnSkipTurn;
                @SkipTurn.canceled -= m_Wrapper.m_NextTurnActionsCallbackInterface.OnSkipTurn;
            }
            m_Wrapper.m_NextTurnActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SkipTurn.started += instance.OnSkipTurn;
                @SkipTurn.performed += instance.OnSkipTurn;
                @SkipTurn.canceled += instance.OnSkipTurn;
            }
        }
    }
    public NextTurnActions @NextTurn => new NextTurnActions(this);

    // Init
    private readonly InputActionMap m_Init;
    private IInitActions m_InitActionsCallbackInterface;
    private readonly InputAction m_Init_PlaceCharacter;
    public struct InitActions
    {
        private @Controls m_Wrapper;
        public InitActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @PlaceCharacter => m_Wrapper.m_Init_PlaceCharacter;
        public InputActionMap Get() { return m_Wrapper.m_Init; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InitActions set) { return set.Get(); }
        public void SetCallbacks(IInitActions instance)
        {
            if (m_Wrapper.m_InitActionsCallbackInterface != null)
            {
                @PlaceCharacter.started -= m_Wrapper.m_InitActionsCallbackInterface.OnPlaceCharacter;
                @PlaceCharacter.performed -= m_Wrapper.m_InitActionsCallbackInterface.OnPlaceCharacter;
                @PlaceCharacter.canceled -= m_Wrapper.m_InitActionsCallbackInterface.OnPlaceCharacter;
            }
            m_Wrapper.m_InitActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PlaceCharacter.started += instance.OnPlaceCharacter;
                @PlaceCharacter.performed += instance.OnPlaceCharacter;
                @PlaceCharacter.canceled += instance.OnPlaceCharacter;
            }
        }
    }
    public InitActions @Init => new InitActions(this);
    private int m_NewcontrolschemeSchemeIndex = -1;
    public InputControlScheme NewcontrolschemeScheme
    {
        get
        {
            if (m_NewcontrolschemeSchemeIndex == -1) m_NewcontrolschemeSchemeIndex = asset.FindControlSchemeIndex("New control scheme");
            return asset.controlSchemes[m_NewcontrolschemeSchemeIndex];
        }
    }
    public interface IDefaultActions
    {
        void OnClick(InputAction.CallbackContext context);
    }
    public interface INextTurnActions
    {
        void OnSkipTurn(InputAction.CallbackContext context);
    }
    public interface IInitActions
    {
        void OnPlaceCharacter(InputAction.CallbackContext context);
    }
}
