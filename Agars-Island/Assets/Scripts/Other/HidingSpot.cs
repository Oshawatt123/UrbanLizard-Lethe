using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : Interactable
{
    private GameObject Player;
    private PlayerMovement MoveScript;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        MoveScript = Player.GetComponent<PlayerMovement>();
    }

    public override void Interact()
    {
        if(MoveScript.IsHiding == false)
        {
            //Play Enter hiding spot animation

            //Set IsHiding
            MoveScript.IsHiding = true;
            //Prevent Player Movement
            MoveScript.enabled = false;
        }
        else
        {
            //Play Leave hiding spot animation

            //Set IsHiding
            MoveScript.IsHiding = false;
            //Enable Movement
            MoveScript.enabled = true;
        }
    }
}
