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
    private bool isPositionAccepted;
    private int arrayIndex = 0;
    private List<Vector3> selectedTileList = new List<Vector3>();
    [SerializeField] ShipScript.ShipType type;
    [SerializeField] ShipScript selectedShip;
    [SerializeField] TileScript selectedTile;
    [SerializeField] Button nextButton;
    [SerializeField] Button textChangePosition;

    void Start()
    {
        selectedShip = null;
        selectedTile = null;
        nextButton.gameObject.SetActive(false);
        textChangePosition.gameObject.SetActive(false);
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
                            InitializeNewPosition(ship, hit, isItFirstPlacement);
                        }

                        else
                        {
                            isItFirstPlacement = false;
                            selectedTileList.Clear();

                            InitializeNewPosition(ship, hit, isItFirstPlacement);
                        }
                    }
                }
            }

            if (isShipChosen)
            {
                if (hit.collider != null && hit.collider.CompareTag("Cell"))
                {
                    //tile logic
                    TileScript tile = hit.collider.GetComponent<TileScript>();
                    selectedTile = tile;

                    isPositionAccepted = tryAddToArray(hit.transform.position);

                    if (isPositionAccepted)
                    //ship logic
                    {
                        chosenPosition = hit.transform.position;
                        selectedShip.MoveShip(chosenPosition, type);
                        isShipChosen = false;
                    }

                    else
                    {
                        textChangePosition.gameObject.SetActive(true);
                        Invoke("HideTextChangePosition", 1.0f);
                    }

                    if (isItFirstPlacement && isPositionAccepted)
                    {
                        numberOfShips = numberOfShips - 1;
                    }

                }

            }
        }

        //rotate ship
        if (Input.GetKeyDown(KeyCode.X) && isShipChosen)
        {
            selectedShip.RotateShip();
        }

        if (numberOfShips == 0)
        {
            DisplayButtonNext();
        }
    }

    private void HideTextChangePosition()
    {
        textChangePosition.gameObject.SetActive(false);
    }

    public void DisplayButtonNext()
    {
        nextButton.gameObject.SetActive(true);
    }

    private void InitializeNewPosition(ShipScript ship, RaycastHit2D hit, bool isItFirstPlacement)
    {
        isShipChosen = true;
        selectedShip = ship;
        chosenPosition = hit.point;

        //get a type of ship
        type = ship.CheckType(hit.collider.gameObject);


        //initialize shining during click 
        ship.ChangeColor(hit.collider.gameObject, chosenPosition, type);

        //if it isn't first placement then remove position
        if(isItFirstPlacement)
        {
            selectedTileList.Remove(ship.transform.position);
            selectedTileList.Add(chosenPosition);
        }

    }

    private bool tryAddToArray(Vector3 tilePosition)
    {
        if (selectedTileList.Contains(tilePosition))
        {
            return false;
        }

        selectedTileList.Add(tilePosition);
        return true;

    }


}
