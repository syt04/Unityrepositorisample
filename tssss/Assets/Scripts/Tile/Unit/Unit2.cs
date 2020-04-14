//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using DG.Tweening;
//using UnityEngine.UI;

//public class Unit2 : MonoBehaviour
//{
//    public enum Teams
//    {
//        Player1,
//        Player2
//    }

//    public enum UnitTypes
//    {
//        Neko,
//        Matatabi,
//        Koban,
//    }
//    public UnitTypes unitType;
//    public Teams team;
//    public int attackRangeMin;
//    public int attackRangeMax;

//    public Inventory inventory2;
//    public Inventory charPanel;
//    private Inventory chest;

//    public MyState myState;

//    bool buton = false;
//    public Button button;

//    bool on = false;

//    private int x;
//    public int X
//    {
//        get { return x; }
//        set { x = value; }
//    }
//    private int y;
//    public int Y
//    {
//        get { return x; }
//        set { y = value; }
//    }
//    private int moveAmount = 4;
//    private int speed = 100;
//    private int kk;
//    public int hp;
//    public string unitname;

//    public int Speed
//    {
//        get { return speed; }
//        set { speed = value; }
//    }


//    public Main_Stage map;
//    public MyState mystate;


//    bool isFocused = false;
//    bool CanMove = true;


//    public Sprite image;
//    public int baseIntellect;
//    public int baseAgility;
//    public int baseStrength;
//    public int baseStamina;
//    public int baseEnegy;
//    public int baseKk;
//    public int baseReEnegy = 4;
//    public int baseMax_HP = 100;
//    public int baseSpeed = 100;
//    public int baseMovement = 9;


//    public int intellect;
//    public int agility;
//    public int strength;
//    public int stamina;


//    private int gold;
//    public int Gold
//    {
//        get { return gold; }
//        set
//        {
//            // goldText.text = "Gold :" + value;
//            gold = value;
//        }
//    }

//    Button button2;

//    public int Enegy;

//    GameObject A;





//    PlayerStatus Status;

//    private int reEnegy = 4;
//    public int ReEnegy
//    {
//        get { return reEnegy; }
//        set { reEnegy = value; }
//    }


//    private static Unit instance;
//    public static Unit Instance
//    {
//        get
//        {
//            if (instance == null)
//            {
//                instance = GameObject.FindObjectOfType<Unit>();
//            }
//            return instance;
//        }
//    }



//    public bool IsFocused
//    {
//        get { return isFocused; }
//    }
//    private Inventory inventory;

//    Button button4;
//    Button button3;

//    void Start()
//    {

//        UnitStatusLODE();

//        // inventory = Instantiate(GameObject.Find("Inventory").GetComponent<Inventory>());

//        inventory = Instantiate(inventory2);
//        inventory.transform.SetParent(GameObject.Find("Inventoryposi").transform);
//        inventory.gameObject.SetActive(true);
//        inventory.transform.position = inventory.transform.parent.position;
//        inventory.GetComponent<CanvasGroup>().alpha = 0;
//        inventory.GetComponent<CanvasGroup>().blocksRaycasts = false;

//        // button = GameObject.Find("Button").GetComponent<Button>();
//        button2 = Instantiate(button);
//        button2.transform.SetParent(GameObject.Find("inv").transform);
//        button2.GetComponent<Button>().onClick.AddListener(delegate { inventory.Open(); });

//        button2.transform.position = button2.transform.parent.position;
//        button2.transform.Find("Text").GetComponent<Text>().text = "Inventory";

//        button3 = Instantiate(button);
//        button3.transform.SetParent(GameObject.Find("kou").transform);
//        button3.transform.position = button3.transform.parent.position;
//        button3.transform.Find("Text").GetComponent<Text>().text = "Attack";

//        button4 = Instantiate(button);
//        button4.transform.SetParent(GameObject.Find("end").transform);
//        button4.transform.position = button4.transform.parent.position;
//        button4.transform.Find("Text").GetComponent<Text>().text = "Turn End";



//        button3.GetComponent<Button>().onClick.AddListener(delegate { Dame(); });
//        button4.GetComponent<Button>().onClick.AddListener(delegate { EndTurn(); });






//        Gold = 0;
//        SetStats(0, 0, 0, 0);

//        this.Enegy = 0;
//        PlayerStatus.Instance.SetUnit(this);

//        //GameObject ininven = (GameObject) GameObject.Instantiate(inventory);
//        //ininven.name = "Inventory";
//        //ininven.transform.SetParent(GameObject.Find("Canvas").transform);
//        //ininven.SetActive(true);


//        mystate.Speed = speed;
//        mystate.reEnegy = reEnegy;

//    }

//    public void UnitStatusLODE()
//    {

//        A = GameObject.Find("PlayerStatus");
//        Status = A.GetComponent<PlayerStatus>();

//        //  Enegy = baseEnegy;
//        kk = baseKk;

//        reEnegy = baseReEnegy;

//        speed = baseSpeed;

//        hp = baseMax_HP;

//        moveAmount = baseMovement;
//    }


//    public void EnegyRE()
//    {
//        Enegy = reEnegy;

//    }

//    public void SetReEnegy(int x)
//    {

//        reEnegy = x;
//    }

//    public void MoveTo(Main_tile cell)
//    {
//        //if (button2.GetComponent<CanvasGroup>().alpha == 1)
//        //{
//        //    //buton = !buton;
//        //    button2.GetComponent<CanvasGroup>().alpha = 0;
//        //}
//        //if (inventory.GetComponent<CanvasGroup>().alpha == 1)
//        //{
//        //    inventory.IsOpen = false;
//        //    inventory.GetComponent<CanvasGroup>().alpha = 0;
//        //}
//        // Immoving = true;
//        if (this.Enegy > 0)
//        {

//            //SetCamera();

//            //GetComponent<Button>().enabled = false;

//            // Debug.Log("this.transform.position" +transform.position);

//            // Debug.Log("MoveTo "+ "x = "+x+" y= "+y);
//            map.ResetMovableCells();

//            // Debug.Log("ResetMovaaftar MoveTo " + "x = " + x + " y= " + y);
//            var routeCells = map.CalculateRouteCells(this.x, this.y, moveAmount, cell);
//            var sequence = DOTween.Sequence();
//            for (var i = 1; i < routeCells.Length; i++)
//            {
//                var routeCell = routeCells[i];
//                sequence.Append(transform.DOMove(routeCell.transform.position, 0.1f).SetEase(Ease.Linear));

//            }
//            sequence.OnComplete(() =>
//            {
//                x = routeCells[routeCells.Length - 1].X;
//                y = routeCells[routeCells.Length - 1].Y;
//                isFocused = false;
//                // GetComponent<Button>().enabled = true;

//                transform.position = new Vector3(x, y, 0);
//            });


//            isFocused = false;
//            /*
//            x = cell.x;
//            y = cell.y;
//            transform.position = new Vector3(x, y, 0);
//            */
//            Enegy -= 1;


//            Debug.Log("移動した。残Unitエナジー" + Enegy);
//            // Immoving = false;

//        }
//        else
//        {
//            Debug.Log("Enegyないよ");

//        }
//        // SetCamera();
//        // Debug.Log("Immoving before" + Immoving);
//        // Immoving = false;
//        //Debug.Log("Immoving after" + Immoving);



//    }



//    public void OnClick()
//    {
//        if (mystate.Myturn && Enegy > 0)
//        {


//            // 攻撃対象の選択中であれば攻撃アクション実行
//            if (map.GetCells(x, y).IsAttackable)
//            {
//                map.AttackTo(map.FocusingUnit, this);
//                return;
//            }

//            // 自分以外のユニットが選択状態であれば、そのユニットの選択を解除
//            if (null != map.FocusingUnit && this != map.FocusingUnit)
//            {
//                map.FocusingUnit.isFocusing = false;
//                map.ClearHighlight();
//            }

//            //isFocusing = !isFocusing;
//            //if (isFocusing)
//            //{
//            //    map.HighlightMovableCells(x, y, moveAmount);
//            //    map.HighlightAttackableCells(x, y, attackRangeMin, attackRangeMax);
//            //}
//            //else
//            //{
//            //    map.ClearHighlight();
//            //}


//            isFocused = !isFocused;
//            if (isFocused && Enegy > 0 && CanMove)
//            {
//                map.HighlightMovableCells(x, y, moveAmount);
//                map.HighlightAttackableCells(x, y, attackRangeMin, attackRangeMax);
//            }
//            else
//            {
//                map.ResetMovableCells();
//            }

//            Debug.Log("ユニットクリック");


//            // Debug.Log(" isFocused" + isFocused);
//        }
//    }

//    void Update()
//    {
//        if (!PlayerStatus.Instance.GetUnits().Contains(this))
//        {
//            PlayerStatus.Instance.SetUnit(this);

//        }

//        mystate.Helth(this.hp);

//        if (mystate.Myturn && Enegy > 0)
//        {
//            GameObject.Find("ran").GetComponent<CanvasGroup>().alpha = 1;
//            GameObject.Find("ran").GetComponent<CanvasGroup>().blocksRaycasts = true;

//            button2.GetComponent<CanvasGroup>().alpha = 1;
//            button2.GetComponent<CanvasGroup>().blocksRaycasts = true;

//            button3.GetComponent<CanvasGroup>().alpha = 1;
//            button3.GetComponent<CanvasGroup>().blocksRaycasts = true;

//            button4.GetComponent<CanvasGroup>().alpha = 1;
//            button4.GetComponent<CanvasGroup>().blocksRaycasts = true;

//        }
//        else
//        {
//            //GameObject.Find("ran").GetComponent<CanvasGroup>().alpha = 0;
//            //GameObject.Find("ran").GetComponent<CanvasGroup>().blocksRaycasts = false;

//            button2.GetComponent<CanvasGroup>().alpha = 0;
//            button2.GetComponent<CanvasGroup>().blocksRaycasts = false;

//            button3.GetComponent<CanvasGroup>().alpha = 0;
//            button3.GetComponent<CanvasGroup>().blocksRaycasts = false;

//            button4.GetComponent<CanvasGroup>().alpha = 0;
//            button4.GetComponent<CanvasGroup>().blocksRaycasts = false;

//            if (inventory.GetComponent<CanvasGroup>().alpha == 1)
//            {
//                inventory.Open();
//            }

//        }


//        //if (Input.GetKeyDown(KeyCode.B))
//        //{
//        //    inventory.Open();

//        //}
//        EndmyTurnchack();
//        if (mystate.Myturn)
//        {
//            GameObject.Find("ran").GetComponent<CanvasGroup>().alpha = 1;
//            GameObject.Find("ran").GetComponent<CanvasGroup>().blocksRaycasts = true;

//            if (Input.GetKeyDown(KeyCode.K))
//                InputA();

//            if (Input.GetKeyDown(KeyCode.E))
//            {
//                if (chest != null)
//                {
//                    if (chest.canvasGroup.alpha == 0 || chest.canvasGroup.alpha == 1)
//                    {
//                        chest.Open();

//                    }

//                }
//            }
//            if (Input.GetKeyDown(KeyCode.C))
//            {
//                charPanel = GameObject.FindObjectOfType<CharacterPanel>();

//                if (charPanel != null)
//                {
//                    charPanel.Open();
//                }
//            }


//            //Setturn();
//        }

//    }

//    public void SetEnegy(int Enegy)
//    {

//        this.Enegy = Enegy;

//    }

//    public int GetEnegy()
//    {
//        return this.Enegy;
//    }

//    void InputA()
//    {
//        int randomType = UnityEngine.Random.Range(0, 3);
//        //instantiates for adding to the inventory
//        GameObject tmp = Instantiate(InventoryManager.Instance.itemObject);
//        //variable for selecting an item inside the category
//        int randomItem;

//        //adds the item script to the ovject
//        tmp.AddComponent<ItemScript>();
//        //creates a reference tp the scripts
//        ItemScript newItem = tmp.GetComponent<ItemScript>();


//        switch (randomType) //selects an item
//        {
//            case 0:
//                //Find selects an item
//                randomItem = UnityEngine.Random.Range(0, InventoryManager.Instance.ItemContainer.Consumeables.Count);
//                //Ginds the item in the list
//                newItem.Item = InventoryManager.Instance.ItemContainer.Consumeables[randomItem];
//                break;
//            case 1:
//                randomItem = UnityEngine.Random.Range(0, InventoryManager.Instance.ItemContainer.Weapons.Count);
//                newItem.Item = InventoryManager.Instance.ItemContainer.Weapons[randomItem];
//                break;
//            case 2:
//                randomItem = UnityEngine.Random.Range(0, InventoryManager.Instance.ItemContainer.Equipment.Count);
//                newItem.Item = InventoryManager.Instance.ItemContainer.Equipment[randomItem];
//                break;
//        }
//        //adds the item to the inventory
//        inventory.AddItem(newItem);
//        Destroy(tmp);

//        Debug.Log("inputA");

//    }

//    private void OnTriggerEnter2D(Collider2D other)
//    {

//        //if (other.tag == "Item")  //if we collide with an item that we can pick up
//        //{
//        //    //Pick 0 or 1 or 2
//        //    int randomType = UnityEngine.Random.Range(0, 3);

//        //    //instantiates for adding to the inventory
//        //    GameObject tmp = Instantiate(InventoryManager.Instance.itemObject);
//        //    //variable for selecting an item inside the category
//        //    int randomItem;

//        //    //adds the item script to the ovject
//        //    tmp.AddComponent<ItemScript>();
//        //    //creates a reference tp the scripts
//        //    ItemScript newItem = tmp.GetComponent<ItemScript>();


//        //    switch (randomType) //selects an item
//        //    {
//        //        case 0:
//        //            //Find selects an item
//        //            randomItem = UnityEngine.Random.Range(0, InventoryManager.Instance.ItemContainer.Consumeables.Count);
//        //            //Ginds the item in the list
//        //            newItem.Item = InventoryManager.Instance.ItemContainer.Consumeables[randomItem];
//        //            break;
//        //        case 1:
//        //            randomItem = UnityEngine.Random.Range(0, InventoryManager.Instance.ItemContainer.Weapons.Count);
//        //            newItem.Item = InventoryManager.Instance.ItemContainer.Weapons[randomItem];
//        //            break;
//        //        case 2:
//        //            randomItem = UnityEngine.Random.Range(0, InventoryManager.Instance.ItemContainer.Equipment.Count);
//        //            newItem.Item = InventoryManager.Instance.ItemContainer.Equipment[randomItem];
//        //            break;
//        //    }
//        //    //adds the item to the inventory
//        //    inventory.AddItem(newItem);
//        //    Destroy(tmp);

//        //}

//        if (other.tag == "Chest" || other.tag == "Vendor" || other.tag == "Luot")
//        {
//            // helperText.gameObject.SetActive(true);
//            chest = other.GetComponent<InventoryLink>().linkedInventory;
//            Debug.Log(" OnTriggerEnter2D");
//        }

//        if (other.tag == "CraftingBench")
//        {
//            //helperText.gameObject.SetActive(true);
//            chest = other.GetComponent<CraftingBenchScript>().CraftingBench;
//        }

//        if (other.tag == "Material")
//        {
//            for (int i = 0; i < 5; i++)
//            {
//                for (int x = 0; x < 3; x++)
//                {
//                    GameObject tmp = Instantiate(InventoryManager.Instance.itemObject);

//                    tmp.AddComponent<ItemScript>();
//                    ItemScript newMaterial = tmp.GetComponent<ItemScript>();

//                    newMaterial.Item = InventoryManager.Instance.ItemContainer.Materials[x];

//                    inventory.AddItem(newMaterial);
//                    Destroy(tmp);
//                }
//            }
//        }


//    }

//    private void OnTriggerExit2D(Collider2D other)
//    {
//        if (other.gameObject.tag == "Chest" || other.gameObject.tag == "CraftingBench" || other.gameObject.tag == "Vendor" || other.tag == "Luot")
//        {
//            if (chest.IsOpen)
//            {
//                chest.Open();
//            }

//            chest = null;
//        }
//    }

//    public void PointerEnter()
//    {

//        string hpstring = string.Format("<size=14><color=red>HP:{0}</color></size>", hp);
//        string epstring = string.Format("<size=14><color=green>EP:{0}</color></size>", Enegy);
//        StatusManager.Instance.Show(unitname, hpstring, epstring, stamina, strength, intellect, agility, image);

//    }



//    public void SetStats(int agility, int strength, int stamina, int intellect)
//    {
//        this.agility = agility + baseAgility;
//        this.strength = strength + baseStrength;
//        this.stamina = stamina + baseStamina;
//        this.intellect = intellect + baseIntellect;

//        // statsText.text = string.Format("Stamina:{0}\nStrength:{1}\nIntellect:{2}\nAgility:{3}", this.stamina, this.strength, this.intellect, this.agility);
//    }


//    public Unit ActiveUnit
//    {

//        get
//        {
//            //if (unitprefab == null)
//            //{
//            //    unitprefab = Instantiate(unit);
//            //    unitprefab.name = "UnitA";
//            //}
//            //  A = GameObject.FindWithTag("Player");
//            //    unit = A.GetComponent<Unit>();
//            return this;

//        }
//        //unitContainer
//    }


//    public void EndmyTurnchack()
//    {


//        if (mystate.Myturn && Enegy <= 0 && on)
//        {


//            EndTurn();
//        }
//        else
//        if (mystate.Myturn && Enegy <= 0 && !on)
//        {
//            Enegy = reEnegy;
//            on = true;
//        }

//    }

//    public void EndTurn()
//    {
//        mystate.Myturn = false;
//        mystate.MyturnEnd = true;
//        //Debug.Log(this.name+"End");
//        on = false;
//        Enegy = 0;
//    }


//    public void Dame()
//    {
//        hp -= 10;

//    }

//    bool isFocusing = false;
//    public bool IsFocusing { get { return isFocusing; } set { isFocusing = value; } }
//    bool isMoved = false;
//    public bool IsMoved
//    {
//        get { return isMoved; }
//        set
//        {
//            isMoved = value;
//            GetComponent<Button>().interactable = !isMoved;
//            if (isMoved && IsFocusing)
//            {
//                OnClick();
//            }
//        }
//    }

//    public Main_tile Cell { get { return map.GetCells(x, y); } }

//    /// <summary>
//    /// ダメージを与えます
//    /// </summary>
//    /// <param name="attacker">Attacker.</param>
//    public void Damage(Unit attacker)
//    {
//        hp = Mathf.Max(0, hp - CalcurateDamageValue(attacker));
//    }

//    /// <summary>
//    /// ダメージ値を計算します
//    /// </summary>
//    /// <param name="attacker">Attacker.</param>
//    public int CalcurateDamageValue(Unit attacker)
//    {
//        // 三つ巴的な相性ダメージ Kuro < Shiro < Tora < Kuro ...
//        var unitTypeBonus = new float[] { 1f, 2f, 0.5f }[(((int)attacker.unitType - (int)unitType) + 3) % 3];
//        var damage = Mathf.RoundToInt(attacker.AttackPower * unitTypeBonus * (1f - attacker.Cell.ReduceRate));
//        return damage;
//    }

//    public int AttackPower { get { return Mathf.RoundToInt(attackPowerBase * (Mathf.Ceil((float)hp / (float)baseMax_HP * 10f) / 10f)); } }
//    [SerializeField]
//    int attackPowerBase;
//}
