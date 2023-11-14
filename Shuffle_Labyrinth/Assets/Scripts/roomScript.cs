using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomScript : MonoBehaviour
{
    float moveTime;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject theLevel = level;
        moveTime = GameObject.Find("Level").GetComponent<levelScript>().moveTime;
        Debug.Log(moveTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Moves this room from it's current position to the new position described by the 
    void Move(float newX, float newY)
    {
        //for (int i = 0; i < )
    }

    //Different function for the edge cases (when a room needs to get to the other side of the grid)
    void Move(bool axis)        //axis describes which axis the moved row/column is on. true = x-axis
    {

    }
}
