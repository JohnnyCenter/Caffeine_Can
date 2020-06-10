using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crashHandler : MonoBehaviour
{
    [SerializeField]
    private playerController Player;
    private bool hit = false;

    private void Awake()
    {
        Player = FindObjectOfType<playerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Platform")
        {
            if (Player.dashActive)
                hit = true;
            else
                StartCoroutine(Player.Knockback());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Platform")
        {
            hit = false;
        }
    }
    private void Update()
    {
        if (hit)
        {
                Player.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 1, Player.transform.position.z);
        }
    }
}
