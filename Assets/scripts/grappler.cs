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
        //canGrapple = disJ.connectedBody.CompareTag("ground");
        if (disJ.enabled)
        {
            //float step = speed * Time.deltaTime;
            Vector2 target = new Vector2(mouseP.x, mouseP.y + 1);
            gameObject.transform.position = Vector2.Lerp(transform.position, target, Time.deltaTime);
            //gameObject.transform.position = new Vector2(mouseP.x, mouseP.y);
        }
    }

    public void OnGrapple(InputAction.CallbackContext context)
    {
        
        if (context.performed)
        {
            mouseP = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            lineR.SetPosition(0, mouseP);
            lineR.SetPosition(1, transform.position);
            disJ.connectedAnchor = mouseP;
            disJ.enabled = true;
            lineR.enabled = true;
            if (mouseP.x < gameObject.transform.position.x)
            {
                gameObject.transform.localScale = new Vector2(-1, 1);
            }
            if (mouseP.x > gameObject.transform.position.x)
            {
                gameObject.transform.localScale = new Vector2(1, 1);
            }
        }
        else if (context.canceled)
        {
            disJ.enabled = false;
            lineR.enabled = false;
        }
        if (disJ.enabled)
        {
            lineR.SetPosition(1, transform.position);
        }
    }
}
