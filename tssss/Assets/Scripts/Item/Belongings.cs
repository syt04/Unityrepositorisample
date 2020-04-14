using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Belongings : MonoBehaviour
{
    [SerializeField]
    ItemManager1 ItemManager;

 
     Dictionary<MasterItemModel, int> numOfItem = new Dictionary<MasterItemModel, int>();

    //MasterWeaponModel weaponeqipment;

    List<MasterItemModel> MyItemlist;


    public void AddItem(MasterItemModel Item,int num)
    {
        if (numOfItem.ContainsKey(Item))
        {

            numOfItem[Item] += num;

        }
        else
        {

            numOfItem.Add(Item, num);

        }

        MyItemlist.Add(Item);

    }

    /*
    public void Chach(MasterItemModel Item,int x)
    {
        


      //  List<MasterItemModel> keyList = new List<MasterItemModel>(numOfItem.Keys);

      //  foreach (MasterItemModel key in keyList)
      //  {
            if (numOfItem.ContainsKey(Item))
            {

            numOfItem[Item] += x;

        }
        else
        {

            numOfItem.Add(Item, x);

        }
      //  }
    }
    */

    public void Throw(MasterItemModel Item,int x)
    {
        numOfItem[Item] -= x;
        if(numOfItem[Item] <= 0)
        {
            numOfItem.Remove(Item);

        }
        MyItemlist.Remove(Item);

    }

    public Dictionary<MasterItemModel, int> GetnumOfItem
    {
        get{

            return numOfItem;
        }

    }
    /*
    public void Eqipment(Unit unit,MasterWeaponModel weaponeqipment)
    {
        unit.kk += weaponeqipment.GetAttackPower();

    }
    */

    public void Update()
    {
        MyItemlist = numOfItem.Keys.ToList();
    }


    public List<MasterItemModel> GetItemKyeList()
    {
       
        return MyItemlist;
    }

    /*
    public MasterItemModel ListItemSerch(string searchName)
    {
      return ItemManager.ItemDataBase.GetItemLists().Find(itemName => itemName.GetName() == searchName);

    }
*/
}

