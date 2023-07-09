using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IsInside : MonoBehaviour
{
    public Transform CheckIfAnyInside;
    public Transform Ship;
    private int ShipsCounter;
    private int MaxShips;
    public Text text;
    private bool isShipMoved = false;

    void Start()
    {
        CheckIfAnyInside.position = Ship.position;
        ShipsCounter = MaxShips;
    }

    void Update()
    {
        IsInTargetArea(CheckIfAnyInside, Ship);
        text.text = "x" + ShipsCounter.ToString();
    }


    private void IsInTargetArea(Transform CheckIfAnyInside, Transform Ship)
    {
        float targetPosition = CheckIfAnyInside.position.x;
        float shipPosition = Ship.position.x;


        if(shipPosition == targetPosition)
        {
            if(Ship.CompareTag("4cells_ship"))
            {
                MaxShips = 1;
                ShipsCounter = 1;
            }
            if (Ship.CompareTag("3cells_ship"))
            {
                MaxShips = 2;

                ShipsCounter = ShipsCounter + 1;

                if(ShipsCounter > 2)
                {
                    ShipsCounter = 2;
                }
            }
            if (Ship.CompareTag("2cells_ship"))
            {
                MaxShips = 3;
                ShipsCounter = ShipsCounter + 1;

                if (ShipsCounter > 3)
                {
                    ShipsCounter = 3;
                }
            }
            if (Ship.CompareTag("1cell_ship"))
            {
                MaxShips = 4;
                ShipsCounter = ShipsCounter + 1;

                if (ShipsCounter > 4)
                {
                    ShipsCounter = 4;
                }
            }

            isShipMoved = false;

        }

        else if (shipPosition != targetPosition && !isShipMoved)
        {
            ShipsCounter = ShipsCounter - 1;

            if (ShipsCounter < 0)
            {
                ShipsCounter = 0;
            }
            isShipMoved = true;

        }

    }


}
