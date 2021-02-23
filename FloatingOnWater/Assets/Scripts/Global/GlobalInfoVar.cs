using UnityEngine;

public class GlobalInfoVar : MonoBehaviour
{
    public static GlobalInfoVar instance;

    private void Awake()
    {
        instance = this;
    }


    //主左推进器
    public float leftThrus;
    //主右推进器
    public float rightThrus;
    ////前左侧推进器
    //public float upLeftThrus;
    ////前右侧推进器
    //public float upRightThrus;
    //前侧推进器
    public float upThrus;
    //后侧推进器
    public float backThrus;
    //方向盘舵角
    public float rudderPointer;



    //经度
    public double Long = 0;
    //纬度
    public double Lat = 0;
    //对地航速
    public float GSpeed = 0;
    //对水航速
    public float WSpeed = 0;
    //航向
    public float Course = 0;
    //艏向
    public float Heading = 0;
    //舵角
    public float RAngle = 0;
    //发送机1转速
    public float Engine1Speed = 0;
    //发动机2转速
    public float Engine2Speed = 0;
    //横荡
    public float RollAngle = 0;
    //纵荡
    public float PitchAngle = 0;
    //艏摇
    public float BowAngle = 0;
    //锚链
    public float ChainLength = 0;


    //加速度
    //角速度


    //状态
    //固态雷达
    public int SSRader = 0;
    //毫米波雷达
    public int MRader = 0;
    //激光雷达
    public int LRader = 0;
    //GPS
    public int GPS = 0;
    //罗经
    public int ComPass = 0;
    //风速风向仪器
    public int Anemometer = 0;
    //主推器1
    public int engine1 = 0;
    //主推器2
    public int engine2 = 0;
    //侧推1
    public int thrust1 = 0;
    //侧推2
    public int thrust2 = 0;


    //环境
    //时间
    public float currentTime = 0;
    //天气
    public float currentWea = 0;
    //绝对风速
    public float ASOWind = 0;
    //绝对风向
    public float ADOWind = 0;
    //相对风速
    public float RSOWind = 0;
    //相对风向
    public float RDOWind = 0;
    //绝对流速
    public float ASOWater = 0;
    //绝对流向
    public float ADOWater = 0;
    //相对流速
    public float RSOWater = 0;
    //相对流向
    public float TDOWater = 0;
    //二阶波浪力
    public float WWave = 0;
    //水深
    public float DepthOfWater = 0;


    //侧推1图标
    public bool Thrust1Run = false;
    //侧推2图标
    public bool Thrust2Run = false;
    //主推1图标
    public bool Engine1Run = false;
    //主推2图标
    public bool Engine2Run = false;
}
