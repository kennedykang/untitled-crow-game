using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class grappler2 : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer lineR;
    public TargetJoint2D targetJ;
    public Animator move;
    public AudioClip grappleSound;

    private AudioSource audioSource;
    private float lastGrappleTime = 0f;
    private float grappleCooldown = 3f;
    private bool isGrounded;
    private Vector2 mouseP;
    
    private float grappleDuration = 2.5f;
    private float grappleStartTime;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        targetJ.enabled = false;
        lineR.enabled = false;
        move.SetBool("isGrappling", false);
    }

    void Update()
    {
        if (targetJ.enabled)
        {
            lineR.SetPosition(1, transform.position);

            if (Time.time - grappleStartTime >= grappleDuration)
            {
                DisableGrapple();
            }
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (Time.time - lastGrappleTime >= grappleCooldown && isGrounded)
            {
                mouseP = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                lineR.SetPosition(0, mouseP);
                lineR.SetPosition(1, transform.position);
                targetJ.target = mouseP;
                targetJ.enabled = true;
                lineR.enabled = true;

                lastGrappleTime = Time.time;
                grappleStartTime = Time.time;

                audioSource.PlayOneShot(grappleSound);
            }
            move.SetBool("isGrappling", true);
        }
        else if (context.canceled)
        {
            DisableGrapple();
        }
    }

    private void DisableGrapple()
    {
        targetJ.enabled = false;
        lineR.enabled = false;
        move.SetBool("isGrappling", false);
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

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
