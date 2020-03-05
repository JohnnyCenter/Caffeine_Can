using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayercontroller : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    private Rigidbody2D myRigidbody;

    private BoxCollider2D myCollider;

    public bool onPlatform = false;

    public bool isSliding = false;
    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
       
    }

    private void Update()
    {
        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);

        if(Input.touchCount > 0)
        {
            if(Input.GetTouch (0).phase == TouchPhase.Began)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
            }
        }

        //Mouse and Keyboard Controller for testing purposes
        if (Input.GetKeyDown(KeyCode.Space) && onPlatform == true)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && onPlatform == true)
        {
            transform.localScale = new Vector3(0.5f, 0.4f, 1);
        }
    }
}
