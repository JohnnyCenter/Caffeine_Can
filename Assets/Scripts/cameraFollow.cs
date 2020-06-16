using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public playerController thePlayer; //Assignable GameObject with playerController script component on them

    [SerializeField]
    private Parallax Background;

    private Vector3 lastPlayerPosition; //Empty Vector that will determine where the player is
    private float distanceToMoveX, distanceToMoveY; //Empty float that calculates the distance between the camera and player
    [SerializeField]
    private float deadZoneBarrier, camHeight;

    public Camera cam;

    private void Start()
    {
        thePlayer = FindObjectOfType<playerController>(); //Finds the "playerController" component on the assigned gameobject
        lastPlayerPosition = thePlayer.transform.position; //Assigns the Vector to the gameobject to its current position
        cam.orthographicSize = 15;
        transform.position = new Vector3(0, 0, -10);
        deadZoneBarrier = 7.8f;
        camHeight = 13;
    }

    private void FixedUpdate()
    {
        if (thePlayer.transform.position.y > deadZoneBarrier)
        {
            MoveUp();
            Background.Up();
            
        }
        else
        {
            MoveDown();
            Background.Down();
        }
    }

    private void Update()
    {
        if (thePlayer.dead == false) //If the player is alive the camera follows the player
        {
            Follow();

            if (thePlayer.dashActive)
            {
                cam.orthographicSize = 14;
            }
            else
                cam.orthographicSize = 15;
        }

        if (thePlayer.transform.position.y < -10)
        {
            thePlayer.anim.SetInteger("DeathType", 2);
            thePlayer.rb.constraints = RigidbodyConstraints2D.FreezePosition;
            thePlayer.Kill();
        }
    }

    void Follow()
    {
        distanceToMoveX = thePlayer.transform.position.x - lastPlayerPosition.x; //Determines the distance between camerera and player on the x axis

        //distanceToMoveY = thePlayer.transform.position.y - lastPlayerPosition.y;

        transform.position = new Vector3(transform.position.x + distanceToMoveX, transform.position.y + distanceToMoveY, transform.position.z); //Moves the camera to catch up with the player

        lastPlayerPosition = thePlayer.transform.position; //Keeps updating the player's position
    }

    void MoveUp()
    {
        if (transform.position.y < camHeight)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        }
    }

    void MoveDown()
    {
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
        }
    }

    public void Win()
    {
        cam.orthographicSize = 5;
        transform.position = new Vector3(transform.position.x, -4.25f, transform.position.z);
    }
}
