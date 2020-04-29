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
    private float dashSpeed; //How fast the Player moves while dashing
    [SerializeField]
    private float dashTime; //How long the Player dashes
    [SerializeField]
    private UIController uiController;
    #endregion

    private Rigidbody2D rb; //Names the Rigidbody component on the Player "rb"

    #region Bool Checks
    [Header("Bool Checks")]
    public bool onPlatform; //Bool to check if the Player is touching a platform
    public bool isRunning = false; //Bool to check if the Player is running
    public bool dashActive = false; //Bool to check if the Player is dashing
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Grabs the Rigidbody component from the gameobject (the player)
        Run();
    }

    private void FixedUpdate()
    {
        if (isRunning)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y); //Makes the player autorun by adding velocity to the rigidbody
        }

        if (dashActive)
        {
            rb.velocity = new Vector2(dashSpeed, rb.velocity.y); //Changes from "moveSpeed" to "dashSpeed"
        }
    }

    private void Update()
    {
        #region Touch Controls
        //Touch Controls (Using Unity Native Touch Controls)
        if (isRunning)
        {
            if (Input.touchCount > 0) //If the screen is touched while the Player is running
            {
                if (Input.GetTouch(0).phase == TouchPhase.Ended && Swipe.Tap) //If it is a tap we move on (Not a swipe or a hold)
                {
                    if (onPlatform) //If we're on the ground we jump
                        Jump();
                    else
                        vineAttack(); //If we're in the air we perform a Vine Attack
                }

                if (Swipe.SwipeDown) //If we swipe down we perform a roll
                {
                    Roll();
                }

                if (Swipe.SwipeRight && uiController.dashCount > 0) //If we swipe right we perform a dash
                {
                    StartCoroutine(Dash());
                }
            }
            #endregion
        }
    }

    [ContextMenu("Run")]
    void Run()
    {
        isRunning = true; //Sets the bool isRunning true
        rb.constraints = RigidbodyConstraints2D.None; //Unfreezes all 
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; //Freezes the rotation so that the player doesn't rotate while running
    }

    [ContextMenu("Stop")]
    void Stop()
    {
        isRunning = false; //Sets the bool isRunning false
        rb.constraints = RigidbodyConstraints2D.FreezePosition; //Freezes the positon of the Player
    }

    #region Jump Function
    [ContextMenu("Jump")]
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    #endregion

    #region Vine Attack Function
    void vineAttack()
    {
        Debug.Log("Attacked");
    }
    #endregion

    #region Roll Function
    [ContextMenu("Roll")]
    void Roll()
    {
        //isRolling = true;
        Debug.Log("Rolled");
    }
    #endregion

    #region Dash Function
    IEnumerator Dash()
    {
        dashActive = true; //Enables dash
        isRunning = false; //Disables running so that we can't perform other moves while dashing (jumping, rolling, vine attacking)
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation; //We freeze the y position so that we remain in the air while dashing
        uiController.DashUse();
        yield return new WaitForSeconds(dashTime); //Determines how long the dash last based on the "dashTime" that we determine in the inspector
        dashActive = false; //Disables the dash
        Run(); //After the dash is finished we begin the "Run" function
    }
    #endregion
}
