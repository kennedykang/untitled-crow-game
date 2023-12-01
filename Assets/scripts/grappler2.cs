using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class grappler2 : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer lineR;
    public TargetJoint2D targetJ;
    Vector2 mouseP;
    public Animator move;

    private float lastGrappleTime = 0f;
    private float grappleCooldown = 3f; // 3 seconds cooldown

    // Start is called before the first frame update
    void Start()
    {
        targetJ.enabled = false;
        move.SetBool("isGrappling", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (targetJ.enabled)
        {
            //Vector2 target = new Vector2(mouseP.x, mouseP.y + 1);
            //player.transform.position = Vector2.Lerp(player.transform.position, , Time.deltaTime);
            //Vector3 offset = new Vector3(0.25f, 0f);
            lineR.SetPosition(1, transform.position);
            //disJ.distance = Vector2.Distance(disJ.connectedAnchor, player.transform.position);
        }
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Check if enough time has passed since the last grapple
            if (Time.time - lastGrappleTime >= grappleCooldown)
            {
                mouseP = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                lineR.SetPosition(0, mouseP);
                lineR.SetPosition(1, transform.position);
                targetJ.target = mouseP;
                targetJ.enabled = true;
                lineR.enabled = true;
                //disJ.distance = Vector2.Distance(disJ.connectedAnchor, player.transform.position);

                // Update the last grapple time
                lastGrappleTime = Time.time;
            }
            move.SetBool("isGrappling", true);
        }
        else if (context.canceled)
        {
            targetJ.enabled = false;
            lineR.enabled = false;
            move.SetBool("isGrappling", false);
        }
    }

    private void FixedUpdate()
    {
       if (targetJ.enabled)
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
}
