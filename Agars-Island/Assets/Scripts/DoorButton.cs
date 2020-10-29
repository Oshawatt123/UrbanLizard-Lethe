using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : Interactable
{
    public Door door;

    private bool pressedOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        Init();
        // add this button to the door
        door.AddInteract();
    }

    public override void Interact()
    {
        base.Interact();

        if (!pressedOnce)
        {
            pressedOnce = true;
            door.Interact();
        }
    }
}
