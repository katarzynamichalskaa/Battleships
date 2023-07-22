using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    private int numberOfShips = 10;
    private Vector3 chosenPosition;
    private bool isShipChosen = false;
    private bool isPositionAccepted;
    private bool isPositionAcceptedAfterRemoving;
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

        nextButton = GameObject.Find("NextButton").GetComponent<Button>();
        textChangePosition = GameObject.Find("ChangePosition").GetComponent<Button>();

        nextButton.gameObject.SetActive(false);
        textChangePosition.gameObject.SetActive(false);
    }

    private void Update()
    {

        if(nextButton == null || textChangePosition == null)
        {
            nextButton = GameObject.Find("NextButton").GetComponent<Button>();
            textChangePosition = GameObject.Find("ChangePosition").GetComponent<Button>();
        }

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
                            InitializeNewPosition(ship, hit);
                        }

                        else
                        {
                            UnityEngine.Debug.Log("You can't change position of your ship already!");
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
                        numberOfShips = numberOfShips - 1;
                        isShipChosen = false;
                    }

                    else
                    {
                        textChangePosition.gameObject.SetActive(true);
                        Invoke("HideTextChangePosition", 1.0f);
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

    private void InitializeNewPosition(ShipScript ship, RaycastHit2D hit)
    {
        isShipChosen = true;
        selectedShip = ship;
        chosenPosition = hit.point;

        //get a type of ship
        type = ship.CheckType(hit.collider.gameObject);


        //initialize shining during click 
        ship.ChangeColor(hit.collider.gameObject, type);

    }

    private bool tryAddToArray(Vector3 tilePosition)
    {
        UnityEngine.Debug.Log(type);

        if (type == ShipScript.ShipType.OneCellShip)
        {
            if (selectedTileList.Contains(tilePosition))
            {
                return false;
            }

            selectedTileList.Add(tilePosition);

            return true;
        }

        else if (type == ShipScript.ShipType.TwoCellsShip)
        {
            UnityEngine.Debug.Log("nana");

            int Rotated = selectedShip.ReturnIfRotated();

            UnityEngine.Debug.Log(Rotated);


            if (Rotated == 1)
            {

                if (selectedTileList.Contains(tilePosition))
                {
                    return false;
                }

                selectedTileList.Add(tilePosition);
                selectedTileList.Add(tilePosition + new Vector3(0.9f, 0f, 0f));

                return true;
            }

            else
            {
                if (selectedTileList.Contains(tilePosition))
                {
                    return false;
                }

                selectedTileList.Add(tilePosition);
                selectedTileList.Add(tilePosition + new Vector3(0f, 0.9f, 0f));

                return true;
            }

            return false;
        }



        return false;



    }


}
