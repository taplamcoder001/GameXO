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
    public bool result = false;

    private List<Vector2> coordinates;
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
        matrix = new string[boardSize + 1, boardSize + 1];
        coordinates = new List<Vector2>();
        girdLayout.constraintCount = boardSize;
        CreateBoard();
    }
    void CreateBoard()
    {
        for (int i = 1; i <= boardSize; i++)
        {
            for (int j = 1; j <= boardSize; j++)
            {
                Instantiate(cellPrefab, board); // Cell born out in Board
                Cell.Instance.row = i;
                Cell.Instance.column = j;
                matrix[i, j] = null;
            }

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Undo();
        }
    }

    public void Undo()
    {
        if (coordinates.Count <= 0)
        {
            Debug.Log("Not run");
        }
        else
        {
            matrix[(int)coordinates[coordinates.Count - 1][0], (int)coordinates[coordinates.Count - 1][1]] = null;
            if (Cell.Instance.row == (int)coordinates[coordinates.Count - 1][0] && Cell.Instance.column == (int)coordinates[coordinates.Count - 1][1])
            {
                Cell.Instance.ChangeImage("null");
            }
            Debug.Log(Cell.Instance.row);
        }
    }

    public bool CheckCell(int row, int column)
    {
        bool hasValues;
        if (matrix[row, column] == null)
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
        if (CheckCell(row, column))
        {
            matrix[row, column] = currentTurn;         /// Save name string currentTurn
            coordinates.Add(new Vector2(row, column)); /// Save variable coordinates for undo

            //Check column
            int count = 0;
            for (int i = row - 1; i >= 1; i--)  // Check up
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
            for (int i = row + 1; i <= boardSize; i++)  // Check down
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
            for (int j = column + 1; j <= boardSize; j++)  // Check right
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
            for (int j = column - 1; j >= 1; j--)  // Check left
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
            for (int j = column + 1; j <= boardSize; j++)  // Check cheo len phai
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
            for (int j = column - 1; j >= 1; j--)  // Check cheo duoi trai
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
            for (int j = column - 1; j >= 1; j--)  // Check cheo len trai
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
            for (int j = column + 1; j <= boardSize; j++)  // Check cheo duoi phai
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
