using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public GameObject camera;

    float maxSpeed = 10f;
    float curSpeed;

    float sprintSpeed;
    Rigidbody rb;

    public bool gameIsPaused = false;
    public GameObject menuObject;

    public int[] currRoom;      //Position of the room the player is currently in

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

        //pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    //resume game
    public void Resume()
    {
        menuObject.SetActive(false);
        gameIsPaused = false;
        Time.timeScale = 1f;

    }

    //pause game
    public void Pause()
    {
        menuObject.SetActive(true);
        gameIsPaused = true;
        Time.timeScale = 0f;
    }
}

