using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    private InventoryTracker playerInventory;

    [SerializeField] private string PickupType;

    private MeshRenderer[] render;
    private Collider collider;
    private AudioSource pickUpNoise;
    // Start is called before the first frame update
    void Start()
    {
        playerInventory = GameObject.FindWithTag("Player").GetComponent<InventoryTracker>();

        render = GetComponentsInChildren<MeshRenderer>();
        collider = GetComponentInChildren<Collider>();
        pickUpNoise = GetComponentInChildren<AudioSource>();
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
            case "Keycard2":
                playerInventory.SetKeycardLevel(2);
                break;
            case "Keycard3":
                playerInventory.SetKeycardLevel(3);
                break;
            case "Flashlight":
                playerInventory.GiveFlashlight();
                break;
            case "Note":
                playerInventory.NoteCollection(this.gameObject);
                break;
        }

        pickUpNoise.pitch = Random.Range(0.5f, 1.0f);
        pickUpNoise.Play();

        collider.enabled = false;

        foreach (MeshRenderer mesh in render)
        {
            mesh.enabled = false;
        }
        
        Destroy(gameObject, 1.0f);
    }
}
