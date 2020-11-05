using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        anim = GetComponent<Animator>();
    }
    
    virtual public void Interact()
    {
        Debug.Log("Base Interact");
        if (anim)
            anim.SetTrigger("Interact");
    }
}
