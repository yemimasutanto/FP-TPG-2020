using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 12;
    [SerializeField] private GameObject[,] grid;
    [SerializeField] private int rowValue;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private int rowsPerLevel = 20;

    private int streak = 1;
    private int numRows = 0;

    // Start is called before the first frame update
    void Start()
    {
        grid = new GameObject[width, height];
    }

    public void LandPiece(Group piece)
    {
        for (int i = 0; i < piece.blocks.Length; i++)
        {
            // Store each block in the grid.
            StoreBlockInGrid(piece.blocks[i]);
        }

        DeleteFullRows();

        if (gameManager)
        {
            gameManager.ConstructPiece();
        }
        CheckGameOver();
    }

    void StoreBlockInGrid(GameObject block)
    {
        Vector2 intPosition = RoundVector.RoundedVector(new Vector2(block.transform.position.x, block.transform.position.y));
        grid[(int)intPosition.x, (int)intPosition.y] = block;
    }

    public bool IsWithinAndEmpty(Vector2 boardPosition)
    {
        if (IsWithinBoard(boardPosition))
        {
            if (IsEmptySpot(boardPosition))
            {
                return true;
            }
        }
        return false;
    }
    
    bool IsEmptySpot(Vector2 boardPosition)
    {
        if (grid[(int)boardPosition.x, (int)boardPosition.y] == null)
        {
            return true;
        }
        return false;
    }

    bool IsWithinBoard(Vector2 boardPosition)
    {
        // Is it within the x positions?
        if (boardPosition.x >= 0 && boardPosition.x < width)
        {
            // Is it within the y positions?
            if (boardPosition.y >= 0 && boardPosition.y < height)
            {
                return true;
            }
        }
        return false;
    }

    void DecreaseRow(int row)
    {
        for (int i = 0; i < width; i++)
        {
            if (grid[i, row])
            {
                // Put the piece here into the space below
                grid[i, row - 1] = grid[i, row];

                // Make the previous space empty
                grid[i, row] = null;

                // Move the piece object
                grid[i, row - 1].gameObject.transform.position += Vector3.down;
            }
        }
    }

    void DecreaseRowsAbove(int row)
    {
        for (int i = row; i < height; i++)
        {
            DecreaseRow(i);
        }
    }

    void DeleteFullRows()
    {
        for (int i = 0; i < height; i++)
        {
            if (IsRowFull(i))
            {
                DeleteRow(i);
                DecreaseRowsAbove(i + 1);
                i--;
                scoreManager.IncreaseScore(rowValue * streak);
                streak += 1;
                numRows += 1;
            }
        }
        streak = 1;
        if(numRows / rowsPerLevel >= 1)
        {
            numRows = 0;
            gameManager.SpeedUp();
        }
    }

    void DeleteRow(int row)
    {
        for (int i = 0; i < width; i++)
        {
            // grid[i, row].SpecialEffect();
            Destroy(grid[i, row].gameObject);
            grid[i, row] = null;
        }
    }

    bool IsRowFull(int row)
    {
        for (int i = 0; i < width; i++)
        {
            if (!grid[i, row])
            {
                return false;
            }
        }
        return true;
    }

    void CheckGameOver()
    {
        for(int i = 0; i < width; i++)
        {
            if(grid[i, 12] != null)
            {
                gameManager.EndGame();
            }
        }
    }
}
