using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    [Range(1000f,2000f)]
    [SerializeField] private float PushForce = 1000f;
    public float forwardSpeed = 10;
    public float roateSpeed = 50;
    Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("前进");
            body.AddForce(transform.forward * PushForce);
            //body.velocity = transform.forward * forwardSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("左转");
            body.angularVelocity = -transform.up * roateSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("右转");
            body.angularVelocity = transform.up * roateSpeed;
        }
    }
}
