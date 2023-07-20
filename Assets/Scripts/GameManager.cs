using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    private int numberOfShips = 10;
    private Vector3 chosenPosition;
    private bool isShipChosen = false;
    private bool isItFirstPlacement = true;
    private int rotationCounter = 0;
    [SerializeField] ShipScript.ShipType type;
    [SerializeField] ShipScript selectedShip;
    [SerializeField] TileScript selectedTile;
    [SerializeField] Button nextButton;

    void Start()
    {
        selectedShip = null;
        selectedTile = null;
        nextButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (!isShipChosen)
            {
                if (hit.collider != null)
                {
                    ShipScript ship = hit.collider.GetComponent<ShipScript>();


                    if (ship != null)
                    {
                        if (hit.point.x >= -7.9f && hit.point.x <= -3f && hit.point.y >= -6f && hit.point.y <= 4f)
                        {
                            isItFirstPlacement = true;
                            InitializeNewPosition(ship, hit);
                        }

                        else
                        {
                            isItFirstPlacement = false;
                            InitializeNewPosition(ship, hit);
                        }
                    }
                }
            }

            if(isShipChosen)
            {
                if (hit.collider != null && hit.collider.CompareTag("Cell"))
                {
                    //tile logic
                    TileScript tile = hit.collider.GetComponent<TileScript>();
                    selectedTile = tile;
                    tile.CheckIfChosen(hit.collider.gameObject, isItFirstPlacement);

                    //ship stuff
                    chosenPosition = hit.transform.position;
                    selectedShip.MoveShip(chosenPosition);
                    isShipChosen = false;

                    if(isItFirstPlacement)
                    {
                        numberOfShips = numberOfShips - 1;
                    }
                }

            }
        }

        //rotate ship
        if (Input.GetKeyDown(KeyCode.X))
        {
            rotationCounter++;
            UnityEngine.Debug.Log("X key pressed!");
            selectedShip.RotateShip(rotationCounter);

            if(rotationCounter == 4)
            {
                rotationCounter = 0;
            }
        }

        if (numberOfShips == 0)
        {
            DisplayButtonNext();
        }
    }



    public void DisplayButtonNext()
    {
        nextButton.gameObject.SetActive(true);
    }

    private void InitializeNewPosition(ShipScript ship, RaycastHit2D hit)
    {
        isShipChosen = true;
        selectedShip = ship;
        chosenPosition = hit.point;

        //get a type of ship
        type = ship.CheckType(hit.collider.gameObject);

        //initialize shining during click 
        ship.ChangeColor(hit.collider.gameObject, chosenPosition, type);
        
        UnityEngine.Debug.Log(type);


    }


}
