using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public List<MasterItemModel> ItemList = new List<MasterItemModel>();

    public List<MasterItemModel> GetItemLists()
    {
        return ItemList;
    }


    //ListステータスのList
    public List<MasterItemModel> MasterWeaponList = new List<MasterItemModel>();



    public List<MasterItemModel> GetMasterWeaponLists()
    {
        return MasterWeaponList;
    }


    public List<MasterItemModel> MasterBouguList = new List<MasterItemModel>();



    public List<MasterItemModel> GetMasterBouguLists()
    {
        return MasterBouguList;
    }


}
/*
[System.Serializable]
public class Items
{
    public string name = "";
    public string info = "";
}

    */
