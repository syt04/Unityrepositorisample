using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoardCreator : MonoBehaviour
{
    private static BoardCreator instance;
    public static BoardCreator Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BoardCreator>();
            }
            return instance;

        }
    }

    public TileType[][] Tiles { get => tiles; set => tiles = value; }

    public class TileType
    {
       private bool room;
       private int roomNumber;

        public bool Room { get => room; set => room = value; }

        public void RoomNumber(int roomNumber)
        {
            this.roomNumber = roomNumber;
        }

    }

    //public int columns;//= 100;
    //public int rows;//= 100;
    //public IntRange numRooms;// = new IntRange(15, 20);
    //public IntRange roomWidth;//= new IntRange(3, 10);
    //public IntRange roomHeight;// = new IntRange(3, 10);
    //public IntRange corridorLength;// = new IntRange(6, 10);


    public GameObject player;

    private TileType[][] tiles;
    private Room[] rooms;
    private Corridor[] corridors;
   // private GameObject boardHolder;

    // Start is called before the first frame update
    void Start()
    {
        //boardHolder = new GameObject("BoardHolder");

        //SetUpTilesArray();
        
        //CreateRoomsAndCorridors();

        SetTilesValuesForRooms();
        SetTilesValuesForCorridors();

        //InstantiateTiles();
        //InstantiateOuterWalls();


    }


   public void SetUpTilesArray(int width,int height)
    {
        Tiles = new TileType[width+2][];


        for(int i = 0; i< Tiles.Length; i++)
        {
            Tiles[i] = new TileType[height+2];

        }

    }

    public void CreateRoomsAndCorridors(int width,int height, IntRange numRooms, IntRange roomWidth, IntRange roomHeight, IntRange corridorLength) {

        rooms = new Room[numRooms.Random];

        corridors = new Corridor[rooms.Length - 1];

        rooms[0] = new Room();
        corridors[0] = new Corridor();

        rooms[0].SetupRoom(roomWidth, roomHeight, width, height);

        corridors[0].SetupCorridor(rooms[0], corridorLength, roomWidth, roomHeight, width, height, true);

        for(int i = 1; i < rooms.Length; i++)
        {
            rooms[i] = new Room();

            rooms[i].SetupRoom(roomWidth, roomHeight, width, height, corridors[i - 1]);

            if(i < corridors.Length)
            {
                corridors[i] = new Corridor();

                corridors[i].SetupCorridor(rooms[i], corridorLength, roomWidth, roomHeight, width, height, false);

            }

            if(i == rooms.Length * .5f)
            {
                Vector3 playerPos = new Vector3(rooms[i].xPos, rooms[i].yPos, 0);
                //Instantiate(player, playerPos,Quaternion.identity);

            }
        }
    }

    public void SetTilesValuesForRooms()
    {

        for(int i = 0; i < rooms.Length; i++)
        {
            Room currentRoom = rooms[i];

            for(int j = 0; j < currentRoom.roomWidth; j++)
            {
                int xCoord = currentRoom.xPos + j;

                for(int k = 0; k < currentRoom.roomHeight; k++)
                {
                    int yCoord = currentRoom.yPos + k;

                    Tiles[xCoord][yCoord].Room = true ;
                    Tiles[xCoord][yCoord].RoomNumber(i) ;

                }

            }

        }


    }

    public void SetTilesValuesForCorridors() {

        for(int i = 0; i < corridors.Length; i++)
        {
            Corridor currentCorridor = corridors[i];

            for(int j =0;j < currentCorridor.corridorLength; j++)
            {
                int xCoord = currentCorridor.startXPos;
                int yCoord = currentCorridor.startYPos;

                switch (currentCorridor.direction)
                {
                    case Direction.N:
                        yCoord += j;
                        break;
                    case Direction.E:
                        xCoord += j;
                        break;
                    case Direction.S:
                        yCoord -= j;
                        break;
                    case Direction.W:
                        xCoord -= j;
                        break;

                }

                Tiles[xCoord][yCoord].Room = false;

            }

        }

    }

    //void InstantiateTiles()
    //{
    //    for(int i = 0; i < tiles.Length; i++)
    //    {
    //        for(int j =0;j < tiles[i].Length; j++)
    //        {
    //            InstantiateFromArray(floorTiles, i, j);

    //            if(tiles[i][j] == TileType.Wall)
    //            {
    //                InstantiateFromArray(wallTiles, i, j);
    //            }

    //        }

    //    }
    //}

    //void InstantiateOuterWalls()
    //{
    //    float leftEdgeX = -1f;
    //    float rightEdgeX = columns + 0f;
    //    float bottomEdgeY = -1f;
    //    float topEdgeY = rows + 0f;

    //    InstantiateVerticalOuterWall(leftEdgeX, bottomEdgeY, topEdgeY);
    //    InstantiateVerticalOuterWall(rightEdgeX, bottomEdgeY, topEdgeY);

    //    InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, bottomEdgeY);
    //    InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, topEdgeY);

    //}


    //void InstantiateVerticalOuterWall(float xCoord,float startingY,float endingY)
    //{
    //    float currentY = startingY;

    //    while(currentY <= endingY)
    //    {
    //        InstantiateFromArray(outerWallTiles, xCoord, currentY);

    //        currentY++;
    //    }

    //}

    //void InstantiateHorizontalOuterWall(float startingX,float endingX,float yCoord) {

    //    float currentX = startingX;

    //    while(currentX <= endingX)
    //    {
    //        InstantiateFromArray(outerWallTiles, currentX, yCoord);

    //        currentX++;

    //    }

    //}


    //void InstantiateFromArray(GameObject[] prefabs,float xCoord,float yCoord)
    //{
    //    int randomIndex = Random.Range(0, prefabs.Length);

    //    Vector3 position = new Vector3(xCoord, yCoord,0f);

    //    GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;

    //    tileInstance.transform.parent = boardHolder.transform;
    //}

}
