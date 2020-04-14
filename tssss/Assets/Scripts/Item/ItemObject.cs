using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
   int x = 0;
   int y = 0;
   public string name;

   public int num = 0;

    public ItemManager1 Itemmanager;
    GameObject gameobject;
    Belongings belongings;
    //Unit unit;
    GameObject A;

    MasterItemModel Item;

    public void Start()
    {
        //WeaponData buttn;
        //   buttn = Resources.Load("MasterItemModel/WeponData", typeof(WeaponData)) as WeaponData;

        //gameobject = GameObject.FindWithTag("ItemManager");
       // Itemmanager = gameobject.GetComponent<ItemManager>();
        Item = Itemmanager.GetItemList(name);
        // Item = Itemmanager.GetWeponList(name);



        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = Item.GetSprite;

 

    }

    public void Update()
    {
        Item = Itemmanager.GetItemList(name);
        // Item = Itemmanager.GetWeponList(name);

        A = GameObject.FindWithTag("Player");
     //   unit = A.GetComponent<Unit>();

        belongings = GameObject.Find("Belongings").GetComponent<Belongings>();
       //Debug.Log(belongings);

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = Item.GetSprite;

        //if(x == unit.x&& y== unit.y) {

        //    Chach(Item, num);
        //    Destroy(gameObject);


        //}

        

    }


    /*
    public void GenaretoItem(ItemObject Item,int x , int y)
    {
        Item.gameObject.SetActive(true);
        Item.transform.position += new Vector3(x, y);
        Item.x = x;
        Item.y = y;
    }
    */

    public void Chach(MasterItemModel Item, int x)
    {

        

        //  List<MasterItemModel> keyList = new List<MasterItemModel>(numOfItem.Keys);

        //  foreach (MasterItemModel key in keyList)
        //  {
        if (belongings.GetnumOfItem.ContainsKey(Item))
        {

            belongings.GetnumOfItem[Item] += x;

        }
        else
        {

            belongings.GetnumOfItem.Add(Item, x);

        }
        //  }

        Debug.Log("Chach" + Item.GetName + belongings.GetnumOfItem[Item]);
    }




    public void SetX(int x)
    {
        this.x = x;

    }

    public int GetX
    {
        get
        {
            return this.x;
        }
    }

    public void SetY(int y)
    {
        this.y = y;

    }

    public int GetY
    {
        get{ 
        return this.y;
       }
    }


    public void SetNum(int num)
    {
        this.num = num;

    }

    public int GetNum
    {
        get
        {
            return num;
        }
    }

    public MasterItemModel GetItem
    {
        get
        {
            return Item;
        }

    }


    public string GetName
    {
        get
        {
            return name;
        }
    }

    public void SetName(string name)
    {
        this.name = name;

    }
}
