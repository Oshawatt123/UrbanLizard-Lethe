using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryTracker : MonoBehaviour
{
    private int batteries;

    private int meds;

    [SerializeField] private TextMeshProUGUI batteryText;
    [SerializeField] private TextMeshProUGUI medsText;
    // Start is called before the first frame update
    void Start()
    {
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        
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
