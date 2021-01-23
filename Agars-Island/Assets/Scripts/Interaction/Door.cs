using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField] private bool singleInteract;
    private int interactsToOpen = 0;
    private int interactions = 0;

    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddInteract()
    {
        interactsToOpen++;
    }

    public void Interact()
    {
        Debug.Log("Connected button has been pressed");
        interactions++;
        
        Debug.Log(interactions.ToString() + " out of " + interactsToOpen.ToString() + " to open door");
        if (interactions >= interactsToOpen || singleInteract)
        {
            Debug.Log("Opening door {singleInteract:" + singleInteract.ToString() + "}");
            Open();
        }
    }

    private void Open()
    {
        anim.SetTrigger("OpenDoor");
    }
}
