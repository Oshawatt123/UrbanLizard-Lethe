using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongWireController : MonoBehaviour
{
    //List of all Key tiles
    public List<GameObject> WireTiles;
    public bool PuzzleComplete;

    // Update is called once per frame
    void Update()
    { 

    }

    public void CheckPuzzle()
    {
        //Check each important tile in puzzle
        for (int i = 0; i < WireTiles.Count; i++)
        {
            GameObject Tile = WireTiles[i];
            //Get Desired Tile Rotation
            int TileRot = Tile.GetComponent<TileScript>().DesiredRotation;
            //If rotation is equal to desired tile rotation
            if (TileRot == Tile.transform.localRotation.y)
            {
                //Check if on final tile
                if (i == WireTiles.Count - 1)
                {
                    PuzzleComplete = true;
                }
            }
            //If tile is not in correct rotation, stop checking puzzle as impossible to be completed
            else
            {
                PuzzleComplete = false;
                break;
            }
        }
    }
}
