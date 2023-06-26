using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public static Board Instance { get; private set; }
    public Transform board;
    public GridLayoutGroup girdLayout;
    public GameObject cellPrefab;
    public string currentTurn = "x";
    public int boardSize;
    private bool result = false;

    private string[,] matrix;

    private void Awake()
    {
        if (Instance != null && Instance == this)
        {
            Destroy(this);
        }
        Instance = this;
    }
    private void Start()
    {
        matrix = new string[boardSize, boardSize];
        girdLayout.constraintCount = boardSize;
        CreateBoard();
    }

    void CreateBoard()
    {
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                Instantiate(cellPrefab, board); // Cell born out in Board
                Cell.Instance.row = i;
                Cell.Instance.column = j;
                matrix[i, j] = null;
            }

        }
    }

    public bool CheckCell(int row, int column)
    {
        bool hasValues;
        if(matrix[row,column]==null)
        {
            hasValues = true;
        }
        else
        {
            hasValues = false;
        }
        return hasValues;
    }

    public bool Check(int row, int column)
    {
        if (CheckCell(row,column))
        {
            matrix[row, column] = currentTurn;

            //Check column
            int count = 0;
            for (int i = row - 1; i >= 0; i--)  // Check up
            {
                if (matrix[i, column] == currentTurn)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            for (int i = row + 1; i < boardSize; i++)  // Check down
            {
                if (matrix[i, column] == currentTurn)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            if (count >= 4)
            {
                result = true;
            }

            count = 0;
            for (int j = column + 1; j < boardSize; j++)  // Check right
            {
                if (matrix[row, j] == currentTurn)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            for (int j = column - 1; j >= 0; j--)  // Check left
            {
                if (matrix[row, j] == currentTurn)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            if (count >= 4)
            {
                result = true;
            }

            count = 0;
            for (int j = column + 1; j < boardSize; j++)  // Check cheo len phai
            {
                if (matrix[row + (column - j), j] == currentTurn)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            for (int j = column - 1; j >= 0; j--)  // Check cheo duoi trai
            {
                if (matrix[row + (column - j), j] == currentTurn)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            if (count >= 4)
            {
                result = true;
            }

            count = 0;
            for (int j = column - 1; j >= 0; j--)  // Check cheo len trai
            {
                if (matrix[row - (column - j), j] == currentTurn)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            for (int j = column + 1; j < boardSize; j++)  // Check cheo duoi phai
            {
                if (matrix[row - (column - j), j] == currentTurn)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            if (count >= 4)
            {
                result = true;
            }
        }
        return result;
    }
}
