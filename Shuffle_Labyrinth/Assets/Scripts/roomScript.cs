using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class roomScript : MonoBehaviour
{
    float speed;        //The speed of which the rooms move
    float newX, newZ;   //The new position for when the room moves
    float roomLength;   //The length of one room

    bool moving;            //Whether or not the room is currently moving
    bool edgeCase;          //Whether or not the room is an edge case
    Vector3 mDirection;     //MoveDirection. The direction the room has to move when the player shifts it

    GameObject [,] grid;

    int gridSize;       //The gridsize of the current level     | These two variables are
    int[] pos;         //This rooms position in this level     | set when spawned (instantiated)


    // Start is called before the first frame update
    void Start()
    {
        getGrid();
        roomLength = GameObject.Find("Level").GetComponent<levelScript>().roomLength;
        speed = GameObject.Find("Level").GetComponent<levelScript>().speed;

        moving = false;
        edgeCase = false;
    }

    //Sets the position in the grid
    public void SetPosition(int x, int z)
    {
        pos = new int[2];

        pos[0] = x;
        pos[1] = z;
    }

    // Update is called once per frame
    void Update()
    {

        if (moving)
        {
            if (edgeCase)
            {
                transform.position += mDirection * speed * Time.deltaTime;   //Changes the position using edgeDirection

                //If the current relevant position is half a roomlength farther away from the new position than it's start position
                //Note: The relevant position is wichever axis the room is moving on
                if (Mathf.Abs( (newX + mDirection.x * roomLength * (gridSize - 0.5f) ) - transform.position.x) < 0.01 &&
                    Mathf.Abs( (newZ + mDirection.z * roomLength * (gridSize - 0.5f) ) - transform.position.z) < 0.01)
                {
                    //Puts the room half a roomLength away from its destination
                    transform.position = new Vector3(newX - mDirection.x * roomLength/2, 0, newZ - mDirection.z * roomLength/2);
                }

                /*if (edgeDirection.x > 0)        //If it's moving in a positive x-direction
                {
                    //If the current x-position is half a roomlength farther away than it's startposition
                    if ( Mathf.Abs( (newX + (gridSize-0.5f)*roomLength) - transform.position.x) < 0.01)  
                    {
                        Debug.Log("Yahoo");
                        transform.position = new Vector3(newX-roomLength, 0, transform.position.z);
                    }
                }*/

                if (Mathf.Abs(newX - transform.position.x) < 0.01 &&   //The current position is within
                    Mathf.Abs(newZ - transform.position.z) < 0.01)     //0.01 of the new position
                {
                    transform.position = new Vector3(newX, 0, newZ);
                    moving = false;
                    edgeCase = false;
                }

            }
            else
            {
                transform.position += mDirection * speed * Time.deltaTime;

                if ( Mathf.Abs(newX - transform.position.x) < 0.01 &&   //The current position is within
                     Mathf.Abs(newZ - transform.position.z) < 0.01 )    //0.01 of the new position
                {
                    transform.position = new Vector3(newX, 0, newZ);
                    moving = false;
                }
            }
        }

    }

    /**
     * Moves this room from it's current position to the new position described by the arguments
     * 
     * @param edgeDirection. The direction that the room will be moving. Normalized.
     * @param edgeCase. Whether or not it is an edgeCase (a room at the outer edge of a room/column)
     */
    public void Move(Vector3 direction, bool eCase)
    {
        mDirection = direction;
        edgeCase = eCase;
        moving = true;

        if (edgeCase)
        {
            newX = transform.position.x - mDirection.x * roomLength * (gridSize-1); //The opposite side of the grid is the same
            newZ = transform.position.z - mDirection.z * roomLength * (gridSize-1); //as current position - the width of the grid 
        } else
        {
            newX = transform.position.x + mDirection.x * roomLength;  // The new x and z positions are the original positions +
            newZ = transform.position.z + mDirection.z * roomLength;  // one roomLength in the moveDirection
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //when player enter room, the room tell player where he is located
        if(other.gameObject.CompareTag("Player")){
            //other.GetComponent<playerController>().currRoom = getRoomPosition();
            other.GetComponent<playerController>().currRoom = pos;
        }
    }


    public int [] getRoomPosition(){
        getGrid();
        for(int i = 0; i < gridSize; i++){
            for(int j = 0; j < gridSize; j++) {
                if(grid[i,j] == gameObject){
                    int[] pos = {i,j};
                    return pos;
                }
            }
        }
        return null;
    }

    public void getGrid(){
        grid = GameObject.Find("Level").GetComponent<levelScript>().grid;
        gridSize = GameObject.Find("Level").GetComponent<levelScript>().gridSize;
    }

    /*private void ShiftRoom(bool edgeCase)
    {
        if (edgeCase)
        {
            transform.position += edgeDirection * speed * Time.deltaTime;   //Changes the position using edgeDirection

            //Debug.Log(edgeDirection);

            //If it's moving in the x-direction
            if (edgeDirection.x > 0)        //If it's moving in a positive x-direction
            {
                //If the current x-position is half a roomlength farther away than it's startposition
                if (Mathf.Abs((newX + (gridSize - 0.5f) * roomLength) - transform.position.x) < 0.01)
                {
                    transform.position = new Vector3(newX - roomLength, 0, transform.position.z);
                }

            } else if (edgeDirection.x < 0) //If it's moving in a negative x-direction
            {
                if (Mathf.Abs(newX - transform.position.x) < 0.01)
                {
                    transform.position = new Vector3(newX + roomLength, 0, transform.position.z);
                }
            }


            //If it's moving in the z-direction. Basically the same as the code above
            if (edgeDirection.z > 0)        //If it's moving in a positive z-direction
            {
                //If the current z-position is half a roomlength farther away than it's startposition
                if (Mathf.Abs((newZ + (gridSize - 0.5f) * roomLength) - transform.position.z) < 0.01)
                {
                    transform.position = new Vector3(transform.position.x, 0, newZ - roomLength);
                }

            }
            else if (edgeDirection.z < 0) //If it's moving in a negative z-direction
            {
                //Debug.Log(newZ);
                if (Mathf.Abs((newZ - (gridSize - 0.5f) * roomLength) - transform.position.z) < 0.01)
                {
                    //Debug.Log("Yahoo");
                    transform.position = new Vector3(transform.position.x, 0, newZ - roomLength);
                }
            }


        }
        else
        {
            transform.position += new Vector3(          //Changes the position
                (newX - transform.position.x),          //The change in x-position for each step
                0,
                (newZ - transform.position.z)).normalized * speed * Time.deltaTime;
        }


        if (Mathf.Abs(newX - transform.position.x) < 0.01 &&    //The current position is within
            Mathf.Abs(newZ - transform.position.z) < 0.01 )     //0.01 of the new position
        {
            transform.position = new Vector3(newX, 0, newZ);
            moving = false;
            edgeCase = false;
        }
    }*/

}