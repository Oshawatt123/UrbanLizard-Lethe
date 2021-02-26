using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    //Camera Transform
    [SerializeField] private Transform CamTransform;
    //Distance player can interact at
    [SerializeField] private float InteractLength;
    //Hud manager script
    private HUDManager HUDmanager;
    [SerializeField] private LayerMask layers;

    private bool lookingAtInteractable = false;
    //Interactable in focus
    private Transform focusInteractable;
    
    // Start is called before the first frame update
    void Start()
    {
        //Get HUD manager
        HUDmanager = GetComponent<HUDManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if pressing to interact
        if (Input.GetKeyDown(KeyCode.E))
        {
            //If should be starting interaction
            if (HUDmanager.ShouldInteract())
            {
                Debug.Log("E");
                Interact();
            }
            //Otherwise stop interacting
            else
            {
                HUDmanager.ResetHUD();
            }
        }
        
        // ### check if we're looking at an interactable ###
        lookingAtInteractable = false;
        // get forward from camera
        Vector3 rayDirection = CamTransform.forward * InteractLength;
        
        // cast a ray
        RaycastHit rayInfo;
        Debug.DrawRay(CamTransform.position, rayDirection, Color.magenta, 10.0f);
        if (Physics.Raycast(CamTransform.position, rayDirection, out rayInfo, InteractLength, ~layers))
        {
            //Debug.Log("Ray hit " + rayInfo.transform.name);
            // check tag on object
            Transform hitObj = rayInfo.transform;
            if (hitObj.CompareTag("Interactable"))
            {
                focusInteractable = hitObj;
                lookingAtInteractable = true;
            }
        }
        //Show e to interact hint
        if (lookingAtInteractable)
        {
            HUDmanager.ShowInteractHint();
        }
        //Hide hint
        else
        {
            HUDmanager.HideInteractHint();
        }
    }

    private void Interact()
    {
        if (lookingAtInteractable)
        {
            Debug.Log("Ray hit interactable");
            // get interact script and call Ineract()
            focusInteractable.GetComponent<Interactable>().Interact();
        }
    }
}
