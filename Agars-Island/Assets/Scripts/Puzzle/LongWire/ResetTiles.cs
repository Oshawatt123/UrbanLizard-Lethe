using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTiles : MonoBehaviour
{
    private void OnMouseDown()
    {
        //Trigger total puzzle reset
        this.transform.parent.GetComponent<LongWireController>().ResetPuzzle();
    }
}
