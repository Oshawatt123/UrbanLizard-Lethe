using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    virtual public void Interact()
    {
        if (anim)
            anim.SetTrigger("Interact");
    }
}
