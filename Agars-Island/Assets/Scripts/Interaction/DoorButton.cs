using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : Interactable
{
    public Door door;

    private bool pressedOnce = false;

    private InventoryTracker playerInv;
    [SerializeField] private int clearanceLevel = 0;
    private int locked;
    
    [SerializeField] private Animator lightAnim;
    private AudioSource audioSource;

    [SerializeField] private AudioClip keycardAcceptAudio;
    [SerializeField] private AudioClip keycardDeniedAudio;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        // add this button to the door
        door.AddInteract();

        playerInv = GameObject.Find("Player").GetComponent<InventoryTracker>();
        lightAnim = GetComponentInChildOnly<Animator>(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public override void Interact()
    {
        if (playerInv.GetKeycardLevel() >= clearanceLevel)
        {
            base.Interact();

            // feedback
            lightAnim.SetTrigger("Pass");
            audioSource.PlayOneShot(keycardAcceptAudio);
            
            if (chkpnt) chkpnt.CheckPoint();

            if (!pressedOnce)
            {
                pressedOnce = true;
                door.Interact();
            }
        }
        else
        {
            //feedback
            lightAnim.SetTrigger("Fail");
            audioSource.PlayOneShot(keycardDeniedAudio);
        }
    }

    private T GetComponentInChildOnly<T>(GameObject parent) where T:Component
    {
        T[] comps = parent.GetComponentsInChildren<T>();

        if (comps.Length > 1)
        {
            for (int i = 1; i < comps.Length; i++)
            {
                T comp = comps[i];
                if (comp.gameObject.GetInstanceID() != GetInstanceID())
                {
                    return comp;
                }
            }
        }

        return null;
    }
    
}
