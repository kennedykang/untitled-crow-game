using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crowwalking : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    [SerializeField] float vel;
    void Start()
    {
        Vector2 velo = new Vector2(vel, 0);
        rb.velocity = velo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 flip = new Vector2(-1, 0);
        if (transform.position.x < -3.25)
        {
            gameObject.transform.localScale = new Vector2(-1, 1);
            rb.velocity = rb.velocity * flip;
        }
        else if (transform.position.x > 6)
        {
            gameObject.transform.localScale = new Vector2(1, 1);
            rb.velocity = rb.velocity * flip;
        }
    }
}
