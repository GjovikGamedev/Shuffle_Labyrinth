using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class levelScript : MonoBehaviour
{
    public GameObject[] roomList;
    private GameObject[,] grid;     //A 5 by 5 grid (0-4).

    [SerializeField] private int size;      //The length and height of the grid

    // Start is called before the first frame update
    void Start()
    {
        grid = new GameObject[size-1, size-1];        //Sets the length of the grid
        grid[0, 0] = Instantiate(roomList[0], new Vector3(0, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
