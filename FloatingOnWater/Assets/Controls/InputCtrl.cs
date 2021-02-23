// GENERATED AUTOMATICALLY FROM 'Assets/Controls/InputCtrl.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputCtrl : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputCtrl()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputCtrl"",
    ""maps"": [
        {
            ""name"": ""streerCtrl"",
            ""id"": ""1cfa0183-f3ea-4f3d-8b29-822221aaefc5"",
            ""actions"": [
                {
                    ""name"": ""streeringLeft"",
                    ""type"": ""Button"",
                    ""id"": ""2d2018b7-dd55-44dd-8a27-c1883d8d3a40"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""streeringRight"",
                    ""type"": ""Button"",
                    ""id"": ""891521c6-a220-4b40-947b-4910329d2eca"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""X"",
                    ""type"": ""Button"",
                    ""id"": ""36ffc64c-8533-4023-b407-62969477677c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""O"",
                    ""type"": ""Button"",
                    ""id"": ""a52e3128-5ab4-48d5-9c25-c7ebb1ca08ec"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Square"",
                    ""type"": ""Button"",
                    ""id"": ""93233afd-c485-4ecf-acab-e227bdd57d62"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Triangle"",
                    ""type"": ""Button"",
                    ""id"": ""22246054-9753-4c79-b8fa-01f260ce9cc5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""49a07c12-9a3f-44ca-bfa7-11f44d33101c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""a3a909b4-8092-4221-8ee7-5792e46696d8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""90805a36-2b59-4079-859d-67450f492eaa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""5a207e07-739e-4fb7-9614-1fb6656e3204"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Plus"",
                    ""type"": ""Button"",
                    ""id"": ""56605b43-8556-46f8-bdba-636aea288c59"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Less"",
                    ""type"": ""Button"",
                    ""id"": ""2378c17e-d8b2-49d2-9720-f49e21a80470"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6ceda49e-4b11-4476-a685-b6bdd5688827"",
                    ""path"": ""<HID::Logitech G29 Driving Force Racing Wheel>/stick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""streeringLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""58f7bed9-330d-442e-b726-8d2eaebc7ef9"",
                    ""path"": ""<HID::Logitech G29 Driving Force Racing Wheel>/stick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""streeringRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c1495a3-1d18-42e0-bbab-50d45ee3c334"",
                    ""path"": ""<HID::Logitech G29 Driving Force Racing Wheel>/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a47f29f5-32ed-4d7d-803a-ec95b708b1ab"",
                    ""path"": ""<HID::Logitech G29 Driving Force Racing Wheel>/button3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""O"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""244ee65f-bca3-43ce-b82a-c3a16405826c"",
                    ""path"": ""<HID::Logitech G29 Driving Force Racing Wheel>/button2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Square"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dbdcfc9e-e323-46b6-8cf0-7b60b686c945"",
                    ""path"": ""<HID::Logitech G29 Driving Force Racing Wheel>/button4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Triangle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9e83b45f-5fd3-476b-b7f2-29a4765a6ed9"",
                    ""path"": ""<HID::Logitech G29 Driving Force Racing Wheel>/hat/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""08cf0c05-9b58-49cf-b132-083c0bdb269d"",
                    ""path"": ""<HID::Logitech G29 Driving Force Racing Wheel>/hat/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c35a08ad-8376-4fbb-aa14-3e02c0edf4f6"",
                    ""path"": ""<HID::Logitech G29 Driving Force Racing Wheel>/hat/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""056df2d9-d61c-4f49-88b2-140455b5754c"",
                    ""path"": ""<HID::Logitech G29 Driving Force Racing Wheel>/hat/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9b54d0cc-98ea-40e4-b9a5-524f67435b22"",
                    ""path"": ""<HID::Logitech G29 Driving Force Racing Wheel>/button20"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Plus"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1bfc4235-92d5-49b8-8f72-34695208d6d9"",
                    ""path"": ""<HID::Logitech G29 Driving Force Racing Wheel>/button21"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Less"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""throttleCtrl"",
            ""id"": ""42739901-2797-4bd1-9900-2169a5161551"",
            ""actions"": [
                {
                    ""name"": ""UpThruster"",
                    ""type"": ""Button"",
                    ""id"": ""7cc47d66-2243-437e-9e72-cf58727f90c7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DownThruster"",
                    ""type"": ""Button"",
                    ""id"": ""39cf02cb-6fef-419d-8c1b-691190a4c25f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""947e3569-0c10-47ae-8724-8be02a3e59dd"",
                    ""path"": ""<HID::Mad Catz Saitek Pro Flight X-56 Rhino Throttle>/stick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UpThruster"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1cf38780-0d16-4d08-95b6-3f5a42f00afb"",
                    ""path"": ""<HID::Mad Catz Saitek Pro Flight X-56 Rhino Throttle>/stick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DownThruster"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""stickctrl"",
            ""id"": ""84bca1c4-cc92-4a05-ba4b-c082d8732fc7"",
            ""actions"": [
                {
                    ""name"": ""up"",
                    ""type"": ""Value"",
                    ""id"": ""13dc7194-c743-4cb7-a83e-5a8b0b50d4d3"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""down"",
                    ""type"": ""Value"",
                    ""id"": ""f2a03159-e87e-4ee5-9fd1-bf046698ec2b"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""left"",
                    ""type"": ""Button"",
                    ""id"": ""9a912abd-8c78-4de9-ade9-2e0474ba95dd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""647a6f67-7574-4ed2-a6a8-b4eacb39efa5"",
                    ""path"": ""<Joystick>/stick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5e777c11-27b3-4c47-957d-805f0e5596df"",
                    ""path"": ""<Joystick>/stick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b47fa18e-6b3d-4c1a-9079-5f0c190edef0"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // streerCtrl
        m_streerCtrl = asset.FindActionMap("streerCtrl", throwIfNotFound: true);
        m_streerCtrl_streeringLeft = m_streerCtrl.FindAction("streeringLeft", throwIfNotFound: true);
        m_streerCtrl_streeringRight = m_streerCtrl.FindAction("streeringRight", throwIfNotFound: true);
        m_streerCtrl_X = m_streerCtrl.FindAction("X", throwIfNotFound: true);
        m_streerCtrl_O = m_streerCtrl.FindAction("O", throwIfNotFound: true);
        m_streerCtrl_Square = m_streerCtrl.FindAction("Square", throwIfNotFound: true);
        m_streerCtrl_Triangle = m_streerCtrl.FindAction("Triangle", throwIfNotFound: true);
        m_streerCtrl_Up = m_streerCtrl.FindAction("Up", throwIfNotFound: true);
        m_streerCtrl_Down = m_streerCtrl.FindAction("Down", throwIfNotFound: true);
        m_streerCtrl_Left = m_streerCtrl.FindAction("Left", throwIfNotFound: true);
        m_streerCtrl_Right = m_streerCtrl.FindAction("Right", throwIfNotFound: true);
        m_streerCtrl_Plus = m_streerCtrl.FindAction("Plus", throwIfNotFound: true);
        m_streerCtrl_Less = m_streerCtrl.FindAction("Less", throwIfNotFound: true);
        // throttleCtrl
        m_throttleCtrl = asset.FindActionMap("throttleCtrl", throwIfNotFound: true);
        m_throttleCtrl_UpThruster = m_throttleCtrl.FindAction("UpThruster", throwIfNotFound: true);
        m_throttleCtrl_DownThruster = m_throttleCtrl.FindAction("DownThruster", throwIfNotFound: true);
        // stickctrl
        m_stickctrl = asset.FindActionMap("stickctrl", throwIfNotFound: true);
        m_stickctrl_up = m_stickctrl.FindAction("up", throwIfNotFound: true);
        m_stickctrl_down = m_stickctrl.FindAction("down", throwIfNotFound: true);
        m_stickctrl_left = m_stickctrl.FindAction("left", throwIfNotFound: true);
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

    // streerCtrl
    private readonly InputActionMap m_streerCtrl;
    private IStreerCtrlActions m_StreerCtrlActionsCallbackInterface;
    private readonly InputAction m_streerCtrl_streeringLeft;
    private readonly InputAction m_streerCtrl_streeringRight;
    private readonly InputAction m_streerCtrl_X;
    private readonly InputAction m_streerCtrl_O;
    private readonly InputAction m_streerCtrl_Square;
    private readonly InputAction m_streerCtrl_Triangle;
    private readonly InputAction m_streerCtrl_Up;
    private readonly InputAction m_streerCtrl_Down;
    private readonly InputAction m_streerCtrl_Left;
    private readonly InputAction m_streerCtrl_Right;
    private readonly InputAction m_streerCtrl_Plus;
    private readonly InputAction m_streerCtrl_Less;
    public struct StreerCtrlActions
    {
        private @InputCtrl m_Wrapper;
        public StreerCtrlActions(@InputCtrl wrapper) { m_Wrapper = wrapper; }
        public InputAction @streeringLeft => m_Wrapper.m_streerCtrl_streeringLeft;
        public InputAction @streeringRight => m_Wrapper.m_streerCtrl_streeringRight;
        public InputAction @X => m_Wrapper.m_streerCtrl_X;
        public InputAction @O => m_Wrapper.m_streerCtrl_O;
        public InputAction @Square => m_Wrapper.m_streerCtrl_Square;
        public InputAction @Triangle => m_Wrapper.m_streerCtrl_Triangle;
        public InputAction @Up => m_Wrapper.m_streerCtrl_Up;
        public InputAction @Down => m_Wrapper.m_streerCtrl_Down;
        public InputAction @Left => m_Wrapper.m_streerCtrl_Left;
        public InputAction @Right => m_Wrapper.m_streerCtrl_Right;
        public InputAction @Plus => m_Wrapper.m_streerCtrl_Plus;
        public InputAction @Less => m_Wrapper.m_streerCtrl_Less;
        public InputActionMap Get() { return m_Wrapper.m_streerCtrl; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(StreerCtrlActions set) { return set.Get(); }
        public void SetCallbacks(IStreerCtrlActions instance)
        {
            if (m_Wrapper.m_StreerCtrlActionsCallbackInterface != null)
            {
                @streeringLeft.started -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnStreeringLeft;
                @streeringLeft.performed -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnStreeringLeft;
                @streeringLeft.canceled -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnStreeringLeft;
                @streeringRight.started -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnStreeringRight;
                @streeringRight.performed -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnStreeringRight;
                @streeringRight.canceled -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnStreeringRight;
                @X.started -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnX;
                @X.performed -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnX;
                @X.canceled -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnX;
                @O.started -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnO;
                @O.performed -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnO;
                @O.canceled -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnO;
                @Square.started -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnSquare;
                @Square.performed -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnSquare;
                @Square.canceled -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnSquare;
                @Triangle.started -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnTriangle;
                @Triangle.performed -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnTriangle;
                @Triangle.canceled -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnTriangle;
                @Up.started -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnUp;
                @Up.performed -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnUp;
                @Up.canceled -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnUp;
                @Down.started -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnDown;
                @Down.performed -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnDown;
                @Down.canceled -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnDown;
                @Left.started -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnLeft;
                @Left.performed -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnLeft;
                @Left.canceled -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnLeft;
                @Right.started -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnRight;
                @Right.performed -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnRight;
                @Right.canceled -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnRight;
                @Plus.started -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnPlus;
                @Plus.performed -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnPlus;
                @Plus.canceled -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnPlus;
                @Less.started -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnLess;
                @Less.performed -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnLess;
                @Less.canceled -= m_Wrapper.m_StreerCtrlActionsCallbackInterface.OnLess;
            }
            m_Wrapper.m_StreerCtrlActionsCallbackInterface = instance;
            if (instance != null)
            {
                @streeringLeft.started += instance.OnStreeringLeft;
                @streeringLeft.performed += instance.OnStreeringLeft;
                @streeringLeft.canceled += instance.OnStreeringLeft;
                @streeringRight.started += instance.OnStreeringRight;
                @streeringRight.performed += instance.OnStreeringRight;
                @streeringRight.canceled += instance.OnStreeringRight;
                @X.started += instance.OnX;
                @X.performed += instance.OnX;
                @X.canceled += instance.OnX;
                @O.started += instance.OnO;
                @O.performed += instance.OnO;
                @O.canceled += instance.OnO;
                @Square.started += instance.OnSquare;
                @Square.performed += instance.OnSquare;
                @Square.canceled += instance.OnSquare;
                @Triangle.started += instance.OnTriangle;
                @Triangle.performed += instance.OnTriangle;
                @Triangle.canceled += instance.OnTriangle;
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
                @Plus.started += instance.OnPlus;
                @Plus.performed += instance.OnPlus;
                @Plus.canceled += instance.OnPlus;
                @Less.started += instance.OnLess;
                @Less.performed += instance.OnLess;
                @Less.canceled += instance.OnLess;
            }
        }
    }
    public StreerCtrlActions @streerCtrl => new StreerCtrlActions(this);

    // throttleCtrl
    private readonly InputActionMap m_throttleCtrl;
    private IThrottleCtrlActions m_ThrottleCtrlActionsCallbackInterface;
    private readonly InputAction m_throttleCtrl_UpThruster;
    private readonly InputAction m_throttleCtrl_DownThruster;
    public struct ThrottleCtrlActions
    {
        private @InputCtrl m_Wrapper;
        public ThrottleCtrlActions(@InputCtrl wrapper) { m_Wrapper = wrapper; }
        public InputAction @UpThruster => m_Wrapper.m_throttleCtrl_UpThruster;
        public InputAction @DownThruster => m_Wrapper.m_throttleCtrl_DownThruster;
        public InputActionMap Get() { return m_Wrapper.m_throttleCtrl; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ThrottleCtrlActions set) { return set.Get(); }
        public void SetCallbacks(IThrottleCtrlActions instance)
        {
            if (m_Wrapper.m_ThrottleCtrlActionsCallbackInterface != null)
            {
                @UpThruster.started -= m_Wrapper.m_ThrottleCtrlActionsCallbackInterface.OnUpThruster;
                @UpThruster.performed -= m_Wrapper.m_ThrottleCtrlActionsCallbackInterface.OnUpThruster;
                @UpThruster.canceled -= m_Wrapper.m_ThrottleCtrlActionsCallbackInterface.OnUpThruster;
                @DownThruster.started -= m_Wrapper.m_ThrottleCtrlActionsCallbackInterface.OnDownThruster;
                @DownThruster.performed -= m_Wrapper.m_ThrottleCtrlActionsCallbackInterface.OnDownThruster;
                @DownThruster.canceled -= m_Wrapper.m_ThrottleCtrlActionsCallbackInterface.OnDownThruster;
            }
            m_Wrapper.m_ThrottleCtrlActionsCallbackInterface = instance;
            if (instance != null)
            {
                @UpThruster.started += instance.OnUpThruster;
                @UpThruster.performed += instance.OnUpThruster;
                @UpThruster.canceled += instance.OnUpThruster;
                @DownThruster.started += instance.OnDownThruster;
                @DownThruster.performed += instance.OnDownThruster;
                @DownThruster.canceled += instance.OnDownThruster;
            }
        }
    }
    public ThrottleCtrlActions @throttleCtrl => new ThrottleCtrlActions(this);

    // stickctrl
    private readonly InputActionMap m_stickctrl;
    private IStickctrlActions m_StickctrlActionsCallbackInterface;
    private readonly InputAction m_stickctrl_up;
    private readonly InputAction m_stickctrl_down;
    private readonly InputAction m_stickctrl_left;
    public struct StickctrlActions
    {
        private @InputCtrl m_Wrapper;
        public StickctrlActions(@InputCtrl wrapper) { m_Wrapper = wrapper; }
        public InputAction @up => m_Wrapper.m_stickctrl_up;
        public InputAction @down => m_Wrapper.m_stickctrl_down;
        public InputAction @left => m_Wrapper.m_stickctrl_left;
        public InputActionMap Get() { return m_Wrapper.m_stickctrl; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(StickctrlActions set) { return set.Get(); }
        public void SetCallbacks(IStickctrlActions instance)
        {
            if (m_Wrapper.m_StickctrlActionsCallbackInterface != null)
            {
                @up.started -= m_Wrapper.m_StickctrlActionsCallbackInterface.OnUp;
                @up.performed -= m_Wrapper.m_StickctrlActionsCallbackInterface.OnUp;
                @up.canceled -= m_Wrapper.m_StickctrlActionsCallbackInterface.OnUp;
                @down.started -= m_Wrapper.m_StickctrlActionsCallbackInterface.OnDown;
                @down.performed -= m_Wrapper.m_StickctrlActionsCallbackInterface.OnDown;
                @down.canceled -= m_Wrapper.m_StickctrlActionsCallbackInterface.OnDown;
                @left.started -= m_Wrapper.m_StickctrlActionsCallbackInterface.OnLeft;
                @left.performed -= m_Wrapper.m_StickctrlActionsCallbackInterface.OnLeft;
                @left.canceled -= m_Wrapper.m_StickctrlActionsCallbackInterface.OnLeft;
            }
            m_Wrapper.m_StickctrlActionsCallbackInterface = instance;
            if (instance != null)
            {
                @up.started += instance.OnUp;
                @up.performed += instance.OnUp;
                @up.canceled += instance.OnUp;
                @down.started += instance.OnDown;
                @down.performed += instance.OnDown;
                @down.canceled += instance.OnDown;
                @left.started += instance.OnLeft;
                @left.performed += instance.OnLeft;
                @left.canceled += instance.OnLeft;
            }
        }
    }
    public StickctrlActions @stickctrl => new StickctrlActions(this);
    public interface IStreerCtrlActions
    {
        void OnStreeringLeft(InputAction.CallbackContext context);
        void OnStreeringRight(InputAction.CallbackContext context);
        void OnX(InputAction.CallbackContext context);
        void OnO(InputAction.CallbackContext context);
        void OnSquare(InputAction.CallbackContext context);
        void OnTriangle(InputAction.CallbackContext context);
        void OnUp(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
        void OnLeft(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
        void OnPlus(InputAction.CallbackContext context);
        void OnLess(InputAction.CallbackContext context);
    }
    public interface IThrottleCtrlActions
    {
        void OnUpThruster(InputAction.CallbackContext context);
        void OnDownThruster(InputAction.CallbackContext context);
    }
    public interface IStickctrlActions
    {
        void OnUp(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
        void OnLeft(InputAction.CallbackContext context);
    }
}
