﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongWireController : MonoBehaviour
{
    //List of all Key tiles
    public List<GameObject> KeyWireTiles;
    public List<GameObject> AllWireTiles;
    public bool PuzzleComplete;
    public GameObject CompleteIndic; 

    public void CheckPuzzle()
    {
        //Check each important tile in puzzle
        for (int i = 0; i < KeyWireTiles.Count; i++)
        {
            GameObject Tile = KeyWireTiles[i];
            TileScript CurrentTileScript = Tile.GetComponent<TileScript>();
            //Get each Desired Tile Rotation
            for (int r = 0; r < CurrentTileScript.DesiredRotations.Count; r++)
            {
                float TileRot = CurrentTileScript.DesiredRotations[r];
                Debug.Log("Tile" + i + "= " + TileRot);
                //If rotation is equal to desired tile rotation
                if (TileRot == Tile.transform.localRotation.eulerAngles.y)
                {
                    //Check if on final tile
                    if (i == KeyWireTiles.Count - 1)
                    {
                        PuzzleComplete = true;
                        CompleteIndic.SetActive(true);
                        return;
                    }
                }
                //If tile is last desired rotation, stop checking puzzle as impossible to be completed
                else if(r == CurrentTileScript.DesiredRotations.Count - 1)
                {
                    PuzzleComplete = false;
                    CompleteIndic.SetActive(false);
                    return;
                }
            }
        }
    }

    public void ResetPuzzle()
    {
        foreach(GameObject Tile in AllWireTiles)
        {
            Tile.GetComponent<TileScript>().ResetTile();
        }
    }
}
