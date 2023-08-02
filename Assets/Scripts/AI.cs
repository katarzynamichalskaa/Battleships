using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AI : MonoBehaviour
{
    private List<Tuple<int, int>> usedIndices = new List<Tuple<int, int>>();
    private List<Tuple<int, int>> temporaryIndices = new List<Tuple<int, int>>();
    [SerializeField] GameObject[,] enemyTiles = new GameObject[10, 10];

    void Start()
    {
        Generate(4, 1); //generate 4 one-tile-sized ships
        Generate(3, 2); //generate 3 two-tiles-sized ships
        Generate(2, 3); //generate 2 three-tiles-sized ships
        Generate(1, 4); //generate 1 four-tiles-sized ship

        foreach (var position in usedIndices)
        {
            UnityEngine.Debug.Log("Pozycja [" + position.Item1 + "," + position.Item2 + "]");
        }
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

            temporaryIndices.Clear();

            if (shipSize > 1)
            {
                int direction;
                int nextX;
                int nextY;
                bool isValidPosition;

                do
                {
                    direction = UnityEngine.Random.Range(0, 4); //generate random direction (0 - up, 1 - down, 2 - left, 3 - right)
                    nextX = x;
                    nextY = y;
                    isValidPosition = true;

                    for (int j = 0; j < shipSize - 1; j++)
                    {
                        if (direction == 0 && nextY < 9) //up
                        {
                            nextY++;
                        }
                        else if (direction == 1 && nextY > 0) //down
                        {
                            nextY--;
                        }
                        else if (direction == 2 && nextX > 0) //left
                        {
                            nextX--;
                        }
                        else if (direction == 3 && nextX < 9) // right
                        {
                            nextX++;
                        }
                        else
                        {
                            temporaryIndices.Clear();
                            isValidPosition = false;
                            break;
                        }

                        if (usedIndices.Contains(new Tuple<int, int>(nextX, nextY)))
                        {
                            temporaryIndices.Clear();
                            isValidPosition = false;
                            break;
                        }

                        temporaryIndices.Add(new Tuple<int, int>(nextX, nextY));

                    }
                } while (!isValidPosition);
            }

            usedIndices.AddRange(temporaryIndices);
        }
    }
}
