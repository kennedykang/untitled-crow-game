using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class playercontroller : MonoBehaviour
{

    public Rigidbody2D rb;
    [SerializeField] float vel;
    float inputX;
    float movementX;
    [SerializeField] float jumpPower;
    //bool isJumping = false;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public BoxCollider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(vel * movementX, rb.velocity.y);

        //rb.velocity = Vector2.up * jumpPower;
        
        
    }

    public void OnMove(InputAction.CallbackContext movement)
    {
        movementX = movement.ReadValue<Vector2>().x;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

    }

    private void FixedUpdate()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        if (inputX > 0)
        {
            gameObject.transform.localScale = new Vector2(1, 1);
        }
        if (inputX < 0)
        {
            gameObject.transform.localScale = new Vector2(-1, 1);
        }

    }
    bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, groundLayer);
    }

}