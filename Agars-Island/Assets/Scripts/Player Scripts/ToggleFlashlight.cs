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

    // Start is called before the first frame update
    void Start()
    {
        LightScript = this.GetComponentInChildren<Flashlight_PRO>();
        Battery = 100f;
        Inventory = this.GetComponent<InventoryTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LightScript.is_enabled)
        {
            //Drain Flashlight when on
            Battery -= DrainSpeed * Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0) && canToggle && Battery > 0 && hasFlashlight)
        {
            LightScript.Switch();
        }

        if (Input.GetKeyDown(KeyCode.L) && Inventory.batteries > 0)
        {
            Inventory.RemoveBattery(1);
            Battery = 100f;
        }
    }
}
