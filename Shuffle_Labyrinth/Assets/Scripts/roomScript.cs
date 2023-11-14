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
    bool halfWay;
    bool positive;
    float roomLength;
    GameObject [,] grid;
    float gridSize;
    // Start is called before the first frame update
    void Start()
    {
        getGrid();
        roomLength = GameObject.Find("Level").GetComponent<levelScript>().moveTime;
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
        /*newX = transform.position.x;
        newZ = transform.position.z;

        if (axis)
        {
            for (int i = 0; i < 5; i++)     //The fives here should eventually be replaced by gridSize
            {
                if (grid[0, i] == this)
                {
                    newX = roomLength * 5;
                    positive = true;        //Goes in a positive direction
                }
            }

            for (int i = 0; i < 5; i++)     //The fives here should eventually be replaced by gridSize
            {
                if (grid[5, i] == this)
                {
                    newX = 0;
                    positive = false;        //Goes in a positive direction
                }
            }

        } else
        {
            for (int i = 0; i < 5; i++)     //The fives here should eventually be replaced by gridSize
            {
                if (grid[i, 0] == this)
                {
                    newZ = roomLength * 5;
                    positive = true;        //Goes in a positive direction
                }
            }

            for (int i = 0; i < 5; i++)     //The fives here should eventually be replaced by gridSize
            {
                if (grid[i, 5] == this)
                {
                    newZ = 0;
                    positive = false;        //Goes in a positive direction
                }
            }
        }
       
        moving = true;
        edgeCase = true;
        halfWay = false;*/
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