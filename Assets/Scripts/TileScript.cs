using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    private Vector3[] selectedTileArray;
    private static int arrayIndex;

    void Start()
    {
        arrayIndex = 0;
        selectedTileArray = new Vector3[20];
    }


    public void CheckIfChosen(GameObject tile, bool isItFirstPlacement)
    {
        if (isItFirstPlacement)
        {
            selectedTileArray[arrayIndex] = tile.transform.position;
            arrayIndex++;
        }
        
        else
        {
            arrayIndex--;
            selectedTileArray[arrayIndex] = tile.transform.position;
            arrayIndex++;
        }
    }
}
