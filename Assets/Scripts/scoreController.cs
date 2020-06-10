using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreController : MonoBehaviour
{
    [SerializeField]
    private UIController uiController;
    [SerializeField]
    private pickUp pickups;
    [SerializeField]
    private playerController player;
    public int dropletScore, enemyScore, saveScore, dashScore, baseScore, finalScore;
    public float currentScore;
    public int killCount;

    private void Update()
    {
        currentScore = (player.transform.position.x);
    }

    public void CalculateScore()
    {
        baseScore = ((int)currentScore * 100) + (uiController.dashTrack * 10000);
        dropletScore = pickups.totalDropCount * 1000;
        enemyScore = killCount * 5000;
        //dashScore = uiController.dashTrack * 10000;
        saveScore = pickups.saves * 15000;

        finalScore = dashScore + enemyScore + dropletScore + baseScore + saveScore;
    }

}
