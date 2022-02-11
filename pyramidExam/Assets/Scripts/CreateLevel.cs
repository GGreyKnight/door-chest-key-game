using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLevel : MonoBehaviour
{
    public GameObject floorPrefab;
    public GameObject wallPrefab;

    public int roomX = 0;
    public int roomY = 0;

    private void Awake()
    {
        createFloor(roomX, roomY);
    }

    private void createFloor(int roomX, int roomY)
    {
        GameObject floor = Instantiate(floorPrefab);

        floor.transform.localScale = new Vector3((roomX*0.1f)+1,1,(roomY*0.1f)+1);
    }

    private void createWalls(int roomX, int roomY)
    {

    }

    void Start()
    {
        
    }
}
