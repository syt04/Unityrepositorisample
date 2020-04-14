using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

public class InventoryManager : MonoBehaviour
{

    private static InventoryManager instance;

    public static InventoryManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<InventoryManager>();

            }

            return instance;
        }

    }




    /// <summary>
    /// The slots prefab
    /// </summary>
    public GameObject slotPrefab;

    /// <summary>
    /// A prefab used for instantiating the hoverObject
    /// </summary>
    public GameObject iconPrefab;

    public GameObject itemObject;

    /// <summary>
    ///A reference to the object that hovers next to the mouse 
    /// </summary>
    private  GameObject hoverObject;
    public GameObject HoverObject
    {
        get { return hoverObject; }
        set { hoverObject = value; }
    }




   // public GameObject dropItem;


    public GameObject tooltipObject;


    public Text sizeTextObject;


    public Text visualTextObject;

    /// <summary>
    /// A reference to the inventorys canvas
    /// </summary>
    public Canvas canvas;


    /// <summary>
    /// the slots that we are moving and item from
    /// </summary>
    private  Slot from;
    public Slot From
    {
        get { return from; }
        set { from = value; }
    }

    /// <summary>
    /// the slots that we are moving and item to
    /// </summary>
    private  Slot to;
    public Slot To
    {
        get { return to; }
        set { to = value; }
    }



    /// <summary>
    /// The clicked object
    /// </summary>
    private  GameObject clicked;
    public GameObject Clicked
    {
        get { return clicked; }
        set { clicked = value; }
    }


    /// <summary>
    /// the amount of items to pickup(this is the text on the UI element we use for splitting
    /// </summary>
    public Text stackText;


    /// <summary>
    /// The UI element that we are using when we need to split a stack
    /// </summary>
    public GameObject selectStackSize;


    /// <summary>
    /// The amount of items we have in our "hand"
    /// </summary>
    private int splitAmount;
    public int SplitAmount
    {
        get { return splitAmount; }
        set { splitAmount = value; }
    }

    /// <summary>
    /// The maximum amount of items we are allowed to remove from the stack
    /// </summary>
    private int maxStackCount;
    public int MaxStackCount
    {
        get { return maxStackCount; }
        set { maxStackCount = value; }
    }



    /// <summary>
    /// this is sed to store our items when moving them from one slot to another
    /// </summary>
    private Slot movingSlot;
    public Slot MovingSlot
    {
        get { return movingSlot; }
        set { movingSlot = value; }

    }




    /// <summary>
    /// A reference to the EventSystem
    /// </summary>
    public EventSystem eventSystem;



    private ItemContainer itemContainer = new ItemContainer();

    public ItemContainer ItemContainer
    {
        get { return itemContainer; }
        set { itemContainer = value; }
    }

    
    public void Awake()
    {
        /*
        XmlDocument doc = new XmlDocument();
        TextAsset myXmlAsset = Resources.Load<TextAsset>("Items");
        doc.LoadXml(myXmlAsset.text);
        */
       // Debug.Log("doc.Name");
        //Loads all the items from the xml document
        Type[] itemTypes = { typeof(Equipment), typeof(Weapon), typeof(Consumeable),typeof(Material) };
        XmlSerializer serializer = new XmlSerializer(typeof(ItemContainer), itemTypes);


        TextReader textReader = new StreamReader(Application.streamingAssetsPath+ "/Items.xml") ;
        itemContainer = (ItemContainer)serializer.Deserialize(textReader);       
        textReader.Close();
        //CraftingBench.Instance.CreateBlueprints();
        
    }    

    /// <summary>
    /// Sets the stacks info,so that we know how many items we can remove
    /// </summary>
    /// <param name="maxStackCount"></param>
    public void SetStackInfo(int maxStackCount)
    {

        selectStackSize.SetActive(true); //Shows the UI for splitting a stack
        tooltipObject.SetActive(false); //Hides the tooltip so that it doesn't overlap the splitstack ui
        splitAmount = 0; //Resets the amount of split items
        this.maxStackCount = maxStackCount; //stores the maxCount;
        stackText.text = splitAmount.ToString(); //Writes writes the selected amount of itesm in the UI


    }
    /// <summary>
    /// saves every single inventory in the scene
    /// </summary>
    public void Save()
    {
        //finds all inventories
        GameObject[] inventories = GameObject.FindGameObjectsWithTag("Inventory");
        GameObject[] chests = GameObject.FindGameObjectsWithTag("Chest");

        //loads all inventories
        foreach(GameObject inventory in inventories)
        {
            inventory.GetComponent<Inventory>().SaveInventory();
        }
        foreach(GameObject chest in chests)
        {
            chest.GetComponent<InventoryLink>().SaveInventory();
        }
    }

    /// <summary>
    /// loads every single inventory in the scene
    /// </summary>
    public void Load()
    {
        GameObject[] inventories = GameObject.FindGameObjectsWithTag("Inventory");
        GameObject[] chests = GameObject.FindGameObjectsWithTag("Chest");

        foreach (GameObject inventory in inventories)
        {
            inventory.GetComponent<Inventory>().LoadInventory();
        }

        foreach (GameObject chest in chests)
        {
            chest.GetComponent<InventoryLink>().LoadInventory();
        }
    }


}
