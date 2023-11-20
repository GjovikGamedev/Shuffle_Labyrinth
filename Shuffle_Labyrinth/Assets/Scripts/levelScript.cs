using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class levelScript : MonoBehaviour
{
    public GameObject[] roomList;
    public float roomLength;
    public float moveTime;          //How many seconds the shuffling is supposed to take
    public float speed;

    public GameObject[,] grid;     //A 5 by 5 grid (0-4).
    public int gridSize;      //The length and height of the grid. How many squares
    

    // Start is called before the first frame update
    void Start()
    {
        grid = new GameObject[gridSize, gridSize];        //Sets the length of the grid

        int numRooms = roomList.Length;               //The number of unique rooms
        int roomType = 0;                            //Used to cycle through rooms in the following loop
        for (int i = 0; i < gridSize; i++)
        {
            for(int j = 0; j < gridSize; j++) {
                //Should probably append the instantiated objects to the level object sometime
                grid[j, i] = Instantiate(roomList[roomType], new Vector3(0, 0, 0), Quaternion.identity);

                grid[j, i].transform.Rotate(-90, 0, 0);         //Rotates the rooms to fit in with the top-down perspective. The prefabs are actually already rotated, but for some reason it doesn't apply to these instantiated ones
                //Places the rooms evenly in the grid
                grid[j, i].transform.position = new Vector3(j*roomLength, 0, i*roomLength);

                if (++roomType >= numRooms)          //Goes to the next room in the roomList. If outside roomlist, then go to the beginning of roomlist
                {
                    roomType = 0;
                }
            }
        }

        //yield return new WaitForSeconds(.3f); 

        //grid[0, 0].GetComponent<roomScript>().Move(1*roomLength, 0);
        //grid[4, 0].GetComponent<roomScript>().Move(5 * roomLength, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            grid[0, 0].GetComponent<roomScript>().Move(false);
            //grid[4, 0].GetComponent<roomScript>().Move(true);
            //grid[0, 4].GetComponent<roomScript>().Move(false);
            //grid[4, 4].GetComponent<roomScript>().Move(true);

            //grid[3, 1].GetComponent<roomScript>().Move(roomLength*3, roomLength*2);
        }
    }
}
