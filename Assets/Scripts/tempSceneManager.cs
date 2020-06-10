using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tempSceneManager : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("Prototype");
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("StartScreen");
    }
}
