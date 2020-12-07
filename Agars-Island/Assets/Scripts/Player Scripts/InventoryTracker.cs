﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryTracker : MonoBehaviour
{
    public int batteries;

    public int meds;

    [SerializeField] private TextMeshProUGUI batteryText;
    [SerializeField] private TextMeshProUGUI medsText;
    // Start is called before the first frame update
    void Start()
    {
        int[] loadedData = LoadInventory.Load();
        batteries = loadedData[0];
        meds = loadedData[1];
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // TODO: Change this to call from a single object to avoid dependency issues
    private void OnApplicationQuit()
    {
        int[] saveData = {batteries, meds};
        LoadInventory.Save(saveData);
    }

    public void AddBattery(int number) { batteries += number; UpdateText(); }
    
    public void RemoveBattery(int number) { batteries -= number; UpdateText(); }
    
    public void AddMeds(int number) { meds += number; UpdateText(); }
    
    public void RemoveMeds(int number) { meds -= number; UpdateText(); }

    private void UpdateText()
    {
        batteryText.text = batteries.ToString();
        medsText.text = meds.ToString();
    }
}
