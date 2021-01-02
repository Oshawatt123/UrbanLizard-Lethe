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

    [SerializeField] private TextMeshProUGUI batteryText;
    [SerializeField] private TextMeshProUGUI medsText;

    private ToggleFlashlight TF;

    // Start is called before the first frame update
    void Start()
    {
        int[] loadedData = LoadInventory.Load();
        batteries = loadedData[0];
        meds = loadedData[1];
        keycardLevel = loadedData[2];
        UpdateText();

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

    public void AddBattery(int number) { batteries += number; UpdateText(); }
    
    public void RemoveBattery(int number) { batteries -= number; UpdateText(); }
    
    public void AddMeds(int number) { meds += number; UpdateText(); }
    
    public void RemoveMeds(int number) { meds -= number; UpdateText(); }

    public void SetKeycardLevel(int level) { keycardLevel = level; }

    public int GetKeycardLevel() { return keycardLevel; }

    private void UpdateText()
    {
        batteryText.text = batteries.ToString();
        medsText.text = meds.ToString();
    }

    public void GiveFlashlight()
    {
        TF.hasFlashlight = true;
        TF.canToggle = true;
        Debug.Log("Player given flashlight");
    }

}
