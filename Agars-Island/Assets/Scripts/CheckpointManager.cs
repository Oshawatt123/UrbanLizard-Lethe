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
    
    [SerializeField] private TextMeshProUGUI taskCompText;
    private Animator taskCompAnim;
    private TextMeshProUGUI newTaskText;
    
    [SerializeField] private GameObject Player;

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
        taskCompAnim = taskCompText.gameObject.GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FlashlightGot()
    {
        taskCompText.text = "|COMPLETE| Find a light source";
        taskCompAnim.SetTrigger("FadeIn");
        
        taskText.text = "☐ Fix the backup generator";
    }

    public void GeneratorFix()
    {
        taskCompText.text = "|COMPLETE| Fix the backup generator";
        taskCompAnim.SetTrigger("FadeIn");
        
        taskText.text = "☐ Turn on the generator in the security room";
        GeneratorFixed = true;
    }

    public void ReleaseEnemy()
    {
        taskCompText.text = "|COMPLETE| Turn on the generator in the security room";
        taskCompAnim.SetTrigger("FadeIn");
        
        taskText.text = "☐ Spoopy boi is here to nibble your nipple";
        enemy.SetActive(true);
        GeneratorOn = true;
        Player.GetComponent<PlayerSanity>().enabled = true;
    }
}
