using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{

    public enum ShipType
    {
        OneCellShip,
        TwoCellsShip,
        ThreeCellsShip,
        FourCellsShip,
        Unknown
    }

    private List<Vector3> shipPositions = new List<Vector3>();
    private ShipType type;
    private bool isShipPlaced = false;
    private int ListIndex = 0;
    [SerializeField] float xOffset;
    [SerializeField] GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void ChangeColor(GameObject obj, Vector3 position)
    {
        type = CheckType(obj);
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
        else if (type == ShipType.FourCellsShip)
        {
            Color customColor = new Color(0.2f, 0.4f, 0.6f, 0.8f);
            StartCoroutine(Blink(obj.GetComponent<Renderer>().material, customColor));
        }
        else
        {
            Debug.Log("Unknown ship");
        }
    }
    public void MoveShip(Vector3 position)
    {
        transform.position = new Vector3(position.x + xOffset, position.y, position.z);
        isShipPlaced = true;
    }


    private ShipType CheckType(GameObject ship)
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
        else if (ship.CompareTag("4cells_ship"))
        {
            return ShipType.FourCellsShip;
        }
        else
        {
            return ShipType.Unknown;
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

}
