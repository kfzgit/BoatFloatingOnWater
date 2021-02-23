using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitPanel : PanelBase
{
    private Button simulaBtn;
    private Button dataDriBtn;

    #region 生命周期
    //初始化
    public override void Init(params object[] args)
    {
        base.Init(args);
        skinPath = "UIPanel/InitPanel";
        layer = PanelLayer.Panel;
    }

    public override void OnShowing()
    {
        base.OnShowing();
        Transform skinTrans = skin.transform;
        simulaBtn = skinTrans.Find("simulaBtn").GetComponent<Button>();
        dataDriBtn = skinTrans.Find("dataDriBtn").GetComponent<Button>();

        simulaBtn.onClick.AddListener(OnSimulaBtn);
        dataDriBtn.onClick.AddListener(OndataDriBtn);
    }
    #endregion

    public void OnSimulaBtn()
    {
        SceneManager.LoadScene("ShipSixDofPro1");
        PanelMgr.instance.OpenPanel<SailingPanel>("");
        //Time.timeScale = 1;
        Close();
    }
    public void OndataDriBtn()
    {
        SceneManager.LoadScene("ShipSixDofPro2");
        PanelMgr.instance.OpenPanel<SycDataPanel>("");
        //Time.timeScale = 1;
        Close();
    }
}
