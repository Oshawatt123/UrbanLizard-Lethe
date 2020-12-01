using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleFlashlight : MonoBehaviour
{
    private Flashlight_PRO LightScript;
    [HideInInspector] public bool canToggle = true;

    // Start is called before the first frame update
    void Start()
    {
        LightScript = this.GetComponentInChildren<Flashlight_PRO>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canToggle)
        {
            LightScript.Switch();

        }
    }
}
