using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    private int numberOfShips = 10;
    private Vector3 chosenPosition;
    private bool isShipChosen = false;
    [SerializeField] ShipScript selectedShip;
    [SerializeField] Button nextButton;


    void Start()
    {
        selectedShip = null;
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
                    // Check if hiten object is equipped with ShipScript component
                    ShipScript ship = hit.collider.GetComponent<ShipScript>();
                    if (ship != null)
                    {
                        isShipChosen = true;
                        selectedShip = ship;
                        chosenPosition = hit.point;
                        ship.ChangeColor(hit.collider.gameObject, chosenPosition);
                    }
                }
            }

            if(isShipChosen)
            {
                if (hit.collider != null && hit.collider.CompareTag("Cell"))
                {
                    numberOfShips = numberOfShips - 1;
                    chosenPosition = hit.transform.position;
                    selectedShip.MoveShip(chosenPosition);
                    isShipChosen = false;
                }
            }
        }

        if(numberOfShips == 0)
        {
            DisplayButtonNext();
        }
    }

    public void DisplayButtonNext()
    {
        nextButton.gameObject.SetActive(true);

    }


}
