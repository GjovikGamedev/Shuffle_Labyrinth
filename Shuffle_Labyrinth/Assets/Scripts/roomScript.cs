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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Moves this room from it's current position to the new position described by the 
    public void Move(float newX, float newZ)
    {
        float numSteps = moveTime/Time.deltaTime;           //How many steps the room is gonna take in order to be done by moveTime
        for (int i = 0; i < numSteps; i++)   //Should I be using fixed deltatime for numSteps?
        {
            transform.position += new Vector3(              //Changes the position
                (newX-transform.position.x) / numSteps,     //The change in x-position for each step
                0,
                (newZ-transform.position.z) / numSteps);    //The change in z-position for each step
        }

        transform.position = new Vector3(newX, 0, newZ);    //Moves it into place properly at the end. This is more of a
                                                            //precaution, in case some rounding errors or something happened in the loop
    }

    //Different function for the edge cases (when a room needs to get to the other side of the grid)
    public void Move(bool axis)        //axis describes which axis the moved row/column is on. true = x-axis
    {

    }
}
