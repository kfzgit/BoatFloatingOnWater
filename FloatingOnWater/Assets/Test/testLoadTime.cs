using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testLoadTime : MonoBehaviour
{
    private Text CurrrentTimeText;
    private int hour;
    private int minute;
    private int second;
    private int year;
    private int month;
    private int day;

    // Use this for initialization
    void Start()
    {
        CurrrentTimeText = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        //获取当前时间
        hour = DateTime.Now.Hour;
        minute = DateTime.Now.Minute;
        second = DateTime.Now.Second;
        year = DateTime.Now.Year;
        month = DateTime.Now.Month;
        day = DateTime.Now.Day;

        //格式化显示当前时间
        CurrrentTimeText.text = string.Format("{0:D2}:{1:D2}:{2:D2} " + "{3:D4}/{4:D2}/{5:D2}", hour, minute, second, year, month, day);

#if UNITY_EDITOR
        Debug.Log("W now " + System.DateTime.Now);     //当前时间（年月日时分秒）
        Debug.Log("W utc " + System.DateTime.UtcNow);  //当前时间（年月日时分秒）
#endif
    }
}
