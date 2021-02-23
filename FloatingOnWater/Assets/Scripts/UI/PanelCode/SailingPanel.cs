using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class SailingPanel : PanelBase
{
    private Toggle camMode;
    private Button InfoBtn;
    private Transform InfoPanel;
    private Transform PanelPos1;

    //车
    private Image forSail1;
    private Image forSail2;
    private Image nullSail1;
    private Image nullSail2;
    private Image backSail1;
    private Image backSail2;

    //
    private Slider LeftThrus;
    private Slider RightThrus;
    private Slider UpLeftThrus;
    private Slider UpRightThrus;
    private Slider BackThrus;
    private Transform Pointer;

    private Button ReturnBtn;

    private Button MainOpenBtn;
    private Button SubOpenBtn;
    private Button ShowHideCBtn;
    private Button SwitchCamBtn;
    private Image MainOpenImage;
    private Image SubOpenImage;
    private Image ShowHideCImage;
    private Image SwitchCamImage;

    //推开
    private Sprite MOpenSprite;
    //推关
    private Sprite MCloseSprite;
    //显示舱壁
    private Sprite ShowCSprite;
    //隐藏舱壁
    private Sprite HideCSprite;

    private Tweener tweener1;
    private bool isIn = true;

    #region 生命周期
    //初始化
    public override void Init(params object[] args)
    {
        base.Init(args);
        skinPath = "UIPanel/SailingPanel";
        layer = PanelLayer.Panel;
    }

    public override void OnShowing()
    {
        base.OnShowing();
        Transform skinTrans = skin.transform;

        camMode = skinTrans.Find("Toggle").GetComponent<Toggle>();
        camMode.onValueChanged.AddListener(OnCamMode);

        InfoPanel = skinTrans.Find("InfoPanel");
        PanelPos1 = skinTrans.Find("PanelPos1");

        forSail1 = InfoPanel.Find("forSail1").GetComponent<Image>();
        forSail2 = InfoPanel.Find("forSail2").GetComponent<Image>();
        nullSail1 = InfoPanel.Find("nullSail1").GetComponent<Image>();
        nullSail2 = InfoPanel.Find("nullSail2").GetComponent<Image>();
        backSail1 = InfoPanel.Find("backSail1").GetComponent<Image>();
        backSail2 = InfoPanel.Find("backSail2").GetComponent<Image>();

        LeftThrus = InfoPanel.Find("LeftThrus").GetComponent<Slider>();
        RightThrus = InfoPanel.Find("RightThrus").GetComponent<Slider>();
        UpLeftThrus = InfoPanel.Find("UpLeftThrus").GetComponent<Slider>();
        UpRightThrus = InfoPanel.Find("UpRightThrus").GetComponent<Slider>();
        BackThrus = InfoPanel.Find("BackThrus").GetComponent<Slider>();
        Pointer = skinTrans.Find("RudderPanel").Find("Pointer");

        ReturnBtn = skinTrans.Find("ReturnBtn").GetComponent<Button>();
        ReturnBtn.onClick.AddListener(OnReturnBtn);

        //
        InfoBtn = skinTrans.Find("InfoBtn").GetComponent<Button>();
        InfoBtn.onClick.AddListener(OnInfoBtn);

        MainOpenBtn = InfoPanel.Find("MainOpenBtn").GetComponent<Button>();
        SubOpenBtn = InfoPanel.Find("SubOpenBtn").GetComponent<Button>();
        ShowHideCBtn = skinTrans.Find("ShowHideCBtn").GetComponent<Button>();
        SwitchCamBtn = skinTrans.Find("SwitchCamBtn").GetComponent<Button>();
        MainOpenImage = InfoPanel.Find("MainOpenBtn").GetComponent<Image>();
        SubOpenImage = InfoPanel.Find("SubOpenBtn").GetComponent<Image>();
        ShowHideCImage = skinTrans.Find("ShowHideCBtn").GetComponent<Image>();
        SwitchCamImage = skinTrans.Find("SwitchCamBtn").GetComponent<Image>();

        MOpenSprite = Resources.Load("UIPic/1", typeof(Sprite)) as Sprite;
        MCloseSprite = Resources.Load("UIPic/2", typeof(Sprite)) as Sprite;
        ShowCSprite = Resources.Load("UIPic/7", typeof(Sprite)) as Sprite;
        HideCSprite = Resources.Load("UIPic/9", typeof(Sprite)) as Sprite;

        MainOpenBtn.onClick.AddListener(OnMainOpenBtn);
        SubOpenBtn.onClick.AddListener(OnSubOpenBtn);
        ShowHideCBtn.onClick.AddListener(OnShowHideCBtn);
        SwitchCamBtn.onClick.AddListener(OnSwitchCamBtn);

        //
        tweener1 = InfoPanel.DOLocalMoveX(PanelPos1.localPosition.x, 0.5f);
        tweener1.SetAutoKill(false);
        tweener1.Pause();
    }

    //帧更新
    public override void Update()
    {
        //control
        if (ctrlMgr.instance.GPlusReleased())
        {
            if (!GlobalVar.MainThru)
            {
                MainOpenImage.sprite = MOpenSprite;
                SubOpenImage.sprite = MCloseSprite;
                GlobalVar.MainThru = true;
                GlobalVar.subThru = false;
            }
            else
            {
                MainOpenImage.sprite = MCloseSprite;
                GlobalVar.MainThru = false;
            }
        }
        if (ctrlMgr.instance.GLessReleased())
        {
            if (!GlobalVar.subThru)
            {
                SubOpenImage.sprite = MOpenSprite;
                MainOpenImage.sprite = MCloseSprite;
                GlobalVar.subThru = true;
                GlobalVar.MainThru = false;
            }
            else
            {
                SubOpenImage.sprite = MCloseSprite;
                GlobalVar.subThru = false;
            }
        }
        if (ctrlMgr.instance.GTriangleReleased())
        {
            GlobalVar.CamMode = GlobalVar.CamMode == 1 ? 0 : 1;
        }
        if (ctrlMgr.instance.GSquareReleased())
        {
            if (isIn)
            {
                tweener1.PlayForward();
                isIn = false;
            }
            else
            {
                tweener1.PlayBackwards();
                isIn = true;
            }
        }

        //
        LeftThrus.value = 0.5f * GlobalInfoVar.instance.leftThrus + 0.5f;
        RightThrus.value = 0.5f * GlobalInfoVar.instance.rightThrus + 0.5f;
        if(GlobalInfoVar.instance.upThrus >= 0)
        {
            UpLeftThrus.value = 1 - GlobalInfoVar.instance.upThrus;
            UpRightThrus.value = 0;
        }
        else
        {
            UpLeftThrus.value = 1;
            UpRightThrus.value = -GlobalInfoVar.instance.upThrus;
        }
        BackThrus.value = 0.5f * GlobalInfoVar.instance.backThrus + 0.5f;
        Pointer.eulerAngles = new Vector3(0, 0, 150.0f * GlobalInfoVar.instance.rudderPointer);

        if(GlobalInfoVar.instance.leftThrus > 0)
        {
            forSail1.color = new Color(0.5f, 1, 0.5f, 1);
            forSail2.color = new Color(0.5f, 1, 0.5f, 1);
            nullSail1.color = new Color(1, 1, 1, 1);
            nullSail2.color = new Color(1, 1, 1, 1);
            backSail1.color = new Color(1, 1, 1, 1);
            backSail2.color = new Color(1, 1, 1, 1);
        }
        else if(GlobalInfoVar.instance.leftThrus < 0)
        {
            forSail1.color = new Color(1, 1, 1, 1);
            forSail2.color = new Color(1, 1, 1, 1);
            nullSail1.color = new Color(1, 1, 1, 1);
            nullSail2.color = new Color(1, 1, 1, 1);
            backSail1.color = new Color(0.5f, 1, 0.5f, 1);
            backSail2.color = new Color(0.5f, 1, 0.5f, 1);
        }
        else
        {
            forSail1.color = new Color(1, 1, 1, 1);
            forSail2.color = new Color(1, 1, 1, 1);
            nullSail1.color = new Color(0.5f, 1, 0.5f, 1);
            nullSail2.color = new Color(0.5f, 1, 0.5f, 1);
            backSail1.color = new Color(1, 1, 1, 1);
            backSail2.color = new Color(1, 1, 1, 1);
        }
    }

    #endregion

    void OnReturnBtn()
    {
        SceneManager.LoadScene("StartScene");
        Close();
    }

    void OnMainOpenBtn()
    {
        if(!GlobalVar.MainThru)
        {
            MainOpenImage.sprite = MOpenSprite;
            SubOpenImage.sprite = MCloseSprite;
            GlobalVar.MainThru = true;
            GlobalVar.subThru = false;
        }
        else
        {
            MainOpenImage.sprite = MCloseSprite;
            GlobalVar.MainThru = false;
        }
    }
    void OnSubOpenBtn()
    {
        if (!GlobalVar.subThru)
        {
            SubOpenImage.sprite = MOpenSprite;
            MainOpenImage.sprite = MCloseSprite;
            GlobalVar.subThru = true;
            GlobalVar.MainThru = false;
        }
        else
        {
            SubOpenImage.sprite = MCloseSprite;
            GlobalVar.subThru = false;
        }
    }
    void OnShowHideCBtn()
    {
        bool value = boatPlayer.instance.ShowHideBoatC();
        if(value)
        {
            ShowHideCImage.sprite = HideCSprite;
        }
        else
        {
            ShowHideCImage.sprite = ShowCSprite;
        }
    }
    void OnSwitchCamBtn()
    {
        GlobalVar.CamMode = GlobalVar.CamMode == 1 ? 0 : 1;
    }

    void OnCamMode(bool value)
    {
        if(value)
        {
            GlobalVar.CamMode = 0;
        }
        else
        {
            GlobalVar.CamMode = 1;
        }
    }

    void OnInfoBtn()
    {
        if(isIn)
        {
            tweener1.PlayForward();
            isIn = false;
        }
        else
        {
            tweener1.PlayBackwards();
            isIn = true;
        }
    }
}
