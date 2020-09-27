// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""LevelMenu"",
            ""id"": ""3cd795d8-91f3-48f4-8c4e-02493bbcd85f"",
            ""actions"": [
                {
                    ""name"": ""Exit"",
                    ""type"": ""Button"",
                    ""id"": ""1fe43ab4-5599-4b8e-b1fc-4a2d140e1993"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0658e8e9-4c68-4ed6-8068-bba6c6353a87"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Exit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // LevelMenu
        m_LevelMenu = asset.FindActionMap("LevelMenu", throwIfNotFound: true);
        m_LevelMenu_Exit = m_LevelMenu.FindAction("Exit", throwIfNotFound: true);
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

    // LevelMenu
    private readonly InputActionMap m_LevelMenu;
    private ILevelMenuActions m_LevelMenuActionsCallbackInterface;
    private readonly InputAction m_LevelMenu_Exit;
    public struct LevelMenuActions
    {
        private @InputMaster m_Wrapper;
        public LevelMenuActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Exit => m_Wrapper.m_LevelMenu_Exit;
        public InputActionMap Get() { return m_Wrapper.m_LevelMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(LevelMenuActions set) { return set.Get(); }
        public void SetCallbacks(ILevelMenuActions instance)
        {
            if (m_Wrapper.m_LevelMenuActionsCallbackInterface != null)
            {
                @Exit.started -= m_Wrapper.m_LevelMenuActionsCallbackInterface.OnExit;
                @Exit.performed -= m_Wrapper.m_LevelMenuActionsCallbackInterface.OnExit;
                @Exit.canceled -= m_Wrapper.m_LevelMenuActionsCallbackInterface.OnExit;
            }
            m_Wrapper.m_LevelMenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Exit.started += instance.OnExit;
                @Exit.performed += instance.OnExit;
                @Exit.canceled += instance.OnExit;
            }
        }
    }
    public LevelMenuActions @LevelMenu => new LevelMenuActions(this);
    public interface ILevelMenuActions
    {
        void OnExit(InputAction.CallbackContext context);
    }
}
