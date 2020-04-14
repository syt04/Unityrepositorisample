using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerUnits : Inventory
{
    public Slot[] equipmentSlots;

    private static PlayerUnits instance;
    public static PlayerUnits Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PlayerUnits>();
            }

            return PlayerUnits.instance;
        }

    }

    public Slot WeaponSlot
    {
        get { return equipmentSlots[9]; }
    }

    public Slot OffHandSlot
    {
        get { return equipmentSlots[10]; }
    }


    void Awake()
    {
        equipmentSlots = transform.GetComponentsInChildren<Slot>();
    }

    public override void CreateLayout()
    {

    }

    public void EquipItem(Slot slot, ItemScript item)
    {

        if (item.Item.ItemType == ItemType.MAINHAND || item.Item.ItemType == ItemType.TWOHAND && OffHandSlot.IsEmpty)
        {
            Slot.SwapItems(slot, WeaponSlot);
        }
        else
        {
            Slot.SwapItems(slot, Array.Find(equipmentSlots, x => x.canContain == item.Item.ItemType));
        }


    }

    public override void ShowToolTip(GameObject slot)
    {
        Debug.Log("CharPanel Tooltip");

        //saves a reference to the slot we just moused over
        Slot tmpslot = slot.GetComponent<Slot>();

        //if the slot contains an item and we arent splittingor moving any items then we can show the tooltip
        if (slot.GetComponentInParent<Inventory>().IsOpen && !tmpslot.IsEmpty && InventoryManager.Instance.HoverObject == null && !InventoryManager.Instance.selectStackSize.activeSelf)
        {
            //Gets the infomation from the item on the slot we just moved our mouse over
            InventoryManager.Instance.visualTextObject.text = tmpslot.CurrentItem.GetTooltip(this);

            //makes sure that the tooltip has the correct size
            InventoryManager.Instance.sizeTextObject.text = InventoryManager.Instance.visualTextObject.text;

            //shows the tool tip
            InventoryManager.Instance.tooltipObject.SetActive(true);

            //sets the position
            InventoryManager.Instance.tooltipObject.transform.position = slot.transform.position;
        }
    }

    public void CalcStats()
    {
        int agility = 0;
        int strength = 0;
        int stamina = 0;
        int intellect = 0;

        foreach (Slot slot in equipmentSlots)
        {
            if (!slot.IsEmpty)
            {
                Equipment e = (Equipment)slot.CurrentItem.Item;
                agility += e.Agility;
                strength += e.Strength;
                stamina += e.Stamina;
                intellect += e.Intellect;
            }
        }

        // Unit.Instance.SetStats(agility, strength, stamina, intellect);
    }

    public override void SaveInventory()
    {
        string content = string.Empty;

        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (!equipmentSlots[i].IsEmpty)
            {
                content += i + "-" + equipmentSlots[i].Items.Peek().Item.ItemName + ";";
                //0-Hammer;
            }
        }

        PlayerPrefs.SetString("CharPanel", content);
        PlayerPrefs.Save();
    }

    public override void LoadInventory()
    {
        foreach (Slot slot in equipmentSlots)
        {
            slot.ClearSlot();
        }

        string content = PlayerPrefs.GetString("CharPanel");
        string[] splitContent = content.Split(';');
        for (int i = 0; i < splitContent.Length - 1; i++)
        {
            string[] splitValues = splitContent[i].Split('-');
            //[0]1
            //[1]MightyHanmmer
            int index = Int32.Parse(splitValues[0]);
            string itemName = splitValues[1];

            GameObject loadedItem = Instantiate(InventoryManager.Instance.itemObject);


            loadedItem.AddComponent<ItemScript>();


            if (index == 9 || index == 10)
            {
                loadedItem.GetComponent<ItemScript>().Item = InventoryManager.Instance.ItemContainer.Weapons.Find(x => x.ItemName == itemName);
            }
            else
            {
                loadedItem.GetComponent<ItemScript>().Item = InventoryManager.Instance.ItemContainer.Equipment.Find(x => x.ItemName == itemName);
            }

            equipmentSlots[index].AddItem(loadedItem.GetComponent<ItemScript>());

            Destroy(loadedItem);

            CalcStats();

        }
    }
}
