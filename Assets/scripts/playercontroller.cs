using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class playercontroller : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] private float vel;
    private float movementX;
    [SerializeField] private float jumpPower;
    public Transform groundCheck;
    public float groundCheckDistance = 0.1f; // Distance to check for the ground
    public LayerMask groundLayer;
    public BoxCollider2D coll;
    [SerializeField] private AudioSource jumpSoundEffect;
    public GameObject levelComplete;
    public GameObject startLevel;
    public TextMeshProUGUI countText;
    private int count;

    void Start()
    {
        count = 0;
        SetCountText();
        levelComplete.SetActive(false);
    }

    void Update()
    {
        rb.velocity = new Vector2(vel * movementX, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext movement)
    {
        movementX = movement.ReadValue<Vector2>().x;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
    }

    private void FixedUpdate()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        if (inputX > 0)
        {
            gameObject.transform.localScale = new Vector2(1, 1);
        }
        else if (inputX < 0)
        {
            gameObject.transform.localScale = new Vector2(-1, 1);
        }
    }

    private bool isGrounded()
    {
        // Send a raycast directly below the player
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
        return hit.collider != null;
    }
    
    void SetCountText()
    {
        countText.text = "Feathers x" + count.ToString();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("finish")) && count >= 3)
        {
            levelComplete.SetActive(true);
        }
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
        if (other.gameObject.CompareTag("start"))
        {
            other.gameObject.SetActive(false);
            startLevel.SetActive(false);
        }
    }

}
