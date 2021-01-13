using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    [SerializeField] public GameObject[] blocks;
    [SerializeField] private float moveDelay;
    [SerializeField] private float moveDelta;

    private float moveDelayCounter;
    private bool isActive = true;
    private Grid grid;

    public void Setup(Grid newGrid)
    {
        moveDelayCounter = moveDelay;
        grid = newGrid;
    }

    void Move(Vector2 moveDirection)
    {
        transform.position += new Vector3(moveDirection.x, moveDirection.y, 0);
        RoundPosition();
    }

    void RoundPosition()
    {
        Vector2 tempVector = RoundVector.RoundedVector(new Vector2(transform.position.x, transform.position.y));
        transform.position = new Vector3(tempVector.x, tempVector.y);
    }

    void Rotate(int rotateAmount)
    {
        transform.Rotate(0f, 0f, rotateAmount);
        RotateChildren(-rotateAmount);
        if(!isValidPosition())
        {
            transform.Rotate(0f, 0f, -rotateAmount);
            RotateChildren(rotateAmount);
        }
        RoundPosition();
    }

    void RotateChildren(int amount)
    {
        for(int i = 0; i < blocks.Length; i++)
        {
            blocks[i].transform.Rotate(0f, 0f, amount);
        }
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
            tempVector = RoundVector.RoundedVector(tempVector);
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
        moveDelayCounter -= Time.deltaTime;
        if(Input.GetKey(KeyCode.UpArrow) && moveDelayCounter <= 0)
        {
            Rotate(90);
            moveDelayCounter -= moveDelta;
        }

        if (Input.GetKey(KeyCode.LeftArrow) && moveDelayCounter <= 0)
        {
            Move(Vector2.left);
            if (!isValidPosition())
            {
                Move(Vector2.right);
            }
            moveDelayCounter -= moveDelta;
        }
        if (Input.GetKey(KeyCode.RightArrow) && moveDelayCounter <= 0)
        {
            Move(Vector2.right);
            if (!isValidPosition())
            {
                Move(Vector2.left);
            }
            moveDelayCounter -= moveDelta;
        }
        if (Input.GetKey(KeyCode.DownArrow) && moveDelayCounter <= 0)
        {
            Move(Vector2.down);
            if (!isValidPosition())
            {
                Move(Vector2.up);
                // Tell the grid to land the piece.
                grid.LandPiece(this);
                isActive = false;
            }
            moveDelayCounter -= moveDelta;
        }
        if(moveDelayCounter <= 0)
        {
            moveDelayCounter = moveDelay;
        }
    }
}
