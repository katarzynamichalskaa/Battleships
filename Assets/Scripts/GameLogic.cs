using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameLogic : MonoBehaviour
{
    private int Turn = 1;
    [SerializeField] GameObject playerTiles;
    [SerializeField] GameObject playerShips;


    // Start is called before the first frame update
    void Start()
    {

        
            playerTiles = GameObject.Find("Tiles");
            playerShips = GameObject.Find("ShipsToPlace");
        
  
    }

    // Update is called once per frame
    void Update()
    {



            while (Turn == 1)
            {
                playerTiles.SetActive(false);
                playerShips.SetActive(false);
                //Turn = 0;
            }
        

    }
}
