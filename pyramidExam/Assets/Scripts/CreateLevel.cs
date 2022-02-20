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
    public GameObject chestPrefab;

    private float wallCenter;
    private float wallHeight;
    private float wallThickness;

    private List<GameObject> walls;

    private void Awake()
    {
        wallCenter = wallPrefab.transform.position.y;
        wallHeight = wallPrefab.transform.localScale.y;
        wallThickness = wallPrefab.transform.localScale.z;

        CreateFloor(roomX, roomY);
        
        if(roomX > 2 && roomY > 2)
        {
            CreateWalls(roomX, roomY);
            CreateDoors(roomX, roomY);
            CreateChest(roomX, roomY);
        }
    }

    private void CreateFloor(float roomX, float roomY)
    {
        GameObject floor = Instantiate(floorPrefab);

        floor.transform.localScale = new Vector3((roomX*0.1f)+1,1,(roomY*0.1f)+1);
    }

    private void CreateChest(float roomX, float roomY)
    {
        GameObject chest = Instantiate(chestPrefab);

        chest.transform.position = new Vector3(Random.Range(1, roomX*0.1f), 0, Random.Range(1, roomY * 0.1f));
        chest.transform.Rotate(new Vector3(0, Random.Range(0, 4) * 90, 0));
    }

    private void CreateWalls(float roomX, float roomY)
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
                walls[i].transform.localScale = new Vector3(roomY + wallThickness, wallHeight, wallThickness);
            }
        }

        //wall positions
        {
            walls[0].transform.position = new Vector3(0, wallCenter, roomY / 2);
            walls[1].transform.position = new Vector3(roomX / 2, wallCenter, 0);
            walls[1].transform.Rotate(0, 90, 0);
            walls[2].transform.position = new Vector3(0, wallCenter, -roomY / 2);
            walls[2].transform.Rotate(0, 180, 0);
            walls[3].transform.position = new Vector3(-roomX / 2, wallCenter, 0);
            walls[3].transform.Rotate(0, 270, 0);
        }
    }

    private void CreateDoors(int roomX, int roomY)
    {
        int randomWall = Random.Range(0, walls.Count);
        Debug.Log(randomWall);
        if(walls[randomWall].transform.localScale.x>=1)
        {
            float randomPlaceOnWall = Random.Range(0, Mathf.RoundToInt(walls[randomWall].transform.localScale.x));
            Debug.Log(randomPlaceOnWall);
            GameObject door = Instantiate(doorPrefab);
            door.transform.rotation = walls[randomWall].transform.rotation;
            door.transform.position = new Vector3(walls[randomWall].transform.position.x, door.transform.position.y, walls[randomWall].transform.position.z);
            if(door.transform.position.x == 0)
            {
                //move door to its destination, we add .5f because doors ofsett (center is at .5f)
                door.transform.position += new Vector3(-walls[randomWall].transform.localScale.x/2 + randomPlaceOnWall + .5f, 0, 0);
                if (randomPlaceOnWall == 0)
                {
                    door.transform.position += new Vector3(wallThickness, 0, 0);
                }
                else if (randomPlaceOnWall == Mathf.RoundToInt(walls[randomWall].transform.localScale.x))
                {
                    door.transform.position += new Vector3(-wallThickness, 0, 0);
                }

                //scale and move wall to make place for door
                walls[randomWall].transform.position += new Vector3(randomPlaceOnWall / 2 + .5f, 0, 0);
                float restWallToFill = walls[randomWall].transform.localScale.x - (walls[randomWall].transform.localScale.x - randomPlaceOnWall);
                Debug.Log(restWallToFill);
                walls[randomWall].transform.localScale -= new Vector3(randomPlaceOnWall + 1, 0, 0);

                //add new wall on empty space between doors and other wall
                {
                    if(randomPlaceOnWall != 0)
                    {
                        walls.Add(Instantiate(wallPrefab));
                        walls[walls.Count - 1].transform.rotation = walls[randomWall].transform.rotation;
                        walls[walls.Count - 1].transform.localScale = new Vector3(restWallToFill, wallHeight, wallThickness);
                        walls[walls.Count - 1].transform.position = walls[randomWall].transform.position;
                        walls[walls.Count - 1].transform.position -= new Vector3(restWallToFill / 2 + walls[randomWall].transform.localScale.x / 2 + 1, 0, 0);
                    }
                }
            }
            else
            {
                door.transform.position += new Vector3(0, 0, -walls[randomWall].transform.localScale.x / 2 + randomPlaceOnWall + .5f);
                if (randomPlaceOnWall == 0)
                {
                    door.transform.position += new Vector3(0, 0, wallThickness);
                }
                else if (randomPlaceOnWall == Mathf.RoundToInt(walls[randomWall].transform.localScale.x))
                {
                    door.transform.position += new Vector3(0, 0, -wallThickness);
                }

                walls[randomWall].transform.position += new Vector3(0, 0, randomPlaceOnWall / 2 + .5f);
                float restWallToFill = walls[randomWall].transform.localScale.x - (walls[randomWall].transform.localScale.x - randomPlaceOnWall);
                Debug.Log(restWallToFill);
                walls[randomWall].transform.localScale -= new Vector3(randomPlaceOnWall + 1, 0, 0);

                {
                    if (randomPlaceOnWall != 0)
                    {
                        walls.Add(Instantiate(wallPrefab));
                        walls[walls.Count - 1].transform.rotation = walls[randomWall].transform.rotation;
                        walls[walls.Count - 1].transform.localScale = new Vector3(restWallToFill, wallHeight, wallThickness);
                        walls[walls.Count - 1].transform.position = walls[randomWall].transform.position;
                        walls[walls.Count - 1].transform.position -= new Vector3(0, 0, restWallToFill / 2 + walls[randomWall].transform.localScale.x / 2 + 1);
                    }
                }
            }
        }
    }


    void Start()
    {
        
    }
}
