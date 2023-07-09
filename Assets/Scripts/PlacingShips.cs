using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingShips : MonoBehaviour
{
    public List<Transform> targetAreas;
    private Vector3 initialPosition;
    private Vector3 dragOffset;
    private bool isDragging = false;

    private void Start()
    {
        initialPosition = transform.position;
    }

    void OnMouseDown()
    {
        isDragging = true;
        dragOffset = transform.position - GetMousePos();
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetMousePos() + dragOffset;
        }
    }

    private void OnMouseUp()
    {
        if (IsInAnyTargetArea())
        {
            Debug.Log("Statek umieszczony w odpowiednim miejscu!");
        }
        else
        {
            transform.position = initialPosition;
        }
    }

    private bool IsInAnyTargetArea()
    {
        foreach (Transform targetArea in targetAreas)
        {
            if (IsInTargetArea(targetArea))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsInTargetArea(Transform targetArea)
    {
        Vector3 targetPosition = targetArea.position;
        Vector3 shipPosition = transform.position;
        float targetWidth = targetArea.localScale.x;
        float targetHeight = targetArea.localScale.y;

        bool isInsideX = shipPosition.x >= targetPosition.x - targetWidth / 2f && shipPosition.x <= targetPosition.x + targetWidth / 2f;
        bool isInsideY = shipPosition.y >= targetPosition.y - targetHeight / 2f && shipPosition.y <= targetPosition.y + targetHeight / 2f;
        bool isOnEdgeX = Mathf.Approximately(shipPosition.x, targetPosition.x - targetWidth / 2f) || Mathf.Approximately(shipPosition.x, targetPosition.x + targetWidth / 2f);
        bool isOnEdgeY = Mathf.Approximately(shipPosition.y, targetPosition.y - targetHeight / 2f) || Mathf.Approximately(shipPosition.y, targetPosition.y + targetHeight / 2f);

        return (isInsideX && isInsideY) || (isOnEdgeX && isInsideY) || (isInsideX && isOnEdgeY);
    }


    Vector3 GetMousePos()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}
