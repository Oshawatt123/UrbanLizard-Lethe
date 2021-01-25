using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CheckpointManager : MonoBehaviour
{
    [HideInInspector] public static CheckpointManager instance;

    [HideInInspector] public bool GeneratorFixed;
    [HideInInspector] public bool GeneratorOn;

    [SerializeField] private GameObject enemy;
    [SerializeField] private TextMeshProUGUI taskText;

    [HideInInspector] public UnityEvent FlashLightEvents;
    [HideInInspector] public UnityEvent GeneratorFixEvents;
    [HideInInspector] public UnityEvent GeneratorOnEvents;
    
    void Awake()
    {
        if (instance)
            Destroy(gameObject);
        else
            instance = this;

        taskText.text = "☐ Find a light source";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FlashlightGot()
    {
        taskText.text = "☐ Fix the backup generator";
    }

    public void GeneratorFix()
    {
        taskText.text = "☐ Turn on the generator in the security room";
        GeneratorFixed = true;
    }

    public void ReleaseEnemy()
    {
        taskText.text = "☐ Spoopy boi is here to nibble your nipple";
        enemy.SetActive(true);
        GeneratorOn = true;
    }
}
