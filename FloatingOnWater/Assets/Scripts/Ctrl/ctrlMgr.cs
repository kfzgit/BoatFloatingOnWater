using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ctrlMgr : MonoBehaviour
{
    public static ctrlMgr instance;

    private InputCtrl inputCtrl;

    //LogitechGSDK.LogiControllerPropertiesData properties;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("SteeringInit:" + LogitechGSDK.LogiSteeringInitialize(false));

        inputCtrl = new InputCtrl();
        inputCtrl.Enable();
    }

    void OnApplicationQuit()
    {
        Debug.Log("SteeringShutdown:" + LogitechGSDK.LogiSteeringShutdown());
    }

    // Update is called once per frame
    void Update()
    {
        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
        {
            //properties = new LogitechGSDK.LogiControllerPropertiesData();
            //properties.wheelRange = 90;
            //properties.forceEnable = true;
            //properties.overallGain = 80;
            //properties.springGain = 80;
            //properties.damperGain = 80;
            //properties.allowGameSettings = true;
            //properties.combinePedals = false;
            //properties.defaultSpringEnabled = true;
            //properties.defaultSpringGain = 80;
            //LogitechGSDK.LogiSetPreferredControllerProperties(properties);

            //if (LogitechGSDK.LogiButtonTriggered(0, 0))
            //    Debug.Log("KeyCode.Joystick1Button0  方向盘 X 键");
            //if (LogitechGSDK.LogiButtonTriggered(0, 1))
            //    Debug.Log("KeyCode.Joystick1Button1  方向盘 □ 键");
        }

        //if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        //{
        //    Debug.Log("KeyCode.Joystick1Button0  方向盘 X 键");
        //}

        //valueLeft = g29Ctrl.streerCtrl.streeringLeft.ReadValue<float>();
        ////print("left " + value1);
        //valueRight = g29Ctrl.streerCtrl.streeringRight.ReadValue<float>();
        ////print("right " + value2);
    }

    //X56 推进器的值
    float valueUp = 0;
    float valueDown = 0;
    float throttleValue = 0;
    public float XthrottleValue()
    {
        valueUp = inputCtrl.throttleCtrl.UpThruster.ReadValue<float>();
        valueDown = -inputCtrl.throttleCtrl.DownThruster.ReadValue<float>();
        throttleValue = valueDown == 0 ? valueUp : valueDown;
        return throttleValue;
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

    //G29 UP DOWN
    public float GUpDownValue()
    {
        if(inputCtrl.streerCtrl.Up.ReadValue<float>() == 1)
        {
            return 1;
        }
        if (inputCtrl.streerCtrl.Down.ReadValue<float>() == 1)
        {
            return -1;
        }
        return 0;
    }
    //G29 LEFT RIGHT
    public float GLeftRightValue()
    {
        if (inputCtrl.streerCtrl.Left.ReadValue<float>() == 1)
        {
            return -1;
        }
        if (inputCtrl.streerCtrl.Right.ReadValue<float>() == 1)
        {
            return 1;
        }
        return 0;
    }

    //G29 Up
    private float GUpP0 = 0;
    private float GUpP1 = 0;
    public bool GUpPressed()
    {
        GUpP1 = inputCtrl.streerCtrl.Up.ReadValue<float>();
        if(GUpP0 == 0 && GUpP1 == 1)
        {
            GUpP0 = 1;
            return true;
        }
        if(GUpP1 == 0)
        {
            GUpP0 = 0;
        }

        return false;
    }

    private float GUp0 = 0;
    private float GUp1 = 0;
    public bool GUpReleased()
    {
        GUp0 = inputCtrl.streerCtrl.Up.ReadValue<float>();

        if(GUp0 == 1)
        {
            GUp1 = 1;
        }

        if(GUp1 == 1 && GUp0 == 0)
        {
            GUp1 = 0;
            return true;
        }

        return false;
    }

    //G29 Down
    private float GDown0 = 0;
    private float GDown1 = 0;
    public bool GDownReleased()
    {
        GDown0 = inputCtrl.streerCtrl.Down.ReadValue<float>();

        if(GDown0 == 1)
        {
            GDown1 = 1;
        }

        if(GDown1 == 1 && GDown0 == 0)
        {
            GDown1 = 0;
            return true;
        }

        return false;
    }

    //G29 Left
    private float GLeft0 = 0;
    private float GLeft1 = 0;
    public bool GLeftReleased()
    {
        GLeft0 = inputCtrl.streerCtrl.Left.ReadValue<float>();

        if (GLeft0 == 1)
        {
            GLeft1 = 1;
        }

        if (GLeft1 == 1 && GLeft0 == 0)
        {
            GLeft1 = 0;
            return true;
        }

        return false;
    }

    //G29 Right
    private float GRight0 = 0;
    private float GRight1 = 0;
    public bool GRightReleased()
    {
        GRight0 = inputCtrl.streerCtrl.Right.ReadValue<float>();

        if (GRight0 == 1)
        {
            GRight1 = 1;
        }

        if (GRight1 == 1 && GRight0 == 0)
        {
            GRight1 = 0;
            return true;
        }

        return false;
    }

    //G29 X
    private float GX0 = 0;
    private float GX1 = 0;
    public bool GXReleased()
    {
        GX0 = inputCtrl.streerCtrl.X.ReadValue<float>();

        if (GX0 == 1)
        {
            GX1 = 1;
        }

        if (GX1 == 1 && GX0 == 0)
        {
            GX1 = 0;
            return true;
        }

        return false;
    }

    //G29 O
    private float GO0 = 0;
    private float GO1 = 0;
    public bool GOReleased()
    {
        GO0 = inputCtrl.streerCtrl.O.ReadValue<float>();

        if (GO0 == 1)
        {
            GO1 = 1;
        }

        if (GO1 == 1 && GO0 == 0)
        {
            GO1 = 0;
            return true;
        }

        return false;
    }

    //G29 Square
    private float GSquare0 = 0;
    private float GSquare1 = 0;
    public bool GSquareReleased()
    {
        GSquare0 = inputCtrl.streerCtrl.Square.ReadValue<float>();

        if (GSquare0 == 1)
        {
            GSquare1 = 1;
        }

        if (GSquare1 == 1 && GSquare0 == 0)
        {
            GSquare1 = 0;
            return true;
        }
        return false;
    }

    //G29 Triangle
    private float GTriangle0 = 0;
    private float GTriangle1 = 0;
    public bool GTriangleReleased()
    {
        GTriangle0 = inputCtrl.streerCtrl.Triangle.ReadValue<float>();

        if (GTriangle0 == 1)
        {
            GTriangle1 = 1;
        }

        if (GTriangle1 == 1 && GTriangle0 == 0)
        {
            GTriangle1 = 0;
            return true;
        }
        return false;
    }

    //G29 Plus
    private float GPlus0 = 0;
    private float GPlus1 = 0;
    public bool GPlusReleased()
    {
        GPlus0 = inputCtrl.streerCtrl.Plus.ReadValue<float>();

        if (GPlus0 == 1)
        {
            GPlus1 = 1;
        }

        if (GPlus1 == 1 && GPlus0 == 0)
        {
            GPlus1 = 0;
            return true;
        }
        return false;
    }

    //G29 Less
    private float GLess0 = 0;
    private float GLess1 = 0;
    public bool GLessReleased()
    {
        GLess0 = inputCtrl.streerCtrl.Less.ReadValue<float>();

        if (GLess0 == 1)
        {
            GLess1 = 1;
        }

        if (GLess1 == 1 && GLess0 == 0)
        {
            GLess1 = 0;
            return true;
        }
        return false;
    }


    void OnDestroy()
    {
        inputCtrl.Disable();
        Debug.Log("SteeringShutdown:" + LogitechGSDK.LogiSteeringShutdown());
    }
}
