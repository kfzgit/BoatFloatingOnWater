using UnityEngine;
public class FollowMouseRoate : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    // Use this for initialization
    void Start()

    {

    }
    // Update is called once per frame
    void Update()
    {
        // 获得鼠标当前位置的X和Y
        float mouseX = Input.GetAxis("Mouse X") * moveSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * moveSpeed;
        // 鼠标在Y轴上的移动号转为摄像机的上下运动，即是绕着X轴反向旋转
        Camera.main.transform.localRotation = Camera.main.transform.localRotation * Quaternion.Euler(-mouseY, 0, 0);
        // 鼠标在X轴上的移动转为主角左右的移动，同时带动其子物体摄像机的左右移动
        transform.localRotation = transform.localRotation * Quaternion.Euler(0, mouseX, 0);
    }
}
