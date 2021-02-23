using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class boatPlayer : MonoBehaviour
{
    public static boatPlayer instance;
    //[DllImport("accWashOut")]
    //static extern float WashoutFilterValue(float u_in);

    private Rigidbody rbody;

    private GameObject boatBodys;

    private Transform SWheelT;

    //前后侧推螺旋桨
    private Transform FrontProp;
    private Transform BackProp;

    //主相机与目标物体之间的距离 
    public float distance = 25.0f;
    private float eulerAngles_x;
    private float eulerAngles_y;

    //水平滚动相关 
    public int distanceMax = 100;//主相机与目标物体之间的最大距离 
    public int distanceMin = 3;//主相机与目标物体之间的最小距离 
    public float xSpeed = 2;//主相机水平方向旋转速度 

    //垂直滚动相关 
    public int yMaxLimit = 60;//最大y（单位是角度） 
    public int yMinLimit = -10;//最小y（单位是角度） 
    public float ySpeed = 70.0f;//主相机纵向旋转速度 

    //滚轮相关 
    public float MouseScrollWheelSensitivity = 20.0f;//鼠标滚轮灵敏度（备注：鼠标滚轮滚动后将调整相机与目标物体之间的间隔） 

    //初始化
    float rotY = 0;
    float rotX = 0;
    float rotZ = 0;

    private float speed = 0;
    private float acc = 0;
    private float yaw = 0;
    private float pitch = 0;
    private float roll = 0;

    float speedCur = 0;

    //转向角速度
    private float TurnSpeed = 0;
    private float TurnSpeed1 = 0;
    //船最大速度
    private float MAXSPEED = 35.0178056639f;
    //速度转速限制下的中间量


    //浮力模拟体
    Ceto.Buoyancy FrontLeft;
    Ceto.Buoyancy FrontRight;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        boatBodys = transform.Find("PhysicalBody").Find("Body").gameObject;
        SWheelT = transform.Find("PhysicalBody").Find("PsyBody").Find("Torus001");
        FrontProp = transform.Find("PhysicalBody").Find("PsyBody").Find("MSBR2");
        BackProp = transform.Find("PhysicalBody").Find("PsyBody").Find("MSBR1");

        Vector3 eulerAngles = Camera.main.transform.eulerAngles;//当前物体的欧拉角 
        eulerAngles_x = eulerAngles.y;
        eulerAngles_y = eulerAngles.x;

        rotY = limitAngle(transform.eulerAngles.y);
        rotX = limitAngle(transform.eulerAngles.x);
        rotZ = limitAngle(transform.eulerAngles.z);

        //发送初始化位置数据
        DataMgr.instance.SendCombineData(rbody, 0, 0, 0, 0, Mathf.PI / 2, transform);

        FrontLeft = transform.Find("PhysicalBody").Find("Buoyancy").Find("FrontLeft").GetComponent<Ceto.Buoyancy>();
        FrontRight = transform.Find("PhysicalBody").Find("Buoyancy").Find("FrontRight").GetComponent<Ceto.Buoyancy>();
    }

    public bool ShowHideBoatC()
    {
        boatBodys.SetActive(!boatBodys.activeSelf);
        return boatBodys.activeSelf;
    }

    // Update is called once per frame
    void Update()
    {
        if (ctrlMgr.instance.GOReleased())
        {
            ShowHideBoatC();
        }

        PlayerControl();
        CameraControl();

        yaw = -(limitAngle(transform.eulerAngles.y) - rotY) / 180 * Mathf.PI * 5;
        pitch = -(limitAngle(transform.eulerAngles.x) - rotX) / 180 * Mathf.PI;
        roll = (limitAngle(transform.eulerAngles.z) - rotZ) / 180 * Mathf.PI + Mathf.PI / 2;

        //加速度使船头抬起
        if(acc >= 0)
        {
            FrontLeft.radius = 1 + (acc > 1f ? 1f : acc);
            FrontRight.radius = 1 + (acc > 1f ? 1f : acc);
        }
        else
        {
            FrontLeft.radius = 1 + (acc < -0.6f ? -0.6f : acc);
            FrontRight.radius = 1 + (acc < -0.6f ? -0.6f : acc);
        }
        
        //发送数据到六自由度设备
        //DataMgr.instance.SendCombineData(rbody, speedCur, WashoutFilterValue(acc), yaw, pitch, roll, transform);
        DataMgr.instance.SendCombineData(rbody, speedCur, acc, yaw, pitch, roll, transform);

        if (GlobalInfoVar.instance)
        {
            GlobalInfoVar.instance.GSpeed = speedCur;
            GlobalInfoVar.instance.RollAngle = limitAngle(transform.eulerAngles.z) - rotZ;
            GlobalInfoVar.instance.PitchAngle = limitAngle(transform.eulerAngles.x) - rotX;
            GlobalInfoVar.instance.BowAngle = yaw;
        }
    }

    void PlayerControl()
    {
        #region 侧推
        float UT = 0;
        float BT = 0;
        if (GlobalVar.subThru)
        {
            UT = Input.GetAxis("Horizontal2");
            BT = Input.GetAxis("Vertical2");

            rbody.AddForceAtPosition(0.45f * UT * transform.right, FrontProp.position);
            rbody.AddForceAtPosition(-0.45f * BT * transform.right, BackProp.position);
            transform.Rotate(0, 4.5f * (UT + BT) * Time.deltaTime, 0);
        }

        GlobalInfoVar.instance.upThrus = UT;
        GlobalInfoVar.instance.backThrus = BT;
        #endregion

        #region 主推以及方向盘

        //float X = Input.GetAxis("Horizontal");
        float X = ctrlMgr.instance.GsteeringValue();

        GlobalInfoVar.instance.rudderPointer = X;
        SWheelT.localEulerAngles = new Vector3(SWheelT.localEulerAngles.x, SWheelT.localEulerAngles.y, X * 450);

        float Y = 0;
        if (GlobalVar.MainThru)
        {
            Y = Input.GetAxis("Vertical1");
        }
        //float Y = ctrlMgr.instance.XthrottleValue();

        GlobalInfoVar.instance.leftThrus = Y;
        GlobalInfoVar.instance.rightThrus = Y;

        #region 正车空车倒车情况判断与优化
        if (Y > 0.01f && speedCur > -0.01f)
        {
            //acc = increSpeed(speed,true);
            //speed += acc * Time.deltaTime * Mathf.Abs(Y);
            acc = increSpeed(Y, speed, true);
            if(speed >= -0.01f)
            {
                speed += acc * Time.deltaTime;
            }
            //speed = Mathf.Clamp(speed, 0.0f, MAXSPEED * Y);
            if(speedCur >= MAXSPEED * Y)
            {
                acc = increSpeed(Y, speed, false);
                speedCur += acc * Time.deltaTime;
                speed += acc * Time.deltaTime;
            }
            else
            {
                speedCur = speed;
            }
        }
        else if (Y < -0.01f && speedCur < 0.01f)
        {
            //speed以正值计算
            acc = -increSpeed(-Y, speed, true);
            if (speed >= -0.01f)
            {
                speed += -acc * Time.deltaTime;
            }
            //speed = Mathf.Clamp(speed, MAXSPEED * Y, 0.0f);
            if (speedCur <= MAXSPEED * Y)
            {
                acc = -increSpeed(-Y, speed, false);
                speedCur += acc * Time.deltaTime;
                speed += -acc * Time.deltaTime;
            }
            else
            {
                speedCur = -speed;
            }
        }
        else if (Y < -0.01f && speedCur > 0.0f)
        {
            acc = 2.065f * Y;
            speedCur += acc * Time.deltaTime;
            if(speed > 0)
            {
                speed += acc * Time.deltaTime;
            }
            else
            {
                speed = 0;
            }
        }
        else if (Y > 0.01f && speedCur < 0.0f)
        {
            acc = 2.065f * Y;
            speedCur += acc * Time.deltaTime;
            if (speed > 0)
            {
                speed += -acc * Time.deltaTime;
            }
            else
            {
                speed = 0;
            }
        }
        else if(speedCur > 0)//正车到空车
        {
            acc = increSpeed(0, speed, false);
            speed = speedCur;
            if (speedCur < 0.01f)
            {
                speedCur = 0;
                acc = 0;
            }
            else
            {
                speedCur += acc * Time.deltaTime;
            }
        }
        else//倒车到空车
        {
            acc = -increSpeed(0, speed, false);
            speed = -speedCur;
            if (speedCur > -0.01f)
            {
                speedCur = 0;
                acc = 0;
            }
            else
            {
                speedCur += acc * Time.deltaTime;
            }
        }
        #endregion

        TurnSpeed = 0.0029f * speedCur * speedCur;
        rbody.MovePosition(transform.position + transform.forward * speedCur * Time.deltaTime);
        TurnSpeed1 = X * Time.deltaTime * 4.5f * TurnSpeed;
        //rbody.MoveRotation(rbody.rotation * Quaternion.Euler(transform.up * X * TurnSpeed * Time.deltaTime));
        transform.Rotate(0, TurnSpeed1, 0);

        #endregion
    }

    /// <summary>
    /// 速度、转速与加速度曲线//0.5144节与m转换
    /// </summary>
    /// <param name="speed">速度</param>
    /// <param name="n">发动机转速映射</param>
    /// <returns>加速度</returns>
    float increSpeed(float n, float speed, bool isSail)
    {
        //speed = Mathf.Clamp(speed, 0.0f, MAXSPEED);
        float iSpeed;
        if (isSail)//如果是加速开船
        {
            iSpeed = (-0.05897f * speed + 2.065f * n);
            iSpeed = Mathf.Clamp(iSpeed, 0.0f, 2.065f);
        }
        else
        {
            //iSpeed = 0.05897f * speed;
            iSpeed = -0.05897f * speed * (1 - n) * 4.2f;
            iSpeed = Mathf.Clamp(iSpeed, -2.065f, 0);
        }

        return iSpeed;
    }

    /// <summary>
    /// 速度与加速度曲线//0.5144节与m转换
    /// </summary>
    /// <param name="speed"></param>
    /// <returns>加速度</returns>
    float increSpeed(float speed, bool isSail)
    {
        //y = -0.002 * x * x + 0.03 * x + .
        speed = Mathf.Clamp(speed, 0.0f, MAXSPEED);
        float iSpeed;
        if(isSail)//如果是开船
        {
            //iSpeed = (-0.05897f * speed + 2.065f);//-0.05897*u+2.065
            iSpeed = (-0.05897f * speed + 2.065f) + 0.1f;//offset
            iSpeed = Mathf.Clamp(iSpeed, 0.0f, 2.065f);
        }
        else
        {
            //iSpeed = 0.05897f * speed;
            iSpeed = -(0.05897f * speed + 0.1f);//offset
            iSpeed = Mathf.Clamp(iSpeed, -2.065f, 0);
        }
        
        return iSpeed;
    }

    void CameraControl()
    {
        if(GlobalVar.CamMode == 0)
        {
            Camera.main.transform.position = transform.Find("PhysicalBody").Find("FlagPoint").position;
            if (ctrlMgr.instance.GLeftRightValue() != 0 || ctrlMgr.instance.GUpDownValue() != 0)
            {
                float rotX = Camera.main.transform.localEulerAngles.y;
                float rotY = Camera.main.transform.localEulerAngles.x;
                //rotX += Input.GetAxis("Mouse X") * 10;
                //rotY -= Input.GetAxis("Mouse Y") * 10;
                rotX += ctrlMgr.instance.GLeftRightValue();
                rotY -= ctrlMgr.instance.GUpDownValue();
                Camera.main.transform.rotation = Quaternion.Euler(RotYClamp(rotY, 0), rotX, 0);
            }
            else
            {
                Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, transform.rotation, 0.8f * Time.deltaTime);
                if (Quaternion.Angle(transform.rotation, Camera.main.transform.rotation) < 0.1f)
                {
                    Camera.main.transform.rotation = transform.rotation;
                }
            }
        }
        if(GlobalVar.CamMode == 1)
        {
            if (Input.GetMouseButton(1))
            {
                eulerAngles_x += ((Input.GetAxis("Mouse X") * xSpeed) * distance) * 0.03f;
                eulerAngles_y -= (Input.GetAxis("Mouse Y") * ySpeed) * 0.03f;
                eulerAngles_y = ClampAngle(eulerAngles_y, (float)yMinLimit, (float)yMaxLimit);
            }
            //eulerAngles_x += ((Input.GetAxis("Mouse X") * xSpeed) * distance) * timeport;
            //eulerAngles_y -= (Input.GetAxis("Mouse Y") * ySpeed) * timeport;
            //eulerAngles_y = ClampAngle(eulerAngles_y, (float)yMinLimit, (float)yMaxLimit);

            Quaternion quaternion = Quaternion.Euler(eulerAngles_y, eulerAngles_x, (float)0);

            distance = Mathf.Clamp(distance - (Input.GetAxis("Mouse ScrollWheel") * MouseScrollWheelSensitivity), (float)distanceMin, (float)distanceMax);

            Vector3 vector = ((Vector3)(quaternion * new Vector3((float)0, (float)0, -this.distance))) + this.transform.position;

            //更改主相机的旋转角度和位置
            Camera.main.transform.rotation = quaternion;
            Camera.main.transform.position = vector;
        }

        
    }

    //把角度限制到给定范围内 
    public float ClampAngle(float angle, float min, float max)
    {
        while (angle < -360)
        {
            angle += 360;
        }
        while (angle > 360)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }

    public float RotYClamp(float rotY, int lookmode)
    {
        if (rotY >= 360f)
        {
            rotY -= 360f;
        }
        if (rotY >= 180f && rotY < 360f)
        {
            if (lookmode == 0)
            {
                rotY = Mathf.Clamp(rotY, 330f, 359.9999f);
            }
            else
            {
                rotY = Mathf.Clamp(rotY, 280f, 359.9999f);
            }
        }
        if (rotY >= 0 && rotY < 180)
        {
            if (lookmode == 0)
            {
                rotY = Mathf.Clamp(rotY, 0f, 30f);
            }
            else
            {
                rotY = Mathf.Clamp(rotY, 0f, 80f);
            }
        }
        return rotY;
    }

    float limitAngle(float value)
    {
        if (value > 180)
        {
            value -= 360;
        }
        return value;
    }
}
