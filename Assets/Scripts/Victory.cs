using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    [SerializeField]
    private cameraFollow mainCamera;
    [SerializeField]
    private playerController player;
    [SerializeField]
    private UIController uiController;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(Sequence());
        }
    }

    IEnumerator Sequence()
    {
        player.dead = true;
        yield return new WaitForSeconds(2);
        player.Stop();
        mainCamera.Win();
        uiController.Finsihed();

    }
}
