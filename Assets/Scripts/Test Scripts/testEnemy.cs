using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testEnemy : MonoBehaviour
{
    [SerializeField]
    private testVA player;
    [SerializeField]
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        sr.color = new Color(255f, 255f, 0f, 255f);
    }

    private void Update()
    {
        if (player.inRange)
        {
            transform.gameObject.tag = "Active Enemy";
        }
        else
        {
            transform.gameObject.tag = "Enemy";
        }
        //rb.velocity = new Vector2(-10, rb.velocity.y);
        if (player.inRange && player.onPlatform == false)
            sr.color = new Color(255f, 0f, 255f, 255f);
        else
            sr.color = new Color(255f, 255f, 255f, 255f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (player.invulnerable)
            {
                Destroy(gameObject);
            }
            else if (player.attack)
            {
                player.rb.velocity = new Vector2(rb.velocity.x, 50);
                Destroy(gameObject);
            }
            else
            {
                player.Kill();
            }
        }
    }

    private void OnBecameInvisible()
    {
        player.inRange = false;
        Debug.Log("Out");
    }

    private void OnBecameVisible()
    {
        player.inRange = true;
        Debug.Log("In");
    }
}
