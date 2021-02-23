using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMgr : MonoBehaviour
{
    public Transform boatTrans;

    Vector3 eulerA = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GlobalInfoVar.instance)
        {
            //eulerA = new Vector3(-GlobalInfoVar.instance.PitchAngle, GlobalInfoVar.instance.BowAngle, -GlobalInfoVar.instance.RollAngle);
            eulerA = new Vector3(-GlobalInfoVar.instance.PitchAngle, 0, -GlobalInfoVar.instance.RollAngle);
            boatTrans.eulerAngles = Vector3.Lerp(boatTrans.eulerAngles, eulerA,1);
        }
    }
}
