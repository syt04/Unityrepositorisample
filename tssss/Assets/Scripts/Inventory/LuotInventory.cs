using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LuotInventory : Inventory
{
    private List<Stack<ItemScript>> luotItems;
    private int luotSlots;


    public override void CreateLayout()
    {
        allSlots = new List<GameObject>();

        for (int i = 0; i < slots; i++)
        {
            GameObject newSlot = Instantiate(InventoryManager.Instance.slotPrefab);

            newSlot.name = "Slot";

            newSlot.transform.SetParent(this.transform);

            allSlots.Add(newSlot);

            newSlot.GetComponent<Button>().onClick.AddListener
                (
                delegate { MoveItem(newSlot); }
                );

            newSlot.SetActive(false);

        }

        //calculates the hoverYOffset by taking 1% of the slot size
        hoverYOffset = slotSize * 0.01f;
    }

    public void UpdateLayout(List<Stack<ItemScript>> items, int rows, int slots)
    {
        this.luotItems = items;
        this.luotSlots = slots;

        //calculates the width of the inventory
        inventoryWidth = (slots / rows) * (slotSize + slotPaddingLeft) + slotPaddingLeft;
        //calculates the hight of the inventory
        inventoryHight = rows * (slotSize + slotPaddingTop) + slotPaddingTop;

        //Creates a reference to the inventory's RectTransform
        inventoryRect = GetComponent<RectTransform>();

        //Sets the with and height of the inventory
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, inventoryWidth);
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventoryHight);

        //Calculates the amount of columns
        int columns = slots / rows;

        int index = 0;


        for (int y = 0; y < rows; y++)
        {

            for (int x = 0; x < columns; x++)
            {
                //Instantiates the slot and creates areference to it
                GameObject newSlot = allSlots[index];

                //makes a reference to the rect transform 
                RectTransform slotRect = newSlot.GetComponent<RectTransform>();


                //Sets the canvas as the parent of the slots, so that it will be visible on the screen
                newSlot.transform.SetParent(this.transform.parent);

                //Sets the slots position
                slotRect.localPosition = inventoryRect.localPosition + new Vector3(slotPaddingLeft * (x + 1) + (slotSize * x), -slotPaddingTop * (y + 1) - (slotSize * y));

                //Sets the size of the slot
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize * InventoryManager.Instance.canvas.scaleFactor);
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize * InventoryManager.Instance.canvas.scaleFactor);

                newSlot.transform.SetParent(this.transform);

                if (items.Count != 0 && items.Count >= index && items[index].Count > 0)
                {
                    newSlot.GetComponent<Slot>().AddItems(items[index]);
                }

                index++;

            }
        }
        string[] a = { "Health potion" , "Awesome Sword" };
        GiveItem(a);

    }

    public override void Open()
    {
        base.Open();

        if (IsOpen)
        {
            MoveItemsFromChest();
        }
    }


    public void MoveItemsToChest()
    {

        luotItems.Clear();

        for (int i = 0; i < luotSlots; i++)
        {
            Slot tmpSlot = allSlots[i].GetComponent<Slot>();

            if (!tmpSlot.IsEmpty)
            {
                luotItems.Add(new Stack<ItemScript>(tmpSlot.Items));

                if (!IsOpen)
                {
                    tmpSlot.ClearSlot();

                }
            }
            else
            {
                luotItems.Add(new Stack<ItemScript>());
            }
            if (!IsOpen)
            {
                allSlots[i].SetActive(false);

            }
        }
    }

    public void MoveItemsFromChest()
    {
        for (int i = 0; i < luotSlots; i++)
        {
            if (luotItems.Count != 0 && luotItems.Count >= i && luotItems[i] != null && luotItems[i].Count > 0)
            {
                GameObject newSlot = allSlots[i];
                newSlot.GetComponent<Slot>().AddItems(luotItems[i]);


            }

        }

        for (int i = 0; i < luotSlots; i++)
        {
            allSlots[i].SetActive(true);
        }

    }

    protected override IEnumerator FadeOut()
    {
        yield return StartCoroutine(base.FadeOut());

        MoveItemsToChest();
    }


    public override void LoadInventory()
    {
        foreach (GameObject slot in allSlots)
        {
            slot.GetComponent<Slot>().ClearSlot();
        }
    }

    public override void SaveInventory()
    {

    }

    protected void GiveItem(string[] itemName)
    {

        Item tmp = null;
        int amount = itemName.Length;

        for (int i = 0; i < amount; i++) //adds the correct amount of items to the inventory
        {
            GameObject loadedItem = Instantiate(InventoryManager.Instance.itemObject);

            if (tmp == null)
            {
                tmp = InventoryManager.Instance.ItemContainer.Consumeables.Find(item => item.ItemName == itemName[i]);

            }
            if (tmp == null)
            {
                tmp = InventoryManager.Instance.ItemContainer.Equipment.Find(item => item.ItemName == itemName[i]);
            }
            if (tmp == null)
            {
                tmp = InventoryManager.Instance.ItemContainer.Weapons.Find(item => item.ItemName == itemName[i]);
            }
            if (tmp == null)
            {
                tmp = InventoryManager.Instance.ItemContainer.Materials.Find(item => item.ItemName == itemName[i]);
            }


            loadedItem.AddComponent<ItemScript>();
            loadedItem.GetComponent<ItemScript>().Item = tmp;
            allSlots[i].GetComponent<Slot>().AddItem(loadedItem.GetComponent<ItemScript>());

            Destroy(loadedItem);

        }


    }
}
