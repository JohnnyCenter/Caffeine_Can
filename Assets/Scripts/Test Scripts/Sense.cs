using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Sense : MonoBehaviour
{
    public LayerMask checkLayers;
    public float checkRadius;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, checkRadius, checkLayers);
            Array.Sort(colliders, new testInRange(transform));

            foreach (Collider2D item in colliders)
            {
                Debug.Log(item.name);
            }
        }
    }

    void CheckDistance()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, checkRadius, checkLayers);
        Array.Sort(colliders, new testInRange(transform));

        foreach (Collider2D item in colliders)
        {
            Debug.Log(item.name);
        }
    }
}
