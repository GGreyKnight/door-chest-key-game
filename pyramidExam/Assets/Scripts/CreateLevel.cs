using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLevel : MonoBehaviour
{
    public int roomX = 0;
    public int roomY = 0;

    public GameObject floorPrefab;
    public GameObject wallPrefab;
    public GameObject doorPrefab;

    private float wallCenter;
    private float wallHeight;
    private float wallThickness;

    private List<GameObject> walls;

    private void Awake()
    {
        wallCenter = wallPrefab.transform.position.y;
        wallHeight = wallPrefab.transform.localScale.y;
        wallThickness = wallPrefab.transform.localScale.z;

        createFloor(roomX, roomY);
        
        if(roomX > 0 && roomY > 0)
        {
            createWalls(roomX, roomY);
            createDoors(roomX, roomY);
        }
    }

    private void createFloor(float roomX, float roomY)
    {
        GameObject floor = Instantiate(floorPrefab);

        floor.transform.localScale = new Vector3((roomX*0.1f)+1,1,(roomY*0.1f)+1);
    }

    private void createWalls(float roomX, float roomY)
    {
        walls = new List<GameObject>();
        for (int i = 0; i < 4; i++)
        {
            walls.Add(Instantiate(wallPrefab));
            if(i%2==0)
            {
                walls[i].transform.localScale = new Vector3(roomX + wallThickness, wallHeight, wallThickness);
            }
            else
            {
                walls[i].transform.localScale = new Vector3(roomY - wallThickness, wallHeight, wallThickness);
            }
        }

        //wall positions
        {
            walls[0].transform.position = new Vector3(0, wallCenter, roomY / 2);
            walls[1].transform.position = new Vector3(roomX / 2, wallCenter, 0);
            walls[1].transform.Rotate(0, 90, 0);
            walls[2].transform.position = new Vector3(0, wallCenter, -roomY / 2);
            walls[3].transform.position = new Vector3(-roomX / 2, wallCenter, 0);
            walls[3].transform.Rotate(0, -90, 0);
        }
        
    }

    private void createDoors(int roomX, int roomY)
    {
        int randomWall = Random.Range(0, walls.Count);
        Debug.Log(randomWall);
        if(walls[randomWall].transform.localScale.x>=1)
        {
            int randomPlaceOnWall = Random.Range(0, Mathf.RoundToInt(walls[randomWall].transform.localScale.x));
            Debug.Log(randomPlaceOnWall);
        }


    }


    void Start()
    {
        
    }
}
