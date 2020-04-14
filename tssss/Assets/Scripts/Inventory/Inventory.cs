using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Main_Stage map;

    public bool set = true;

    /// <summary>
    /// The number of rows
    /// </summary>
    public int rows;  // Number of slot rows;


    /// <summary>
    /// The number of slots
    /// </summary>
    public int slots;  //Number of slots

    /// <summary>
    /// The number of empty slots in the inventory
    /// </summary>
    private int emptySlots;

    /// <summary>
    /// The width and height of the inventory
    /// </summary>
    protected float inventoryWidth, inventoryHight;
    /// <summary>
    /// The left and top slots padding
    /// </summary>
    public float slotPaddingLeft, slotPaddingTop;   // The top space berween eachh slot

    /// <summary>
    /// The size of each slot
    /// </summary>
    public float slotSize;  // The size of each slot

    /// <summary>
    /// A list of all the slots in the inventory
    /// </summary>
    protected List<GameObject> allSlots;





    /// <summary>
    /// A reference to the inventorys RectTransform
    /// </summary>
    protected RectTransform inventoryRect;


    protected float hoverYOffset;


    /// <summary>
    /// The inventory's canvas grop,this is used for hiding the inventory
    /// </summary>
    public CanvasGroup canvasGroup;

    private bool fadingIn;

    private bool fadingOut;
    public bool FadingOut
    {
        get { return fadingOut; }

    }


    public float fadeTime;

    /*
    /// <summary>
    /// The UI element that we are using when we need to split a stack
    /// </summary>
    public GameObject selectStackSize;
    */


    private bool isOpen;

    public bool instantClose = false;

    public static bool mouseInside = false;


    public bool IsOpen
    {
        get { return isOpen; }
        set { isOpen = value; }
    }







    public int EmptySlots
    {
        get { return emptySlots; }
        set { emptySlots = value; }
    }
    /// <summary>
    /// a reference to the player
    /// </summary>
    protected static GameObject playerRef;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        isOpen = false;
        playerRef = GameObject.Find("Unit");

        //Creates the inventory layout
        CreateLayout();


        InventoryManager.Instance.MovingSlot = GameObject.Find("MovingSlot").GetComponent<Slot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0)) // Chacks if the user lifted the first mousebutton
        {
            //removes the selected item from the inventory
            //if (!mouseInside && InventoryManager.Instance.From != null) //if we click outside the inventory and the have picked up an item
            //{
            //    InventoryManager.Instance.From.GetComponent<Image>().color = Color.white;  //rests the slots color

            //    //foreach (ItemScript item in InventoryManager.Instance.From.Items)
            //    //{
            //    //    float angle = UnityEngine.Random.Range(0.0f, Mathf.PI * 2);

            //    //    Vector3 v = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));

            //    //    v *= 25;


            //    //    GameObject tmpDrp = (GameObject)GameObject.Instantiate(InventoryManager.Instance.dropItem, playerRef.transform.position - v, Quaternion.identity);


            //    //    tmpDrp.AddComponent<ItemScript>();
            //    //    tmpDrp.GetComponent<ItemScript>().Item = item.Item;
            //    //}


            //    InventoryManager.Instance.From.ClearSlot();  //removes the item from the slot

            //    if (InventoryManager.Instance.From.transform.parent == CharacterPanel.Instance.transform)
            //    {
            //        CharacterPanel.Instance.CalcStats();
            //    }

            //    Destroy(GameObject.Find("Hover"));  //removes the hover icon

            //    //Resets the objects
            //    InventoryManager.Instance.To = null;
            //    InventoryManager.Instance.From = null;


            //}
            //if (!InventoryManager.Instance.eventSystem.IsPointerOverGameObject(-1) && !InventoryManager.Instance.MovingSlot.IsEmpty)
            //{

            //    foreach (ItemScript item in InventoryManager.Instance.MovingSlot.Items)
            //    {
            //        float angle = UnityEngine.Random.Range(0.0f, Mathf.PI * 2);

            //        Vector3 v = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));

            //        v *= 25;

            //        GameObject tmpDrp = (GameObject)GameObject.Instantiate(InventoryManager.Instance.dropItem, playerRef.transform.position - v, Quaternion.identity);


            //        tmpDrp.AddComponent<ItemScript>();
            //        tmpDrp.GetComponent<ItemScript>().Item = item.Item;

            //    }



            //    InventoryManager.Instance.MovingSlot.ClearSlot();
            //    Destroy(GameObject.Find("Hover"));
            //}
        }


        if (InventoryManager.Instance.HoverObject != null)  //checks if the hoverobject exists
        {
            //the hoverobject's position
            Vector2 position;

            //translates the mouse screen position into a local position and stores it in the position
            RectTransformUtility.ScreenPointToLocalPointInRectangle(InventoryManager.Instance.canvas.transform as RectTransform, Input.mousePosition, InventoryManager.Instance.canvas.worldCamera, out position);

            //AdditionalCanvasShaderChannels the offset to the position
            position.Set(position.x, position.y - hoverYOffset);

            //sets the hoverobject's position
            InventoryManager.Instance.HoverObject.transform.position = InventoryManager.Instance.canvas.transform.TransformPoint(position);

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
        }

    }

    public void OnDrag()
    {
        if (isOpen)
        {
            MoveInventory(); //Moves the inventory around
        }

    }

    public void PointerExit()
    {
        //Debug.Log("PointerExit");
        mouseInside = false;
        HideToolTip();
    }

    public void PointerEnter()
    {
        if (canvasGroup.alpha > 0)
        {
            //  Debug.Log("PointerEnter");
            mouseInside = true;
        }
    }


    public virtual void Open()
    {

        if (canvasGroup.alpha > 0) //if our inventory is visible, then we know that it is open
        {
            canvasGroup.alpha = 0;
            // StartCoroutine("FadeOut");  //close the inventory
            PutItemBack(); //put all items we have in our hand back in the inventory
            HideToolTip();
            isOpen = false;
            canvasGroup.blocksRaycasts = false;


        }
        else //if it isn't open then it's closed and we need to fade in
        {

            //StartCoroutine("FadeIn");
            canvasGroup.alpha = 1;
            isOpen = true;
            canvasGroup.blocksRaycasts = true;
        }


    }

    /// <summary>
    /// shows the tooltip
    /// </summary>
    /// <param name="slot"></param>
    public virtual void ShowToolTip(GameObject slot)
    {
        //saves a reference to the slot we just moused over
        Slot tmpslot = slot.GetComponent<Slot>();

        //if the slot contains an item and we arent splittingor moving any items then we can show the tooltip
        if (slot.GetComponentInParent<Inventory>().isOpen && !tmpslot.IsEmpty && InventoryManager.Instance.HoverObject == null && !InventoryManager.Instance.selectStackSize.activeSelf)
        {
            //Gets the infomation from the item on the slot we just moved our mouse over
            InventoryManager.Instance.visualTextObject.text = tmpslot.CurrentItem.GetTooltip(this);

            //makes sure that the tooltip has the correct size
            InventoryManager.Instance.sizeTextObject.text = InventoryManager.Instance.visualTextObject.text;

            //shows the tool tip
            InventoryManager.Instance.tooltipObject.SetActive(true);

            //calculates the position while taking the padding into account
            float xPos = slot.transform.position.x + slotPaddingLeft;
            float yPos = slot.transform.position.y - slot.GetComponent<RectTransform>().sizeDelta.y - slotPaddingTop;

            //sets the position
            InventoryManager.Instance.tooltipObject.transform.position = new Vector2(xPos, yPos);
        }



    }
    public void HideToolTip()
    {
        InventoryManager.Instance.tooltipObject.SetActive(false);

    }

    public virtual void SaveInventory()
    {
        string content = string.Empty;

        for (int i = 0; i < allSlots.Count; i++)
        {
            Slot tmp = allSlots[i].GetComponent<Slot>();
            if (!tmp.IsEmpty)//we only want to save the info if the slot contains an item
            {
                //Creates a string with this format :slotindex-itemtype-amountofitems;this string can be read so that we can rebuild the inventory
                content += i + "-" + tmp.CurrentItem.Item.ItemName.ToString() + "-" + tmp.Items.Count.ToString() + ";";
                //"0-Mana-3;2-HEALT-2;"
            }

        }
        //Stores all the info in the playerPrefs
        PlayerPrefs.SetString(gameObject.name + "content", content);
        PlayerPrefs.SetInt(gameObject.name + "slots", slots);
        PlayerPrefs.SetInt(gameObject.name + "rows", rows);
        PlayerPrefs.SetFloat(gameObject.name + "slotPaddingLeft", slotPaddingLeft);
        PlayerPrefs.SetFloat(gameObject.name + "slotPaddingTop", slotPaddingTop);
        PlayerPrefs.SetFloat(gameObject.name + "slotSize", slotSize);
        PlayerPrefs.SetFloat(gameObject.name + "xPos", inventoryRect.position.x);
        PlayerPrefs.SetFloat(gameObject.name + "yPos", inventoryRect.position.y);
        PlayerPrefs.Save();


    }
    /// <summary>
    /// Loads the inventory
    /// </summary>
    public virtual void LoadInventory()
    {
        //Loads all the inventory's data from the playerprefs
        string content = PlayerPrefs.GetString(gameObject.name + "content");

        if (content != string.Empty)
        {
            slots = PlayerPrefs.GetInt(gameObject.name + "slots");
            rows = PlayerPrefs.GetInt(gameObject.name + "rows");
            slotPaddingLeft = PlayerPrefs.GetFloat(gameObject.name + "slotPaddingLeft");
            slotPaddingTop = PlayerPrefs.GetFloat(gameObject.name + "slotPaddingTop");
            slotSize = PlayerPrefs.GetFloat(gameObject.name + "slotSize");


            //Sets the inventorys position
            inventoryRect.position = new Vector3(PlayerPrefs.GetFloat(gameObject.name + "xPos"), PlayerPrefs.GetFloat(gameObject.name + "yPos"), inventoryRect.position.z);

            //Recreates the inventory's layout
            CreateLayout();

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
                    allSlots[index].GetComponent<Slot>().AddItem(loadedItem.GetComponent<ItemScript>());
                    Destroy(loadedItem);

                }

            }
        }
    }

    //creates the inventory's layout
    public virtual void CreateLayout()
    {

        if (allSlots != null)
        {
            foreach (GameObject go in allSlots)
            {
                Destroy(go);
            }
        }

        //Instantiates the allSlot's list
        allSlots = new List<GameObject>();

        //calculates the hoverYOffset by taking 1% of the slot size
        hoverYOffset = slotSize * 0.10f;

        //stores the number of empty slots
        emptySlots = slots;

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

        for (int y = 0; y < rows; y++)
        {

            for (int x = 0; x < columns; x++)
            {
                //Instantiates the slot and creates areference to it
                GameObject newSlot = (GameObject)Instantiate(InventoryManager.Instance.slotPrefab);

                //makes a reference to the rect transform 
                RectTransform slotRect = newSlot.GetComponent<RectTransform>();

                //sets the slots name
                newSlot.name = "Slot";

                //Sets the canvas as the parent of the slots, so that it will be visible on the screen
                newSlot.transform.SetParent(this.transform.parent);

                //Sets the slots position
                slotRect.localPosition = inventoryRect.localPosition + new Vector3(slotPaddingLeft * (x + 1) + (slotSize * x), -slotPaddingTop * (y + 1) - (slotSize * y));

                //Sets the size of the slot
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize * InventoryManager.Instance.canvas.scaleFactor);
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize * InventoryManager.Instance.canvas.scaleFactor);
                newSlot.transform.SetParent(this.transform);

                //adds the new slots to the slot list
                allSlots.Add(newSlot);

                newSlot.GetComponent<Button>().onClick.AddListener
                    (
                    delegate { MoveItem(newSlot); }

                    );
            }
        }
    }










    public bool AddItem(ItemScript item)
    {

        if (item.Item.MaxSize == 1) //If the item isn't stackable
        {
            //Places the item at an empty slot
            return PlaceEmpty(item);

        }
        else //If the item is stackable
        {
            foreach (GameObject slot in allSlots)  //runs through all slots in the inventory
            {
                Slot tmp = slot.GetComponent<Slot>();  //create a reference to the slot

                if (!tmp.IsEmpty)  //if the item isn't empty
                {

                    if (tmp.CurrentItem.Item.ItemName == item.Item.ItemName && tmp.IsAvailable) // check if the om the slot is the same type as the item we want to pick up
                    {
                        if (!InventoryManager.Instance.MovingSlot.IsEmpty && InventoryManager.Instance.Clicked.GetComponent<Slot>() == tmp.GetComponent<Slot>())
                        {
                            continue;
                        }
                        else
                        {
                            tmp.AddItem(item);  //adds the item to the inventory
                            return true;
                        }
                    }

                }
            }
            if (emptySlots > 0) //Places the item on an empty slots
            {

                return PlaceEmpty(item);
            }
        }
        return false;
    }


    public void MoveInventory()
    {
        Vector2 mousePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(InventoryManager.Instance.canvas.transform as RectTransform, new Vector3(Input.mousePosition.x - (inventoryRect.sizeDelta.x / 2 * InventoryManager.Instance.canvas.scaleFactor), Input.mousePosition.y + (inventoryRect.sizeDelta.y / 2 * InventoryManager.Instance.canvas.scaleFactor)), InventoryManager.Instance.canvas.worldCamera, out mousePos);

        transform.position = InventoryManager.Instance.canvas.transform.TransformPoint(mousePos);

    }


    private bool PlaceEmpty(ItemScript item)
    {
        if (emptySlots > 0)  //if we have atleast 1 empty slot
        {
            foreach (GameObject slot in allSlots)  //runs through all slots
            {
                Slot tmp = slot.GetComponent<Slot>(); // creates a reference to the slot

                if (tmp.IsEmpty)//if the slot is empty
                {
                    tmp.AddItem(item); //Adds the item

                    return true;

                }

            }
        }


        return false;

    }

    public virtual void MoveItem(GameObject clicked)
    {
        if (isOpen)
        {
            CanvasGroup cg = clicked.transform.parent.GetComponent<CanvasGroup>();


            if (cg != null && cg.alpha > 0 || clicked.GetComponent<CanvasGroup>().alpha > 0)
            {



                //Careates a reference to the object that we just clicked
                InventoryManager.Instance.Clicked = clicked;

                if (!InventoryManager.Instance.MovingSlot.IsEmpty) //Checks if we are splitting an item
                {
                    Slot tmp = clicked.GetComponent<Slot>();  //Get's a reference to the slot we just clicked


                    if (tmp.IsEmpty) //if the clicked slot is empty, then we can simply put all items down
                    {

                        tmp.AddItems(InventoryManager.Instance.MovingSlot.Items);  ////Puts all the items down
                        InventoryManager.Instance.MovingSlot.Items.Clear();         //clears the moving slot
                        Destroy(GameObject.Find("Hover")); //removes the hover object

                    }
                    else if (!tmp.IsEmpty && InventoryManager.Instance.MovingSlot.CurrentItem.Item.ItemName == tmp.CurrentItem.Item.ItemName && tmp.IsAvailable)  //クリックしたスロットが空ではないかつmovingslotの要素とクリックした要素が同じかつクリックした要素がmaxSize > Items.Count
                    {
                        //Merges two stacks of the same type
                        MergeStacks(InventoryManager.Instance.MovingSlot, tmp);  //マージスタック実行

                    }
                }
                else if (InventoryManager.Instance.From == null && clicked.transform.parent.GetComponent<Inventory>().isOpen && !Input.GetKey(KeyCode.LeftShift)) // If we haven't picked up an item
                {
                    if (!clicked.GetComponent<Slot>().IsEmpty && !GameObject.Find("Hover")) //If the slot we vliked sin't empty
                    {

                        InventoryManager.Instance.From = clicked.GetComponent<Slot>();  //the slot we ar emoving from

                        InventoryManager.Instance.From.GetComponent<Image>().color = Color.gray;  //sets the from slots color to gray , to visually indicate that its the slot we are moving from

                        CreateHoverIcon();


                    }

                }
                else if (InventoryManager.Instance.To == null && !Input.GetKey(KeyCode.LeftShift))   //selects the slot we are moving to
                {
                    //Debug.Log("to == null && !Input.GetKey(KeyCode.LeftShift)");
                    InventoryManager.Instance.To = clicked.GetComponent<Slot>();  //sets the to object
                    Destroy(GameObject.Find("Hover")); // destorys the hover object
                }
                if (InventoryManager.Instance.To != null && InventoryManager.Instance.From != null)  // if both to and from are null then we are done moving
                {
                    if (!InventoryManager.Instance.To.IsEmpty && InventoryManager.Instance.From.CurrentItem.Item.ItemName == InventoryManager.Instance.To.CurrentItem.Item.ItemName && InventoryManager.Instance.To.IsAvailable)
                    {
                        MergeStacks(InventoryManager.Instance.From, InventoryManager.Instance.To);
                    }
                    else
                    {
                        Slot.SwapItems(InventoryManager.Instance.From, InventoryManager.Instance.To);

                    }
                    //Resets all values
                    InventoryManager.Instance.From.GetComponent<Image>().color = Color.white;
                    InventoryManager.Instance.To = null;
                    InventoryManager.Instance.From = null;
                    Destroy(GameObject.Find("Hover"));


                }
            }
            /*
            if (CraftingBench.Instance.isOpen)
            {
                CraftingBench.Instance.UpdatePreview();
            }
            */
        }


    }
    private void CreateHoverIcon()
    {
        InventoryManager.Instance.HoverObject = (GameObject)Instantiate(InventoryManager.Instance.iconPrefab);  //Instantiates the hover object


        InventoryManager.Instance.HoverObject.GetComponent<Image>().sprite = InventoryManager.Instance.Clicked.GetComponent<Image>().sprite;  //Sets the sprites on the hover object so that it reflects the object we are moing

        InventoryManager.Instance.HoverObject.name = "Hover"; // Sets the name of the hover object

        //Creates references to the transforms
        RectTransform hoverTransform = InventoryManager.Instance.HoverObject.GetComponent<RectTransform>();
        RectTransform clickedTransform = InventoryManager.Instance.Clicked.GetComponent<RectTransform>();

        ///Sets the size of the hoverobject so that it has the same size as the cliked object
        hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, clickedTransform.sizeDelta.x);
        hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, clickedTransform.sizeDelta.y);

        //sets the hoverobject's parent as the canvas, so that it is visible in the game
        InventoryManager.Instance.HoverObject.transform.SetParent(GameObject.Find("Canvas").transform, true);

        //Sets the local scale to make usre that it has the correct size
        InventoryManager.Instance.HoverObject.transform.localScale = InventoryManager.Instance.Clicked.gameObject.transform.localScale;

        InventoryManager.Instance.HoverObject.transform.GetChild(0).GetComponent<Text>().text = InventoryManager.Instance.MovingSlot.Items.Count > 1 ? InventoryManager.Instance.MovingSlot.Items.Count.ToString() : string.Empty;
        // Debug.Log(hoverObject.transform.GetChild(0).GetComponent<Text>().text);
    }


    private void PutItemBack()   //持ってる状態でインベントリをけす銅さ？
    {
        if (InventoryManager.Instance.From != null)
        {
            Destroy(GameObject.Find("Hover"));
            InventoryManager.Instance.From.GetComponent<Image>().color = Color.white;
            InventoryManager.Instance.From = null;

        }
        else if (!InventoryManager.Instance.MovingSlot.IsEmpty)
        {
            //Removes the hover icon
            Destroy(GameObject.Find("Hover"));
            //puts the items back one by one
            foreach (ItemScript item in InventoryManager.Instance.MovingSlot.Items)
            {
                //puts the items back one by one
                InventoryManager.Instance.Clicked.GetComponent<Slot>().AddItem(item);

            }

            InventoryManager.Instance.MovingSlot.ClearSlot();//makes sure that the moving slot is empty
        }
        //Hides the UI for splitting a stack
        InventoryManager.Instance.selectStackSize.SetActive(false);

    }

    /// <summary>
    /// Splits a stack of items
    /// </summary>
    public void SplitStack()   // →←Button
    {
        ///Hids the UI for splitting a stack
        InventoryManager.Instance.selectStackSize.SetActive(false);

        if (InventoryManager.Instance.SplitAmount == InventoryManager.Instance.MaxStackCount)//if we picked up all the items then we dont need to
        {
            MoveItem(InventoryManager.Instance.Clicked);

        }
        else if (InventoryManager.Instance.SplitAmount > 0) //if the split amount is larger than 0 than we need to pick u
        {
            InventoryManager.Instance.MovingSlot.Items = InventoryManager.Instance.Clicked.GetComponent<Slot>().RemoveItems(InventoryManager.Instance.SplitAmount);

            CreateHoverIcon(); //careates the hover icon
        }

    }

    public void ChangeStackText(int i)  //OKButton    
    {
        InventoryManager.Instance.SplitAmount += i;

        if (InventoryManager.Instance.SplitAmount < 0)
        {

            InventoryManager.Instance.SplitAmount = 0;
        }
        if (InventoryManager.Instance.SplitAmount > InventoryManager.Instance.MaxStackCount)
        {
            InventoryManager.Instance.SplitAmount = InventoryManager.Instance.MaxStackCount;

        }

        InventoryManager.Instance.stackText.text = InventoryManager.Instance.SplitAmount.ToString();

    }


    public void MergeStacks(Slot source, Slot destination)
    {
        int max = destination.CurrentItem.Item.MaxSize - destination.Items.Count; //  (先　の先頭の要素の)最大数　ー　(先の)要素数     5 - 2   max3   5-3 max2
        //Debug.Log("max"+ max);
        int count = source.Items.Count < max ? source.Items.Count : max;  // （元の要素数 < max  ならcount  元の要素数）  元の要素がmax 以上ならmax

        for (int i = 0; i < count; i++)   //count 数繰り返す
        {
            // Debug.Log("count" + count);
            destination.AddItem(source.RemoveItem());   //先に元のpopをstackに加える
            InventoryManager.Instance.HoverObject.transform.GetChild(0).GetComponent<Text>().text = InventoryManager.Instance.MovingSlot.Items.Count.ToString();
        }
        if (source.Items.Count == 0)  // 元の要素数が0なら
        {
            source.ClearSlot(); //元のスタックを消す
            Destroy(GameObject.Find("Hover"));

        }

    }

    protected virtual IEnumerator FadeOut()
    {
        if (!fadingOut)  //checks if we are alredy fading out
        {
            //sets the current state
            fadingOut = true;
            fadingIn = false;

            //makes sure that we are not fading out the at same time
            StopCoroutine("FadeIn");

            //sets the values for fading
            float startAlpha = canvasGroup.alpha;

            //calculates the rate so that we can fade over * amount of seconds
            float rate = 1.0f / fadeTime;

            //progresses over the set time
            float progress = 0.0f;

            //progresses over the set time
            while (progress < 1.0)
            {
                //lerps from the start alpha to 0 to make the inventory
                canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, progress);

                //adds to the progress so that we will get c;pse tp pit goal
                progress += rate * Time.deltaTime;

                if (instantClose)
                {
                    break;
                }


                yield return null;
            }
            //sets the end codition to make sure we are 100% invisble
            canvasGroup.alpha = 0;
            //set the status
            instantClose = false;
            fadingOut = false;
        }

    }


    private IEnumerator FadeIn()
    {
        if (!fadingIn)
        {

            fadingOut = false;
            fadingIn = true;
            StopCoroutine("FadeOut");

            float startAlpha = canvasGroup.alpha;

            float rate = 1.0f / fadeTime;

            float progress = 0.0f;

            while (progress < 1.0)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, 1, progress);

                progress += rate * Time.deltaTime;

                yield return null;
            }

            canvasGroup.alpha = 1;
            fadingIn = false;
        }

    }
}



