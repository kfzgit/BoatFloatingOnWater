using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //相机模式
    public static int CamMode = 0;
    //实时数据接收开启与否
    public static bool RTDataOpen = false;

    //模拟数据驱动进度条值索引
    public static int SIndex = 0;
    //模拟数据驱动进度条值的最大值
    public static int SDataNum = 1;

    public static bool MainThru = false;
    public static bool subThru = false;
}
