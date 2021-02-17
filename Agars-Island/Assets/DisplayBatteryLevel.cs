using System.Collections;
using System.Collections.Generic;
using TMPro.SpriteAssetUtilities;
using UnityEngine;
using UnityEngine.UI;

public class DisplayBatteryLevel : MonoBehaviour
{

    [SerializeField] private List<GameObject> bars;
    [SerializeField] private ToggleFlashlight flashLight;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float barsToActivate = RadiatorGames.Math.Mapping.Map(0, flashLight.maxBattery, 0, bars.Count,
            flashLight.Battery);
        
        for(int i = 0; i < bars.Count; i++)
        {
            if (i > barsToActivate)
                bars[i].SetActive(false);
            else
                bars[i].SetActive(true);
        }
    }
}
