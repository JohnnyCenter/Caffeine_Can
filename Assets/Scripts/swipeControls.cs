using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swipeControls : MonoBehaviour
{
    private bool swipeLeft, swipeRight, swipeUp, swipeDown, swiped;  //A bool for the four possible swipe directions
    [SerializeField]
    private float tapTime; //Differentiates between a tap and a swipe
    private bool tap = false;
    private bool touching = false; //This bool detects wether or not we're touching the screen
    private Vector2 startPos, swipeDelta; //"startPos" vector is the position of the intial touch while "swipeDelta" is the current touch position

    #region Public Variables 
    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }
    public bool Tap { get { return tap; } }

    public bool Swiped { get { return swiped; } }
    #endregion

    private void Update()
    {
        swipeLeft = swipeRight = swipeUp = swipeDown = false; //Keeps everything off for each frame so that we don't get multiple swipes

        #region Input
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                touching = true; //Enables the bool "touching" when a touch happens on screen
                startPos = Input.touches[0].position; //Makes "startPos" the position of the initial touch
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                touching = false; //Disables the bool "touching" when a finger leaves the screen
                Reset(); //Runs our reset function so that we're ready for another touch or swipe
            }
        }
        #endregion

        #region Swipe Delta
        swipeDelta = Vector2.zero; //Spawns swipeDelta
        if (touching)
        {
            if (Input.touches.Length > 0)
                swipeDelta = Input.touches[0].position - startPos; //If we're begining to drag our finger swipeDelta becomes the position between "startPos" and current touch
        }
        #endregion

        #region Deadzone
        if (swipeDelta.magnitude > 50) //Creates a deadzone. The Integer determines the size of our deadzone. The deadzone determines how far we need to drag our finger for a swipe to register
        {
            float x = swipeDelta.x; //Grabs the x position of swipeDelta and names it x
            float y = swipeDelta.y; //Grabs the y position of swipeDelta and names it y
            if (Mathf.Abs(x) > Mathf.Abs(y)) //We use Mathf absolute so that it includes positive and negative numbers. If x is larger than y then it's either a Left or a Right swipe
            {
                if (x < 0)
                {
                    swipeLeft = true; //We swiped Left
                }
                else
                {
                    swipeRight = true; //We swiped Right
                }
            }
            else //If y is bigger than x then it's either a Up or Down swipe
            {
                if (y < 0)
                {
                    swipeDown = true; //We swiped Down
                    swiped = true;
                    StartCoroutine(Stop());
                }
                else
                {
                    swipeUp = true; //We swiped Up
                }
            }

            Reset(); //After we've done a swipe we reset everything when the finger leaves the screen
        }
        #endregion
    }

    private void Reset() //Our reset function
    {
        startPos = swipeDelta = Vector2.zero;
        touching = false;
    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(0.5f);
        swiped = false;
    }
}
