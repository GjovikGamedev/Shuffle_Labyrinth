using System.Collections;
using System.Collections.Generic;
//using System.Numerics;    Messes up with some quaternion stuff I did, since both this and unityEngine 
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

    GameObject[,] grid;         //The grid of the current level
    int gridSize;               //The gridsize of the current level

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // sprintSpeed = walkSpeed + (walkSpeed / 2);
        facing = 3;     //Starts off with facing up
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

        //Reads the room shifting inputs
        if (Input.GetKeyDown(KeyCode.K))    //Leftshifting
        {
            roomShift(-1);
        } else if (Input.GetKeyDown(KeyCode.L))     //Rightshifting
        {
            roomShift(1);
        }

        //Some testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(facing);
        }
    }

    private void getGrid()
    {
        grid = GameObject.Find("Level").GetComponent<levelScript>().grid;
        gridSize = GameObject.Find("Level").GetComponent<levelScript>().gridSize;   //Only actually needs to get the gridSize when the level changes
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

    /**
     * Finds all the relevant rooms and shifts them, if possible.
     * 
     * @param leftRight. Which direction you shifted
     */
    private void roomShift(int leftRight)
    {
        getGrid();
        float roomX;        //The x-position of the room we're shifting
        float roomZ = currRoom[1];        //The z-position of the room we're shifting

        //Changes the mDirection to the direction of where you are facing
        switch (facing)
        {
            case 1:
                mDirection = new Vector3(1, 0, 0);
                break;
            case 2:
                mDirection = new Vector3(-1, 0, 0);
                break;
            case 3:
                mDirection = new Vector3(0, 0, 1);
                break;
            case 4:
                mDirection = new Vector3(0, 0, -1);
                break;
        }

        roomX = currRoom[0] + mDirection.x;
        roomZ = currRoom[1] + mDirection.z;
        Debug.Log("x = " + roomX);
        Debug.Log("z = " + roomZ);
        Debug.Log("------------------");

        if (roomX >= 0 && roomX < gridSize &&       //The room position is within the bounds of the grid
            roomZ >= 0 && roomZ < gridSize)
        {
            mDirection = Quaternion.Euler(0, 90*leftRight, 0) * mDirection;     //Rotates mDirection along the y-axis. Don't ask how
            Debug.Log(mDirection);

        } else
        {
            Debug.Log("Ikke ett rom foran deg der");
        }
    }
}

