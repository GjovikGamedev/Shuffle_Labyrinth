using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            if (edgeCase)
            {
                /*if (positive)
                {
                    if (!halfWay)
                    {
                        transform.position += new Vector3(
                            (newX - transform.position.x) / numSteps,       Altfor rotete
                            0,
                            (newZ - transform.position.z) / numSteps);
                    }
                }*/
            }
            else
            {
                transform.position += new Vector3(              //Changes the position
                    (newX - transform.position.x),     //The change in x-position for each step
                    0,
                    (newZ - transform.position.z)).normalized * speed * Time.deltaTime;

                if ( (newX - transform.position.x) < 0.01 && -0.01 < (newX - transform.position.x) &&
                     (newZ - transform.position.z) < 0.01 && -0.01 < (newZ - transform.position.z) )
                {
                    transform.position = new Vector3(newX, 0, newZ);
                    moving = false;
                }
            }
        }

    }

    //Moves this room from it's current position to the new position described by the 
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
            edgeDirection = new Vector3(direction, 0, 0).normalized;   //Creates a normalized vector
        } else
        {
            //Same as above, except here we use the Z-coordinate
            direction = pos[1] - ( (gridSize-1) - pos[1] );
            edgeDirection = new Vector3(0, 0, 1).normalized;   //Finds the direction of
        }

        edgeCase = true;
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

}