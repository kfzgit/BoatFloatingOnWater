using DG.Tweening;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class DataMgr : MonoBehaviour
{
    public static DataMgr instance;

    public float ratioD = 1.0f;

    public Rigidbody boatTestYBody;

    //6Dof Socket
    private Socket socketDof;
    private IPAddress ipDof;
    private EndPoint epDof;

    //接收实时船舶数据socket
    private Socket socketSyc;

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

    //定义一个队列来接收实时船舶数据
    Queue<RTDataModel> RTData = new Queue<RTDataModel>();

    //float rotY = 0;
    float speedY = 0;

    float timeport = 0.01f;

    bool accFlag = false;
    int accIndex = 0;

    #region 实时数据量
    //execute once flag
    bool onceFlag0 = false;
    float timePerSec0 = 0;

    float pitchValue0 = 0;
    float rollValue0 = 0;
    float headValue0 = 0;
    float speedValue0 = 0;
    float courseValue0 = 0;

    RTDataModel rTDataOut;
    Thread c_thread;
    #endregion

    #region 读取txt文件量
    float timePerSec = 0;
    float timePerSec1 = 0;
    float timePerSec2 = 0;
    int dataIndex = 1;
    int dataIndex1 = 5;
    int dataIndex2 = 9;

    //读取txt文件的数据列表
    List<float> L_pitch = new List<float>();
    List<float> L_roll = new List<float>();
    //对地速度
    List<float> L_gspeed = new List<float>();
    //速度计算加速度
    List<float> L_acc = new List<float>();
    List<float> L_course = new List<float>();
    List<float> L_Head = new List<float>();
    List<double> L_long = new List<double>();
    List<double> L_lat = new List<double>();

    #region 总文件
    List<float> L_pitch1 = new List<float>();
    List<float> L_roll1 = new List<float>();
    //对地速度
    List<float> L_gspeed1 = new List<float>();
    //速度计算加速度
    List<float> L_acc1 = new List<float>();
    List<float> L_course1 = new List<float>();
    List<float> L_Head1 = new List<float>();
    List<double> L_long1 = new List<double>();
    List<double> L_lat1 = new List<double>();

    bool AllDataLoad = false;
    Thread loadAllDataText;
    int dataNumAll = 0;
    #endregion

    float headValue = 0;
    float pitchValue = 0;
    float rollValue = 0;
    float accValue = 0;
    float courseValue = 0;
    float GspeedValue = 0;
    double longValue = 0;
    double latValue = 0;

    //过程量
    float speedValue = 0;
    float curSValue = 0;

    //
    int dataNum = 0;
    int dataNumMRU = 0;
    int dataNumGPS = 0;
    int dataNumLJ = 0;

    //execute once flag
    bool onceFlag1 = false;

    bool MRUDataLoad = false;
    bool GPSDataLoad = false;
    bool LJDataLoad = false;

    Thread loadMRUText;
    Thread loadGPSText;
    Thread loadLJText;

    #endregion

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //初始化实时数据接收变量
        rTDataOut = new RTDataModel();

        ConnectDof();

        boatTestYBody = GameObject.Find("zhitenghaoneibu111").GetComponent<Rigidbody>();
    }

    //初始化socket到六自由度平台服务器
    public void ConnectDof()
    {
        socketDof = new Socket(AddressFamily.InterNetwork,
                    SocketType.Dgram, ProtocolType.Udp);

        ipDof = IPAddress.Parse("192.168.1.88");
        epDof = new IPEndPoint(ipDof, 20000);
    }

    //初始化socket接收实时船舶数据
    public bool ConnectSynData()
    {
        if(socketSyc != null && socketSyc.Connected)
        {
            socketSyc.Close();
            if(c_thread != null)
            {
                c_thread.Abort();
            }
            return false;
        }
        else
        {
            try
            {
                int _port = 6666;
                string _ip = "10.0.105.166";

                //创建客户端Socket，获得远程ip和端口号
                socketSyc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Parse(_ip);
                IPEndPoint point = new IPEndPoint(ip, _port);

                socketSyc.Connect(point);
                Debug.Log("连接成功!");


                //确保本地加载关闭
                if(MRUDataLoad || GPSDataLoad)
                {
                    MRUDataLoad = false;
                    GPSDataLoad = false;
                    LJDataLoad = false;
                    loadMRUText.Abort();
                    loadGPSText.Abort();
                    loadLJText.Abort();
                }


                //开启新的线程，不停的接收服务器发来的消息
                c_thread = new Thread(ReceivedSyn);
                c_thread.IsBackground = true;
                c_thread.Start();

                Debug.Log("连接成功111222!");
                return true;
            }
            catch (Exception)
            {
                Debug.Log("IP或者端口号错误...");

                return false;
            }
        }
    }

    // 接收船舶服务端返回的消息
    void ReceivedSyn()
    {
        //发送gps获取数据gps数据
        //send("gps");

        while (true)
        {
            try
            {
                byte[] buffer = new byte[1024 * 1024 * 3];
                //实际接收到的有效字节数
                int len = socketSyc.Receive(buffer);
                if (len == 0)
                {
                    break;
                }
                string str = Encoding.UTF8.GetString(buffer, 0, len);
                RTDataModel rTDataModel = new RTDataModel();
                rTDataModel = JsonConvert.DeserializeObject<RTDataModel>(str);
                RTData.Enqueue(rTDataModel);
                Debug.Log("客户端打印：" + socketSyc.RemoteEndPoint + ":" + str);
            }
            catch {
                Debug.Log("接收失败...");
            }
        }
    }

    // 向船舶数据服务器发送消息
    private void send(string str)
    {
        try
        {
            string msg = str;
            byte[] buffer = new byte[1024 * 1024 * 3];
            buffer = Encoding.UTF8.GetBytes(msg);
            socketSyc.Send(buffer);
        }
        catch { }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            switch(ratioD)
            {
                case 1.0f:
                    ratioD = 1.5f;
                    break;
                case 1.5f:
                    ratioD = 2.0f;
                    break;
                case 2.0f:
                    ratioD = 3.0f;
                    break;
                case 3.0f:
                    ratioD = 1.0f;
                    break;
                default:
                    ratioD = 1.0f;
                    break;
            }
        }
        ////加载本地文件
        //if (MRUDataLoad == true && GPSDataLoad == true && LJDataLoad == true)
        //{
        //    if (onceFlag1 == false)
        //    {
        //        //dataNumMRU  dataNumGPS/5  dataNumLJ/9

        //        if (dataNumMRU < (dataNumGPS / 5))
        //        {
        //            dataNumGPS = dataNumMRU * 5;
        //            if(dataNumMRU < dataNumLJ / 9)
        //            {
        //                dataNumLJ = dataNumMRU * 9;
        //                dataNum = dataNumMRU;
        //            }
        //            else
        //            {
        //                dataNumMRU = dataNumLJ / 9;
        //                dataNumGPS = dataNumMRU * 5;
        //                dataNum = dataNumMRU;
        //            }
        //        }
        //        else
        //        {
        //            dataNumMRU = dataNumGPS / 5;
        //            if(dataNumMRU < dataNumLJ / 9)
        //            {
        //                dataNumLJ = dataNumMRU * 9;
        //                dataNum = dataNumMRU;
        //            }
        //            else
        //            {
        //                dataNumMRU = dataNumLJ / 9;
        //                dataNumGPS = dataNumMRU * 5;
        //                dataNum = dataNumMRU;
        //            }
        //        }
        //        //暂存最大值给面板使用
        //        GlobalVar.SDataNum = dataNum;
        //        GlobalVar.SIndex = 1;

        //        DOTween.To(() => pitchValue, x => pitchValue = x, L_pitch[1], 1);
        //        DOTween.To(() => rollValue, x => rollValue = x, L_roll[1], 1);
        //        DOTween.To(() => accValue, x => accValue = x, L_acc[1], 0.2f);
        //        DOTween.To(() => accValue, x => accValue = x, L_acc[1], 0.2f);
        //        DOTween.To(() => headValue, x => headValue = x, L_Head[1], 0.111f);//headValue

        //        DOTween.To(() => courseValue, x => courseValue = x, L_course[1], 0.2f);
        //        DOTween.To(() => GspeedValue, x => GspeedValue = x, L_gspeed[1], 0.2f);
        //        DOTween.To(() => longValue, x => longValue = x, L_long[1], 0.2f);
        //        DOTween.To(() => latValue, x => latValue = x, L_lat[1], 0.2f);

        //        onceFlag1 = true;
        //    }


        //    //MRUData.text = "pitch = " + pitchValue + "\n" + "roll = " + rollValue + "\n" + "acc = " + accValue + "\n" + "ang = " + angValue;
            

        //    //pitch roll data
        //    if (timePerSec > 1)
        //    {
        //        timePerSec = 0;

        //        //更新面板值给索引点
        //        if(dataIndex != GlobalVar.SIndex)
        //        {
        //            dataIndex = GlobalVar.SIndex;
        //        }

        //        dataIndex++;
        //        if (dataIndex >= dataNumMRU)
        //        {
        //            dataIndex = 0;
        //        }

        //        //给面板进度条的值
        //        GlobalVar.SIndex = dataIndex;

        //        DOTween.To(() => pitchValue, x => pitchValue = x, L_pitch[dataIndex], 1);
        //        DOTween.To(() => rollValue, x => rollValue = x, L_roll[dataIndex], 1);
        //        //L_pitch[dataIndex]
        //        //L_roll[dataIndex]
        //    }
        //    timePerSec += Time.deltaTime;

        //    //head data
        //    if (timePerSec2 > 0.1111f)
        //    {
        //        timePerSec2 = 0;

        //        //更新面板值给索引点
        //        if (dataIndex != GlobalVar.SIndex)
        //        {
        //            dataIndex2 = GlobalVar.SIndex * 9;
        //        }

        //        dataIndex2++;
        //        if (dataIndex2 >= dataNumLJ)
        //        {
        //            dataIndex2 = 0;
        //        }


        //        DOTween.To(() => headValue, x => headValue = x, L_Head[dataIndex2], 0.1111f);
        //    }
        //    timePerSec2 += Time.deltaTime;

        //    //acc data
        //    if (timePerSec1 > 0.2f)
        //    {
        //        timePerSec1 = 0;

        //        //更新面板值给索引点
        //        if (dataIndex != GlobalVar.SIndex)
        //        {
        //            dataIndex1 = GlobalVar.SIndex * 5;
        //        }

        //        dataIndex1++;
        //        if (dataIndex1 >= dataNumGPS)
        //        {
        //            dataIndex1 = 0;
        //        }


        //        DOTween.To(() => accValue, x => accValue = x, L_acc[dataIndex1], 0.2f);
        //        DOTween.To(() => courseValue, x => courseValue = x, L_course[dataIndex1], 0.2f);

        //        DOTween.To(() => GspeedValue, x => GspeedValue = x, L_gspeed[dataIndex1], 0.2f);
        //        DOTween.To(() => longValue, x => longValue = x, L_long[dataIndex1], 0.2f);
        //        DOTween.To(() => latValue, x => latValue = x, L_lat[dataIndex1], 0.2f);
        //    }
        //    timePerSec1 += Time.deltaTime;


        //    //per 10ms send
        //    SendCombineData(0, 0, pitchValue, rollValue);
        //    ////per 10ms send
        //    //SendCombineData(accValue, angValue, pitchValue, rollValue);
        //    //全局数据类（UI显示界面）
        //    if (GlobalInfoVar.instance)
        //    {
        //        GlobalInfoVar.instance.RollAngle = limit4Dot(limitAngle(rollValue));
        //        GlobalInfoVar.instance.PitchAngle = limit4Dot(limitAngle(pitchValue));
        //        GlobalInfoVar.instance.BowAngle = limit4Dot(limitAngle(headValue));
        //        GlobalInfoVar.instance.GSpeed = limit4Dot(GspeedValue);
        //        GlobalInfoVar.instance.Course = limit4Dot(limitAngle(courseValue));
        //        GlobalInfoVar.instance.RAngle = limit4Dot(0);
        //        GlobalInfoVar.instance.Long = limit6Dot(longValue);
        //        GlobalInfoVar.instance.Lat = limit6Dot(latValue);
        //    }
        //}

        //加载本地文件
        if (AllDataLoad == true)
        {
            if (onceFlag1 == false)
            {
                //暂存最大值给面板使用
                GlobalVar.SDataNum = dataNumAll;
                GlobalVar.SIndex = 1;

                DOTween.To(() => pitchValue, x => pitchValue = x, L_pitch1[1], 1);
                DOTween.To(() => rollValue, x => rollValue = x, L_roll1[1], 1);
                DOTween.To(() => accValue, x => accValue = x, L_acc1[1], 1);
                DOTween.To(() => accValue, x => accValue = x, L_acc1[1], 1);
                DOTween.To(() => headValue, x => headValue = x, L_Head1[1], 1);//headValue

                DOTween.To(() => courseValue, x => courseValue = x, L_course1[1], 1);
                DOTween.To(() => GspeedValue, x => GspeedValue = x, L_gspeed1[1], 1);
                DOTween.To(() => longValue, x => longValue = x, L_long1[1], 1);
                DOTween.To(() => latValue, x => latValue = x, L_lat1[1], 1);

                onceFlag1 = true;
            }


            //pitch roll data
            if (timePerSec > 1)
            {
                timePerSec = 0;

                //更新面板值给索引点
                if (dataIndex != GlobalVar.SIndex)
                {
                    dataIndex = GlobalVar.SIndex;
                }

                dataIndex++;
                if (dataIndex >= dataNumAll)
                {
                    dataIndex = 0;
                }

                //给面板进度条的值
                GlobalVar.SIndex = dataIndex;

                DOTween.To(() => pitchValue, x => pitchValue = x, L_pitch1[dataIndex], 1);
                DOTween.To(() => rollValue, x => rollValue = x, L_roll1[dataIndex], 1);
                DOTween.To(() => accValue, x => accValue = x, L_acc1[dataIndex], 1);
                DOTween.To(() => accValue, x => accValue = x, L_acc1[dataIndex], 1);
                DOTween.To(() => headValue, x => headValue = x, L_Head1[dataIndex], 1);//headValue

                DOTween.To(() => courseValue, x => courseValue = x, L_course1[dataIndex], 1);
                DOTween.To(() => GspeedValue, x => GspeedValue = x, L_gspeed1[dataIndex], 1);
                DOTween.To(() => longValue, x => longValue = x, L_long1[dataIndex], 1);
                DOTween.To(() => latValue, x => latValue = x, L_lat1[dataIndex], 1);
            }
            timePerSec += Time.deltaTime;


            //per 10ms send
            SendCombineData(0, 0, ratioD * pitchValue, ratioD * rollValue);
            ////per 10ms send
            //SendCombineData(accValue, angValue, pitchValue, rollValue);
            //全局数据类（UI显示界面）
            if (GlobalInfoVar.instance)
            {
                GlobalInfoVar.instance.RollAngle = limit4Dot(limitAngle(rollValue));
                GlobalInfoVar.instance.PitchAngle = limit4Dot(limitAngle(pitchValue));
                GlobalInfoVar.instance.BowAngle = limit4Dot(limitAngle(headValue));
                GlobalInfoVar.instance.GSpeed = limit4Dot(GspeedValue);
                GlobalInfoVar.instance.Course = limit4Dot(limitAngle(courseValue));
                GlobalInfoVar.instance.RAngle = limit4Dot(0);
                GlobalInfoVar.instance.Long = limit6Dot(longValue);
                GlobalInfoVar.instance.Lat = limit6Dot(latValue);
            }
        }

        //实时数据
        if (GlobalVar.RTDataOpen)
        {
            //执行一次
            if (onceFlag0 == false && RTData != null && RTData.Count > 1)
            {
                rTDataOut = RTData.Dequeue();
                DOTween.To(() => pitchValue0, x => pitchValue0 = x, rTDataOut.pitch, 1);
                DOTween.To(() => rollValue0, x => rollValue0 = x, rTDataOut.roll, 1);
                DOTween.To(() => headValue0, x => headValue0 = x, rTDataOut.head, 1);

                speedValue0 = rTDataOut.s;
                courseValue0 = rTDataOut.c;

                onceFlag0 = true;
            }
            //每1秒处理
            if (timePerSec0 > 1)
            {
                timePerSec0 = 0;

                if (RTData != null && RTData.Count > 1)
                {
                    rTDataOut = RTData.Dequeue();
                    DOTween.To(() => pitchValue0, x => pitchValue0 = x, rTDataOut.pitch, 1);
                    DOTween.To(() => rollValue0, x => rollValue0 = x, rTDataOut.roll, 1);
                    DOTween.To(() => headValue0, x => headValue0 = x, rTDataOut.head, 1);

                    speedValue0 = rTDataOut.s;
                    courseValue0 = rTDataOut.c;
                }
            }
            timePerSec0 += Time.deltaTime;
            //per 10ms send
            SendCombineData(0, headValue0, pitchValue0, rollValue0);
            //全局数据类（UI显示界面）
            if (GlobalInfoVar.instance)
            {
                GlobalInfoVar.instance.RollAngle = limit4Dot(limitAngle(rollValue0));
                GlobalInfoVar.instance.PitchAngle = limit4Dot(limitAngle(pitchValue0));
                GlobalInfoVar.instance.BowAngle = limit4Dot(limitAngle(headValue0));

                GlobalInfoVar.instance.GSpeed = limit4Dot(speedValue0);
                GlobalInfoVar.instance.Course = limit4Dot(courseValue0);
                GlobalInfoVar.instance.RAngle = limit4Dot(0);
                GlobalInfoVar.instance.Long = limit6Dot(0);
                GlobalInfoVar.instance.Lat = limit6Dot(0);
            }
        }
    }

    //开启与关闭数据文件加载
    public bool loadTxtData()
    {
        if(AllDataLoad)
        {
            AllDataLoad = false;
            loadAllDataText.Abort();

            L_pitch1.Clear();
            L_roll1.Clear();
            L_gspeed1.Clear();
            L_acc1.Clear();
            L_course1.Clear();
            L_Head1.Clear();
            L_long1.Clear();
            L_lat1.Clear();
            return false;
        }
        else
        {
            L_pitch1.Clear();
            L_roll1.Clear();
            L_gspeed1.Clear();
            L_acc1.Clear();
            L_course1.Clear();
            L_Head1.Clear();
            L_long1.Clear();
            L_lat1.Clear();

            loadAllDataText = new Thread(LoadAllDataText);
            loadAllDataText.Start();

            return true;
        }
        //if(MRUDataLoad && GPSDataLoad && LJDataLoad)
        //{
        //    MRUDataLoad = false;
        //    GPSDataLoad = false;
        //    LJDataLoad = false;
        //    loadMRUText.Abort();
        //    loadGPSText.Abort();
        //    loadLJText.Abort();

        //    L_pitch.Clear();
        //    L_roll.Clear();
        //    L_gspeed.Clear();
        //    L_acc.Clear();
        //    L_course.Clear();
        //    L_Head.Clear();
        //    L_long.Clear();
        //    L_lat.Clear();

        //    return false;
        //}
        //else
        //{
        //    L_pitch.Clear();
        //    L_roll.Clear();
        //    L_gspeed.Clear();
        //    L_acc.Clear();
        //    L_course.Clear();
        //    L_Head.Clear();
        //    L_long.Clear();
        //    L_lat.Clear();

        //    loadMRUText = new Thread(LoadMRUText);
        //    loadGPSText = new Thread(LoadGPSText);
        //    loadLJText = new Thread(LoadLJText);
        //    loadMRUText.Start();
        //    loadGPSText.Start();
        //    loadLJText.Start();

        //    return true;
        //}
    }

    //加载MRU文件数据
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
            if (strTmps.Length > 3)
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

    //加载GPS文件数据
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
                L_gspeed.Add(speedValue);
                L_acc.Add((speedValue - curSValue) / 0.2f);
                curSValue = speedValue;
                L_course.Add(limitAngle(float.Parse(strTmps[7])));
                L_long.Add(double.Parse(strTmps[3]));
                L_lat.Add(double.Parse(strTmps[1]));
            }
        }

        accValue = L_acc[0];
        courseValue = L_course[0];

        GspeedValue = L_gspeed[0];
        longValue = L_long[0];
        latValue = L_lat[0];

        dataNumGPS = L_acc.Count;
        GPSDataLoad = true;
    }

    //加载LJ文件数据
    void LoadLJText()
    {
        string path = Application.streamingAssetsPath + "/2/lj.txt";
        //文件读写流
        StreamReader sr = new StreamReader(path);
        //读取内容
        string result = sr.ReadToEnd();
        //逐行截取
        string[] data = result.Split('\n');

        for (int i = 1; i < data.Length; i++)
        {
            string[] strTmps = data[i].Split('"');
            if (strTmps.Length > 3)
            {
                L_Head.Add(float.Parse(strTmps[1]));
            }
        }

        headValue = L_Head[0];

        dataNumLJ = L_Head.Count;
        LJDataLoad = true;
    }

    //加载总数据
    void LoadAllDataText()
    {
        string path = Application.streamingAssetsPath + "/2/table_of_control.txt";
        //文件读写流
        StreamReader sr = new StreamReader(path);
        //读取内容
        string result = sr.ReadToEnd();
        //逐行截取
        string[] data = result.Split('\n');

        for (int i = 1; i < data.Length; i++)
        {
            string[] strTmps = data[i].Split('"');
            if (strTmps.Length > 3)
            {
                L_lat1.Add(double.Parse(strTmps[3]));
                L_long1.Add(double.Parse(strTmps[5]));
                speedValue = float.Parse(strTmps[7]);
                L_gspeed1.Add(speedValue);
                L_acc1.Add(speedValue - curSValue);
                curSValue = speedValue;
                L_course1.Add(limitAngle(float.Parse(strTmps[9])));
                L_Head1.Add(float.Parse(strTmps[11]));
                L_pitch1.Add(float.Parse(strTmps[17]));
                L_roll1.Add(float.Parse(strTmps[15]));
            }
        }

        headValue = L_Head1[0];
        pitchValue = L_pitch1[0];
        rollValue = L_roll1[0];
        accValue = L_acc1[0];
        courseValue = L_course1[0];
        GspeedValue = L_gspeed1[0];
        longValue = L_long1[0];
        latValue = L_lat1[0];

        dataNumAll = L_Head1.Count;
        AllDataLoad = true;
    }

    //发送数据到六自由度平台
    public void SendCombineData(float acc, float iyaw, float ipitch, float iroll)
    {
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

            yaw = -limitAngle(iyaw) / 180 * Mathf.PI;
            byte[] bytes4 = new byte[4];
            bytes4 = BitConverter.GetBytes(yaw);
            data1[16] = bytes4[0];
            data1[17] = bytes4[1];
            data1[18] = bytes4[2];
            data1[19] = bytes4[3];

            pitch = limitAngle(ipitch) / 180 * Mathf.PI + 0.05f;
            byte[] bytes5 = new byte[4];
            bytes5 = BitConverter.GetBytes(pitch);
            data1[20] = bytes5[0];
            data1[21] = bytes5[1];
            data1[22] = bytes5[2];
            data1[23] = bytes5[3];

            roll = -limitAngle(iroll) / 180 * Mathf.PI + Mathf.PI / 2 + 0.01f;
            byte[] bytes6 = new byte[4];
            bytes6 = BitConverter.GetBytes(roll);
            data1[24] = bytes6[0];
            data1[25] = bytes6[1];
            data1[26] = bytes6[2];
            data1[27] = bytes6[3];

            acceleration_x = acc;
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

            acceleration_y = (boatTestYBody.velocity.y - speedY) / timeport;
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

            velocity_y = boatTestYBody.velocity.y;
            speedY = velocity_y;
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

            socketDof.SendTo(data1, epDof);
        }
        timeFlag += Time.deltaTime;
    }

    /// <summary>
    /// 给一个加速度值返回顿挫感间隔，为模拟六自由度加速度调整
    /// </summary>
    /// <param name="accValue"></param>
    /// <param name="timeGap"></param>
    /// <returns></returns>
    float accOutput(float accValue,int timeGap)
    {
        if(timeGap == 0)
        {
            return accValue;
        }
        else if(timeGap == 1)
        {
            if (accFlag)
            {
                accFlag = false;
                accValue = 0;
                return accValue;
            }
            else
            {
                accFlag = true;
                return accValue;
            }
        }
        else
        {
            if (accFlag)
            {
                accIndex--;
                if (accIndex < 1)
                {
                    accFlag = false;
                }
                accValue = 0;
                return accValue;
            }
            else
            {
                accIndex++;
                if (accIndex > (timeGap - 1))
                {
                    accFlag = true;
                }
                return accValue;
            }
        }


        
    }

    //发送数据到六自由度平台1
    public void SendCombineData(Rigidbody rbody, float speed,float acc,float iyaw, float ipitch, float iroll, Transform trans)
    {
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

            yaw = iyaw;
            byte[] bytes4 = new byte[4];
            bytes4 = BitConverter.GetBytes(yaw);
            data1[16] = bytes4[0];
            data1[17] = bytes4[1];
            data1[18] = bytes4[2];
            data1[19] = bytes4[3];

            pitch = ipitch;
            byte[] bytes5 = new byte[4];
            bytes5 = BitConverter.GetBytes(pitch);
            data1[20] = bytes5[0];
            data1[21] = bytes5[1];
            data1[22] = bytes5[2];
            data1[23] = bytes5[3];

            roll = iroll;
            byte[] bytes6 = new byte[4];
            bytes6 = BitConverter.GetBytes(roll);
            data1[24] = bytes6[0];
            data1[25] = bytes6[1];
            data1[26] = bytes6[2];
            data1[27] = bytes6[3];

            acceleration_x = accOutput(acc * 10, 5) * trans.forward.z / trans.forward.magnitude;
            byte[] bytes7 = new byte[4];
            bytes7 = BitConverter.GetBytes(acceleration_x);
            data1[28] = bytes7[0];
            data1[29] = bytes7[1];
            data1[30] = bytes7[2];
            data1[31] = bytes7[3];

            acceleration_z = accOutput(acc * 10, 5) * trans.forward.x / trans.forward.magnitude;
            byte[] bytes8 = new byte[4];
            bytes8 = BitConverter.GetBytes(acceleration_z);
            data1[32] = bytes8[0];
            data1[33] = bytes8[1];
            data1[34] = bytes8[2];
            data1[35] = bytes8[3];

            acceleration_y = (rbody.velocity.y - speedY) / timeport;
            byte[] bytes9 = new byte[4];
            bytes9 = BitConverter.GetBytes(acceleration_y);
            data1[36] = bytes9[0];
            data1[37] = bytes9[1];
            data1[38] = bytes9[2];
            data1[39] = bytes9[3];

            velocity_x = speed * trans.forward.z / trans.forward.magnitude;
            byte[] bytes10 = new byte[4];
            bytes10 = BitConverter.GetBytes(velocity_x);
            data1[40] = bytes10[0];
            data1[41] = bytes10[1];
            data1[42] = bytes10[2];
            data1[43] = bytes10[3];

            velocity_z = speed * trans.forward.x / trans.forward.magnitude;
            byte[] bytes11 = new byte[4];
            bytes11 = BitConverter.GetBytes(velocity_z);
            data1[44] = bytes11[0];
            data1[45] = bytes11[1];
            data1[46] = bytes11[2];
            data1[47] = bytes11[3];

            velocity_y = rbody.velocity.y;
            speedY = velocity_y;
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

            socketDof.SendTo(data1, epDof);
        }
        timeFlag += Time.deltaTime;
    }

    float limitAngle(float value)
    {
        if (value > 180)
        {
            value -= 360;
        }
        return value;
    }
    float limit4Dot(float value)
    {
        return ((int)(value * 10000)) / 10000f;
    }
    double limit6Dot(double value)
    {
        return ((int)(value * 1000000)) / 1000000f;
    }
}
