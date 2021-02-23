using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Data : MonoBehaviour
{
    //环境信息数组，顺序以界面从上到下为准
    public string[] EnvironmentalInformation=new string[12];

    //本船基本信息数组，顺序以界面从上到下为准
    public string[] NavigationStatus = new string[13];

    //当前经度
    public Text LongitudeText;
    //当前纬度
    public Text LatitudeText;
    //对地航速
    public Text GroundSpeedText;
    //对水航速
    public Text WaterSpeedText;
    //当前航向
    public Text CourseText;
    //当前艏向
    public Text HeadingText;
    //舵角
    public Text RudderAngleText;
    //发动机1转速
    public Text EngingSpeed1Text;
    //发动机2转速
    public Text EngingSpeed2Text;
    //横荡
    public Text RollAngleText;
    //纵荡
    public Text PitchAngleText;
    //艏摇
    public Text BowAngleText;
    //锚链
    public Text ChainlengthText;

    //固态雷达
    public Image Solid_StateRader;
    //毫米波雷达
    public Image MillimeterRader;
    //激光雷达
    public Image LaseRadar;
    //GPS
    public Image GPS;
    //罗经
    public Image Compass;
    //风速风向仪
    public Image Anemometer;
    //主推器1
    public Image Engine1;
    //主推器2
    public Image Engine2;
    //侧推1
    public Image Thrust1;
    //侧推2
    public Image Thrust2;

    //当前时间
    public Text CurrentTime;
    //当前天气
    public Text CurrentWeather;
    //绝对风速
    public Text AbsoluteSpeedOfWind;
    //绝对风向
    public Text AbsoluteDirectionOfWind;
    //相对风速
    public Text RelativeSpeedOfWind;
    //相对风向
    public Text RelativeDirectionOfWind;
    //绝对流速
    public Text AbsolutionSpeedOfWater;
    //绝对流向
    public Text AbsolutionDirectionOfWater;
    //相对流速
    public Text RelativeSpeedOfWater;
    //相对流向
    public Text RelativeDirectionOdWater;
    //二阶波浪力
    public Text WWave;
    //水深
    public Text DepthOfWater;

    //侧推1图标
    private Image Thrust1Image;
    //侧推2图标
    private Image Thrust2Image;
    //主推1图标
    private Image Enging1Image;
    //主推2 图标
    private Image Engine2Image;


    void Start()
    {
        //暂时为数据赋予初值
     for(int i=0;i<EnvironmentalInformation.Length;i++)
        {
            EnvironmentalInformation[i] = "初始化数据" + i;
        }
     for(int i=0;i<NavigationStatus.Length;i++)
        {
            NavigationStatus[i] = "初始化数据" + i;
        }

        Thrust1Image = GameObject.Find("InfoCanvas/Panel/Basic inforrmation Panel/Image").GetComponent<Image>();

    }


    void Update()
    {

        //环境信息更新数据
        CurrentTime.text = EnvironmentalInformation[0];
        CurrentWeather.text = EnvironmentalInformation[1];
        AbsoluteSpeedOfWind.text = EnvironmentalInformation[2];
        AbsoluteDirectionOfWind.text = EnvironmentalInformation[3];
        RelativeSpeedOfWind.text = EnvironmentalInformation[4];
        RelativeDirectionOfWind.text = EnvironmentalInformation[5];
        AbsolutionSpeedOfWater.text = EnvironmentalInformation[6];
        AbsolutionDirectionOfWater.text = EnvironmentalInformation[7];
        RelativeSpeedOfWater.text = EnvironmentalInformation[8];
        RelativeDirectionOdWater.text = EnvironmentalInformation[9];
        WWave.text = EnvironmentalInformation[10];
        DepthOfWater.text = EnvironmentalInformation[11];


        //本船基本信息更新数据
        if(GlobalInfoVar.instance)
        {
            LongitudeText.text = NavigationStatus[0];
            LatitudeText.text = NavigationStatus[1];
            GroundSpeedText.text = GlobalInfoVar.instance.GSpeed.ToString();// NavigationStatus[2];
            WaterSpeedText.text = NavigationStatus[3];
            CourseText.text = NavigationStatus[4];
            HeadingText.text = NavigationStatus[5];
            RudderAngleText.text = NavigationStatus[6];
            EngingSpeed1Text.text = NavigationStatus[7];
            EngingSpeed2Text.text = NavigationStatus[8];
            RollAngleText.text = GlobalInfoVar.instance.RollAngle.ToString(); //NavigationStatus[9];
            PitchAngleText.text = GlobalInfoVar.instance.PitchAngle.ToString(); //NavigationStatus[10];
            BowAngleText.text = GlobalInfoVar.instance.BowAngle.ToString(); //NavigationStatus[11];
            ChainlengthText.text = NavigationStatus[12];
        }
        else
        {
            LongitudeText.text = NavigationStatus[0];
            LatitudeText.text = NavigationStatus[1];
            GroundSpeedText.text = NavigationStatus[2];
            WaterSpeedText.text = NavigationStatus[3];
            CourseText.text = NavigationStatus[4];
            HeadingText.text = NavigationStatus[5];
            RudderAngleText.text = NavigationStatus[6];
            EngingSpeed1Text.text = NavigationStatus[7];
            EngingSpeed2Text.text = NavigationStatus[8];
            RollAngleText.text = NavigationStatus[9];
            PitchAngleText.text = NavigationStatus[10];
            BowAngleText.text = NavigationStatus[11];
            ChainlengthText.text = NavigationStatus[12];
        }

        Solid_StateRader.color = new Color(255, 0, 0, 47);
        Solid_StateRader.color = new Color(255, 0, 0, 0);
        Solid_StateRader.gameObject.GetComponentInChildren<Text>().text = "正常";
        Solid_StateRader.gameObject.GetComponentInChildren<Text>().color = new Color(0,196,255,255);
        PropellerRotation();
        
    }

    void PropellerRotation()
    {
        Thrust1Image.transform.Rotate(Vector3.forward * Time.deltaTime * -30);
    }
}
