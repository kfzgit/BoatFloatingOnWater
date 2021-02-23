using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class testInputS : MonoBehaviour
{
    private InputCtrl inputCtrl;

    //LogitechGSDK.LogiControllerPropertiesData properties;

    private void Awake()
    {
        Debug.Log("SteeringInit:" + LogitechGSDK.LogiSteeringInitialize(false));

        inputCtrl = new InputCtrl();
        inputCtrl.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnApplicationQuit()
    {
        Debug.Log("SteeringShutdown:" + LogitechGSDK.LogiSteeringShutdown());
    }

    // Update is called once per frame
    void Update()
    {
        //if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
        //{
        //    print("aaaaaaaaa");

        //    properties = new LogitechGSDK.LogiControllerPropertiesData();
        //    properties.wheelRange = 90;
        //    properties.forceEnable = true;
        //    properties.overallGain = 80;
        //    properties.springGain = 80;
        //    properties.damperGain = 80;
        //    properties.allowGameSettings = true;
        //    properties.combinePedals = false;
        //    properties.defaultSpringEnabled = true;
        //    properties.defaultSpringGain = 80;
        //    LogitechGSDK.LogiSetPreferredControllerProperties(properties);

        //    if (LogitechGSDK.LogiButtonTriggered(0, 0))
        //        Debug.Log("KeyCode.Joystick1Button0  方向盘 X 键");
        //    if (LogitechGSDK.LogiButtonTriggered(0, 1))
        //        Debug.Log("KeyCode.Joystick1Button1  方向盘 □ 键");
        //}

        //if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        //{
        //    Debug.Log("KeyCode.Joystick1Button0  方向盘 X 键");
        //}

        //valueLeft = g29Ctrl.streerCtrl.streeringLeft.ReadValue<float>();
        ////print("left " + value1);
        //valueRight = g29Ctrl.streerCtrl.streeringRight.ReadValue<float>();
        ////print("right " + value2);
    }

    //G29 方向盘值
    float valueLeft = 0;
    float valueRight = 0;
    float steeringValue = 0;
    public float GsteeringValue()
    {
        valueLeft = -inputCtrl.streerCtrl.streeringLeft.ReadValue<float>();
        valueRight = inputCtrl.streerCtrl.streeringRight.ReadValue<float>();
        steeringValue = valueRight == 0 ? valueLeft : valueRight;
        return steeringValue;
    }

    //G29 Up
    public bool GUpValue()
    {
        return inputCtrl.streerCtrl.Up.ReadValue<bool>();
    }

    //G29 Down
    public bool GDownValue()
    {
        return inputCtrl.streerCtrl.Down.ReadValue<bool>();
    }

    //G29 Left
    public bool GLeftValue()
    {
        return inputCtrl.streerCtrl.Left.ReadValue<bool>();
    }

    //G29 Right
    public bool GRightValue()
    {
        return inputCtrl.streerCtrl.Right.ReadValue<bool>();
    }

    //G29 X
    public bool GXValue()
    {
        return inputCtrl.streerCtrl.X.ReadValue<bool>();
    }

    //G29 O
    public bool GOValue()
    {
        return inputCtrl.streerCtrl.O.ReadValue<bool>();
    }

    //G29 Square
    public bool GSquareValue()
    {
        return inputCtrl.streerCtrl.Square.ReadValue<bool>();
    }

    //G29 Triangle
    public bool GTriangleValue()
    {
        return inputCtrl.streerCtrl.Triangle.ReadValue<bool>();
    }
}
