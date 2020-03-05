using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCamera : MonoBehaviour
{
    public testPlayercontroller thePlayer;

    private Vector3 lastPlayerPosition;
    private float distanceToMove;

    private void Start()
    {
        thePlayer = FindObjectOfType<testPlayercontroller>();
        lastPlayerPosition = thePlayer.transform.position;
    }

    private void Update()
    {
        distanceToMove = thePlayer.transform.position.x - lastPlayerPosition.x;

        transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);

        lastPlayerPosition = thePlayer.transform.position;
    }
}
