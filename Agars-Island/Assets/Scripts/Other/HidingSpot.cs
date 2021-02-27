using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : Interactable
{
    private GameObject Player;
    private PlayerMovement MoveScript;

    public Transform animStartTransform;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        MoveScript = Player.GetComponent<PlayerMovement>();
    }

    public override void Interact()
    {
        if(MoveScript.IsHiding == false)
        {
            //Set IsHiding
            MoveScript.IsHiding = true;
            //Prevent Player Movement
            //MoveScript.enabled = false;
        }
        else
        {
            //Set IsHiding
            MoveScript.IsHiding = false;
            //Enable Movement
            //MoveScript.enabled = true;
        }
        
        // LA - 27/02/21 ; 20:49
        base.Interact();
    }
}
