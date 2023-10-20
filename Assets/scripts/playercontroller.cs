using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class playercontroller : MonoBehaviour
{

    [SerializeField] Rigidbody2D rb;
    [SerializeField] float vel;
    float inputX;
    Vector2 movementVector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = vel * movementVector;
    }

    void OnMove(InputValue movement)
    {
        movementVector = movement.Get<Vector2>();
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

}
