using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maincamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothSpeed = 0.125f; // Adjust this value to change the smoothing speed
    [SerializeField] private Vector3 offset; // Optional: use if you want the camera to be offset from the player's position

    // Update is called once per frame
    void LateUpdate() // Using LateUpdate to ensure the player has moved before the camera updates
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
