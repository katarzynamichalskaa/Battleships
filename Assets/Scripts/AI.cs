using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    private bool isAvailable = true;
    private List<Tuple<int, int>> usedIndices = new List<Tuple<int, int>>();
    [SerializeField] GameObject[,] enemyTiles = new GameObject[10, 10];

    void Start()
    {
        Generate(4, 1);
        Generate(3, 2);
        Generate(2, 3);
        Generate(1, 4);
    }

    void Generate(int shipsToGenerate, int shipSize)
    {
        for (int i = 0; i < shipsToGenerate; i++)
        {
            int x;
            int y;

            do
            {
                x = UnityEngine.Random.Range(0, 10);
                y = UnityEngine.Random.Range(0, 10);

            } while (usedIndices.Contains(new Tuple<int, int>(x, y)));

            usedIndices.Add(new Tuple<int, int>(x, y));

            UnityEngine.Debug.Log("Pozycja [" + x + "," + y + "]");

            if (shipSize > 1)
            {
                int direction;
                int nextX;
                int nextY;
                int pos = 2;

                do
                {
                    direction = UnityEngine.Random.Range(0, 4); // Losujemy kierunek (0 - góra, 1 - dół, 2 - lewo, 3 - prawo)
                    nextX = x;
                    nextY = y;

                    for (int j = 0; j < shipSize - 1; j++)
                    {
                        if (direction == 0 && nextY < 9) // Góra
                        {
                            nextY++;
                        }
                        else if (direction == 1 && nextY > 0) // Dół
                        {
                            nextY--;
                        }
                        else if (direction == 2 && nextX > 0) // Lewo
                        {
                            nextX--;
                        }
                        else if (direction == 3 && nextX < 9) // Prawo
                        {
                            nextX++;
                        }

                        if (nextX < 0 || nextX >= 10 || nextY < 0 || nextY >= 10 || usedIndices.Contains(new Tuple<int, int>(nextX, nextY)))
                        {
                            break;
                        }

                        UnityEngine.Debug.Log("Pozycja " + pos + " [" + nextX + "," + nextY + "]");
                        pos++;
                    }
                } while (nextX < 0 || nextX >= 10 || nextY < 0 || nextY >= 10 || usedIndices.Contains(new Tuple<int, int>(nextX, nextY)));
            }
        }
    }
}
