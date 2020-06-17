using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempCS1 : MonoBehaviour
{
    [SerializeField]
    private float csTime;
    [SerializeField]
    private tempSceneManager sm;

    private void Start()
    {
        StartCoroutine(NextScene());
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(csTime);
        sm.NextScene();
    }
}
