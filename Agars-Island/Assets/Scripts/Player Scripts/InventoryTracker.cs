using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryTracker : MonoBehaviour
{
    public int batteries;

    public int meds;

    private int keycardLevel= 0;

    private List<GameObject> NotesList = new List<GameObject>();

    [SerializeField] private TextMeshProUGUI batteryText;
    [SerializeField] private TextMeshProUGUI medsText;
    [SerializeField] private TextMeshProUGUI PickUpText;
    private Animator pickUpTextAnim;

    private ToggleFlashlight TF;

    [SerializeField] private AudioSource batteryPickup;
    [SerializeField] private AudioSource paperPickup;
    [SerializeField] private AudioSource genericPickup;

    // Start is called before the first frame update
    void Start()
    {
        int[] loadedData = LoadInventory.Load();
        //batteries = loadedData[0];
        //meds = loadedData[1];
        //keycardLevel = loadedData[2];
        batteries = 0;
        meds = 0;
        keycardLevel = 0;
        UpdateText();

        if (PickUpText)
            pickUpTextAnim = PickUpText.gameObject.GetComponent<Animator>();

        TF = GetComponent<ToggleFlashlight>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // TODO: Change this to call from a single object to avoid dependency issues
    private void OnApplicationQuit()
    {
        int[] saveData = {batteries, meds, keycardLevel};
        LoadInventory.Save(saveData);
    }

    //---------------------------------------- Inventory Management -----------------------------------
    
    public void AddBattery(int number)
    {
        batteries += number; UpdateText();
        ShowPickupText("Picked up battery");
    }
    
    public void RemoveBattery(int number) { batteries -= number; UpdateText(); }

    public void AddMeds(int number)
    {
        meds += number; UpdateText();
        ShowPickupText("Picked up meds");
    }
    
    public void RemoveMeds(int number) 
    { 
        meds -= number; UpdateText(); 
    }

    public void SetKeycardLevel(int level)
    {
        keycardLevel = level;
        ShowPickupText("Picked up keycard level " + level.ToString());
    }

    public int GetKeycardLevel() { return keycardLevel; }

    public void GiveFlashlight()
    {
        TF.hasFlashlight = true;
        TF.canToggle = true;
        Debug.Log("Player given flashlight");
        CheckpointManager.instance.FlashlightGot();
        ShowPickupText("Picked up flashlight");
    }

    //---------------------------------------- Note Collection -----------------------------------
    public void NoteCollection(GameObject Note)
    {
        //Add note to list of collected notes
        NotesList.Add(Note);
        //Disable Note
        Note.SetActive(false);
        
    }

    //-------------------------------------- Display Pickup Text ----------------------------------
    private void ShowPickupText(string text)
    {
        if (pickUpTextAnim)
        {
            PickUpText.text = text;
            if(pickUpTextAnim.GetCurrentAnimatorStateInfo(0).IsName("InventHidden"))
                pickUpTextAnim.SetTrigger("FadeIn");
            
        }
    }
    
    private void UpdateText()
    {
        batteryText.text = batteries.ToString();
        medsText.text = meds.ToString();
    }


}
