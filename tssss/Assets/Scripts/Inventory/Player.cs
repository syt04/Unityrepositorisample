using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
            return instance;
        }
       
    }



    /// <summary>
    /// the player's movement speed
    /// </summary>
    public float speed;

    /// <summary>
    /// a reference to the inventory
    /// </summary>
    public Inventory inventory;

    public Inventory charPanel;

        /// <summary>
        /// A reference to the chest
        /// </summary>
    private Inventory chest;
    [SerializeField]
    private Text goldText;

    public Text statsText;

    public ItemScript[] items = new ItemScript[10];

    public int baseIntellect;
    public int baseAgility;
    public int baseStrength;
    public int baseStamina;

    private int intellect;
    private int agility;
    private int strength;
    private int stamina;

    private int gold;
    public int Gold
    {
        get { return gold; }
        set
        {
            goldText.text = "Gold :" + value;
            gold = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Gold = 0;
        SetStats(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();

        if (Input.GetKeyDown(KeyCode.B))
        {
            inventory.Open();

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(chest != null)
            {
                if(chest.canvasGroup.alpha == 0 || chest.canvasGroup.alpha == 1)
                {
                    chest.Open();
                }
                
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (charPanel != null)
            {
                charPanel.Open();
            }
        }
        if (Input.GetKeyDown(KeyCode.K))
            {
            InputA();
        }

    }

    private void HandleMovement()
    {
        float translation = speed * Time.deltaTime;  //Calculates the players translation so that we will move framerate independent

        
        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * translation, 0, Input.GetAxis("Vertical") * translation)); //Moves the player

    }


    /// <summary>
    /// Handles the player's collision
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
      
        if(other.tag == "Item")  //if we collide with an item that we can pick up
        {
            //Pick 0 or 1 or 2
            int randomType = UnityEngine.Random.Range(0, 3);

            //instantiates for adding to the inventory
            GameObject tmp = Instantiate(InventoryManager.Instance.itemObject);
            //variable for selecting an item inside the category
            int randomItem;

            //adds the item script to the ovject
            tmp.AddComponent<ItemScript>();
            //creates a reference tp the scripts
            ItemScript newItem = tmp.GetComponent<ItemScript>();


            switch (randomType) //selects an item
            {
                case 0:                                                   
                    //Find selects an item
                    randomItem = UnityEngine.Random.Range(0, InventoryManager.Instance.ItemContainer.Consumeables.Count);
                    //Ginds the item in the list
                    newItem.Item = InventoryManager.Instance.ItemContainer.Consumeables[randomItem];                                  
                    break;
                case 1:            
                    randomItem = UnityEngine.Random.Range(0, InventoryManager.Instance.ItemContainer.Weapons.Count);
                    newItem.Item = InventoryManager.Instance.ItemContainer.Weapons[randomItem];               
                    break;
                case 2:                    
                    randomItem = UnityEngine.Random.Range(0, InventoryManager.Instance.ItemContainer.Equipment.Count);
                    newItem.Item = InventoryManager.Instance.ItemContainer.Equipment[randomItem];                   
                    break;
            }
            //adds the item to the inventory
            inventory.AddItem(newItem);
            Destroy(tmp);

        }

        if (other.tag == "Chest" ||other.tag  =="Vendor")
        {
           // helperText.gameObject.SetActive(true);
            chest = other.GetComponent<InventoryLink>().linkedInventory;

        }

        if(other.tag == "CraftingBench")
        {
            //helperText.gameObject.SetActive(true);
            chest = other.GetComponent<CraftingBenchScript>().CraftingBench;
        }

        if(other.tag == "Material")
        {
            for (int i = 0; i < 5; i++)
            {
                for(int x =0; x < 3; x++)
                {
                    GameObject tmp = Instantiate(InventoryManager.Instance.itemObject);

                    tmp.AddComponent<ItemScript>();
                    ItemScript newMaterial = tmp.GetComponent<ItemScript>();

                    newMaterial.Item = InventoryManager.Instance.ItemContainer.Materials[x];

                    inventory.AddItem(newMaterial);
                    Destroy(tmp);
                }
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Chest" || other.gameObject.tag == "CraftingBench"|| other.gameObject.tag =="Vendor")
        {
            if (chest.IsOpen)
            {
                chest.Open();
            }

            chest = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Item")  //if we collide with an item that we can pick up
        {
            if (inventory.AddItem(collision.gameObject.GetComponent<ItemScript>()))//adds the item to the inventory
            { 
                Destroy(collision.gameObject);
            }
        }
    }

    public void SetStats(int agility,int strength,int stamina,int intellect)
    {
        this.agility   = agility + baseAgility;
        this.strength  = strength + baseStrength;
        this.stamina   = stamina + baseStamina;
        this.intellect = intellect + baseIntellect;

        statsText.text = string.Format("Stamina:{0}\nStrength:{1}\nIntellect:{2}\nAgility:{3}",this.stamina,this.strength,this.intellect,this.agility);
    }


    void InputA()
    {
        int randomType = UnityEngine.Random.Range(0, 3);
        //instantiates for adding to the inventory
        GameObject tmp = Instantiate(InventoryManager.Instance.itemObject);
        //variable for selecting an item inside the category
        int randomItem;

        //adds the item script to the ovject
        tmp.AddComponent<ItemScript>();
        //creates a reference tp the scripts
        ItemScript newItem = tmp.GetComponent<ItemScript>();


        switch (randomType) //selects an item
        {
            case 0:
                //Find selects an item
                randomItem = UnityEngine.Random.Range(0, InventoryManager.Instance.ItemContainer.Consumeables.Count);
                //Ginds the item in the list
                newItem.Item = InventoryManager.Instance.ItemContainer.Consumeables[randomItem];
                break;
            case 1:
                randomItem = UnityEngine.Random.Range(0, InventoryManager.Instance.ItemContainer.Weapons.Count);
                newItem.Item = InventoryManager.Instance.ItemContainer.Weapons[randomItem];
                break;
            case 2:
                randomItem = UnityEngine.Random.Range(0, InventoryManager.Instance.ItemContainer.Equipment.Count);
                newItem.Item = InventoryManager.Instance.ItemContainer.Equipment[randomItem];
                break;
        }
        //adds the item to the inventory
        inventory.AddItem(newItem);
        Destroy(tmp);

    }
}
