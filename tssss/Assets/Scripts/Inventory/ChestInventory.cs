using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestInventory : Inventory
{
    private List<Stack<ItemScript>> chestItems;
    private int chestSlots;
    bool Cleate = true;


    public override void CreateLayout()
    {
        allSlots = new List<GameObject>();

        for(int i=0; i< slots; i++)
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

    public void UpdateLayout(List<Stack<ItemScript>> items,int rows,int slots)
    {
        this.chestItems = items;
        this.chestSlots = slots;

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

                if(items.Count != 0&& items.Count >= index && items[index].Count >0)
                {
                    newSlot.GetComponent<Slot>().AddItems(items[index]);
                }

                index++;

            }
        }
        if (Cleate)
        {
 
            GiveItem(8);
           
            Cleate = false;
        }

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

        chestItems.Clear();

        for ( int i = 0;i < chestSlots;i++)
        {
            Slot tmpSlot = allSlots[i].GetComponent<Slot>();

            if (!tmpSlot.IsEmpty)
            {
                chestItems.Add(new Stack<ItemScript>(tmpSlot.Items));

                if (!IsOpen)
                {
                    tmpSlot.ClearSlot();

                }
            }
            else
            {
                chestItems.Add(new Stack<ItemScript>());
            }
            if (!IsOpen)
            {
                allSlots[i].SetActive(false);
               
            }
        }
    }

    public void MoveItemsFromChest()
    {
        for(int i =0; i< chestSlots; i++)
        {
            if(chestItems.Count != 0 && chestItems.Count >= i &&chestItems[i] != null && chestItems[i].Count >0)
            {
                GameObject newSlot = allSlots[i];
                newSlot.GetComponent<Slot>().AddItems(chestItems[i]);


            }

        }

        for(int i =0; i < chestSlots; i++)
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
      foreach(GameObject slot in allSlots)
        {
            slot.GetComponent<Slot>().ClearSlot();
        }
    }

    public override void SaveInventory()
    {
   
    }

    protected void GiveItem(int itemName)
    {
      
        int randomItem;

        Item tmp = null;
        int amount = itemName;
        int randomcount = 1;

        for (int i = 0; i < amount; i++) //adds the correct amount of items to the inventory
        {
            GameObject loadedItem = Instantiate(InventoryManager.Instance.itemObject);
            int randomType = UnityEngine.Random.Range(0, 4);
            

            if (randomType == 0)
            {
               // tmp = InventoryManager.Instance.ItemContainer.Consumeables.Find(item => item.ItemName == itemName[i]);
                randomItem = UnityEngine.Random.Range(0, InventoryManager.Instance.ItemContainer.Consumeables.Count);
                tmp = InventoryManager.Instance.ItemContainer.Consumeables[randomItem];
                randomcount = UnityEngine.Random.Range(1, tmp.MaxSize);
              
            }
            if (randomType == 1)
            {
                //tmp = InventoryManager.Instance.ItemContainer.Equipment.Find(item => item.ItemName == itemName[i]);
                randomItem = UnityEngine.Random.Range(0, InventoryManager.Instance.ItemContainer.Equipment.Count);
                tmp = InventoryManager.Instance.ItemContainer.Equipment[randomItem];
            }
            if (randomType == 2)
            {
                // tmp = InventoryManager.Instance.ItemContainer.Weapons.Find(item => item.ItemName == itemName[i]);
                randomItem = UnityEngine.Random.Range(0, InventoryManager.Instance.ItemContainer.Weapons.Count);
                tmp = InventoryManager.Instance.ItemContainer.Weapons[randomItem];
            }
            if (randomType == 3)
            {
                // tmp = InventoryManager.Instance.ItemContainer.Materials.Find(item => item.ItemName == itemName[i]);
                randomItem = UnityEngine.Random.Range(0, InventoryManager.Instance.ItemContainer.Materials.Count);
                tmp = InventoryManager.Instance.ItemContainer.Materials[randomItem];
                randomcount = UnityEngine.Random.Range(1, tmp.MaxSize);

            }


                loadedItem.AddComponent<ItemScript>();
                loadedItem.GetComponent<ItemScript>().Item = tmp;
                allSlots[i].GetComponent<Slot>().AddItem(loadedItem.GetComponent<ItemScript>());
                
                
        
            
            Destroy(loadedItem);
            tmp = null;
            randomItem = 0;

        }


    }

}
