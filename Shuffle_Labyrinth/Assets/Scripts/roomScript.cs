using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class roomScript : MonoBehaviour
{
    float moveTime;
    float numSteps;
    float newX;
    float newZ;
    float speed;
    //float speedX;
    //float speedZ;

    bool moving;
    bool edgeCase;
    Vector3 edgeDirection;

    GameObject [,] grid;
    int gridSize;
    float roomLength;
    // Start is called before the first frame update
    void Start()
    {
        getGrid();
        roomLength = GameObject.Find("Level").GetComponent<levelScript>().roomLength;
        speed = GameObject.Find("Level").GetComponent<levelScript>().speed;

        moveTime = GameObject.Find("Level").GetComponent<levelScript>().moveTime;

        moving = false;
        edgeCase = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (moving)
        {
            ShiftRoom(edgeCase);
            /*if (edgeCase)
            {
                //Debug.Log("Yahoo");
                transform.position += edgeDirection * speed * Time.deltaTime;   //Changes the position using edgeDirection

                if (edgeDirection.x > 0)        //If it's moving in a positive x-direction
                {
                    //If the current x-position is half a roomlength farther away than it's startposition
                    if ( Mathf.Abs( (newX + (gridSize-0.5f)*roomLength) - transform.position.x) < 0.01)  
                    {
                        Debug.Log("Yahoo");
                        transform.position = new Vector3(newX-roomLength, 0, transform.position.z);
                    }
                }

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
                transform.position += new Vector3(              //Changes the position
                    (newX - transform.position.x),     //The change in x-position for each step
                    0,
                    (newZ - transform.position.z)).normalized * speed * Time.deltaTime;

                if ( Mathf.Abs(newX - transform.position.x) < 0.01 &&   //The current position is within
                     Mathf.Abs(newZ - transform.position.z) < 0.01 )    //0.01 of the new position
                {
                    transform.position = new Vector3(newX, 0, newZ);
                    moving = false;
                }
            }*/
        }

    }

    //Moves this room from it's current position to the new position described by the arguments
    public void Move(float veryNewX, float veryNewZ)
    {
        newX = veryNewX;
        newZ = veryNewZ;
        moving = true;
    }

    //Different function for the edge cases (when a room needs to get to the other side of the grid)
    public void Move(bool axis)        //axis describes which axis the moved row/column is on. true = x-axis
    {
        int[] pos = getRoomPosition();      //Finds its own position in the grid
        int direction;
        if (axis)                           //If it's the x-axis
        {
            //pos[0] == this rooms coordinate. (gridsize-1) - pos[0] == opposite rooms coordinate
            direction = pos[0] - ( (gridSize-1) - pos[0] );       //Negative number if this room is on the left edge, and positive number if its on the right edge
            edgeDirection = new Vector3(direction, 0, 0).normalized;   //Creates a normalized vector that describes which direction it needs to move
            //Debug.Log(edgeDirection);
        } else
        {
            //Same as above, except here we use the Z-coordinate
            direction = pos[1] - ( (gridSize-1) - pos[1] );
            edgeDirection = new Vector3(0, 0, direction).normalized;   
        }

        edgeCase = true;
        moving = true;
        //Debug.Log(edgeCase);
    }
    private void OnTriggerEnter(Collider other)
    {
        //when player enter room, the room tell player where he is located
        if(other.gameObject.CompareTag("Player")){
            other.GetComponent<playerController>().currRoom = getRoomPosition();
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

    private void ShiftRoom(bool edgeCase)
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
    }

}