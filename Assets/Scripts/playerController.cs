﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    #region Player Attributes
    [Header("Player Attributes")]
    [SerializeField]
    private float moveSpeed = 10; //Determines how fast the player autoruns
    private float jumpForce; //Determines how high the player jumps
    [SerializeField]
    private float rollTime = 1;
    private bool rollCool;
    [SerializeField]
    private float fallMultiplier = 2.5f;
    private Vector2 spawnPos;
    [SerializeField]
    private crashHandler cH;
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

    public Rigidbody2D rb; //Names the Rigidbody component on the Player "rb"
    private BoxCollider2D bc;
    public Animator anim;

    #region Bool Checks
    [Header("Bool Checks")]
    public bool onPlatform; //Bool to check if the Player is touching a platform
    public bool isRunning = false; //Bool to check if the Player is running
    public bool dashActive = false; //Bool to check if the Player is dashing
    public bool dead = false;
    public bool invulnerable = false;
    public bool inRange = false;
    public bool attack = false;
    #endregion

    public GameObject Point;
    private Vector3 attackPos;

    Vector2 kbDir = new Vector2(-10, 2);

    [SerializeField]
    private GameObject music;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); //Grabs the Rigidbody component from the gameobject (the player)
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        spawnPos = new Vector3(-5, -5, 0);
        transform.position = spawnPos;
        jumpForce = CalculateJumpForce(Physics2D.gravity.magnitude, 30f);
        music.gameObject.SetActive(false);
        StartCoroutine(Go());
    }

    private void FixedUpdate()
    {
        if (isRunning)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y); //Makes the player autorun by adding velocity to the rigidbody
            uiController.DashEffect.SetInteger("Dash Effect", 0);
            uiController.dashEffect.enabled = false;
        }

        if (dashActive)
        {
            rb.velocity = new Vector2(dashSpeed, rb.velocity.y); //Changes from "moveSpeed" to "dashSpeed"
            uiController.DashEffect.SetInteger("Dash Effect", 1);
            uiController.dashEffect.enabled = true;
        }

        if (attack)
        {
            rb.velocity = new Vector2(5, 15);
        }

        if (rb.velocity.y < 0)
        {
            if (rollCool)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * ((fallMultiplier - 1) * 5) * Time.deltaTime;
                anim.SetBool("Fall", false); 
            }
            else
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
                anim.SetBool("Fall", true);
            }
        }
        else
            anim.SetBool("Fall", false);

        if (rb.velocity.y > 0.1f)
            anim.SetBool("Leap", true);
        else
            anim.SetBool("Leap", false);
    }

    private void Update()
    {
        #region Touch Controls
        //Touch Controls (Using Unity Native Touch Controls)
        if (isRunning)
        {
            if (Input.touchCount > 0) //If the screen is touched while the Player is running
            {
                if (Input.GetTouch(0).phase == TouchPhase.Ended && Swipe.Swiped == false) //If it is a tap we move on (Not a swipe or a hold)
                {
                    if (onPlatform) //If we're on the ground we jump
                        Jump();
                    else
                    {
                        StartCoroutine(VineAttack()); //If we're in the air we perform a Vine Attack
                    }
                }

                if (Swipe.SwipeDown) //If we swipe down we perform a roll
                {
                    StartCoroutine(Roll());
                }

                if (Swipe.SwipeRight && uiController.dashCount > 0) //If we swipe right we perform a dash
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
        anim.SetTrigger("Run");
        isRunning = true; //Sets the bool isRunning true
        if (!dashActive)
        {
            rb.constraints = RigidbodyConstraints2D.None; //Unfreezes all 
            rb.constraints = RigidbodyConstraints2D.FreezeRotation; //Freezes the rotation so that the player doesn't rotate while running
        }
        else
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
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
    IEnumerator VineAttack()
    {
        if (inRange)
        {
            GameObject target = GameObject.FindWithTag("Active Enemy");
            RaycastHit2D collCheck = Physics2D.Linecast(Point.transform.position, target.transform.position);
            anim.SetBool("Attack", true);
            anim.SetInteger("AttackState", 1);

            if (collCheck.collider != null)
            {
                if (collCheck.collider.tag == "Active Enemy")
                {
                    invulnerable = true;
                    isRunning = false;
                    attackPos = target.transform.position;

                    Vector2 currentPos = transform.position;
                    var t = 0f;
                    float timeToMove = 0.25f;

                    while (t < 1)
                    {
                        t += Time.deltaTime / timeToMove;
                        transform.position = Vector2.Lerp(currentPos, attackPos, t);
                        yield return null;
                    }
                    anim.SetInteger("AttackState", 2);
                    attack = true;
                    isRunning = true;
                    yield return new WaitForSeconds(0.25f);
                    invulnerable = false;
                    attack = false;
                    anim.SetBool("Attack", false);
                }
                else
                    anim.SetBool("Attack", false);
            }
        }
        yield return null;
    }
    #endregion

    #region Roll Function
    IEnumerator Roll()
    {
        invulnerable = true;
        cH.Shrink();
        bc.size = new Vector2(1, 0.35f);
        anim.SetBool("Roll", true);
        yield return new WaitForSeconds(0.2f);
        bc.offset = new Vector2(0, -0.17f);
        yield return new WaitForSeconds(rollTime);
        //Cast raycast above your head and don't go out of a roll until raycast is clear
        anim.SetBool("Roll", false);
        cH.Normal();
        bc.offset = new Vector2(0, 0.08f);
        bc.size = new Vector2(1, 0.85f);
        Debug.Log("Finished");
        if(!dashActive)
            invulnerable = false;
    }
    #endregion

    #region Dash Function
    IEnumerator Dash()
    {
        invulnerable = true;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
        dashActive = true; //Enables dash
        isRunning = false; //Disables running so that we can't perform other moves while dashing (jumping, rolling, vine attacking)
        anim.SetBool("Dash", true);
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation; //We freeze the y position so that we remain in the air while dashing
        uiController.DashUse();
        bc.offset = new Vector2(0, 0.08f);
        bc.size = new Vector2(1, 0.85f);
        cH.Normal();
        yield return new WaitForSeconds(dashTime); //Determines how long the dash last based on the "dashTime" that we determine in the inspector
        dashActive = false; //Disables the dash
        invulnerable = false;
        anim.SetBool("Dash", false);
        Run(); //After the dash is finished we begin the "Run" function
    }
    #endregion

    [ContextMenu("Kill")]
    public void Kill()
    {
        isRunning = false;
        dashActive = false;
        anim.SetBool("Death", true);
        dead = true;
        uiController.retryButton.gameObject.SetActive(true);
        music.SetActive(false);
    }

    [ContextMenu("Crash")]
    public IEnumerator Knockback()
    {
        isRunning = false;
        anim.SetBool("Knockback", true);
        anim.SetTrigger("Knocked");
        if (onPlatform)
            rb.AddForce(kbDir * 300);
        else
        {
            rb.AddForce(kbDir * 200);
        }
        yield return new WaitForSeconds(1.5f);
        if (!dead)
        {
           anim.SetBool("Knockback", false);
           Run();
        }
    }

    IEnumerator Go()
    {
        music.SetActive(true);
        uiController.UiElements.gameObject.SetActive(true);
        uiController.Countdown.SetTrigger("Go");
        uiController.DashEffect.SetInteger("Dash Effect", 1);
        yield return new WaitForSeconds(3.5f);
        anim.SetTrigger("Run");
        Run();
    }
}
