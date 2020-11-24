using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using RadiatorGames.Math;

public class GasPuzzleLogic : MonoBehaviour
{
    [SerializeField] private Slider playerSlider;

    [SerializeField] private Slider gasFullSlider;
    private Image gasSliderBGImage;

    private float fillDelta = 0f;
    private float gasFillAmount = 0f;

    [SerializeField] private float pressureMax;
    [SerializeField] private float pressureSustainTime;
    private float timeAtPressureMax;

    [SerializeField] private TextMeshProUGUI timerText;

    public BoxCollider gasArea;

    // Start is called before the first frame update
    void Start()
    {
        gasSliderBGImage = gasFullSlider.GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gasFullSlider.value < gasFullSlider.maxValue)
        {
            fillDelta = playerSlider.value;

            gasFillAmount += fillDelta * Time.deltaTime;

            gasFullSlider.value = gasFillAmount;

            if (fillDelta >= pressureMax)
            {
                timeAtPressureMax = Mathf.Clamp(timeAtPressureMax + Time.deltaTime, 0, pressureSustainTime);
            }
            else
            {
                timeAtPressureMax = Mathf.Clamp(timeAtPressureMax - Time.deltaTime, 0, pressureSustainTime);
            }

            float R = Mapping.Map(0, pressureSustainTime, 0, 1, timeAtPressureMax);
            float A = Mapping.Map(0, pressureSustainTime, 0, 1, timeAtPressureMax);

            timerText.color = new Color(R, 0, 0, A);
            timerText.text = timeAtPressureMax.ToString("0.00");

            gasSliderBGImage.color = new Color(R, 0, 0, A);
        }
        else
        {
            gasArea.isTrigger = true;
        }
    }
}