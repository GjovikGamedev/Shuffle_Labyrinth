using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelScript : MonoBehaviour
{
    public GameObject[] roomList;
    public GameObject[][] grid;     //A 5 by 5 grid (0-4).

    // Start is called before the first frame update
    void Start()
    {
        //grid[0][0] = 
        Debug.Log(GameObject.Find("Room_5"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
