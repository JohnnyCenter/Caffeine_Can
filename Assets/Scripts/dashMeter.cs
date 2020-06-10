using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashMeter : MonoBehaviour
{
    [SerializeField]
    private pickUp PickUp;
    private Animator anim;

    public int Count;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Count = PickUp.dropCount;
        anim.SetInteger("Count", Count);
    }
}
