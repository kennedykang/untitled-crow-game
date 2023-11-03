using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class grappler : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer lineR;
    public DistanceJoint2D disJ;
    Vector2 mouseP;
    //public Rigidbody2D tm;
    //bool canGrapple;
    //float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        disJ.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (disJ.enabled)
        {
            // Calculate the target point above the mouse position
            Vector2 target = new Vector2(mouseP.x, mouseP.y + 1);

            // Move the gameObject towards the target
            gameObject.transform.position = Vector2.Lerp(transform.position, target, Time.deltaTime);
            
            // Update the line renderer's position
            lineR.SetPosition(1, transform.position);

            // Update the distance of the DistanceJoint2D to retract the grappler
            // This assumes that the grappler 'anchor' is at the gameObject's position
            disJ.distance = Vector2.Distance(disJ.connectedAnchor, transform.position);
        }
    }

    public void OnGrapple(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            mouseP = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            lineR.SetPosition(0, mouseP);
            lineR.SetPosition(1, transform.position);
            disJ.connectedAnchor = mouseP;
            disJ.enabled = true;
            lineR.enabled = true;

            // Initialize the distance to the maximum length you want the grappler to have
            // You might want to store this as a public variable to adjust in the editor
            disJ.distance = Vector2.Distance(disJ.connectedAnchor, transform.position);
        }
        else if (context.canceled)
        {
            disJ.enabled = false;
            lineR.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        if (mouseP.x > gameObject.transform.position.x)
        {
            gameObject.transform.localScale = new Vector2(1, 1);
        }
        else if (mouseP.x < gameObject.transform.position.x)
        {
            gameObject.transform.localScale = new Vector2(-1, 1);
        }
    }
}
