using System.Collections;
using System;
using UnityEngine;
using System.Collections.Generic;

public class testInRange : IComparer
{
    private Transform compareTransform;

    public testInRange(Transform compTransform)
    {
        compareTransform = compTransform;
    }

    public int Compare(object x, object y)
    {
        Collider2D xCollider2D = x as Collider2D;
        Collider2D yCollider2D = y as Collider2D;

        Vector3 offset = xCollider2D.transform.position - compareTransform.position;
        float xDistance = offset.sqrMagnitude;

        offset = yCollider2D.transform.position - compareTransform.position;
        float yDistance = offset.sqrMagnitude;

        return xDistance.CompareTo(yDistance);
    }
}
