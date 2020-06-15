using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testVA : MonoBehaviour
{
    #region Player Attributes
    [Header("Player Attributes")]
    [SerializeField]
    private float moveSpeed; //Determines how fast the player autoruns
    private float jumpForce; //Determines how high the player jumps
    [SerializeField]
    private float rollTime;
    private bool rollCool;
    [SerializeField]
    private float fallMultiplier;
    private Vector2 spawnPos;
    #endregion

    public swipeControls Swipe;

    #region Dash
    [Header("Dash")]
    [SerializeField]
    private float dashSpeed; //How fast the Player moves while dashing
    [SerializeField]
    private float dashTime; //How long the Player dashes
    #endregion

    private Rigidbody2D rb; //Names the Rigidbody component on the Player "rb"
    private BoxCollider2D bc;

    #region Bool Checks
    [Header("Bool Checks")]
    public bool onPlatform; //Bool to check if the Player is touching a platform
    public bool isRunning = false; //Bool to check if the Player is running
    public bool dashActive = false; //Bool to check if the Player is dashing
    public bool dead = false;
    public bool invulnerable = false;
    public bool inRange = false;
    #endregion

    RaycastHit2D enemyHit;
    [SerializeField]


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); //Grabs the Rigidbody component from the gameobject (the player)
        bc = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        spawnPos = new Vector3(-5, -5, 0);
        transform.position = spawnPos;
        jumpForce = CalculateJumpForce(Physics2D.gravity.magnitude, 25f);
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

        if (rb.velocity.y < 0)
        {
            if (rollCool)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * ((fallMultiplier - 1) * 5) * Time.deltaTime;
            }
            else
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
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
                if (Input.GetTouch(0).phase == TouchPhase.Ended && Swipe.Swiped == false && rollCool == false) //If it is a tap we move on (Not a swipe or a hold)
                {
                    if (onPlatform) //If we're on the ground we jump
                        Jump();
                    else
                        vineAttack(); //If we're in the air we perform a Vine Attack
                }

                if (Swipe.SwipeDown) //If we swipe down we perform a roll
                {
                    StartCoroutine(Roll());
                }

                if (Swipe.SwipeRight) //If we swipe right we perform a dash
                {
                    StartCoroutine(Dash());
                }
            }
        }
        #endregion
    }

    [ContextMenu("Run")]
    void Run()
    {
        isRunning = true; //Sets the bool isRunning true
        rb.constraints = RigidbodyConstraints2D.None; //Unfreezes all 
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; //Freezes the rotation so that the player doesn't rotate while running
    }

    [ContextMenu("Stop")]
    public void Stop()
    {
        dead = true;
        isRunning = false; //Sets the bool isRunning false
        rb.constraints = RigidbodyConstraints2D.FreezePosition; //Freezes the positon of the Player
        StopAllCoroutines();
    }

    #region Jump Function
    [ContextMenu("Jump")]
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public static float CalculateJumpForce(float gravityStrength, float jumpHeight)
    {
        return Mathf.Sqrt(2 * gravityStrength * jumpHeight);
    }
    #endregion

    #region Vine Attack Function
    void vineAttack()
    {
        //Vector3 fwd = 

        if (inRange)
        {

        }
    }
    #endregion

    #region Roll Function
    IEnumerator Roll()
    {
        invulnerable = true;
        rollCool = true;
        bc.size = new Vector2(1, 0.35f);
        yield return new WaitForSeconds(0.2f);
        bc.offset = new Vector2(0, -0.17f);
        rollCool = false;
        yield return new WaitForSeconds(rollTime);
        //Cast raycast above your head and don't go out of a roll until raycast is clear
        bc.offset = new Vector2(0, 0.08f);
        bc.size = new Vector2(1, 0.85f);
        invulnerable = false;
    }
    #endregion

    #region Dash Function
    IEnumerator Dash()
    {
        StopCoroutine(Roll());
        invulnerable = true;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
        dashActive = true; //Enables dash
        isRunning = false; //Disables running so that we can't perform other moves while dashing (jumping, rolling, vine attacking)
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation; //We freeze the y position so that we remain in the air while dashing
        yield return new WaitForSeconds(dashTime); //Determines how long the dash last based on the "dashTime" that we determine in the inspector
        dashActive = false; //Disables the dash
        invulnerable = false;
        Run(); //After the dash is finished we begin the "Run" function
    }
    #endregion

    [ContextMenu("Kill")]
    public void Kill()
    {
        isRunning = false;
        dashActive = false;
        //Play death animation
        dead = true;
    }

    [ContextMenu("Crash")]
    public IEnumerator Knockback()
    {
        //StopAllCoroutines();
        isRunning = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.position = new Vector3(transform.position.x - 4f, transform.position.y, transform.position.z);
        yield return new WaitForSeconds(0.5f);
        transform.position = new Vector3(transform.position.x - 3.5f, transform.position.y, transform.position.z);
        yield return new WaitForSeconds(0.5f);
        Run();
    }
}
