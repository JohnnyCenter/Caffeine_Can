using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inRange : MonoBehaviour
{
    GameObject Player;


    private List<Collider> colliders = new List<Collider>();
    public List<Collider> GetColliders() { return colliders; }
    private void Start()
    {
        Player = gameObject.transform.parent.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!colliders.Contains(other)) { colliders.Add(other); }
    }

    private void OnTriggerExit(Collider other)
    {
        colliders.Remove(other);
    }
}
