using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreController : MonoBehaviour
{
    [SerializeField]
    private UIController uiController;
    [SerializeField]
    private pickUp pickups;
    public int dropletScore, enemyScore, dashScore, baseScore;
    public float currentScore;
    public int killCount;
    [SerializeField]
    private playerController player;

    private void Update()
    {
        currentScore = (player.transform.position.x);
    }

    void CalculateScore()
    {
        baseScore = (int)currentScore * 100;
        dropletScore = pickups.totalDropCount * 1000;
        enemyScore = killCount * 5000;
        dashScore = uiController.dashTrack * 10000;
    }

}
