using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceMorseButton : MonoBehaviour
{
    [SerializeField] private MorseCodeReader reader;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseUpAsButton()
    {
        reader.AddSpace();
    }
}
