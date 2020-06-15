using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUp : MonoBehaviour
{
    [SerializeField]
    private UIController uiController;

    public int dropCount = 0;
    public int totalDropCount = 0;
    public int saves = 0;

    private void Update()
    {
        if (dropCount > 9)
        {
            Filled();
            //Animation
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Droplet")
        {
            Destroy(other.gameObject);
            dropCount += 1;
            totalDropCount += 1;

            FindObjectOfType<audioManager>().Play("bubblesound22");
        }

        if(other.tag == "Cage")
        {
            Destroy(other.gameObject);
            saves += 1;

            FindObjectOfType<audioManager>().Play("freebean");
        }
    }

    void Filled()
    {
        uiController.dashCount += 1;
        dropCount = 0;
    }
}
