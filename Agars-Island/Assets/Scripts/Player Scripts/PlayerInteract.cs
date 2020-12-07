using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Transform CamTransform;
    [SerializeField] private float InteractLength;
    private HUDManager HUDmanager;
    [SerializeField] private LayerMask layers;
    
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
    }

    private void Interact()
    {
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
                Debug.Log("Ray hit interactable");
                // get interact script and call Ineract()
                hitObj.GetComponent<Interactable>().Interact();
            }
        }

        
    }
}
