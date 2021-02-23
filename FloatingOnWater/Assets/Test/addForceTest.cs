using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addForceTest : MonoBehaviour
{
    Rigidbody abc;
    public WebCamTexture cameraTexture;

    // Start is called before the first frame update
    void Start()
    {
        abc = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        abc.AddForceAtPosition(80 * transform.forward, transform.Find("adisf").position);
        //abc.AddForceAtPosition(-80 * transform.forward, transform.Find("adfew").position);
    }
}
