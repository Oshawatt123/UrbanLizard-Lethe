using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleFlashlight : MonoBehaviour
{
    private Flashlight_PRO LightScript;
    [HideInInspector] public bool canToggle = true;
    public float Battery;
    public float DrainSpeed;

    // Start is called before the first frame update
    void Start()
    {
        LightScript = this.GetComponentInChildren<Flashlight_PRO>();
        Battery = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (LightScript.is_enabled)
        {
            //Drain Flashlight when on
            Battery -= DrainSpeed * Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0) && canToggle && Battery > 0)
        {
            LightScript.Switch();
        }
    }
}
