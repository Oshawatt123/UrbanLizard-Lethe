using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Animator anim;

    [HideInInspector] public GiveCheckPoint chkpnt;

    [HideInInspector] public bool canInteract = true;
    public string interactableType;
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
        if (canInteract)
        {
            Debug.Log("Base Interact");
            if (anim)
                anim.SetTrigger("Interact");
        }
        else
        {
            Debug.Log("Interactable disabled");
        }
    }
}
