using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager1 : MonoBehaviour
{

    //　アイテムデータベース
   // [SerializeField]
   // private WeaponData WeaponDataBase;
    [SerializeField]
    public ItemData ItemDataBase;


    public Belongings belongings;

    GameObject gameobject;


    //　アイテム数管理
   // private Dictionary<MasterItemModel, int> numOfItem = new Dictionary<MasterItemModel, int>();




    // Use this for initialization
    void Start()
    {






        //  WeaponData buttn;
        //  buttn = Resources.Load("MasterItemModel/WeponData", typeof(WeaponData)) as WeaponData;
        /*
                for (int i = 0; i < itemDataBase.GetItemLists().Count; i++)
                {
                    //　アイテム数を適当に設定
                    numOfItem.Add(itemDataBase.GetItemLists()[i], i);
                    //　確認の為データ出力
                    Debug.Log(itemDataBase.GetItemLists()[i].GetItemName() + ": " + itemDataBase.GetItemLists()[i].GetInformation());
                }

                Debug.Log(GetItem("ナイフ").GetInformation());
                Debug.Log(numOfItem[GetItem("ハーブ")]);
            }*/
        // Debug.Log("リソースから101指定 Atack"+ buttn.MasterWeaponList.Find(itemName =>itemName.GetName() =="銃").GetAttackPower());

        // Debug.Log("Listからackk" + GetWeponList("銃").GetAttackPower());

    }


    public void Update()
    {
        gameobject = GameObject.FindWithTag("ItemManager");
        belongings = gameobject.GetComponent<Belongings>();
    }


    //　名前でアイテムを取得/*
    public MasterItemModel GetWeponList(string searchName)
    {
        return ItemDataBase.GetMasterWeaponLists().Find(itemName => itemName.GetName == searchName);
    } 

    public MasterItemModel GetItemList(string searchName)
    {

        return ItemDataBase.GetItemLists().Find(itemName => itemName.GetName == searchName);
    }


}