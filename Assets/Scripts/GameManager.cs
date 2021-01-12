﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] pieces;
    [SerializeField] private Transform constructorPos;
    [SerializeField] private Grid grid;
    [SerializeField] private float moveTime;

    private List<GameObject> theBag = new List<GameObject>();
    private float moveTimeSeconds;
    private Group currentGroup = null;
    
    // Start is called before the first frame update
    void Start()
    {
        FillBag();
        moveTimeSeconds = moveTime;
        ConstructPiece();
    }

    void FillBag()
    {
        for(int i = 0; i < pieces.Length; i++)
        {
            theBag.Add(pieces[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveTimeSeconds -= Time.deltaTime;
        if (moveTimeSeconds <= 0f)
        {
            currentGroup.GameManagerMove(Vector2.down);
            moveTimeSeconds = moveTime;
        }
    }

    GameObject GetRandomFromBag()
    {
        if(theBag.Count == 0)
        {
            FillBag();
        }
        int tempIndex = UnityEngine.Random.Range(0, theBag.Count);
        GameObject tempObj = theBag[tempIndex];
        theBag.Remove(tempObj);
        return tempObj;
    }

    GameObject GetRandomObject(GameObject[] possibleObjects)
    {
        if (possibleObjects.Length == 0) return null;
        int tempIndex = UnityEngine.Random.Range(0, possibleObjects.Length);
        return possibleObjects[tempIndex];
    }

    public void ConstructPiece()
    {
        // GameObject temp = GetRandomObject(pieces);
        GameObject temp = GetRandomFromBag();
        Vector2 tempPos = RoundVector.RoundedVector(constructorPos.position);
        Group tempGroup = Instantiate(temp, tempPos, Quaternion.identity).GetComponent<Group>();
        tempGroup.Setup(grid);
        currentGroup = tempGroup;
    }
}
