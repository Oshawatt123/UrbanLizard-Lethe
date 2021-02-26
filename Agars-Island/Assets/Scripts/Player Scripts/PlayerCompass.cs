﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCompass : MonoBehaviour 
{
    private Transform player;
    Vector3 vector;

    void Start() 
    {
        player = this.transform.root;
    }

    void Update () 
    {
        //Set Compass rotation equal to player rotation
        vector.z = player.eulerAngles.y;
        transform.localEulerAngles = vector;

    }

}