using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;

    public bool GeneratorFixed;
    public bool GeneratorOn;

    [SerializeField] private GameObject enemy;
    
    void Awake()
    {
        if (instance)
            Destroy(gameObject);
        else
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixGenerator()
    {
        GeneratorFixed = true;
    }

    public void ReleaseEnemy()
    {
        enemy.SetActive(true);
    }
}
