using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public GameObject camera;

    float maxSpeed = 10f;
    float curSpeed;

    float sprintSpeed;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // sprintSpeed = walkSpeed + (walkSpeed / 2);
    }

    void FixedUpdate()
    {
        curSpeed = walkSpeed;
        maxSpeed = curSpeed;

        // the movement magic
        rb.velocity = new Vector3(
            Mathf.Lerp(0, Input.GetAxis("Horizontal") * curSpeed, 0.8f),
            0,
            Mathf.Lerp(0, Input.GetAxis("Vertical") * curSpeed, 0.8f)
        );
    }
    void Update()
    {
        if (rb.velocity != new Vector3(0, 0, 0))
        {
            transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity);
        }
    }
}
