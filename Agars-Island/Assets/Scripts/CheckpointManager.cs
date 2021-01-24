using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;

    public bool GeneratorFixed;
    public bool GeneratorOn;

    [SerializeField] private GameObject enemy;
    [SerializeField] private TextMeshProUGUI taskText;
    
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

    public void FixGenerator()
    {
        taskText.text = "☐ Turn on the generator in the security room";
        GeneratorFixed = true;
    }

    public void ReleaseEnemy()
    {
        taskText.text = "☐ Spoopy boi is here to nibble your nipple";
        enemy.SetActive(true);
    }
}
