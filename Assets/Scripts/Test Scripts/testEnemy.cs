using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testEnemy : MonoBehaviour
{
    [SerializeField]
    private testVA player;
    private SpriteRenderer sr;

    private void Awake()
    {
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
                player.rb.velocity = new Vector2(player.rb.velocity.x, 50);
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
    }

    private void OnBecameVisible()
    {
        player.inRange = true;
    }
}
