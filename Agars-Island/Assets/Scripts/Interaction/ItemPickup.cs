using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    private InventoryTracker playerInventory;

    [SerializeField] private string PickupType;
    // Start is called before the first frame update
    void Start()
    {
        playerInventory = GameObject.FindWithTag("Player").GetComponent<InventoryTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        base.Interact();

        switch (PickupType)
        {
            case "Battery":
                playerInventory.AddBattery(1);
                break;
            case "Meds":
                playerInventory.AddMeds(1);
                break;
            case "Keycard1":
                playerInventory.SetKeycardLevel(1);
                break;
            case "Flashlight":
                playerInventory.GiveFlashlight();
                break;
        }

        Destroy(gameObject);
    }
}
