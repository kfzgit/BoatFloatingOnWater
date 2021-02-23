using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System.IO;
using UnityEngine.UI;

public class boatCtrl : MonoBehaviour
{
    public Rigidbody rbody;

    //Socket
    private Socket socket;
    private IPAddress ip;
    private EndPoint ep;

    //船速与转速
    public float Speed = 0;
    public float TurnSpeed = 0;
    //船最大速度
    public float MAXSPEED = 15;

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


    byte[] data1 = new byte[68];

    float timeFlag = 0;

    public float totla_time = 0;
    public float angular_velocity_x;
    public float angular_velocity_z;
    public float angular_velocity_y;
    
    public float yaw;
    public float pitch;
    public float roll;
    public float acceleration_x;
    public float acceleration_z;
    public float acceleration_y;
    
    public float velocity_x;
    public float velocity_z;
    public float velocity_y;
    
    public int position_x;
    public int position_z;
    public int position_y;
    
    public float ToCA;

    float posX = 0;
    float posY = 0;
    float posZ = 0;
    float rotX = 0;
    float rotY = 0,rotY1 = 0;
    float rotZ = 0;
    float speedX = 0;
    float speedY = 0;
    float speedZ = 0;

    float timeport = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        Connect();

        rbody = GetComponent<Rigidbody>();
        Vector3 eulerAngles = Camera.main.transform.eulerAngles;//当前物体的欧拉角 
        eulerAngles_x = eulerAngles.y;
        eulerAngles_y = eulerAngles.x;

        posX = transform.position.x;
        posY = transform.position.y;
        posZ = transform.position.z;
        rotX = limitAngle(transform.eulerAngles.x);
        rotY = limitAngle(transform.eulerAngles.y);
        rotY1 = limitAngle(transform.eulerAngles.y);
        rotZ = limitAngle(transform.eulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerControl();
        CameraControl();

        if (timeFlag > timeport)
        {
            timeFlag = 0;

            totla_time += timeport;
            byte[] bytes0 = new byte[4];
            bytes0 = BitConverter.GetBytes((int)(totla_time * 1000));
            data1[0] = bytes0[0];
            data1[1] = bytes0[1];
            data1[2] = bytes0[2];
            data1[3] = bytes0[3];

            angular_velocity_x = 0;// (limitAngle(transform.eulerAngles.y) - rotY) / 180 * Mathf.PI / timeport;
            rotY = limitAngle(transform.eulerAngles.y);
            //angular_velocity_x = rbody.angularVelocity.y;
            byte[] bytes1 = new byte[4];
            bytes1 = BitConverter.GetBytes(angular_velocity_x);
            data1[4] = bytes1[0];
            data1[5] = bytes1[1];
            data1[6] = bytes1[2];
            data1[7] = bytes1[3];

            //angular_velocity_z = (transform.eulerAngles.x - rotX) / timeport;
            angular_velocity_z = 0;//rbody.angularVelocity.x;
            byte[] bytes2 = new byte[4];
            bytes2 = BitConverter.GetBytes(angular_velocity_z);
            data1[8] = bytes2[0];
            data1[9] = bytes2[1];
            data1[10] = bytes2[2];
            data1[11] = bytes2[3];

            //angular_velocity_y = (transform.eulerAngles.z - rotZ) / timeport;
            angular_velocity_y = 0;//rbody.angularVelocity.z;
            byte[] bytes3 = new byte[4];
            bytes3 = BitConverter.GetBytes(angular_velocity_y);
            data1[12] = bytes3[0];
            data1[13] = bytes3[1];
            data1[14] = bytes3[2];
            data1[15] = bytes3[3];

            yaw = -(limitAngle(transform.eulerAngles.y) - rotY1) / 180 * Mathf.PI;
            byte[] bytes4 = new byte[4];
            bytes4 = BitConverter.GetBytes(yaw);
            data1[16] = bytes4[0];
            data1[17] = bytes4[1];
            data1[18] = bytes4[2];
            data1[19] = bytes4[3];

            //pitch = 0;//-(int)((transform.eulerAngles.x - rotX) * 1000) * 0.001f;
            pitch = (limitAngle(transform.eulerAngles.x) - rotX) / 180 * Mathf.PI;
            byte[] bytes5 = new byte[4];
            bytes5 = BitConverter.GetBytes(pitch);
            data1[20] = bytes5[0];
            data1[21] = bytes5[1];
            data1[22] = bytes5[2];
            data1[23] = bytes5[3];

            //roll = Mathf.PI / 2;// (transform.eulerAngles.z - rotZ + Mathf.PI/2);
            roll = (-(limitAngle(transform.eulerAngles.z) - rotZ) / 180 * Mathf.PI + Mathf.PI/2);
            byte[] bytes6 = new byte[4];
            bytes6 = BitConverter.GetBytes(roll);
            data1[24] = bytes6[0];
            data1[25] = bytes6[1];
            data1[26] = bytes6[2];
            data1[27] = bytes6[3];

            acceleration_x = (Speed * transform.forward.z / transform.forward.magnitude - speedX) / timeport;
            //acceleration_x = (rbody.velocity.z - speedX) / timeport;
            byte[] bytes7 = new byte[4];
            bytes7 = BitConverter.GetBytes(acceleration_x);
            data1[28] = bytes7[0];
            data1[29] = bytes7[1];
            data1[30] = bytes7[2];
            data1[31] = bytes7[3];

            acceleration_z = (Speed * transform.forward.x / transform.forward.magnitude - speedZ) / timeport;
            //acceleration_z = (rbody.velocity.x - speedZ) / timeport;
            byte[] bytes8 = new byte[4];
            bytes8 = BitConverter.GetBytes(acceleration_z);
            data1[32] = bytes8[0];
            data1[33] = bytes8[1];
            data1[34] = bytes8[2];
            data1[35] = bytes8[3];

            acceleration_y = (rbody.velocity.y - speedY) / timeport;
            //acceleration_y = (rbody.velocity.y - speedY) / timeport;
            byte[] bytes9 = new byte[4];
            bytes9 = BitConverter.GetBytes(acceleration_y);
            data1[36] = bytes9[0];
            data1[37] = bytes9[1];
            data1[38] = bytes9[2];
            data1[39] = bytes9[3];

            //velocity_x = (transform.position.z - posZ) / timeport;
            //posZ = transform.position.z;
            velocity_x = Speed * transform.forward.z / transform.forward.magnitude;
            //velocity_x = rbody.velocity.z;
            speedX = velocity_x;
            byte[] bytes10 = new byte[4];
            bytes10 = BitConverter.GetBytes(velocity_x);
            data1[40] = bytes10[0];
            data1[41] = bytes10[1];
            data1[42] = bytes10[2];
            data1[43] = bytes10[3];

            //velocity_z = (transform.position.x - posX) / timeport;
            //posX = transform.position.x;
            velocity_z = Speed * transform.forward.x / transform.forward.magnitude;
            //velocity_z = rbody.velocity.x;
            speedZ = velocity_z;
            byte[] bytes11 = new byte[4];
            bytes11 = BitConverter.GetBytes(velocity_z);
            data1[44] = bytes11[0];
            data1[45] = bytes11[1];
            data1[46] = bytes11[2];
            data1[47] = bytes11[3];

            //velocity_y = (transform.position.y - posY) / timeport;
            //posY = transform.position.y;
            velocity_y = rbody.velocity.y;
            speedY = velocity_y;
            byte[] bytes12 = new byte[4];
            bytes12 = BitConverter.GetBytes(velocity_y);
            data1[48] = bytes12[0];
            data1[49] = bytes12[1];
            data1[50] = bytes12[2];
            data1[51] = bytes12[3];

            position_x = 0;//(int)transform.position.z;
            byte[] bytes13 = new byte[4];
            bytes13 = BitConverter.GetBytes(position_x);
            data1[52] = bytes13[0];
            data1[53] = bytes13[1];
            data1[54] = bytes13[2];
            data1[55] = bytes13[3];

            position_z = 0;// (int)transform.position.y;
            byte[] bytes14 = new byte[4];
            bytes14 = BitConverter.GetBytes(position_z);
            data1[56] = bytes14[0];
            data1[57] = bytes14[1];
            data1[58] = bytes14[2];
            data1[59] = bytes14[3];

            position_y = 0;// (int)transform.position.x;
            byte[] bytes15 = new byte[4];
            bytes15 = BitConverter.GetBytes(position_y);
            data1[60] = bytes15[0];
            data1[61] = bytes15[1];
            data1[62] = bytes15[2];
            data1[63] = bytes15[3];

            data1[64] = 0x54;
            data1[65] = 0x6F;
            data1[66] = 0x43;
            data1[67] = 0x41;

            //posX = transform.position.x;
            //posY = transform.position.y;
            //posZ = transform.position.z;
            ////rotX = transform.eulerAngles.x;
            //rotY = limitAngle(transform.eulerAngles.y);
            ////rotZ = transform.eulerAngles.z;


            socket.SendTo(data1, ep);

        }
        timeFlag += Time.deltaTime;
    }

    float limitAngle(float value)
    {
        if(value > 180)
        {
            value -= 360;
        }
        return value;
    }

    void PlayerControl()
    {
        float X = Input.GetAxis("Horizontal");
        float Y = Input.GetAxis("Vertical");
        if (Y > 0.0001f)
        {
            Speed += increSpeed(Speed) * Mathf.Abs(Y);
            Speed = Mathf.Clamp(Speed, 0.0f, MAXSPEED);
        }
        else
        {
            if(Speed < 0.01f)
            {
                Speed = 0;
            }
            else
            {
                Speed -= increSpeed(Speed);
            }
            //Speed = Mathf.Lerp(Speed, 0, 0.01f * Time.deltaTime);
            //print(Speed);
        }
        TurnSpeed = Speed;
        rbody.MovePosition(transform.position + transform.forward * Speed * Time.deltaTime);
        rbody.MoveRotation(rbody.rotation * Quaternion.Euler(transform.up * X * TurnSpeed * Time.deltaTime));
    }

    //抛物线
    float increSpeed(float speed)
    {
        //y = -0.002 * x * x + 0.03 * x + .
        speed = Mathf.Clamp(speed, 0.0f, MAXSPEED);
        float iSpeed;
        //iSpeed = (-0.002f * speed * speed + 0.03f * speed + 0.001f);
        iSpeed = (-0.05897f * speed + 2.065f) * Time.deltaTime;//-0.05897*u+2.065
        iSpeed = Mathf.Clamp(iSpeed, 0.0f, 2.065f);
        return iSpeed;
    }

    void CameraControl()
    {
        Camera.main.transform.position = transform.Find("FlagPoint").position;
        if (Input.GetMouseButton(1))
        {
            float rotX = Camera.main.transform.localEulerAngles.y;
            float rotY = Camera.main.transform.localEulerAngles.x;
            rotX += Input.GetAxis("Mouse X") * 10;
            rotY -= Input.GetAxis("Mouse Y") * 10;
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


        //if (Input.GetMouseButton(1))
        //{
        //    eulerAngles_x += ((Input.GetAxis("Mouse X") * xSpeed) * distance) * 0.03f;
        //    eulerAngles_y -= (Input.GetAxis("Mouse Y") * ySpeed) * 0.03f;
        //    eulerAngles_y = ClampAngle(eulerAngles_y, (float)yMinLimit, (float)yMaxLimit);
        //}
        ////eulerAngles_x += ((Input.GetAxis("Mouse X") * xSpeed) * distance) * timeport;
        ////eulerAngles_y -= (Input.GetAxis("Mouse Y") * ySpeed) * timeport;
        ////eulerAngles_y = ClampAngle(eulerAngles_y, (float)yMinLimit, (float)yMaxLimit);

        //Quaternion quaternion = Quaternion.Euler(eulerAngles_y, eulerAngles_x, (float)0);

        //distance = Mathf.Clamp(distance - (Input.GetAxis("Mouse ScrollWheel") * MouseScrollWheelSensitivity), (float)distanceMin, (float)distanceMax);

        //Vector3 vector = ((Vector3)(quaternion * new Vector3((float)0, (float)0, -this.distance))) + this.transform.position;

        ////更改主相机的旋转角度和位置
        //Camera.main.transform.rotation = quaternion;
        //Camera.main.transform.position = vector;

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

    //连接服务端
    public bool Connect()
    {
        try
        {
            //socket
            socket = new Socket(AddressFamily.InterNetwork,
                      SocketType.Dgram, ProtocolType.Udp);

            ip = IPAddress.Parse("192.168.1.88");
            ep = new IPEndPoint(ip, 20000);
            return true;
        }
        catch (Exception e)
        {
            Debug.Log("连接失败:" + e.Message);
            return false;
        }
    }
}
