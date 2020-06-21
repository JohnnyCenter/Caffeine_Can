using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crashHandler : MonoBehaviour
{
    [SerializeField]
    private playerController Player;
    private bool hit = false;
    private BoxCollider2D bc;

    private void Awake()
    {
        Player = FindObjectOfType<playerController>();
        bc = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        Normal();
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

        if(other.tag == "Breakable")
        {
            if (Player.dashActive)
                Destroy(other.gameObject);
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

    public void Normal()
    {
        bc.size = new Vector2(0.2f, 0.9f);
        bc.offset = new Vector2(0.5f, 0);
    }

    public void Shrink()
    {
        bc.size = new Vector2(0.2f, 0.3f);
        bc.offset = new Vector2(0.5f, -0.25f);
    }
}
