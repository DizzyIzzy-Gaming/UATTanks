using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int rows;
    public int columns;

    public int mapSeed;

    private float roomWidth = 50f;
    private float roomHeight = 50f;

    public GameObject[] gridPrefabs;
    private Room[,] grid;

    


    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
        GameManager.Instance.SpawnEnemies(4);//Temperorary respawns
        GameManager.Instance.SpawnPlayers();//Temperorary respawns
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public GameObject RandomRoomPrefab()//pull out a random room prefab from a list of rooms
    {
        return gridPrefabs[UnityEngine.Random.Range(0, gridPrefabs.Length)];
    }


    public void GenerateGrid()// generates the map
    {
        switch (GameManager.Instance.mapType)
        {
            //the different types of random generations
            case GameManager.MapGenerationType.Random:
                mapSeed = DateToInt(DateTime.Now);
                break;
            case GameManager.MapGenerationType.MapOfTheDay:
                mapSeed = DateToInt(DateTime.Now.Date);
                break;
            case GameManager.MapGenerationType.CustomSeed:
                // Don't change the seed
                break;
        }

        UnityEngine.Random.InitState(mapSeed);//Initializes random number generator with the mapseed variable we created
        grid = new Room[columns, rows];

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                float xPosition = column * roomWidth;
                float zPosition = row * roomHeight;//

                Vector3 newRoomPosition = new Vector3(xPosition, 0f, zPosition);//adds the new position we just got to a newroomposition vector3 variable 

                GameObject temporaryRoom = Instantiate(RandomRoomPrefab(), newRoomPosition, Quaternion.identity);// spawns room

                Room currentRoom = temporaryRoom.GetComponent<Room>();// grabs the Room component from the freshly instantiated room

                //If we are not the top row open north door
                if (row != rows - 1)
                {
                    currentRoom.doorNorth.SetActive(false);
                }
                //if we are not at the bottom row open the south door
                if (row != 0)
                {
                    currentRoom.doorSouth.SetActive(false);
                }
                //if we are not at the farthest right open east doors
                if (column != columns - 1)
                {
                    currentRoom.doorEast.SetActive(false);
                }
                //if we are not the furthest left open west doors
                if (column != 0)
                {
                    currentRoom.doorWest.SetActive(false);
                }
                grid[column, row] = currentRoom;//adds the freshly instantiated room to a list based on the row and column



                temporaryRoom.transform.parent = this.transform;

                temporaryRoom.name = "Room_" + column + "," + row;// changes the name to match its position
            }
        }


    }
    
    public int DateToInt(DateTime dateToUse)
	{
        //Add our date up and return it
        return dateToUse.Year + dateToUse.Month + dateToUse.Day + dateToUse.Hour + dateToUse.Minute + dateToUse.Second + dateToUse.Millisecond;
	}
}
