using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryLink : MonoBehaviour
{
    /// <summary>
    /// A reference to the chests inventory
    /// </summary>
    public ChestInventory linkedInventory;

    public int rows, slots;

    private List<Stack<ItemScript>> allSlots;



    private bool active = false;

     void Start()
    {
        allSlots = new List<Stack<ItemScript>>(slots);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        //if (other.tag == "Player")
        //{
            Debug.Log("ontoriger");

            if (linkedInventory.FadingOut)
            {
                linkedInventory.instantClose = true;
                linkedInventory.MoveItemsToChest();
            }

            active = true;
            linkedInventory.UpdateLayout(allSlots,rows, slots);
           // chestInventory.MoveItemsToChest();
        //}
    }

    public void On2D()
    {

      
        Debug.Log("ontoriger");
        active = true;
        linkedInventory.UpdateLayout(allSlots, rows, slots);
        // chestInventory.MoveItemsToChest();
       
    }

    private void OnTriggerExit2D(Collider2D other)
    {
       
        //if (other.tag == "Player")
        //{
        //    Debug.Log("ontoriger exit");

            active = false;

        //}
    }

    public void SaveInventory()
    {
        string content = string.Empty;



        for (int i = 0; i < allSlots.Count; i++)
        {
      
            if(allSlots[i] != null && allSlots[i].Count > 0)
            {
                //Creates a string with this format :slotindex-itemtype-amountofitems;this string can be read so that we can rebuild the inventory
                content += i + "-" + allSlots[i].Peek().Item.ItemName + "-" + allSlots[i].Count.ToString() + ";";
                //"0-Mana-3;2-HEALT-2;"
            }

        }
        //Stores all the info in the playerPrefs
        PlayerPrefs.SetString(gameObject.name + "content", content);
        PlayerPrefs.Save();


    }

    public virtual void LoadInventory()
    {
        //Loads all the inventory's data from the playerprefs
        string content = PlayerPrefs.GetString(gameObject.name + "content");
        allSlots = new List<Stack<ItemScript>>();

        for(int i = 0; i < slots; i++)
        {
            allSlots.Add(new Stack<ItemScript>());
        }

        if (content != string.Empty)
        {

            string[] splitContent = content.Split(';'); // 0-Mana-3;

            for (int x = 0; x < splitContent.Length - 1; x++)
            {
                string[] splitValues = splitContent[x].Split('-'); //splits the slots information into single values , so that each index in the splitvalues array contains info about avalue

                int index = Int32.Parse(splitValues[0]); //"inventorindex

                //"MANA" --> ItemType MANA
                string itemName = splitValues[1]; //itemtype

                int amount = Int32.Parse(splitValues[2]); //"amount of items

                Item tmp = null;

                for (int i = 0; i < amount; i++) //adds the correct amount of items to the inventory
                {
                    GameObject loadedItem = Instantiate(InventoryManager.Instance.itemObject);

                    if (tmp == null)
                    {
                        tmp = InventoryManager.Instance.ItemContainer.Consumeables.Find(item => item.ItemName == itemName);

                    }
                    if (tmp == null)
                    {
                        tmp = InventoryManager.Instance.ItemContainer.Equipment.Find(item => item.ItemName == itemName);
                    }
                    if (tmp == null)
                    {
                        tmp = InventoryManager.Instance.ItemContainer.Weapons.Find(item => item.ItemName == itemName);
                    }
                    if (tmp == null)
                    {
                        tmp = InventoryManager.Instance.ItemContainer.Materials.Find(item => item.ItemName == itemName);
                    }


                    loadedItem.AddComponent<ItemScript>();
                    loadedItem.GetComponent<ItemScript>().Item = tmp;
                    allSlots[index].Push(loadedItem.GetComponent<ItemScript>());
                    Destroy(loadedItem);

                }

            }
        }

        if (active)
        {
            linkedInventory.UpdateLayout(allSlots, rows, slots);

        }
    }

}
