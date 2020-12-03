﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UpdateWireMaterials : MonoBehaviour
{
    [SerializeField] private Material colour;

    private MoveWire moveWire;

    private void Start()
    {
        moveWire = GetComponent<MoveWire>();
    }

    // Update is called every time the scene updates
    void Update()
    {
        moveWire.wireBegin.GetComponent<Renderer>().material = colour;
        moveWire.wireBox.GetComponent<Renderer>().material = colour;
        moveWire.wireEnd.GetComponent<Renderer>().material = colour;
    }
}
