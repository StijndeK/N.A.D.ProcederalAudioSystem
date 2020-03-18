using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    bool moveState;

    void Update()
    {
        // check if box is pressed 
        if (Input.GetMouseButtonDown(0))
        {
            // start movestate
            Vector3 cords = transform.position;
            if (Input.mousePosition[0] <= cords[0] + 50 && Input.mousePosition[0] >= cords[0] - 50 && Input.mousePosition[1] <= cords[1] + 50 && Input.mousePosition[1] >= cords[1] - 50)
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
            transform.position = Input.mousePosition;
        }
    }
}
