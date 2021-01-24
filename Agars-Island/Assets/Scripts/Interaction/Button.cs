using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : Interactable
{

    public UnityEvent pressEvent;
    [SerializeField] private bool fireOnce = false;
    [SerializeField] private int timesFired;

    public override void Interact()
    {
        timesFired++;
        if (!(timesFired > 1 && fireOnce))
        {
            pressEvent.Invoke();
        }
        
        base.Interact();
    }
}
