using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testSwipe : MonoBehaviour
{
    public float moveSpeed = 5;
    public float jumpForce = 10;

    public float swipeLength;
    private Vector2 startPos;
    private Vector2 endPos;
    private Vector2 currentPos;
    private Vector2 lastPos;
    private Vector2 swipeDelta;
    public bool Swiped;
    public bool left, right, up, down = false;
    [SerializeField]
    private float deadzone = 50.0f;
    private float sqrDeadzone;

    private Rigidbody2D myRigidbody;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        Swiped = false;
        sqrDeadzone = deadzone * deadzone;
    }

    private void Update()
    {
        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);

        SwipeCheck();
    }

    private void FixedUpdate()
    {
        //SwipeDown();
        DownTest();
    }

    void SwipeCheck()
    {
        currentPos = startPos = Vector2.zero;

        if (Input.touchCount > (0))
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startPos = Input.GetTouch(0).position;
                Debug.Log("StartPos is" + startPos);
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                currentPos = Input.GetTouch(0).position;
                Debug.Log("CurrentPos is" + currentPos);
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)
            {
                startPos = Vector2.zero;
                Debug.Log("StartPos reset to" + startPos);
                currentPos = Vector2.zero;
                Debug.Log("CurrentPos reset to" + currentPos);
                Swiped = false;
                Debug.Log("Swipe Canceled");
                swipeDelta = Vector2.zero;
            }
        }
        swipeDelta = Vector2.zero;

        if (startPos != Vector2.zero && Input.touches.Length != 0)
        {
            swipeDelta = Input.GetTouch(0).position - startPos;
        }
       if (swipeDelta.sqrMagnitude > sqrDeadzone)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0)
                {
                    left = true;
                    Debug.Log("Swiped left");
                }
                else
                {
                    right = true;
                    Debug.Log("Swiped right");
                }
            }
            else
            {
                if (y < 0)
                {
                    down = true;
                    Debug.Log("Swiped down");
                }
                else
                {
                    up = true;
                    Debug.Log("Swiped up");
                }
            }
            startPos = swipeDelta = Vector2.zero;
        }
        
        
        /*if (startPos.y - currentPos.y > swipeLength)
        {
            Swiped = true;
        }*/
    }

    void SwipeDown()
    {
        if (Swiped)
        {
            Debug.Log("Swipe Detected");
            Swiped = false;
            Debug.Log("Swipe finished");
        }
    }
    
    void DownTest()
    {
        if (down)
        {
            Debug.Log("This works!!");
        }
    }
}
