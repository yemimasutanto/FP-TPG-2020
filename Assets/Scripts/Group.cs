using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    [SerializeField] public GameObject[] blocks;
    private bool isActive = true;
    private Grid grid;

    public void Setup(Grid newGrid)
    {
        grid = newGrid;
    }

    void Move(Vector2 moveDirection)
    {
        transform.position += new Vector3(moveDirection.x, moveDirection.y, 0);
        Vector2 tempVector = RoundVector.RoundedVector(new Vector2(transform.position.x, transform.position.y));
        transform.position = new Vector3(tempVector.x, tempVector.y);
    }

    public void GameManagerMove(Vector2 moveDirection)
    {
        Move(moveDirection);
        if (!isValidPosition())
        {
            Move(-moveDirection);
            // Tell the grid to land the piece.
            grid.LandPiece(this);
            isActive = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        FillBlocks();
    }

    void FillBlocks()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            blocks[i] = transform.GetChild(i).gameObject;
        }
    }

    bool isValidPosition()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            Vector2 tempVector = new Vector2(blocks[i].transform.position.x, blocks[i].transform.position.y);
            if (!grid.IsWithinAndEmpty(tempVector))
            {
                return false;
            }
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector2.left);
            if (!isValidPosition())
            {
                Move(Vector2.right);
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector2.right);
            if (!isValidPosition())
            {
                Move(Vector2.left);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(Vector2.down);
            if (!isValidPosition())
            {
                Move(Vector2.up);
                // Tell the grid to land the piece.
                grid.LandPiece(this);
                isActive = false;
            }
        }

    }
}
