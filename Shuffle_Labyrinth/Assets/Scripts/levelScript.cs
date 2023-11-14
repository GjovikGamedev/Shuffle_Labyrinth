using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class levelScript : MonoBehaviour
{
    public GameObject[] roomList;
    public float roomLength;
    private GameObject[,] grid;     //A 5 by 5 grid (0-4).

    [SerializeField] private int gridSize;      //The length and height of the grid. How many squares
    

    // Start is called before the first frame update
    void Start()
    {
        grid = new GameObject[gridSize, gridSize];        //Sets the length of the grid

        int numRooms = roomList.Length;               //The number of unique rooms
        int whichRoom = 0;                            //Used to cycle through rooms in the following loop
        for (int i = 0; i < gridSize; i++)
        {
            for(int j = 0; j < gridSize; j++) {
                grid[j, i] = Instantiate(roomList[0], new Vector3(0, 0, 0), Quaternion.identity);
            }
        }
        //grid[0, 0] = Instantiate(roomList[0], new Vector3(0, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
