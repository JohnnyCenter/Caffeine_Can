using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testVAtrigger : MonoBehaviour
{
    [SerializeField]
    private float triggerSpeed = 0.3f;
    private BoxCollider2D bc;
    private Vector2 startSize, startOffset;
    [SerializeField]
    private Vector2 endSize;
    [SerializeField]
    private Vector2 endOffset;
    private bool TargetFound = false;

    GameObject Player;

    private void Awake()
    {
        bc = GetComponent<BoxCollider2D>();

        Player = gameObject.transform.parent.gameObject;
    }
    private void Start()
    {
        startSize = Vector2.zero;
        bc.size = Vector2.zero;
        bc.offset = Vector2.zero;
        //StartCoroutine(Test2());
    }

    private void FixedUpdate()
    {
        StartCoroutine(Test2());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Debug.Log("Target Acquired");
            TargetFound = true;
            //Player.GetComponent<testVA>().targetFound = true;
        }
    }
    void Test()
    {
        Vector2 bcSize = bc.size;

        if (bcSize.x < endSize.x)
        {
            bcSize += Vector2.one * triggerSpeed * Time.deltaTime;
            
            if (TargetFound == true)
            {
                bcSize = Vector2.zero;
            }
            else if (bcSize.x > endSize.x)
            {
                bcSize = new Vector2(endSize.x, endSize.y);
            }

        }

        bc.size = bcSize;
    }

    private void Reset()
    {
        startSize = Vector2.zero;
        bc.size = Vector2.zero;
        bc.offset = Vector2.zero;
    }
    IEnumerator Test2()
    {
        yield return new WaitForSeconds(3);
        Test();
        /*yield return new WaitForSeconds(4);
        Reset();*/
    }
}
