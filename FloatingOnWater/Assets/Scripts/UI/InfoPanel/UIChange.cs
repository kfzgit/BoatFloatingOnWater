using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIChange : MonoBehaviour
{

    public GameObject BasicInformationPanel;
    public GameObject EnvironmentInformationPanel;
    public GameObject Panel1;
    public GameObject Panel2;
    public  bool Isbasic, IsEnvironment;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GetBasicInformmation()
    {
        Isbasic = true;
        IsEnvironment = false;
    }

    public void GetEnvironmentInformation()
    {
        IsEnvironment = true;
        Isbasic = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(Isbasic)
            {
                Isbasic = false;
                IsEnvironment = true;
            }
            else
            {
                Isbasic = true;
                IsEnvironment = false;
            }
        }

        if(Isbasic)
        {
            Vector3 current = EnvironmentInformationPanel.transform.position;
            Vector3 direc = Panel1.transform.position;
            current = Vector3.Lerp(current, direc, Time.deltaTime * 7);
            EnvironmentInformationPanel.transform.position = current;

            current = BasicInformationPanel.transform.position;
            direc = Panel2.transform.position;
            current = Vector3.Lerp(current, direc, Time.deltaTime * 7);
            BasicInformationPanel.transform.position = current;
        }

        if(IsEnvironment)
        {
            Vector3 current = EnvironmentInformationPanel.transform.position;
            Vector3 direc = Panel2.transform.position;
            current = Vector3.Lerp(current, direc, Time.deltaTime * 7);
            EnvironmentInformationPanel.transform.position = current;

            current = BasicInformationPanel.transform.position;
            direc = Panel1.transform.position;
            current = Vector3.Lerp(current, direc, Time.deltaTime * 7);
            BasicInformationPanel.transform.position = current;
        }


       
    }
}
