using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testSwipeControls : MonoBehaviour
{
    private bool swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDraging = false;
    private Vector2 startTouch, swipeDelta;

    private void Update()
    {
        swipeLeft = swipeRight = swipeUp = swipeDown = false;

        #region Mobile Input
        if (Input.touches.Length > 0)
        {
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                isDraging = true;
                startTouch = Input.touches[0].position;
            }
            else if(Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDraging = false;
                Reset();
            }
        }
        #endregion

        //Calculate the Distance
        swipeDelta = Vector2.zero;
        if (isDraging)
        {
            if (Input.touches.Length > 0)
                swipeDelta = Input.touches[0].position - startTouch;
        }

        //Did we cross the Deadzone?
        if (swipeDelta.magnitude > 50)
        {
            //Which Direction?
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //Left or Right
                if (x < 0)
                {
                    swipeLeft = true;
                    Debug.Log("Swiped left");
                }
                else
                {
                    swipeRight = true;
                    Debug.Log("Swiped right");
                }
            }
            else
            {
                //Up or Down
                if (y < 0)
                {
                    swipeDown = true;
                    Debug.Log("Swiped down");
                }
                else
                {
                    swipeUp = true;
                    Debug.Log("Swiped up");
                }
            }
            
            Reset();
        }
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }

    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }


}