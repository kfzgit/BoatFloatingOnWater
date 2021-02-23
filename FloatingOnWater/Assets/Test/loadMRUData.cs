using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class loadMRUData : MonoBehaviour
{
    public Text MRUData;
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
    float timePerSec = 0;
    float timePerSec1 = 0;
    int dataIndex = 1;
    int dataIndex1 = 1;

    List<float> L_pitch = new List<float>();
    List<float> L_roll = new List<float>();
    List<float> L_acc = new List<float>();
    List<float> L_ang = new List<float>();

    float pitchValue = 0;
    float rollValue = 0;
    float speedValue = 0;
    float curSValue = 0;
    float accValue = 0;
    float angValue = 0;

    int dataNumMRU = 0;
    int dataNumGPS = 0;

    //execute once flag
    bool onceFlag = false;

    bool MRUDataLoad = false;
    bool GPSDataLoad = false;

    // Start is called before the first frame update
    void Start()
    {
        Connect();

        Thread loadMRUText = new Thread(LoadMRUText);
        loadMRUText.Start();

        Thread loadGPSText = new Thread(LoadGPSText);
        loadGPSText.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(MRUDataLoad == true && GPSDataLoad == true)
        {

            if(onceFlag == false)
            {
                if (dataNumMRU < (dataNumGPS / 5))
                {
                    dataNumGPS = dataNumMRU * 5;
                }
                else
                {
                    dataNumMRU = dataNumGPS / 5;
                }

                DOTween.To(() => pitchValue, x => pitchValue = x, L_pitch[dataIndex], 1);
                DOTween.To(() => rollValue, x => rollValue = x, L_roll[dataIndex], 1);
                DOTween.To(() => accValue, x => accValue = x, L_acc[dataIndex1], 0.2f);

                onceFlag = true;
            }
            

            //MRUData.text = "pitch = " + pitchValue + "\n" + "roll = " + rollValue + "\n" + "acc = " + accValue + "\n" + "ang = " + angValue;

            //pitch roll data
            if (timePerSec > 1)
            {
                timePerSec = 0;
                dataIndex++;
                if (dataIndex >= dataNumMRU)
                {
                    dataIndex = 0;
                }


                DOTween.To(() => pitchValue, x => pitchValue = x, L_pitch[dataIndex], 1);
                DOTween.To(() => rollValue, x => rollValue = x, L_roll[dataIndex], 1);
                //L_pitch[dataIndex]
                //L_roll[dataIndex]
            }
            timePerSec += Time.deltaTime;

            //acc data
            if (timePerSec1 > 0.2f)
            {
                timePerSec1 = 0;
                dataIndex1++;
                if (dataIndex1 >= dataNumGPS)
                {
                    dataIndex1 = 0;
                }


                DOTween.To(() => accValue, x => accValue = x, L_acc[dataIndex1], 0.2f);
                DOTween.To(() => angValue, x => angValue = x, L_ang[dataIndex1], 0.2f);
                //DOTween.To(() => rollValue, x => rollValue = x, L_roll[dataIndex1], 1);
            }
            timePerSec1 += Time.deltaTime;


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

                yaw = -angValue / 180 * Mathf.PI;
                byte[] bytes4 = new byte[4];
                bytes4 = BitConverter.GetBytes(yaw);
                data1[16] = bytes4[0];
                data1[17] = bytes4[1];
                data1[18] = bytes4[2];
                data1[19] = bytes4[3];

                pitch = pitchValue / 180 * Mathf.PI;
                byte[] bytes5 = new byte[4];
                bytes5 = BitConverter.GetBytes(pitch);
                data1[20] = bytes5[0];
                data1[21] = bytes5[1];
                data1[22] = bytes5[2];
                data1[23] = bytes5[3];

                roll = -rollValue / 180 * Mathf.PI + Mathf.PI / 2;
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

    void LoadMRUText()
    {
        string path = Application.streamingAssetsPath + "/2/mru.txt";
        //文件读写流
        StreamReader sr = new StreamReader(path);
        //读取内容
        string result = sr.ReadToEnd();
        //逐行截取
        string[] data = result.Split('\n');

        for (int i = 1; i < data.Length; i++)
        {
            string[] strTmps = data[i].Split('"');
            if(strTmps.Length > 3)
            {
                L_pitch.Add(float.Parse(strTmps[1]));
                L_roll.Add(float.Parse(strTmps[3]));
            }
        }

        pitchValue = L_pitch[0];
        rollValue = L_roll[0];

        dataNumMRU = L_pitch.Count;
        MRUDataLoad = true;
    }

    void LoadGPSText()
    {
        string path = Application.streamingAssetsPath + "/2/gps.txt";
        //文件读写流
        StreamReader sr = new StreamReader(path);
        //读取内容
        string result = sr.ReadToEnd();
        //逐行截取
        string[] data = result.Split('\n');

        for (int i = 1; i < data.Length; i++)
        {
            string[] strTmps = data[i].Split('"');
            if (strTmps.Length > 7)
            {
                speedValue = float.Parse(strTmps[5]);
                L_acc.Add((speedValue - curSValue)/0.2f);
                curSValue = speedValue;
                L_ang.Add(limitAngle(float.Parse(strTmps[7])));
            }
        }

        accValue = L_acc[0];
        angValue = L_ang[0];

        dataNumGPS = L_acc.Count;
        GPSDataLoad = true;
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
