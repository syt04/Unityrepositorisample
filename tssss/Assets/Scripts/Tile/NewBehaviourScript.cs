using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
   public Main_Stage map;

    [SerializeField]
    EnemyGanareter enemyGanareter;


    [SerializeField]
    Main_AI enemyAI;

    public GameState gameState;

    private static NewBehaviourScript instance;
    public static NewBehaviourScript Instance
    { get { if (instance == null)
            {
                instance = FindObjectOfType<NewBehaviourScript>();
            }
            return instance;

        }
    }

    [SerializeField]
    int roomMinHeight;
    [SerializeField]
    int roomMaxHeight;
    [SerializeField]
    int roomMinWidth;
    [SerializeField]
    int roomMaxWidth;
    [SerializeField]
    int RoomCountMin;
    [SerializeField]
    int RoomCountMax;
    [SerializeField]
    int corridermin;
    [SerializeField]
    int corridermax;

    [SerializeField]
    int meetPointCount;

    public GameObject chest;
    public int width;
    public int height;
    public Inventory inventory;
    public InventoryManager inventorymanager;
    public ChestInventory chestInventory;

    public Transform canvas;
    public GameObject inventoryPrefab;

    Vector3 vector3 = new Vector3(4f, 4f, 4f);
    IEnumerator Start()
    {
        //// マップ生成
        ///
        map.ResetMapData(width, height);
        map.CreateSpaceData(width, height, roomMinHeight, roomMaxHeight, roomMinWidth, roomMaxWidth, RoomCountMin, RoomCountMax, meetPointCount);

        var roomIntRangewidth = new IntRange(roomMinWidth, roomMaxWidth);
        var roomIntRangeheight = new IntRange(roomMinHeight, roomMaxHeight);
        var roomCountIntRange = new IntRange(RoomCountMin, RoomCountMax);
        var corriderIntRange = new IntRange(corridermin, corridermax);

        map.SetUpTilesArray(width, height);
        map.CreateRoomsAndCorridors(width, height, roomCountIntRange, roomIntRangewidth, roomIntRangeheight, corriderIntRange);
        map.SetTilesValuesForRooms();
        map.SetTilesValuesForCorridors();

        //Debug.Log(map.Tiles.Length);

        map.Generate(width, height,roomMinHeight,roomMaxHeight,roomMinWidth,roomMaxWidth,RoomCountMin,RoomCountMax,corridermin,corridermax);


        yield return null;

        enemyGanareter.Createunit();


        //  CriateChest();

        // AI設定
        gameState.SetAI(Teams.Player2, enemyAI);

        // ターン開始
        gameState.StartTurn(Teams.Player1);



    }



    //void CriateChest()
    //{
      

    //    for(int i =7; i <10; i += 2)
    //    {
    //        GameObject newchest = (GameObject)Instantiate(chest);
    //        GameObject newInventory = (GameObject)Instantiate(inventoryPrefab);

    //        newInventory.transform.SetParent(canvas.transform);
    //        newInventory.transform.SetSiblingIndex(1);
            

    //        InventoryLink chestlink = newchest.GetComponent<InventoryLink>();

    //        newInventory.name = ("Chestinventory" + i);
    //        chestlink.linkedInventory = GameObject.Find("Chestinventory"+i).GetComponent<ChestInventory>();
    //        chestlink.slots = 10;


    //        newchest.name = "chest"+1;


    //        map.PutItem(i, i, newchest);
     
         
    //    }


    //}

}
