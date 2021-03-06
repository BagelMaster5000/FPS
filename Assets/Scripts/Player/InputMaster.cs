// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Player/InputMaster.inputactions'

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
                    ""name"": ""Pause"",
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
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b4529bf5-2a87-4386-88e6-c1fbd54b79ac"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Game"",
            ""id"": ""e64f0e50-ed16-44ec-9ab7-a1f5bc12f237"",
            ""actions"": [
                {
                    ""name"": ""Looking"",
                    ""type"": ""Value"",
                    ""id"": ""068d49ea-2ad6-499f-bf81-2cd917e5d8ab"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Moving"",
                    ""type"": ""Value"",
                    ""id"": ""b15e82e5-33f1-4b43-81fe-9c0352657754"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""7568fb5e-d587-48c5-b567-6829589829c1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprinting"",
                    ""type"": ""Button"",
                    ""id"": ""a11af87d-0ab5-4b3d-9c24-2702da044ae6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Scope"",
                    ""type"": ""Button"",
                    ""id"": ""93c79f0c-87ef-403e-b95d-8232e9908222"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""888d0ebe-5fbf-49e5-bac8-49d18811caf7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""2f04053a-c31a-475a-9393-c760fb3eef22"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Swap Gun"",
                    ""type"": ""Button"",
                    ""id"": ""dc10a9ac-2bdf-4b26-8c02-69f99fd97145"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""c2281bad-519c-4d00-a3fd-2562af865529"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""88135299-fcc2-4b8f-b268-4e89625d2482"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Looking"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b31b3125-518f-4e65-8027-e1e6d4ed95c3"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=9,y=6)"",
                    ""groups"": ""Controller"",
                    ""action"": ""Looking"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""c65d4b43-8cd1-42e5-ac81-21b353c4b331"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Moving"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""328101f0-4dfc-4702-9501-4d5d16662360"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Moving"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""51379913-f8d9-4cb7-8f1c-963e9c2ace02"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Moving"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d71f2fe1-0da4-42de-8ab7-18af14d6e57e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Moving"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9623c677-b66d-450f-9f58-597059c06225"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Moving"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""faa47028-51ef-459a-a409-816da96edcf1"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Moving"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7b9a285c-b3fc-4dad-8e0e-77bcb6f4fb78"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e5fea389-6788-4d97-acba-96d281169a08"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""64677b2a-a5db-4aa5-90d6-8e29b893d33f"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Sprinting"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d0978683-bdc5-41ad-a59c-a29a376520eb"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Sprinting"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d2e8e609-9dcc-4781-848f-556dd9bbbd2d"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Scope"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b2ccc40a-bd4e-4f87-b8f9-479a678d025d"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Scope"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7770e552-dd4e-4201-beb6-857113dc710a"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bbb32f50-e751-44ca-a246-df245f47a4bd"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d369c3c9-fb66-4318-aaa7-8e21d060b3d3"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""abc48cd6-a6f0-47e9-8062-4134cdef1e38"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""709429fb-acdb-4365-8359-538bcf3fe9d9"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Swap Gun"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c953ce2c-aad8-4ba1-8c8b-afd0b505a3fe"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Swap Gun"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7c29bf3d-cabb-4724-984b-c1625f0d5dc3"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9641d7b8-8e0d-4b61-9d4f-103dbbe6a9e2"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard and Mouse"",
            ""bindingGroup"": ""Keyboard and Mouse"",
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
        },
        {
            ""name"": ""Controller"",
            ""bindingGroup"": ""Controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // LevelMenu
        m_LevelMenu = asset.FindActionMap("LevelMenu", throwIfNotFound: true);
        m_LevelMenu_Pause = m_LevelMenu.FindAction("Pause", throwIfNotFound: true);
        // Game
        m_Game = asset.FindActionMap("Game", throwIfNotFound: true);
        m_Game_Looking = m_Game.FindAction("Looking", throwIfNotFound: true);
        m_Game_Moving = m_Game.FindAction("Moving", throwIfNotFound: true);
        m_Game_Jump = m_Game.FindAction("Jump", throwIfNotFound: true);
        m_Game_Sprinting = m_Game.FindAction("Sprinting", throwIfNotFound: true);
        m_Game_Scope = m_Game.FindAction("Scope", throwIfNotFound: true);
        m_Game_Fire = m_Game.FindAction("Fire", throwIfNotFound: true);
        m_Game_Reload = m_Game.FindAction("Reload", throwIfNotFound: true);
        m_Game_SwapGun = m_Game.FindAction("Swap Gun", throwIfNotFound: true);
        m_Game_Interact = m_Game.FindAction("Interact", throwIfNotFound: true);
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
    private readonly InputAction m_LevelMenu_Pause;
    public struct LevelMenuActions
    {
        private @InputMaster m_Wrapper;
        public LevelMenuActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pause => m_Wrapper.m_LevelMenu_Pause;
        public InputActionMap Get() { return m_Wrapper.m_LevelMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(LevelMenuActions set) { return set.Get(); }
        public void SetCallbacks(ILevelMenuActions instance)
        {
            if (m_Wrapper.m_LevelMenuActionsCallbackInterface != null)
            {
                @Pause.started -= m_Wrapper.m_LevelMenuActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_LevelMenuActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_LevelMenuActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_LevelMenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public LevelMenuActions @LevelMenu => new LevelMenuActions(this);

    // Game
    private readonly InputActionMap m_Game;
    private IGameActions m_GameActionsCallbackInterface;
    private readonly InputAction m_Game_Looking;
    private readonly InputAction m_Game_Moving;
    private readonly InputAction m_Game_Jump;
    private readonly InputAction m_Game_Sprinting;
    private readonly InputAction m_Game_Scope;
    private readonly InputAction m_Game_Fire;
    private readonly InputAction m_Game_Reload;
    private readonly InputAction m_Game_SwapGun;
    private readonly InputAction m_Game_Interact;
    public struct GameActions
    {
        private @InputMaster m_Wrapper;
        public GameActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Looking => m_Wrapper.m_Game_Looking;
        public InputAction @Moving => m_Wrapper.m_Game_Moving;
        public InputAction @Jump => m_Wrapper.m_Game_Jump;
        public InputAction @Sprinting => m_Wrapper.m_Game_Sprinting;
        public InputAction @Scope => m_Wrapper.m_Game_Scope;
        public InputAction @Fire => m_Wrapper.m_Game_Fire;
        public InputAction @Reload => m_Wrapper.m_Game_Reload;
        public InputAction @SwapGun => m_Wrapper.m_Game_SwapGun;
        public InputAction @Interact => m_Wrapper.m_Game_Interact;
        public InputActionMap Get() { return m_Wrapper.m_Game; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameActions set) { return set.Get(); }
        public void SetCallbacks(IGameActions instance)
        {
            if (m_Wrapper.m_GameActionsCallbackInterface != null)
            {
                @Looking.started -= m_Wrapper.m_GameActionsCallbackInterface.OnLooking;
                @Looking.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnLooking;
                @Looking.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnLooking;
                @Moving.started -= m_Wrapper.m_GameActionsCallbackInterface.OnMoving;
                @Moving.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnMoving;
                @Moving.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnMoving;
                @Jump.started -= m_Wrapper.m_GameActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnJump;
                @Sprinting.started -= m_Wrapper.m_GameActionsCallbackInterface.OnSprinting;
                @Sprinting.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnSprinting;
                @Sprinting.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnSprinting;
                @Scope.started -= m_Wrapper.m_GameActionsCallbackInterface.OnScope;
                @Scope.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnScope;
                @Scope.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnScope;
                @Fire.started -= m_Wrapper.m_GameActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnFire;
                @Reload.started -= m_Wrapper.m_GameActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnReload;
                @SwapGun.started -= m_Wrapper.m_GameActionsCallbackInterface.OnSwapGun;
                @SwapGun.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnSwapGun;
                @SwapGun.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnSwapGun;
                @Interact.started -= m_Wrapper.m_GameActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_GameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Looking.started += instance.OnLooking;
                @Looking.performed += instance.OnLooking;
                @Looking.canceled += instance.OnLooking;
                @Moving.started += instance.OnMoving;
                @Moving.performed += instance.OnMoving;
                @Moving.canceled += instance.OnMoving;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Sprinting.started += instance.OnSprinting;
                @Sprinting.performed += instance.OnSprinting;
                @Sprinting.canceled += instance.OnSprinting;
                @Scope.started += instance.OnScope;
                @Scope.performed += instance.OnScope;
                @Scope.canceled += instance.OnScope;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
                @SwapGun.started += instance.OnSwapGun;
                @SwapGun.performed += instance.OnSwapGun;
                @SwapGun.canceled += instance.OnSwapGun;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
            }
        }
    }
    public GameActions @Game => new GameActions(this);
    private int m_KeyboardandMouseSchemeIndex = -1;
    public InputControlScheme KeyboardandMouseScheme
    {
        get
        {
            if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and Mouse");
            return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
        }
    }
    private int m_ControllerSchemeIndex = -1;
    public InputControlScheme ControllerScheme
    {
        get
        {
            if (m_ControllerSchemeIndex == -1) m_ControllerSchemeIndex = asset.FindControlSchemeIndex("Controller");
            return asset.controlSchemes[m_ControllerSchemeIndex];
        }
    }
    public interface ILevelMenuActions
    {
        void OnPause(InputAction.CallbackContext context);
    }
    public interface IGameActions
    {
        void OnLooking(InputAction.CallbackContext context);
        void OnMoving(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSprinting(InputAction.CallbackContext context);
        void OnScope(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnSwapGun(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
}
