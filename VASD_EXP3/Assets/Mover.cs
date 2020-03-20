using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    bool moveState;

    /* TODO:
         * relative scaling
         * colliders
    */

    void Update()
    {
        // check if box is pressed 
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 cords = transform.position;
            cords[0] = cords[0] + 75;

            // start movestate
            if (Input.mousePosition[0] <= cords[0] + 50 && Input.mousePosition[0] >= cords[0] - 50 && Input.mousePosition[1] <= cords[1] + 25 && Input.mousePosition[1] >= cords[1] - 25)
            {
                moveState = true;
            }
        }
        // stop movestate when mouse is released
        if (Input.GetMouseButtonUp(0))
        {
            moveState = false;
        }
        // set the box position to mouse position during movestate
        if (moveState == true)
        {
            Vector3 mouseCords = Input.mousePosition;
            mouseCords[0] = mouseCords[0] - 85; 
            transform.position = mouseCords;
        }
    }
}
