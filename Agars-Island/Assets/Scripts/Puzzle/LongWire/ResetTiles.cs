﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTiles : MonoBehaviour
{
    private void OnMouseDown()
    {
        this.transform.parent.GetComponent<LongWireController>().ResetPuzzle();
    }
}
