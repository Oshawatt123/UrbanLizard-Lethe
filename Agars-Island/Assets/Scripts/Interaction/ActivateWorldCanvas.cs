using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RadiatorGames.UI.SwapCanvasGroup;

[RequireComponent(typeof(Collider))]
public class ActivateWorldCanvas : Interactable
{
    private HUDManager HUDmanager;
    // Start is called before the first frame update
    void Start()
    {
        HUDmanager = GameObject.FindWithTag("Player").GetComponent<HUDManager>();
    }
    
    public override void Interact()
    {
        HUDmanager.HidePlayerHUDs(true, GetComponent<Collider>());
        base.Interact();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
