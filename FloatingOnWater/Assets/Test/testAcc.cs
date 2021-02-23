using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

//typedef struct
//{

//    float y_k;
//float y_k_1;
//float u_k;
//float u_k_1;
//float gain_y;
//float gain_u;
//} WASHOUT_FILTER_STRUCT;


public class testAcc : MonoBehaviour
{
    public struct WASHOUT_FILTER
    {
        public float y_k;
        public float y_k_1;
        public float u_k;
        public float u_k_1;
        public float gain_y;
        public float gain_u;
    }

    //Socket
    private Socket socket;
    private IPAddress ip;
    private EndPoint ep;

    byte[] data1 = new byte[68];

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

    float timeport = 0.01f;
    float timeFlag = 0;
    bool startValue = false;

    float accValue = 0;

    static WASHOUT_FILTER acc_washout;// = {0.0,0.0,0.0,0.0,0.9934f,0.9967f};

    // Start is called before the first frame update
    void Start()
    {
        acc_washout = new WASHOUT_FILTER();
        acc_washout.y_k = 0.0f;
        acc_washout.y_k_1 = 0.0f;
        acc_washout.u_k = 0.0f;
        acc_washout.u_k_1 = 0.0f;
        acc_washout.gain_y = 0.9934f;
        acc_washout.gain_u = 0.9967f;

        Connect();
    }

    public float WashoutFilter(float u_in, WASHOUT_FILTER filter)
    {
        filter.u_k = u_in;
        filter.y_k = filter.gain_y * filter.y_k_1
            + filter.gain_u * (filter.u_k - filter.u_k_1);
        filter.y_k_1 = filter.y_k;
        filter.u_k_1 = filter.u_k;

        return filter.y_k;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            startValue = !startValue;
        }
        if (Input.GetMouseButton(1))
        {
            accValue = 1;

            print("WashACC " + WashoutFilter(20, acc_washout));
        }

        if (startValue)
        {


            //per 10ms send socketData
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

                angular_velocity_x = 0;
                byte[] bytes1 = new byte[4];
                bytes1 = BitConverter.GetBytes(angular_velocity_x);
                data1[4] = bytes1[0];
                data1[5] = bytes1[1];
                data1[6] = bytes1[2];
                data1[7] = bytes1[3];

                angular_velocity_z = 0;
                byte[] bytes2 = new byte[4];
                bytes2 = BitConverter.GetBytes(angular_velocity_z);
                data1[8] = bytes2[0];
                data1[9] = bytes2[1];
                data1[10] = bytes2[2];
                data1[11] = bytes2[3];

                angular_velocity_y = 0;
                byte[] bytes3 = new byte[4];
                bytes3 = BitConverter.GetBytes(angular_velocity_y);
                data1[12] = bytes3[0];
                data1[13] = bytes3[1];
                data1[14] = bytes3[2];
                data1[15] = bytes3[3];

                yaw = 0;
                byte[] bytes4 = new byte[4];
                bytes4 = BitConverter.GetBytes(yaw);
                data1[16] = bytes4[0];
                data1[17] = bytes4[1];
                data1[18] = bytes4[2];
                data1[19] = bytes4[3];

                pitch = 0;
                byte[] bytes5 = new byte[4];
                bytes5 = BitConverter.GetBytes(pitch);
                data1[20] = bytes5[0];
                data1[21] = bytes5[1];
                data1[22] = bytes5[2];
                data1[23] = bytes5[3];

                roll = Mathf.PI / 2;
                byte[] bytes6 = new byte[4];
                bytes6 = BitConverter.GetBytes(roll);
                data1[24] = bytes6[0];
                data1[25] = bytes6[1];
                data1[26] = bytes6[2];
                data1[27] = bytes6[3];

                acceleration_x = accValue;
                byte[] bytes7 = new byte[4];
                bytes7 = BitConverter.GetBytes(acceleration_x);
                data1[28] = bytes7[0];
                data1[29] = bytes7[1];
                data1[30] = bytes7[2];
                data1[31] = bytes7[3];

                acceleration_z = 0;
                byte[] bytes8 = new byte[4];
                bytes8 = BitConverter.GetBytes(acceleration_z);
                data1[32] = bytes8[0];
                data1[33] = bytes8[1];
                data1[34] = bytes8[2];
                data1[35] = bytes8[3];

                acceleration_y = 0;
                byte[] bytes9 = new byte[4];
                bytes9 = BitConverter.GetBytes(acceleration_y);
                data1[36] = bytes9[0];
                data1[37] = bytes9[1];
                data1[38] = bytes9[2];
                data1[39] = bytes9[3];

                velocity_x = 0;
                byte[] bytes10 = new byte[4];
                bytes10 = BitConverter.GetBytes(velocity_x);
                data1[40] = bytes10[0];
                data1[41] = bytes10[1];
                data1[42] = bytes10[2];
                data1[43] = bytes10[3];

                velocity_z = 0;
                byte[] bytes11 = new byte[4];
                bytes11 = BitConverter.GetBytes(velocity_z);
                data1[44] = bytes11[0];
                data1[45] = bytes11[1];
                data1[46] = bytes11[2];
                data1[47] = bytes11[3];

                velocity_y = 0;
                byte[] bytes12 = new byte[4];
                bytes12 = BitConverter.GetBytes(velocity_y);
                data1[48] = bytes12[0];
                data1[49] = bytes12[1];
                data1[50] = bytes12[2];
                data1[51] = bytes12[3];

                position_x = 0;
                byte[] bytes13 = new byte[4];
                bytes13 = BitConverter.GetBytes(position_x);
                data1[52] = bytes13[0];
                data1[53] = bytes13[1];
                data1[54] = bytes13[2];
                data1[55] = bytes13[3];

                position_z = 0;
                byte[] bytes14 = new byte[4];
                bytes14 = BitConverter.GetBytes(position_z);
                data1[56] = bytes14[0];
                data1[57] = bytes14[1];
                data1[58] = bytes14[2];
                data1[59] = bytes14[3];

                position_y = 0;
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

                socket.SendTo(data1, ep);

            }
            timeFlag += Time.deltaTime;

        }

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

    float limitAngle(float value)
    {
        if (value > 180)
        {
            value -= 360;
        }
        return value;
    }
}
