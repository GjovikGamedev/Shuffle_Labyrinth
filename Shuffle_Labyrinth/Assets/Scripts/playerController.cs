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

    int facing;             //The direction the player is facing. 1 == right | 2 == left | 3 == up | 4 == down
    Vector3 mDirection;     //The direction the rooms are going to shift

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

        //Determines the direction of the player
        if (Mathf.Abs( Input.GetAxis("Horizontal") ) > Mathf.Abs( Input.GetAxis("Vertical") )       //The player is going more to the side than up/down
            && Mathf.Abs( Input.GetAxis("Horizontal")) > 0)                                         //and is going to the side
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                facing = 1;     //Player is facing right
            }
            else
            { 
                facing = 2;     //Player is facing left
            }

        //If the input up/down is larger than the input to the sides, then the 'else if' sentence is triggered
        } else if (Mathf.Abs( Input.GetAxis("Vertical") ) > 0)   //If the player is moving up or down
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                facing = 3;     //Player is facing up
            }
            else
            {
                facing = 4;     //Player is facing down
            }
        }

        /*
        if ()
        {

        }*/

        //Some testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(facing);
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

