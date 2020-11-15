using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using RadiatorGames.UI.SwapCanvasGroup;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup HUD;
    [SerializeField] private CanvasGroup inventoryGroup;
    private bool inventoryOpen = false;
    
    // Start is called before the first frame update
    void Start()
    {
        GroupSwapper.HideCanvasGroup(inventoryGroup);
        GroupSwapper.ShowCanvasGroup(HUD);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventoryOpen)
            {
                GroupSwapper.HideCanvasGroup(inventoryGroup);
                GroupSwapper.ShowCanvasGroup(HUD);
            }

            else
            {
                GroupSwapper.ShowCanvasGroup(inventoryGroup);
                GroupSwapper.HideCanvasGroup(HUD);
            }

            inventoryOpen = !inventoryOpen;
        }
    }
}
