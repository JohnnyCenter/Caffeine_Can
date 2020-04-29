using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public playerController thePlayer; //Assignable GameObject with playerController script component on them

    private Vector3 lastPlayerPosition; //Empty Vector that will determine where the player is
    private float distanceToMove; //Empty float that calculates the distance between the camera and player

    private void Start()
    {
        thePlayer = FindObjectOfType<playerController>(); //Finds the "playerController" component on the assigned gameobject
        lastPlayerPosition = thePlayer.transform.position; //Assigns the Vector to the gameobject to its current position
    }

    private void Update()
    {
        if (thePlayer.dead == false)
            Follow();
    }

    void Follow()
    {
        distanceToMove = thePlayer.transform.position.x - lastPlayerPosition.x; //Determines the distance between camerera and player on the x axis

        transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z); //Moves the camera to catch up with the player

        lastPlayerPosition = thePlayer.transform.position; //Keeps updating the player's position
    }
}
