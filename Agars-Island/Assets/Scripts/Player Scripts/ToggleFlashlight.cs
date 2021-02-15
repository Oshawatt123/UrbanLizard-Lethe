using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleFlashlight : MonoBehaviour
{
    private Flashlight_PRO LightScript;
    [HideInInspector] public bool canToggle = true;
    public float Battery;
    public float DrainSpeed;
    private InventoryTracker Inventory;
    [HideInInspector] public bool hasFlashlight = false;
    private bool Inspecting;

    // Start is called before the first frame update
    void Start()
    {
        LightScript = this.GetComponentInChildren<Flashlight_PRO>();
        Battery = 100f;
        Inventory = this.GetComponent<InventoryTracker>();
        canToggle = true;
        Inspecting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (LightScript.is_enabled)
        {
            //Drain Flashlight when on
            Battery -= DrainSpeed * Time.deltaTime;
            if(Battery <= 0)
            {
                //Switch light off
                LightScript.Switch();
                //Set to can't toggle
                canToggle = false;
            }
        }

        //Toggle Light on and off
        if (Input.GetMouseButtonDown(0) && canToggle && hasFlashlight)
        {
            LightScript.Switch();
        }

        //Reload battery
        if (Input.GetKeyDown(KeyCode.R) && Inventory.batteries > 0)
        {
            Inventory.RemoveBattery(1);
            Battery = 100f;
            canToggle = true;
        }

        //Inspect Battery Toggle
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!Inspecting)
            {
                //Play inspection animation

            }
            else
            {
                //Play reverse animation

            }
            //Set bool to new value
            Inspecting = !Inspecting;
        }
    }
}
