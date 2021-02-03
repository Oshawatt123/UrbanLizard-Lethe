using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private Animator anim;

    [HideInInspector] public GiveCheckPoint chkpnt;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        chkpnt = GetComponent<GiveCheckPoint>();
        anim = GetComponent<Animator>();
    }
    
    virtual public void Interact()
    {
        Debug.Log("Base Interact");
        if (anim)
            anim.SetTrigger("Interact");
    }
}
