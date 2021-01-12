using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] pieces;
    [SerializeField] private Transform constructorPos;
    [SerializeField] private Grid grid;
    
    // Start is called before the first frame update
    void Start()
    {
        ConstructPiece();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject GetRandomObject(GameObject[] possibleObjects)
    {
        if (possibleObjects.Length == 0) return null;
        int tempIndex = Random.Range(0, possibleObjects.Length);
        return possibleObjects[tempIndex];
    }

    void ConstructPiece()
    {
        GameObject temp = GetRandomObject(pieces);
        Vector2 tempPos = RoundVector.RoundedVector(constructorPos.position);
        Group tempGroup = Instantiate(temp, tempPos, Quaternion.identity).GetComponent<Group>();
        tempGroup.Setup(grid);
    }
}
