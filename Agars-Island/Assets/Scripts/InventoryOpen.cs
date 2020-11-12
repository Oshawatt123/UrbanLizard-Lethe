using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RadiatorGames.UI.SwapCanvasGroup;

public class InventoryOpen : MonoBehaviour
{
    [SerializeField] private CanvasGroup inventoryGroup;
    private bool inventoryOpen = false;
    
    // Start is called before the first frame update
    void Start()
    {
        GroupSwapper.HideCanvasGroup(inventoryGroup);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventoryOpen) GroupSwapper.HideCanvasGroup(inventoryGroup);
            
            else GroupSwapper.ShowCanvasGroup(inventoryGroup);

            inventoryOpen = !inventoryOpen;
        }
    }
}
