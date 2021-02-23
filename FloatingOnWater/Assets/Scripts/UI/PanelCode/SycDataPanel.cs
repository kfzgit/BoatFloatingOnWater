using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SycDataPanel : PanelBase
{
    private int hour;
    private int minute;
    private int second;
    private int year;
    private int month;
    private int day;
    private DayOfWeek week;

    private Text RatioText;

    //time
    private Text TimeText;
    private Text DataText;

    //舵角
    private Text RuAngleText;
    //横荡
    private Text RollText;
    //纵荡
    private Text PitchText;
    //艏摇
    private Text HeadAngText;
    //Long
    private Text LongText;
    //Lat
    private Text LatText;
    //对地航速
    private Text SpeedText;
    //对地航向
    private Text CourseText;

    private Button netBtn;
    private Button localBtn;

    private Button ReturnBtn;

    private Slider JinDuTiao;
    private float curPValue;

    #region 生命周期
    //初始化
    public override void Init(params object[] args)
    {
        base.Init(args);
        skinPath = "UIPanel/SycDataPanel";
        layer = PanelLayer.Panel;
    }

    public override void OnShowing()
    {
        base.OnShowing();
        Transform skinTrans = skin.transform;

        RatioText = skinTrans.Find("RatioText").GetComponent<Text>();

        //获取当前时间
        year = DateTime.Now.Year;
        month = DateTime.Now.Month;
        day = DateTime.Now.Day;
        week = DateTime.Now.DayOfWeek;
        DataText = skinTrans.Find("DataText").GetComponent<Text>();
        DataText.text = month + "月" + day + "日\r\n" + week;
        TimeText = skinTrans.Find("TimeText").GetComponent<Text>();

        RuAngleText = skinTrans.Find("RuAngleText").GetComponent<Text>();
        RollText = skinTrans.Find("RollText").GetComponent<Text>();
        PitchText = skinTrans.Find("PitchText").GetComponent<Text>();
        HeadAngText = skinTrans.Find("HeadAngText").GetComponent<Text>();
        LongText = skinTrans.Find("LongText").GetComponent<Text>();
        LatText = skinTrans.Find("LatText").GetComponent<Text>();
        SpeedText = skinTrans.Find("SpeedText").GetComponent<Text>();
        CourseText = skinTrans.Find("CourseText").GetComponent<Text>();

        netBtn = skinTrans.Find("NetBtn").GetComponent<Button>();
        localBtn = skinTrans.Find("LocalBtn").GetComponent<Button>();

        ReturnBtn = skinTrans.Find("ReturnBtn").GetComponent<Button>();
        ReturnBtn.onClick.AddListener(OnReturnBtn);

        JinDuTiao = skinTrans.Find("JinDuTiao").GetComponent<Slider>();

        netBtn.onClick.AddListener(onNetBtn);
        localBtn.onClick.AddListener(onLocalBtn);

        JinDuTiao.onValueChanged.AddListener(onJinDuTiao);
        
    }

    //帧更新
    public override void Update()
    {
        RatioText.text = DataMgr.instance.ratioD.ToString();

        hour = DateTime.Now.Hour;
        minute = DateTime.Now.Minute;
        second = DateTime.Now.Second;

        TimeText.text = hour + ":" + minute + ":" + second;

        RuAngleText.text = GlobalInfoVar.instance.RAngle.ToString();
        RollText.text = GlobalInfoVar.instance.RollAngle.ToString();
        PitchText.text = GlobalInfoVar.instance.PitchAngle.ToString();
        HeadAngText.text = GlobalInfoVar.instance.BowAngle.ToString();
        LongText.text = GlobalInfoVar.instance.Long.ToString();
        LatText.text = GlobalInfoVar.instance.Lat.ToString();
        SpeedText.text = GlobalInfoVar.instance.GSpeed.ToString();
        CourseText.text = GlobalInfoVar.instance.Course.ToString();

        if (!Input.GetMouseButton(0) && Mathf.Abs((float)GlobalVar.SIndex / GlobalVar.SDataNum - JinDuTiao.value) > 0.05f)
        {
            JinDuTiao.value = (float)GlobalVar.SIndex / GlobalVar.SDataNum;
        }
        
    }

    #endregion

    void OnReturnBtn()
    {
        SceneManager.LoadScene("StartScene");
        //PanelMgr.instance.OpenPanel<InitPanel>("");
        Close();
    }

    void onNetBtn()
    {
        //if(localBtn.GetComponentInChildren<Text>().text == "关闭本地数据测试")
        //{
        //    print("请先关闭本地数据测试");
        //    PanelMgr.instance.OpenPanel<TipPanel>("", "请先关闭本地数据测试");
        //}
        //else
        //{
        //    if(netBtn.GetComponentInChildren<Text>().text == "开启同步连接")
        //    {
        //DataMgr.instance.ConnectSynData();
        if (DataMgr.instance.ConnectSynData())
        {
            //print("开启成功");
            PanelMgr.instance.OpenPanel<TipPanel>("", "开启成功");
            GlobalVar.RTDataOpen = true;
            //netBtn.GetComponentInChildren<Text>().text = "关闭同步连接";
        }
        else
        {
            //print("开启失败");
            PanelMgr.instance.OpenPanel<TipPanel>("", "开启失败或关闭成功");
            GlobalVar.RTDataOpen = false;
        }
        //    }
        //    else
        //    {
        //        netBtn.GetComponentInChildren<Text>().text = "开启同步连接";
        //        GlobalVar.RTDataOpen = false;
        //    }
        //}
    }

    void onLocalBtn()
    {
        //if(netBtn.GetComponentInChildren<Text>().text == "关闭同步连接")
        //{
        //    print("请先关闭同步连接");
        //    PanelMgr.instance.OpenPanel<TipPanel>("", "请先关闭同步连接");
        //}
        //else
        //{
        if(GlobalVar.RTDataOpen)
        {
            PanelMgr.instance.OpenPanel<TipPanel>("", "请先关闭实时同步");
        }
        else
        {
            if(DataMgr.instance.loadTxtData())
            {
                PanelMgr.instance.OpenPanel<TipPanel>("", "开启成功");
            }
            else
            {
                PanelMgr.instance.OpenPanel<TipPanel>("", "开启失败或者关闭成功");
            }
        }
            
        //    localBtn.GetComponentInChildren<Text>().text = localBtn.GetComponentInChildren<Text>().text == "开始本地数据测试" ? "关闭本地数据测试" : "开始本地数据测试";
        //}
    }

    void onJinDuTiao(float value)
    {
        GlobalVar.SIndex = (int)(GlobalVar.SDataNum * value);
    }
}
