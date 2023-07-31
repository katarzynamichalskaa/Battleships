using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    [SerializeField] GameObject[] ships;
    [SerializeField] bool isAvailable;
    bool anyObjectCollides = false;
    private bool once = true;

    void Update()
    {
        CheckCollision();
    }

    void CheckCollision()
    {
        Physics.SyncTransforms();

        foreach (GameObject obj in ships)
        {    
            if (Physics2D.OverlapBox(transform.position, transform.localScale, 0f) == obj.GetComponent<Collider2D>())
            {
                isAvailable = false;
                break;
            }
            else
            {
                isAvailable = true;
            }
           
        }
    }

    public bool ReturnAvailability()
    {
        return isAvailable;
    }

    public void SetAvailability()
    {
        isAvailable = false;
    }
    public void ResetAvailability()
    {
        isAvailable = true;
    }

}
