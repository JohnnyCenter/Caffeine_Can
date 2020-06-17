using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{
    #region Canvas Groups
    [SerializeField]
    private CanvasGroup mainMenu;
    [SerializeField]
    private CanvasGroup levelSelect;
    [SerializeField]
    private CanvasGroup chooseStage;
    [SerializeField]
    private CanvasGroup controls1;
    [SerializeField]
    private CanvasGroup controls2;
    #endregion

    private int bn = 0;

    private void Start()
    {
        mainMenu.gameObject.SetActive(true);
        levelSelect.gameObject.SetActive(false);
        chooseStage.gameObject.SetActive(false);
        controls1.gameObject.SetActive(false);
        controls2.gameObject.SetActive(false);
    }

    public void Back()
    {
        if(bn == 1)
        {
            mainMenu.gameObject.SetActive(true);
            levelSelect.gameObject.SetActive(false);
            chooseStage.gameObject.SetActive(false);
            controls1.gameObject.SetActive(false);
            controls2.gameObject.SetActive(false);
        }
    }

    public void Next()
    {
        controls1.gameObject.SetActive(false);
        controls2.gameObject.SetActive(true);
        bn = 1;
    }

    public void StartGame()
    {
        mainMenu.gameObject.SetActive(false);
        levelSelect.gameObject.SetActive(true);
        bn = 1;
    }

    public void Controls()
    {
        mainMenu.gameObject.SetActive(false);
        controls1.gameObject.SetActive(true);
        bn = 1;
    }
}
