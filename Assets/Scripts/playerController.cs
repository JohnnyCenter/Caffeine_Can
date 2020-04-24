using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    #region Player Attributes
    [Header("Player Attributes")]
    [SerializeField]
    private float moveSpeed; //Determines how fast the player autoruns
    [SerializeField]
    private float jumpForce; //Determines how high the player jumps
    #endregion

    public swipeControls Swipe;

    #region Dash
    [Header("Dash")]
    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private float dashCooldown;
    #endregion

    private Rigidbody2D rb; //Names the Rigidbody component on the Player "rb"

    #region Bool Checks
    [Header ("Bool Checks")]
    public bool onPlatform; //Bool to check if the Player is touching a platform
    public bool isRunning = false; //Bool to check if the Player is running
    public bool dashActive = false; //Bool to check if the Player is dashing
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Grabs the Rigidbody component from the gameobject (the player)
        Run(); 
    }

    private void Update()
    {
        if (isRunning == true)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y); //Makes the player autorun by adding velocity to the rigidbody
        }

        if (dashActive == true)
        {
            isRunning = false;
            rb.velocity = new Vector2(dashSpeed, rb.velocity.y);
            Invoke("Run", dashCooldown);
        }

        #region Touch Controls
        //Touch Controls (Using Unity Native Touch Controls)
        if (Input.touchCount > 0 && onPlatform == true && isRunning==true)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce); //When touching the screen the Player jumps by adding velocity to rb if he is touching a platform
            }
        }
        #endregion

        #region Mouse and Keyboard
        //Mouse and Keyboard (For testing purposes)
        if (Input.GetKeyDown(KeyCode.Space) && onPlatform == true && isRunning==true)
        {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce); //When pressing the spacebar player jumps if he is touching a platform
        }
        #endregion
    }

    [ContextMenu("Run")]
    void Run()
    {
        isRunning = true; //Sets the bool isRunning true
        dashActive = false;
        rb.constraints = RigidbodyConstraints2D.None; //Unfreezes all 
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; //Freezes the rotation so that the player doesn't rotate while running
    }

    [ContextMenu("Stop")]
    void Stop()
    {
        isRunning = false; //Sets the bool isRunning false
        rb.constraints = RigidbodyConstraints2D.FreezePosition; //Freezes the positon of the Player
    }

    [ContextMenu("Dash")]
    void Dash()
    {
        dashActive = true;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
    }
}
