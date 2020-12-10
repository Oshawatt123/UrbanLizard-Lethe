﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GasParticleScript : MonoBehaviour
{

    public ParticleSystem particleSystem;

    private float emmissionRate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var emission = particleSystem.emission;
        emission.rateOverTime = transform.localScale.x * 5;
    }
}
