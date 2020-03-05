using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformCheck : MonoBehaviour
{
    GameObject Player;

    private void Start()
    {
        Player = gameObject.transform.parent.gameObject;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Platform")
        {
            Player.GetComponent<testPlayercontroller>().onPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "Platform")
        {
            Player.GetComponent < testPlayercontroller>().onPlatform = false;
        }
    }
}
