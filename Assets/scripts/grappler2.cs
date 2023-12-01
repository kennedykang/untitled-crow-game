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
    private bool isGrounded; // Tracks if the player is on the ground

    // Ground check variables
    public Transform groundCheck; // A point at the player's feet to check for ground
    public float groundCheckRadius = 0.2f; // Radius of the overlap circle for ground check
    public LayerMask groundLayer; // Layer that represents the ground

    // Start is called before the first frame update
    void Start()
    {
        targetJ.enabled = false;
        lineR.enabled = false;
        move.SetBool("isGrappling", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (targetJ.enabled)
        {
            lineR.SetPosition(1, transform.position);
        }

        // Check if the groundCheck position is overlapping any colliders on the groundLayer
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Check if enough time has passed since the last grapple and if the player is grounded
            if (Time.time - lastGrappleTime >= grappleCooldown && isGrounded)
            {
                mouseP = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                lineR.SetPosition(0, mouseP);
                lineR.SetPosition(1, transform.position);
                targetJ.target = mouseP;
                targetJ.enabled = true;
                lineR.enabled = true;

                lastGrappleTime = Time.time; // Update the last grapple time
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

    // Optionally, for debugging purposes, you can draw the ground check circle in the Scene view
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
