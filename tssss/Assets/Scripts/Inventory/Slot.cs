using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour,IPointerClickHandler
{
    /// <summary>
    /// the slots canvas group
    /// </summary>
    private CanvasGroup canvasGroup;

  

    private Stack<ItemScript> items; //  <Item>= The type of objects the stack will ocntain      items = the stack's identifier
    public Stack<ItemScript> Items
    {
        get { return items; }
        set { items = value; }
    }



    public Text stackTxt;

    public Sprite slotEmpty;

    /// <summary>
    /// the slot's highlighted sprite
    /// </summary>
    public Sprite slotHighlight;

    public bool IsEmpty
    {

        get { return Items.Count == 0; }
    }


    public bool IsAvailable
    {

        get { return CurrentItem.Item.MaxSize > Items.Count; }
    }


    public ItemScript CurrentItem
    {

        get{ return Items.Peek(); }
    }
    /// <summary>
    /// Defines what kind of items this slot can contain
    /// </summary>
    public ItemType canContain;

    private bool clickAble = true;

    public bool ClickAble
    {
        get { return clickAble; }
        set { clickAble = value; }

    }


    void Awake()
    {

        Items = new Stack<ItemScript>(); //Instantiates the items stack

    }



    // Use for initialization
    void Start()
    {
        //Creates areference to the slot slot's recttransform
        RectTransform slotRect = GetComponent<RectTransform>();

        //Creates areference to the stackTxt's recttransform
        RectTransform txtRect = stackTxt.GetComponent<RectTransform>();

        int txtScleFactor = (int)(slotRect.sizeDelta.x * 0.60);

        //Set the min and max textSize of the  stackTxt
        stackTxt.resizeTextMaxSize = txtScleFactor;
        stackTxt.resizeTextMinSize = txtScleFactor;

        //Sets the min and max textsize of the stacktxt
        txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotRect.sizeDelta.x);
        txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotRect.sizeDelta.y);

        if(transform.parent != null)
        {
           
            canvasGroup = transform.parent.GetComponent<CanvasGroup>();            
            /*
            EventTrigger trigger = GetComponentInParent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((eventData) => { transform.parent.GetComponent<Inventory>().ShowToolTip(gameObject); });
            
            trigger.triggers.Add(entry);
*/
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(ItemScript item)
    {

        if (IsEmpty) {
            transform.parent.GetComponent<Inventory>().EmptySlots--;
        }

        Items.Push(item); //Adds the item to the stack

        if(Items.Count >1)  //checks if we have a stacked item
        {
            stackTxt.text = Items.Count.ToString();  //if the item is stacked then we need to write the stack number on to

        }

        ChangeSprite(item.spriteNeutral, item.spriteHighlighted); //Changes the sprite so that it reflects the item hte s

    }

    public void AddItems(Stack<ItemScript> items)
    {

        this.Items = new Stack<ItemScript>(items);

        stackTxt.text = items.Count > 1 ? items.Count.ToString() : string.Empty;

        ChangeSprite(CurrentItem.spriteNeutral, CurrentItem.spriteHighlighted);

        
    }


    private void ChangeSprite(Sprite neutral,Sprite highlight)
    {

        GetComponent<Image>().sprite = neutral;

        SpriteState st = new SpriteState();
        st.highlightedSprite = highlight;
        st.pressedSprite = neutral;

        GetComponent<Button>().spriteState =st;

    }


    /// <summary>
    /// Uses an item on the slot
    /// </summary>
    private void UseItem()
    {
        if (!IsEmpty)
        {

            if (transform.parent.GetComponent<Inventory>() is VendorInventory)
            {
                //if(CurrentItem.Item.BuyPrice <= PlayerScript.Instance.Gold && PlayerScript.Instance.inventory.AddItem(CurrentItem))
                //{
                //    PlayerScript.Instance.Gold -= CurrentItem.Item.BuyPrice;
                //}
            }
            else if (VendorInventory.Instance.IsOpen)
            {
                PlayerScript.Instance.Gold += CurrentItem.Item.SellPrice;
                RemoveItem();
            }
            else if (clickAble) //If there is an item on the slot
            {
                Items.Peek().Use(this); //Removes the top item from the stack and uses it


                stackTxt.text = Items.Count > 1 ? Items.Count.ToString() : string.Empty; // writes the correct stack 

                if (IsEmpty)  // the last item from the inventory
                {

                    ChangeSprite(slotEmpty, slotHighlight); //Change the sprite to empty if the slot is empty

                    transform.parent.GetComponent<Inventory>().EmptySlots++; //Adds 1 to the amount of empty slots


                }

            }
        }


    }

    /// <summary>
    /// clears the slot
    /// </summary>
    public void ClearSlot()
    {
        //clears all items on the slot
        items.Clear();
        //changes the sprite to empty
        ChangeSprite(slotEmpty, slotEmpty);
        //clears the text
        stackTxt.text = string.Empty;


        if(transform.parent != null) {
            transform.parent.GetComponent<Inventory>().EmptySlots++;
        }


    }

    public Stack<ItemScript> RemoveItems(int amount)
    {
        Stack<ItemScript> tmp = new Stack<ItemScript>();

        for(int i = 0; i < amount; i++)
        {

            tmp.Push(items.Pop());
        }


        stackTxt.text = items.Count > 1 ? items.Count.ToString() : string.Empty;

        //Returns the removed item
        return tmp;


    }

    /// <summary>
    /// Removes the top item from the slot and returns it
    /// </summary>
    /// <returns></returns>
    public ItemScript RemoveItem()
    {

        if (!IsEmpty)
        {
            //Remove the item from the stack and stores it in a tmp variable
            ItemScript tmp = items.Pop();


            //Makes sure that the correct number is shown on the slot
            stackTxt.text = items.Count > 1 ? items.Count.ToString() : string.Empty;

            if (IsEmpty)
            {
                ClearSlot();
            }

            //Returns the removed item
            return tmp;
        }
        return null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        

        //If the right mousebutton was clicked and we aren't moveing an item and the inventory is visible
        if (eventData.button == PointerEventData.InputButton.Right && !GameObject.Find("Hover") && canvasGroup != null&& canvasGroup.alpha > 0)
        {
            //Uses an item on the slot
            UseItem();
          

        }
        //checks if we need to show the split stack dialog , this is only done if we shiftclik a slot and we aren't moving an item
        else if (eventData.button == PointerEventData.InputButton.Left && Input.GetKey(KeyCode.LeftShift) && !IsEmpty && !GameObject.Find("Hover") && transform.parent.GetComponent<Inventory>().IsOpen) {

            //the dialogs spawnposition
            Vector2 position;

            //translates the mouse position to onscreen coords so that we can spawn the dialog at the correct position
            RectTransformUtility.ScreenPointToLocalPointInRectangle(InventoryManager.Instance.canvas.transform as RectTransform, Input.mousePosition, InventoryManager.Instance.canvas.worldCamera, out position);

            //shows the dialog
            InventoryManager.Instance.selectStackSize.SetActive(true);

            //sets the position
            InventoryManager.Instance.selectStackSize.transform.position = InventoryManager.Instance.canvas.transform.TransformPoint(position);


            //Tell the inventory the item count on the selected slot
            InventoryManager.Instance.SetStackInfo(items.Count);

                    

      

        }
      
    }

    public static void SwapItems(Slot from,Slot to)
    {

        if(to != null && from != null)
        {
            bool calcStats = from.transform.parent == CharacterPanel.Instance.transform || to.transform.parent == CharacterPanel.Instance.transform;


            if (CanSwap(from,to))
            {

                Stack<ItemScript> tmpTo = new Stack<ItemScript>(to.Items);  //store the items from the to slot, so that we can do a swap

                to.AddItems(from.Items);   //stores the items in the "from" slot in the "to " slot


                if (tmpTo.Count == 0) //if "to" slot if 0 then we dont need to move anything to the "from" slot
                {
                    to.transform.parent.GetComponent<Inventory>().EmptySlots--;
                    from.ClearSlot();//clears the from slot
                                     // Debug.Log("tmpTo.Count ==0");
                }
                else
                {
                    // Debug.Log("tmpTo.Count ==0 else");
                    from.AddItems(tmpTo); //If the "to" slot contains items thne we need to move the to the "from" slot
                }


            }

            if (calcStats)
            {
                CharacterPanel.Instance.CalcStats();
            }

        }




    }

    private static bool CanSwap(Slot from,Slot to)
    {
        ItemType fromType = from.CurrentItem.Item.ItemType;

        if(to.canContain == from.canContain) //swapping two items in the inventory
        {
            return true;
        }
        if(fromType != ItemType.OFFHAND && to.canContain == fromType) //equipping items
        {
            return true;
        }
        if(to.canContain == ItemType.GENERIC && (to.IsEmpty || to.CurrentItem.Item.ItemType == fromType)) //Dequipping an item
        {
            return true;
        }
        if(fromType == ItemType.MAINHAND && to.canContain == ItemType.GENERICWEAPON) //equip main hand
        {
            return true;
        }
        if(fromType == ItemType.TWOHAND && to.canContain == ItemType.GENERICWEAPON && CharacterPanel.Instance.OffHandSlot.IsEmpty)
        {
            return true;
        }
        if(fromType == ItemType.OFFHAND &&(to.IsEmpty||to.CurrentItem.Item.ItemType==ItemType.OFFHAND)&&(CharacterPanel.Instance.WeaponSlot.IsEmpty || CharacterPanel.Instance.WeaponSlot.CurrentItem.Item.ItemType != ItemType.TWOHAND))
        {
            return true;
        }

        return false;
    }

}
