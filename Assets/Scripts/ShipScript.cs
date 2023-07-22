using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ShipScript : MonoBehaviour
{

    public enum ShipType
    {
        OneCellShip,
        TwoCellsShip,
        ThreeCellsShip,
        FourCellsShip,
    }

    private ShipType type;
    private bool isShipPlaced = false;
    private int ListIndex = 0;
    private bool isRotated = false;
    private int Counter = 0;
    [SerializeField] float xOffset;
    [SerializeField] float xOffsetRotated;
    [SerializeField] float yOffset;
    [SerializeField] GameManager gameManager;
    [SerializeField] TileScript selectedTile;


    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void ChangeColor(GameObject obj, ShipType type)
    {
        isShipPlaced = false;

        if (type == ShipType.OneCellShip)
        {
            Color customColor = new Color(0.83f, 0.68f, 0.46f, 0.8f);
            StartCoroutine(Blink(obj.GetComponent<Renderer>().material, customColor));
        }
        else if (type == ShipType.TwoCellsShip)
        {
            Color customColor = new Color(0.71f, 0.40f, 0.16f, 1.0f);
            StartCoroutine(Blink(obj.GetComponent<Renderer>().material, customColor));
        }
        else if (type == ShipType.ThreeCellsShip)
        {
            Color customColor = new Color(0.2f, 0.6f, 0.4f, 1.0f);
            StartCoroutine(Blink(obj.GetComponent<Renderer>().material, customColor));
        }
        else 
        {
            Color customColor = new Color(0.2f, 0.4f, 0.6f, 0.8f);
            StartCoroutine(Blink(obj.GetComponent<Renderer>().material, customColor));
        }
    }

    public void MoveShip(Vector3 position, ShipType type)
    {

        transform.position = new Vector3(position.x + xOffset, position.y, position.z);

        //add offset if the second and fourth one is rotated
        if(isRotated && type == ShipType.FourCellsShip)
        {
            transform.position = new Vector3(position.x + xOffsetRotated, position.y + yOffset, position.z);
        }

        if (isRotated && type == ShipType.TwoCellsShip)
        {
            transform.position = new Vector3(position.x + xOffsetRotated, position.y + yOffset, position.z);
        }

        //LOGIC IF SHIPS ARE ROTATED
        if (isRotated && type == ShipType.FourCellsShip && position.y <= 4.0f && position.y >= 2.5f)
        {
            transform.position = new Vector3(position.x, position.y - 1.3f, position.z);
        }
        else if (isRotated && type == ShipType.FourCellsShip && position.y <= -3.0f && position.y >= -5.0f)
        {
            transform.position = new Vector3(position.x, position.y + 1.3f, position.z);
        }

        else if (isRotated && type == ShipType.ThreeCellsShip && position.y <= 4.0f && position.y >= 2.5f)
        {
            transform.position = new Vector3(position.x, position.y - 1.1f, position.z);
        }
        else if (isRotated && type == ShipType.ThreeCellsShip && position.y <= -3.0f && position.y >= -5.0f)
        {
            transform.position = new Vector3(position.x, position.y + 1.0f, position.z);
        }

        else if (isRotated && type == ShipType.TwoCellsShip && position.y <= -3.0f && position.y >= -5.0f)
        {
            transform.position = new Vector3(position.x, position.y + 0.55f, position.z);
        }

        else if (isRotated && type == ShipType.TwoCellsShip && position.y <= 4.0f && position.y >= 2.5f)
        {
            transform.position = new Vector3(position.x, position.y - 0.5f, position.z) ;
        }



        //LOGIC IF SHIPS AREN'T ROTATED
        if (!isRotated && type == ShipType.FourCellsShip && position.x <= 7.0f && position.x >= 5.5f)
        {
            transform.position = new Vector3(position.x - 1.2f, position.y, position.z);
        }

        else if (!isRotated && type == ShipType.FourCellsShip && position.x <= -1.0f && position.x >= -2.0f)
        {
            transform.position = new Vector3(position.x + 1.2f, position.y, position.z);
        }

        else if (!isRotated && type == ShipType.ThreeCellsShip && position.x <= 7.0f && position.x >= 5.5f)
        {
            transform.position = new Vector3(position.x - 0.9f, position.y, position.z);
        }

        else if (!isRotated && type == ShipType.ThreeCellsShip && position.x <= -1.0f && position.x >= -2.0f)
        {
            transform.position = new Vector3(position.x + 0.9f, position.y, position.z);
        }

        else if (!isRotated && type == ShipType.ThreeCellsShip && position.x <= -1.0f && position.x >= -2.0f)
        {
            transform.position = new Vector3(position.x + 0.6f, position.y, position.z);
        }

        if (!isRotated && type == ShipType.TwoCellsShip && position.x <= 7.0f && position.x >= 5.5f)
        {
            transform.position = new Vector3(position.x - 0.5f, position.y, position.z);
        }

        isShipPlaced = true;
    }


    public ShipType CheckType(GameObject ship)
    {
        if (ship.CompareTag("1cell_ship"))
        {
            return ShipType.OneCellShip;
        }
        else if (ship.CompareTag("2cells_ship"))
        {
            return ShipType.TwoCellsShip;
        }
        else if (ship.CompareTag("3cells_ship"))
        {
            return ShipType.ThreeCellsShip;
        }
        else
        {
            return ShipType.FourCellsShip;
        }
    }

    private IEnumerator Blink(Material material, Color blinkColor)
    {
        Color originalColor = material.color;
        float blinkDuration = 0.2f; 

        while (!isShipPlaced)
        {
            material.color = blinkColor;
            yield return new WaitForSeconds(blinkDuration);
            material.color = originalColor;
            yield return new WaitForSeconds(blinkDuration);
        }
    }

    public void RotateShip()
    {
        Counter++;

        if (Counter == 2)
        {
            transform.Rotate(Vector3.forward, 90f);
            isRotated = false;
            Counter = 0;
        }
        else
        {
            transform.Rotate(Vector3.forward, -90f);
            isRotated = true;
        }
    }

    public int ReturnIfRotated()
    {
        return Counter + 1;
    }

}
