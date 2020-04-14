using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BordCriate : MonoBehaviour
{
    private static BordCriate instance;
    public static BordCriate Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<BordCriate>();
            }
            return instance;
        }

    }

    private int[,] map;  

    const int wall = 9;
    const int road = 0;
    const int room = 1;

    public int[,] Map { get => map; set => map = value; }

    //void Start()
    //{
    //  //  ResetMapData();
    //    //CreateSpaceData();

    //}


    /// <summary>
    /// Mapの二次元配列の初期化
    /// </summary>
    public void ResetMapData(int MapWidth, int MapHeight)
    {
        Map = new int[MapHeight, MapWidth];
        for (int i = 0; i < MapHeight; i++)
        {
            for (int j = 0; j < MapWidth; j++)
            {
                Map[i, j] = wall;
            }
        }
    }


    public void CreateSpaceData(int MapWidth, int MapHeight, int roomMinHeight, int roomMaxHeight, int roomMinWidth, int roomMaxWidth
        , int RoomCountMin, int RoomCountMax, int meetPointCount)
    {
        int roomCount = Random.Range(RoomCountMin, RoomCountMax);

        int[] meetPointsX = new int[meetPointCount];
        int[] meetPointsY = new int[meetPointCount];
        for (int i = 0; i < meetPointsX.Length; i++)
        {
            meetPointsX[i] = Random.Range(MapWidth / 4, MapWidth * 3 / 4);
            meetPointsY[i] = Random.Range(MapHeight / 4, MapHeight * 3 / 4);
            Map[meetPointsY[i], meetPointsX[i]] = room;
        }

        for (int i = 0; i < roomCount; i++)
        {
            int roomHeight = Random.Range(roomMinHeight, roomMaxHeight);
            int roomWidth = Random.Range(roomMinWidth, roomMaxWidth);
            int roomPointX = Random.Range(2, MapWidth - roomMaxWidth - 2);
            int roomPointY = Random.Range(2, MapWidth - roomMaxWidth - 2);

            int roadStartPointX = Random.Range(roomPointX, roomPointX + roomWidth);
            int roadStartPointY = Random.Range(roomPointY, roomPointY + roomHeight);

            bool isRoad = CreateRoomData(roomHeight, roomWidth, roomPointX, roomPointY);

            if (isRoad == false)
            {
                CreateRoadData(roadStartPointX, roadStartPointY, meetPointsX[Random.Range(0, 0)], meetPointsY[Random.Range(0, 0)]);
            }
        }


    }

    /// <summary>
    /// 部屋データを生成。すでに部屋がある場合はtrueを返し、道を作らないようにする
    /// </summary>
    /// <param name="roomHeight">部屋の高さ</param>
    /// <param name="roomWidth">部屋の横幅</param>
    /// <param name="roomPointX">部屋の始点(x)</param>
    /// <param name="roomPointY">部屋の始点(y)</param>
    /// <returns></returns>
    private bool CreateRoomData(int roomHeight, int roomWidth, int roomPointX, int roomPointY)
    {
        bool isRoad = false;
        for (int i = 0; i < roomHeight; i++)
        {
            for (int j = 0; j < roomWidth; j++)
            {
                if (Map[roomPointY + i, roomPointX + j] == room)
                {
                    isRoad = true;
                }
                else
                {
                    Map[roomPointY + i, roomPointX + j] = room;
                }
            }
        }
        return isRoad;
    }

    /// <summary>
    /// 道データを生成
    /// </summary>
    /// <param name="roadStartPointX"></param>
    /// <param name="roadStartPointY"></param>
    /// <param name="meetPointX"></param>
    /// <param name="meetPointY"></param>
    private void CreateRoadData(int roadStartPointX, int roadStartPointY, int meetPointX, int meetPointY)
    {

        bool isRight;
        if (roadStartPointX > meetPointX)
        {
            isRight = true;
        }
        else
        {
            isRight = false;
        }
        bool isUnder;
        if (roadStartPointY > meetPointY)
        {
            isUnder = false;
        }
        else
        {
            isUnder = true;
        }

        if (Random.Range(0, 2) == 0)
        {

            while (roadStartPointX != meetPointX)
            {

                Map[roadStartPointY, roadStartPointX] = road;
                if (isRight == true)
                {
                    roadStartPointX--;
                }
                else
                {
                    roadStartPointX++;
                }

            }

            while (roadStartPointY != meetPointY)
            {

                Map[roadStartPointY, roadStartPointX] = road;
                if (isUnder == true)
                {
                    roadStartPointY++;
                }
                else
                {
                    roadStartPointY--;
                }
            }

        }
        else
        {

            while (roadStartPointY != meetPointY)
            {

                Map[roadStartPointY, roadStartPointX] = road;
                if (isUnder == true)
                {
                    roadStartPointY++;
                }
                else
                {
                    roadStartPointY--;
                }
            }

            while (roadStartPointX != meetPointX)
            {

                Map[roadStartPointY, roadStartPointX] = road;
                if (isRight == true)
                {
                    roadStartPointX--;
                }
                else
                {
                    roadStartPointX++;
                }

            }

        }
    }

    ///// <summary>
    ///// マップデータをもとにダンジョンを生成
    ///// </summary>
    //private void CreateDangeon()
    //{
    //    for (int i = 0; i < MapHeight; i++)
    //    {
    //        for (int j = 0; j < MapWidth; j++)
    //        {
    //            if (Map[i, j] == wall)
    //            {
    //                Instantiate(WallObject, new Vector3(j - MapWidth / 2, i - MapHeight / 2, 0), Quaternion.identity);
    //            }
    //            if (Map[i, j] == road || Map[i, j] == room)
    //            {
    //                Instantiate(FloorObject, new Vector3(j - MapWidth / 2, i - MapHeight / 2, 0), Quaternion.identity);

    //            }

    //        }
    //    }
    //}

    //public void CreatePlayer()
    //{
    //    int flag = 0;
    //    for (int i = 0; i < MapHeight; i++)
    //    {
    //        for (int j = 0; j < MapWidth; j++)
    //        {
    //            if (Map[i, j] == road)
    //            {
    //                Instantiate(PlayerObject, new Vector3(j - MapWidth / 2, i - MapHeight / 2, 0), Quaternion.identity);
    //                flag = 1;
    //                break;
    //            }
    //            if (flag == 1) break;
    //        }
    //    }

    //}

}

