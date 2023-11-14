using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomScript : MonoBehaviour
{
    float moveTime;
    float numSteps;
    float newX;
    float newZ;

    bool moving;
    bool edgeCase;
    bool halfWay;
    bool positive;
    float roomLength;
    GameObject [,] grid;
    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.Find("Level").GetComponent<levelScript>().grid;
        roomLength = GameObject.Find("Level").GetComponent<levelScript>().moveTime;

        moveTime = GameObject.Find("Level").GetComponent<levelScript>().moveTime;
        numSteps = moveTime / Time.deltaTime;

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
                    (newX - transform.position.x) / numSteps,     //The change in x-position for each step
                    0,
                    (newZ - transform.position.z) / numSteps);

                if ((newX - transform.position.x) < 0.1 && (newZ - transform.position.z) < 0.1)
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
}
