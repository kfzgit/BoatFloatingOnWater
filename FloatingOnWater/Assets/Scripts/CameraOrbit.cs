using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public struct CameraParameter
{
    public bool limitXAngle;
    public float minXAngle;
    public float maxXAngle;

    public bool limitYAngle;
    public float minYAngle;
    public float maxYAngle;

    public float orbitSensitive;
    public float mouseMoveRatio;

    public CameraParameter(bool limitXAngle = true ,float minXAngle = 0f,float maxXAngle = 80f
        ,bool limitYAngle = false ,float minYAngle = 0f,float maxYAngle = 0f,float orbitSensitive = 10f
        ,float mouseMoveRatio = 0.3f)
    {
        this.limitXAngle = limitXAngle;
        this.minXAngle = minXAngle;
        this.maxXAngle = maxXAngle;
        this.limitYAngle = limitYAngle;
        this.minYAngle = minYAngle;
        this.maxYAngle = maxYAngle;
        this.orbitSensitive = orbitSensitive;
        this.mouseMoveRatio = mouseMoveRatio;
    }
}

public class CameraOrbit : MonoBehaviour {

    private Vector3 lastMousePos;
    private Vector3 targeEulerAngle;
    private Vector3 eulerAngle;

    public CameraParameter freeOrbitParameter;
    private CameraParameter currentCameraParameter;

    public Transform cameraRootTf;
    public Transform cameraTf;

    private float cameraDistance;
    private float targetCameraDistanse;

    private Quaternion originalRotate;

    private float lastTouchDistance;
    public float minDistance = 5f;
    public float maxDistance = 30f;
    public float mouseScroollRatio = 1f;
    public float zoomSensitive = 1f;


    public float[] yMinAngle;
    public float[] yMaxAngle;
    public bool[] isAlreadyFire;

    void Start()
    {
        originalRotate = cameraTf.rotation;
        cameraDistance = cameraTf.localPosition.z;
        targetCameraDistanse = cameraDistance;
        currentCameraParameter = freeOrbitParameter;
        isAlreadyFire = new bool[yMinAngle.Length];


    }
    void Update()
    {
        Oribit();
        Zoom();
    }

    private void Oribit()
    {
        if (Input.GetMouseButtonDown(1))
        {
            lastMousePos = Input.mousePosition;

        }
        if (Input.GetMouseButton(1))
        {
            targeEulerAngle.x += (-Input.mousePosition.y + lastMousePos.y) * currentCameraParameter.mouseMoveRatio;
            targeEulerAngle.y += (Input.mousePosition.x - lastMousePos.x) * currentCameraParameter.mouseMoveRatio;
            if (currentCameraParameter.limitXAngle)
            {
                targeEulerAngle.x = Mathf.Clamp(targeEulerAngle.x, currentCameraParameter.minXAngle,
                    currentCameraParameter.maxXAngle);
            }
            else if (currentCameraParameter.limitYAngle)
            {
                targeEulerAngle.y = Mathf.Clamp(targeEulerAngle.y,currentCameraParameter.minYAngle,
                    currentCameraParameter.maxYAngle);
            }
            lastMousePos = Input.mousePosition;
        }

        if (Input.touchCount < 2)
        {
            eulerAngle = Vector3.Lerp(eulerAngle,targeEulerAngle,Time.fixedDeltaTime*currentCameraParameter.orbitSensitive);
            cameraRootTf.rotation = originalRotate * Quaternion.Euler(eulerAngle);
        }
    }

    private void Zoom()
    {
        if (Input.touchCount < 2)
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                cameraDistance = -cameraTf.localPosition.z;
                targetCameraDistanse = cameraDistance - Input.GetAxis("Mouse ScrollWheel") * cameraDistance * mouseScroollRatio;
                targetCameraDistanse = Mathf.Clamp(targetCameraDistanse,minDistance,maxDistance);
            }
            else
            {
                //if (Input.GetTouch(1).phase ==  TouchPhase.Began)
                //{
                //    lastTouchDistance = Vector2.Distance(Input.GetTouch(0).position,Input.GetTouch(1).position);
                //}
                //if (Input.GetTouch(1).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Moved)
                //{
                //    cameraDistance = -cameraTf.localPosition.z;
                //    targetCameraDistanse = cameraDistance - (Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position) -
                //        lastTouchDistance) * mouseScroollRatio;
                //    lastMousePos = Input.mousePosition;
                //}
            }
            if (Mathf.Abs (targetCameraDistanse - cameraDistance ) > 0.1f)
            {
                cameraDistance = Mathf.Lerp(cameraDistance,targetCameraDistanse,Time.fixedDeltaTime*zoomSensitive);
                cameraTf.localPosition = new Vector3(0f,0f,-cameraDistance);
            }
        }
    }
}
