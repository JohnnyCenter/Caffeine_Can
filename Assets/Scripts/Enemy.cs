using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private playerController player;
    [SerializeField]
    private UIController uiController;
    [SerializeField]
    private scoreController Score;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (player.invulnerable)
            {
                Destroy(gameObject);
                uiController.dashCount += 1;
            }
            else
            {
                player.Kill();
            }     
        }
    }

    void KillEnemy()
    {
        uiController.dashCount += 1;
        Score.killCount += 1;
    }
}
