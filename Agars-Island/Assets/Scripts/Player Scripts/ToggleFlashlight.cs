using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleFlashlight : MonoBehaviour
{
    private Flashlight_PRO LightScript;
    [HideInInspector] public bool canToggle = true;
    public float Battery;
    [SerializeField] public float maxBattery = 100f;
    public float DrainSpeed;
    private InventoryTracker Inventory;
    [HideInInspector] public bool hasFlashlight = false;
    private bool Inspecting;

    private Animator flashLightAnim;

    // Start is called before the first frame update
    void Start()
    {
        LightScript = this.GetComponentInChildren<Flashlight_PRO>();
        flashLightAnim = LightScript.gameObject.GetComponent<Animator>();
        
        Battery = maxBattery;
        Inventory = this.GetComponent<InventoryTracker>();
        canToggle = true;
        Inspecting = false;
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

        if (hasFlashlight)
        {
            //Toggle Light on and off
            if (Input.GetMouseButtonDown(0) && canToggle)
            {
                LightScript.Switch();
            }

            //Reload battery
            if (Input.GetKeyDown(KeyCode.R) && Inventory.batteries > 0)
            {
                Inventory.RemoveBattery(1);
                Battery = maxBattery;
                canToggle = true;
                flashLightAnim.SetTrigger("ReloadBattery");
            }

            //Inspect Battery Toggle
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (!Inspecting)
                {
                    //Play inspection animation
                    flashLightAnim.SetTrigger("CheckBattery");
                }
                else
                {
                    //Play reverse animation
                    flashLightAnim.SetTrigger("HolsterTorch");
                }

                //Set bool to new value
                Inspecting = !Inspecting;
            }
        }
    }
}
