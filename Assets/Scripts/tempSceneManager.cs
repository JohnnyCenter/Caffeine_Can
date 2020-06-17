using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tempSceneManager : MonoBehaviour
{
    private int nextScene;

    private void Start()
    {
        nextScene = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void Return()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void NextScene()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void Level1()
    {
        SceneManager.LoadScene("CutScene1");
    }

    public void Level2()
    {
        SceneManager.LoadScene("CutScene2");
    }

    public void Level3()
    {
        SceneManager.LoadScene("CutScene3");
    }
}
