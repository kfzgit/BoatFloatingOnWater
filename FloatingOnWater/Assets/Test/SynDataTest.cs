using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine.UI;
using System;

public class SynDataTest : MonoBehaviour
{

    public Button connectBtn;
    public Button sendBtn;
    public Text state;
    public Text dataText;

    string aaa = "";

    Socket socketSend;

    // Start is called before the first frame update
    void Start()
    {
        connectBtn.onClick.AddListener(StartConnect);
        sendBtn.onClick.AddListener(StartSend);
    }

    void StartConnect()
    {
        Connect();
    }

    void StartSend()
    {
        send("gps");
    }

    // Update is called once per frame
    void Update()
    {
        dataText.text = aaa;
    }

    private void Connect()
    {
        try
        {
            int _port = 3627;
            string _ip = "10.0.105.49";

            //创建客户端Socket，获得远程ip和端口号
            socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(_ip);
            IPEndPoint point = new IPEndPoint(ip, _port);

            socketSend.Connect(point);
            Debug.Log("连接成功!");
            state.text = "连接成功";
            //开启新的线程，不停的接收服务器发来的消息
            Thread c_thread = new Thread(Received);
            c_thread.IsBackground = true;
            c_thread.Start();
        }
        catch (Exception)
        {
            Debug.Log("IP或者端口号错误...");
            state.text = "IP或者端口号错误";
        }

    }

    /// <summary>
    /// 接收服务端返回的消息
    /// </summary>
    void Received()
    {
        while (true)
        {
            try
            {
                byte[] buffer = new byte[1024 * 1024 * 3];
                //实际接收到的有效字节数
                int len = socketSend.Receive(buffer);
                if (len == 0)
                {
                    break;
                }
                string str = Encoding.UTF8.GetString(buffer, 0, len);
                Debug.Log("客户端打印：" + socketSend.RemoteEndPoint + ":" + str);
                aaa = str;
            }
            catch { }
        }
    }

    /// <summary>
    /// 向服务器发送消息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void send(string str)
    {
        try
        {
            string msg = str;
            byte[] buffer = new byte[1024 * 1024 * 3];
            buffer = Encoding.UTF8.GetBytes(msg);
            socketSend.Send(buffer);
        }
        catch { }
    }
}
