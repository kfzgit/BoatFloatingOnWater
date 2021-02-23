using UnityEngine;
using System.Collections;

public class Root : MonoBehaviour {

    private static Root instance;

    public Canvas UICanvas;
    public GameObject CanvasEventSystem;
    public Canvas InfoCanvas;
    public Camera InfoCamera;
    public GameObject GlobalDataObj;
    public GameObject DataRootObj;

    void Awake()
    {
        if (instance != null)
        {
            //Destroy(gameObject);
            Destroy(UICanvas.gameObject);
            Destroy(CanvasEventSystem);
            Destroy(InfoCanvas.gameObject);
            Destroy(InfoCamera.gameObject);
            Destroy(GlobalDataObj);
            //Destroy(DataRootObj);
            return;
        }
        else
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(UICanvas.gameObject);
            DontDestroyOnLoad(CanvasEventSystem);
            DontDestroyOnLoad(InfoCanvas.gameObject);
            DontDestroyOnLoad(InfoCamera.gameObject);
            DontDestroyOnLoad(GlobalDataObj);
            //DontDestroyOnLoad(DataRootObj);
        }

        
    }

    // Use this for initialization
    void Start () 
    {
        Application.runInBackground = true;
        //Time.timeScale = 0;
        PanelMgr.instance.OpenPanel<InitPanel>("");
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }

    public void Quit()
    {
        //打包时不能使用
        //UnityEditor.EditorApplication.isPlaying = false;
        //测试时不能执行，打包后可以执行
        Application.Quit();
    }
}

///UI分层类型
public enum PanelLayer
{
    //面板
    Panel,
    //提示
    Tips,
}