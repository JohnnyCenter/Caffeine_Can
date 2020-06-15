using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlatformCheck : MonoBehaviour
{
    GameObject Player; //Names the GameObject "Player"

    private void Start()
    {
        Player = gameObject.transform.parent.gameObject; //Attempts to locate the parent of the GameObject it's attached to and determines it as the "Player" GameObject
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Platform") //If it collides with anything tagged with "Platform it changes the bool "onPLatform" in the playerController script to true
        {
            Player.GetComponent<testVA>().onPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Platform") //When exiting the collider (jumping or falling off) change the bool back to false
        {
            Player.GetComponent<testVA>().onPlatform = false;
        }
    }
}
