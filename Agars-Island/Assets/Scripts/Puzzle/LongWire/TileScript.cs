using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public List<int> DesiredRotations;
    public Quaternion StartRotation;

    private void Start()
    {
        StartRotation = this.transform.rotation;
    }

    //Set Up Rotation
    private void OnMouseDown()
    {
        RotateTile();
    }

    public void RotateTile()
    {
        this.transform.Rotate(0, 90, 0);
        //Check overall puzzle completion
        this.transform.root.GetComponent<LongWireController>().CheckPuzzle();
    }

    public void ResetTile()
    {
        this.transform.rotation = StartRotation;
    }
}
