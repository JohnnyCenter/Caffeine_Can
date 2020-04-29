using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    #region Dash Related
    [SerializeField]
    private TextMeshProUGUI dashCounter;
    public int dashCount, dashTrack;
    #endregion
    private playerController player;
    [SerializeField]
    public Button retryButton;

    private void Awake()
    {
        player = GetComponent<playerController>();
    }

    private void Start()
    {
        retryButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        dashCounter.text = dashCount.ToString();
    }

    public void DashUse()
    {
        dashCount -= 1;
        dashTrack += 1;
    }

}
