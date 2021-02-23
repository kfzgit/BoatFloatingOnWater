using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class testAcc1 : MonoBehaviour
{
    //[DllImport("Dll2")]
    //static extern float WashoutFilterValue(float f);
    [DllImport("accWashOut")]
    static extern float WashoutFilterValue(float u_in);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print(WashoutFilterValue(2.0f));
    }
}
