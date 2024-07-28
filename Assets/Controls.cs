//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Controls.inputactions
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

public partial class @Controls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""KeyAction"",
            ""id"": ""b71be27a-d4c4-4dd4-b109-124c1f334ee4"",
            ""actions"": [
                {
                    ""name"": ""Keyboard"",
                    ""type"": ""Value"",
                    ""id"": ""c8d13def-1fef-4bbe-80ea-42bdc1fa9566"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""3123a5c9-83cd-43ec-be41-0b921eb2cf0a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Keyboard"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""2a0e815e-3bd7-4cd8-9999-1ff8418ed4fd"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Keyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d00cf15c-d380-4b8a-b2b2-49f4d3a59ed5"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Keyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6fcf2e25-e9fc-42cc-bee4-ec70a00b260a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Keyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8b8c74ce-919d-4faf-b72b-0ec276cb5542"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Keyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Controller"",
            ""bindingGroup"": ""Controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // KeyAction
        m_KeyAction = asset.FindActionMap("KeyAction", throwIfNotFound: true);
        m_KeyAction_Keyboard = m_KeyAction.FindAction("Keyboard", throwIfNotFound: true);
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

    // KeyAction
    private readonly InputActionMap m_KeyAction;
    private List<IKeyActionActions> m_KeyActionActionsCallbackInterfaces = new List<IKeyActionActions>();
    private readonly InputAction m_KeyAction_Keyboard;
    public struct KeyActionActions
    {
        private @Controls m_Wrapper;
        public KeyActionActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Keyboard => m_Wrapper.m_KeyAction_Keyboard;
        public InputActionMap Get() { return m_Wrapper.m_KeyAction; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyActionActions set) { return set.Get(); }
        public void AddCallbacks(IKeyActionActions instance)
        {
            if (instance == null || m_Wrapper.m_KeyActionActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_KeyActionActionsCallbackInterfaces.Add(instance);
            @Keyboard.started += instance.OnKeyboard;
            @Keyboard.performed += instance.OnKeyboard;
            @Keyboard.canceled += instance.OnKeyboard;
        }

        private void UnregisterCallbacks(IKeyActionActions instance)
        {
            @Keyboard.started -= instance.OnKeyboard;
            @Keyboard.performed -= instance.OnKeyboard;
            @Keyboard.canceled -= instance.OnKeyboard;
        }

        public void RemoveCallbacks(IKeyActionActions instance)
        {
            if (m_Wrapper.m_KeyActionActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IKeyActionActions instance)
        {
            foreach (var item in m_Wrapper.m_KeyActionActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_KeyActionActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public KeyActionActions @KeyAction => new KeyActionActions(this);
    private int m_ControllerSchemeIndex = -1;
    public InputControlScheme ControllerScheme
    {
        get
        {
            if (m_ControllerSchemeIndex == -1) m_ControllerSchemeIndex = asset.FindControlSchemeIndex("Controller");
            return asset.controlSchemes[m_ControllerSchemeIndex];
        }
    }
    public interface IKeyActionActions
    {
        void OnKeyboard(InputAction.CallbackContext context);
    }
}