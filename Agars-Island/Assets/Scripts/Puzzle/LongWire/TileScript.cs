using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public int DesiredRotation;
    private Quaternion Rotator = new Quaternion(0, 90, 0, 0);

    //Set Up Rotation
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            RotateTile();
        }
    }

    public void RotateTile()
    {
        this.transform.Rotate(0, 90, 0);
        //Check overall puzzle completion
        this.transform.root.GetComponent<LongWireController>().CheckPuzzle();
    }
}
