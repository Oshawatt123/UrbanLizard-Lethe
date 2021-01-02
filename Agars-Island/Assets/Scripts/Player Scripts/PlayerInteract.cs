using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Transform CamTransform;
    [SerializeField] private float InteractLength;
    private HUDManager HUDmanager;
    [SerializeField] private LayerMask layers;

    private bool lookingAtInteractable = false;
    private Transform focusInteractable;
    
    // Start is called before the first frame update
    void Start()
    {
        HUDmanager = GetComponent<HUDManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (HUDmanager.ShouldInteract())
            {
                Debug.Log("E");
                Interact();
            }
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
            Debug.Log("Ray hit " + rayInfo.transform.name);
            // check tag on object
            Transform hitObj = rayInfo.transform;
            if (hitObj.CompareTag("Interactable"))
            {
                focusInteractable = hitObj;
                lookingAtInteractable = true;
            }
        }

        if (lookingAtInteractable)
        {
            HUDmanager.ShowInteractHint();
        }
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
