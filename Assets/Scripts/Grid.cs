using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 12;
    [SerializeField] private Group[,] grid;
    // Start is called before the first frame update
    void Start()
    {
        grid = new Group[width, height];
        Group example = grid[2, 3];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsWithinBoard(Vector2 boardPosition)
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
        for (int i = 0; i < height; i++)
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
            }
        }
    }

    void DeleteRow(int row)
    {
        for (int i = 0; i < width; i++)
        {
            // grid[i, row].SpecialEffect();
            Destroy(grid[i, row].gameObject);
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
}
