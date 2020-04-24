using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testSwipeUse : MonoBehaviour
{
    public testSwipeControls swipeControls;
    private float jumpForce = 10;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (swipeControls.SwipeUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
