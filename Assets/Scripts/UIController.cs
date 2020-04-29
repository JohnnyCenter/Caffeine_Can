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
    public int dashCount;
    #endregion

    private void Update()
    {
        dashCounter.text = dashCount.ToString();
    }

    public void DashUse()
    {
        dashCount -= 1;
    }
}
