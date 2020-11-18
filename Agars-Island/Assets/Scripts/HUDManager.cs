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
    
    // this boolean is not *needed*
    // however, checking if the canvas group is null is very expensive
    // whereas boolean checking is as cheap as it gets
    
    // PERFORMANCEEEEEEEEE
    // run this shit on a pentium duo
    private bool puzzleOpen = false;
    private CanvasGroup puzzleGroup = null;

    private List<CanvasGroup> allCanvases = new List<CanvasGroup>();

    private static PlayerMovement playerMovement;
    
    // Start is called before the first frame update
    void Start()
    {
        GroupSwapper.HideCanvasGroup(inventoryGroup);
        GroupSwapper.ShowCanvasGroup(HUD);

        allCanvases.Add(HUD);
        allCanvases.Add(inventoryGroup);

        playerMovement = GetComponent<PlayerMovement>();
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
                MouseModeGame();
            }
            else if (puzzleOpen)
            {
                ResetHUD();
            }

            else
            {
                GroupSwapper.ShowCanvasGroup(inventoryGroup);
                GroupSwapper.HideCanvasGroup(HUD);
                MouseModeUI();
            }

            inventoryOpen = !inventoryOpen;
        }
    }

    /// <summary>
    /// Hides all the player's HUD canvas groups, and optionally shows another canvasgroup
    /// </summary>
    /// <param name="puzzleCanvasGroup">Optional: new canvas group to open</param>
    public void HidePlayerHUDs(CanvasGroup puzzleCanvasGroup = null)
    {
        // this check here is allowed, as it won't be performed too often
        this.puzzleOpen = puzzleCanvasGroup != null;
        puzzleGroup = puzzleCanvasGroup;
        inventoryOpen = false;

        playerMovement.RotationSpeed = 0f;
        
        foreach (CanvasGroup group in allCanvases)
        {
            GroupSwapper.HideCanvasGroup(group);
        }
        
        MouseModeUI();
    }

    public void ResetHUD()
    {
        puzzleOpen = false;
        // hide puzzle group first otherwise it'll be set to null in HidePlayerHUDs
        if (puzzleGroup)
            GroupSwapper.HideCanvasGroup(puzzleGroup);
        
        HidePlayerHUDs();

        GroupSwapper.ShowCanvasGroup(HUD);

        MouseModeGame();
    }


    public static void MouseModeGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerMovement.RotationSpeed = 10f;
    }

    public static void MouseModeUI()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        playerMovement.RotationSpeed = 0f;
    }
}
