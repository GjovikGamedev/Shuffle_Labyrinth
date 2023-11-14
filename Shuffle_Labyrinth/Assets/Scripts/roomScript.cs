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
    // Start is called before the first frame update
    void Start()
    {
        //GameObject theLevel = level;
        moveTime = GameObject.Find("Level").GetComponent<levelScript>().moveTime;
        numSteps = moveTime / Time.deltaTime;
        moving = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (moving)
        {
            transform.position += new Vector3(              //Changes the position
                (newX - transform.position.x) / numSteps,     //The change in x-position for each step
                0,
                (newZ - transform.position.z) / numSteps);

            if ((newX - transform.position.x) < 0.1 && (newZ - transform.position.z) < 0.1)
            {
                transform.position = new Vector3 (newX, 0, newZ);
                moving = false;
            }
        }

    }

    //Moves this room from it's current position to the new position described by the 
    public void Move(float veryNewX, float veryNewZ)
    {
        /*float numSteps = moveTime/Time.fixedDeltaTime;           //How many steps the room is gonna take in order to be done by moveTime
        for (int i = 0; i < numSteps; i++)   //Should I be using fixed deltatime for numSteps?
        {
            Debug.Log("Yay");
            transform.position += new Vector3(              //Changes the position
                (newX-transform.position.x) / numSteps,     //The change in x-position for each step
                0,
                (newZ-transform.position.z) / numSteps);    //The change in z-position for each step
        }

        Debug.Log(numSteps);
        transform.position = new Vector3(newX, 0, newZ);    //Moves it into place properly at the end. This is more of a
                                                            //precaution, in case some rounding errors or something happened in the loop
        */
        newX = veryNewX;
        newZ = veryNewZ;
        moving = true;
    }

    //Different function for the edge cases (when a room needs to get to the other side of the grid)
    public void Move(bool axis)        //axis describes which axis the moved row/column is on. true = x-axis
    {

    }
}
