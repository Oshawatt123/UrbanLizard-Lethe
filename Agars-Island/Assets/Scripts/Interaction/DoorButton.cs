using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : Interactable
{
    public Door door;

    private bool pressedOnce = false;

    private InventoryTracker playerInv;
    [SerializeField] private int clearanceLevel = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        Init();
        // add this button to the door
        door.AddInteract();

        playerInv = GameObject.Find("Player").GetComponent<InventoryTracker>();
    }

    public override void Interact()
    {
        if (playerInv.GetKeycardLevel() >= clearanceLevel)
        {
            base.Interact();

            if (!pressedOnce)
            {
                pressedOnce = true;
                door.Interact();
            }
        }
    }
}
