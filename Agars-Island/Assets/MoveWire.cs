using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class MoveWire : MonoBehaviour
{
    private Camera main;

    private Vector3 startPoint;

    [SerializeField] private Transform wireBox;


    private float originalSize;
    
    private Vector3 wireBoxScale;

    [SerializeField] private Transform wireBegin;

    [SerializeField] private GameObject wireEnd;

    public LayerMask wireEndLayer;

    private bool wireComplete;
    // Start is called before the first frame update
    void Start()
    {
        main = Camera.main;

        startPoint = transform.position;

        wireBoxScale = wireBox.localScale;

        originalSize = wireBox.GetComponent<Renderer>().bounds.size.z; // this is the size of the object's DEPTH (what looks to be its length from the player's POV)
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnMouseDrag()
    {
        // Get mouse position
        //Debug.Log("Dragging");
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = main.WorldToScreenPoint(transform.position).z; // gets the z position of the object in screen space
        
        // convert our mouse position (x, y) and use the z position of object in screen space
        // to generate a world position
        Vector3 newLocation = Camera.main.ScreenToWorldPoint(mousePos);

        //Debug.Log(newLocation);

        // update our location
        transform.position = newLocation;

        // set out local Z position to 0, so we are still flush with the box behind
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
        //move to new position
        newLocation = transform.position;
        
        
        // Calculate rotation
        Vector3 direction = newLocation - wireBegin.position;
        transform.right = direction;

        
        // Make the wire as long as it needs to be
        float dist = direction.magnitude;
        
        Vector3 rescale = wireBox.localScale;
        rescale.x = getScaleFromSize(dist);
        

        wireBox.localScale = rescale;
    }

    private void OnMouseUpAsButton()
    {
        //RaycastHit hitEnd;
        //bool atEnd = Physics.SphereCast(transform.position, 1f, transform.up, out hitEnd, 1f, wireEndLayer);

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 0.1f, transform.forward, 0.1f, wireEndLayer);

        for (int i = 0; i < hits.Length; i++)
        {
            Debug.Log(hits[i].collider.transform.name);

            if (hits[i].collider.gameObject == wireEnd)
            {
                Debug.Log("Wire complete!");
                wireComplete = true;
                transform.position = wireEnd.transform.position;

                // set out local Z position to 0, so we are still flush with the box behind
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);

                // Calculate rotation
                Vector3 direction = wireEnd.transform.position - wireBegin.position;
                transform.right = direction;

        
                // Make the wire as long as it needs to be
                float dist = direction.magnitude;
        
                Vector3 rescale = wireBox.localScale;
                rescale.x = getScaleFromSize(dist);
        

                wireBox.localScale = rescale;
            }
        }
        
        if (hits.Length == 0)
        {
            transform.position = startPoint;

            // set out local Z position to 0, so we are still flush with the box behind
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);

            // Calculate rotation
            Vector3 direction = startPoint - wireBegin.position;
            transform.right = direction;

        
            // Make the wire as long as it needs to be
            float dist = direction.magnitude;
        
            Vector3 rescale = wireBox.localScale;
            rescale.x = getScaleFromSize(dist);
        

            wireBox.localScale = rescale;
        }
    }

    float getScaleFromSize(float newSize)
    {
        return (newSize / originalSize) * wireBoxScale.x;
    }

    private void OnDrawGizmos()
    {
        Bounds bounds = wireBox.GetComponent<Renderer>().bounds;

        //Debug.LogWarning(bounds.size.y.ToString());

        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
}
