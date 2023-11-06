using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falloutscript : MonoBehaviour
{
    private Vector3 resetPosition = new Vector3(-7.577f, -1.501f, 0.03658727f);

    private int fallCount = 0;

    // Update is called once per frame
    void Update()
    {
        // Check if the player's y position is less than -10
        if(transform.position.y < -10)
        {
            // Increment the fall counter
            fallCount++;

            // Reset the player's position to the specified coordinates
            transform.position = resetPosition;
            // For debugging purposes
            Debug.Log("Fall Count: " + fallCount);
        }
    }
}
