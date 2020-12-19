using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DragObject : MonoBehaviour
{

    private Camera main;
    private Vector3 anchorPosition;

    [SerializeField] private LayerMask boundsLayer;

    private Vector2 xBounds, yBounds;

    private Vector3 mouseDelta;
    private Vector3 prevMouse;

    private Vector3 prevMouseWorldPosition;

    private bool canDrag;
    
    private enum Orientation
    {
        RIGHT,
        UP
    }

    [SerializeField] private Orientation orientation;
    
    
    
    // DEBUG DRAWING
    private bool drawShere;
    private Vector3 rayHitPosition = Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        main = Camera.main;
        anchorPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

        // calculate mouse Delta position per frame
        // opportunity to move to a manager script for performance
        // but this shouldn't be an issue
        mouseDelta = Input.mousePosition - prevMouse;
        
        prevMouse = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        if (canDrag)
        {
            CalculateStop();

            // #### Get mouse position ####

            Vector3 mousePos = Input.mousePosition;

            mousePos.z =
                main.WorldToScreenPoint(transform.position).z; // gets the z position of the object in screen space

            // convert our mouse position (x, y) and use the z position of object
            // to generate a world position
            Vector3 newLocation = Camera.main.ScreenToWorldPoint(mousePos);
            newLocation.z = transform.position.z;

            // #### Calculate Movement ####

            if (prevMouseWorldPosition != Vector3.zero)
            {
                // Translate our world position to a local position relative to our parent object
                

                // Calculate movement of mouse in world space
                Vector3 movement = newLocation - prevMouseWorldPosition;
                
                // #### Update our location ####

                // move the object
                Vector3 newPosition = transform.localPosition + movement;
                // reset z position
                newPosition.z = anchorPosition.z;
                Debug.Log(newPosition);
                
                // apply movement
                transform.localPosition = newPosition;
                
                ClampPosition();

                //transform.position = newLocation;



                //transform.localPosition = newLocalPosition;
            }

            prevMouseWorldPosition = newLocation;
        }
    }

    private void OnMouseExit() { canDrag = false; prevMouseWorldPosition = Vector3.zero; }
    private void OnMouseEnter() { canDrag = true; }

    void CalculateStop()
    {
        // cast a ray depending on orientation

        if (orientation == Orientation.RIGHT)
        {
            // cast a horizontal ray the direcition the mouse is moving
            Vector3 direction = Vector3.forward * (mouseDelta.x > 0 ? -1 : 1);

            
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(transform.position, direction, out hitInfo, Mathf.Infinity, boundsLayer))
            {
                Debug.DrawRay(transform.position, direction, Color.green);
                float xStop = hitInfo.transform.position.x;

                // Debug
                rayHitPosition = hitInfo.transform.position;
            }
            else
            {
                Debug.DrawRay(transform.position, direction, Color.red);
            }
        }
    }
    
    void ClampPosition()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(rayHitPosition, 0.1f);
    }
}