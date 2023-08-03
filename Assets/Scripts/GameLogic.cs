using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameLogic : MonoBehaviour
{
    TileScript selectedTile;
    private int Turn = 1;
    private bool isTileChoosen = false;
    [SerializeField] GameObject fire;
    [SerializeField] List<GameObject> playerTiles = new List<GameObject>();
    [SerializeField] List<GameObject> playerShips = new List<GameObject>();
    Dictionary<string, List<GameObject>> shipDictionary = new Dictionary<string, List<GameObject>>();

    // Start is called before the first frame update
    void Start()
    {
        selectedTile = null;
        GameObject[] cells = GameObject.FindGameObjectsWithTag("Cell");
        playerTiles.AddRange(cells);
        ShipFinding();
    }

    // Update is called once per frame
    void Update()
    {
        if (Turn == 1)
        {
            SetActive(playerTiles);
            SetActive(playerShips);

            if(!isTileChoosen)
            {
                ChooseTile();
            }


        }

        if (Turn == 2)
        {

        }


    }

    void SetActive(List<GameObject> list)
    {
        foreach (GameObject gameObject in list)
        {
            gameObject.SetActive(false);
        }
    }

    void ShipFinding()
    {
        shipDictionary.Add("1cell_ship", new List<GameObject>());
        shipDictionary.Add("2cells_ship", new List<GameObject>());
        shipDictionary.Add("3cells_ship", new List<GameObject>());
        shipDictionary.Add("4cells_ship", new List<GameObject>());

        GameObject[] allShips = FindObjectsOfType<GameObject>();

        foreach (GameObject ship in allShips)
        {
            string shipTag = ship.tag;

            if (shipDictionary.ContainsKey(shipTag))
            {
                shipDictionary[shipTag].Add(ship);
            }
        }

        foreach (List<GameObject> shipsList in shipDictionary.Values)
        {
            playerShips.AddRange(shipsList);
        }
    }

    void ChooseTile()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("EnemyCell"))
            {
                TileScript tile = hit.collider.GetComponent<TileScript>();
                selectedTile = tile;
                isTileChoosen = true;

                if (selectedTile.ReturnAvailability() == true)
                {
                    Instantiate(fire, selectedTile.transform.position, Quaternion.identity);
                }

                else
                {
                    UnityEngine.Debug.Log("You missed!");

                }

                Turn = 2;
            }

            else
            {
                isTileChoosen = false;
            }
        }
    }
}
