using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testVA : MonoBehaviour
{
    public testVAtrigger trigger;
    public bool targetFound = false;

    private void Update()
    {
        if (targetFound)
            Debug.Log("Works");
    }
}
