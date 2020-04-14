//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using DG.Tweening;


//public enum Teams
//{
//    Player1,
//    Player2
//}

//public enum UnitTypes
//{
//    F,
//    W,
//    R,
//}

//public enum LvType
//{
//    SOUZYUKU,
//    HUTUU,
//    TAIKIBANSEI
//}

//public class UnitScript : MonoBehaviour
//{


//    GameObject Idou;

//    public Inventory inventory;
//    Inventory ininven;

//    bool destory = false;

//    private LvType lvType;
//    public LvType LvType
//    {
//        get { return lvType; }
//        set { lvType = value; }
//    }

//    private UnitTypes unitType;
//    public UnitTypes UnitType
//    {
//        get { return unitType; }
//        set { unitType = value; }
//    }


//    public int x;
//    public int y;
//    [SerializeField]
//    private int moveAmount;
//    public int MoveAmount
//    {
//        get { return moveAmount; }
//        set { moveAmount = value; }
//    }


//    [SerializeField]
//    private int attackRangeMin;

//    public int AttackRangeMin
//    {
//        get { return AttackRangeMin; }
//    }

//    [SerializeField]
//    private int attackRangeMax;

//    public int AttackRangeMax
//    {
//        get { return attackRangeMax; }
//    }



//    public Teams team;

//    public int lv;
//    [SerializeField]
//    protected Main_Stage map;


//    [SerializeField]
//    int lifeMax;
//    //[SerializeField]
//    //int attackPowerBase;
//    //[SerializeField]
//    //private int baseIntellect;
//    //[SerializeField]
//    //private int baseAgility;
//    //[SerializeField]
//    //private int baseStrength;
//    //[SerializeField]
//    //private int baseYoroi;

//    public int Intellect
//    {
//        get { return intellect; }
//        set { intellect = value; }
//    }

//    public int Agility
//    {
//        get { return agility; }
//        set { agility = value; }
//    }

//    public int Strength
//    {
//        get { return strength; }
//        set { strength = value; }
//    }
//    protected Inventory chest;

//    public int Armor
//    {
//        get { return armor; }
//        set { armor = value; }
//    }
//    [SerializeField]
//    private int intellect;
//    [SerializeField]
//    private int agility;
//    [SerializeField]
//    private int strength;
//    [SerializeField]
//    private int armor;
//    [SerializeField]
//    private int exp;

//    [SerializeField]
//    List<string> skils = new List<string>();



//    public int Exp
//    {
//        get { return exp; }
//        set { exp = value; }
//    }

//    [SerializeField]
//    private string unitName;
//    public string UnitName
//    {
//        get { return unitName; }
//        set { unitName = value; }
//    }

//    public Sprite image;


//    private bool atacked = false;
//    public bool Atacked
//    {
//        set { atacked = value; }
//        get { return atacked; }
//    }

//    public int life;
//    protected bool isFocusing = false;
//    protected bool isMoved = false;

//    public int LifeMax
//    {
//        get { return lifeMax; }
//        set { lifeMax = value; }
//    }

//    public int Life { get { return life; } }

//    public int AttackPower
//    {
//        get
//        {
//            return strength; //attackPowerBase;/* Mathf.RoundToInt(attackPowerBase * (Mathf.Ceil((float)life / (float)lifeMax * 10f) / 10f));*/
//        }
//    }

//    public bool IsFocusing { get { return isFocusing; } set { isFocusing = value; } }


//    public bool IsMoved
//    {
//        get { return isMoved; }
//        set
//        {
//            isMoved = value;
//            //GetComponent<Button>().interactable = !isMoved;
//            if (isMoved && IsFocusing)
//            {
//                OnClick();
//            }
//        }
//    }

//    public Main_tile Cell
//    {
//        get
//        {
//            return map.GetCells(x, y);
//        }
//    }

//    public void SetStatusFromData()
//    {
//        this.LifeMax = UnitManager.Instance.GetEnemyStatus(unitName).LifeMax;
//        this.moveAmount = UnitManager.Instance.GetEnemyStatus(unitName).MoveAmount;
//        this.image = UnitManager.Instance.GetEnemyStatus(unitName).UnitImage;
//        this.intellect = UnitManager.Instance.GetEnemyStatus(unitName).Intellect;
//        this.agility = UnitManager.Instance.GetEnemyStatus(unitName).Agility;
//        this.strength = UnitManager.Instance.GetEnemyStatus(unitName).Strength;
//        this.armor = UnitManager.Instance.GetEnemyStatus(unitName).Armor;
//        this.attackRangeMin = UnitManager.Instance.GetEnemyStatus(unitName).AttackRangeMin;
//        this.attackRangeMax = UnitManager.Instance.GetEnemyStatus(unitName).AttackRangeMax;
//        this.skils = UnitManager.Instance.GetEnemyStatus(unitName).Skils;


//    }

//    public void Update()
//    {
//        if (team == Teams.Player1)
//            Unitsousa();
//    }




//    public void Unitsousa()
//    {


//        if (isFocusing)
//        {

//            GameObject.Find("ran").GetComponent<CanvasGroup>().alpha = 1;
//            GameObject.Find("ran").GetComponent<CanvasGroup>().blocksRaycasts = true;

//        }
//        else
//        {



//            if (chest != null)
//            {
//                if (chest.canvasGroup.alpha == 1)
//                {
//                    chest.Open();

//                }
//            }

//        }

//        if (isMoved)
//        {

//            if (chest != null)
//            {
//                if (chest.canvasGroup.alpha == 1)
//                {
//                    chest.Open();

//                }
//            }

//        }

//        if (isMoved && Atacked)
//        {
//            gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
//            if (chest != null)
//            {
//                if (chest.canvasGroup.alpha == 1)
//                {
//                    chest.Open();

//                }
//            }


//        }
//        else
//        {
//            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
//        }
//    }

//    public void OnClick()
//    {


//        if (!destory)
//        {

//            // 攻撃対象の選択中であれば攻撃アクション実行
//            if (map.GetCells(x, y).IsAttackable && !map.FocusingUnit.Atacked)
//            {
//                //     Debug.Log("map.attackto this" + this);
//                map.AttackTo(map.FocusingUnit, this);
//                map.FocusingUnit.Atacked = true;
//                map.FocusingUnit.IsMoved = true;
//                return;
//            }
//            Isfocusing();
//        }
//    }


//    public void Isfocusing()
//    {

//        // 自分以外のユニットが選択状態であれば、そのユニットの選択を解除
//        if (null != map.FocusingUnit && this != map.FocusingUnit)
//        {
//            map.FocusingUnit.isFocusing = false;
//            //    Debug.Log("map.focusingUnit" + map.FocusingUnit);
//            map.ClearHighlight();
//        }


//        isFocusing = !isFocusing;
//        map.FocusingUnit = this;

//        //  Debug.Log("isfocusing" + isFocusing + name);
//        if (isFocusing)
//        {
//            map.HighlightMovableCells(x, y, moveAmount);
//            map.FirstFocusingcells(this);
//            map.HighlightAttackableCells(x, y, attackRangeMin, attackRangeMax, this);

//        }
//        else
//        {

//            map.ClearHighlight();
//        }


//    }


//    /// <summary>
//    /// ダメージを与えます
//    /// </summary>
//    /// <param name="attacker">Attacker.</param>
//    public void Damage(UnitScript attacker)
//    {
//        life = Mathf.Max(0, life - CalcurateDamageValue(attacker));
//    }

//    /// <summary>
//    /// ダメージ値を計算します
//    /// </summary>
//    /// <param name="attacker">Attacker.</param>
//    public int CalcurateDamageValue(UnitScript attacker)
//    {
//        // 三つ巴的な相性ダメージ Kuro < Shiro < Tora < Kuro ...
//        var unitTypeBonus = new float[] { 1f, 2f, 0.5f }[(((int)attacker.unitType - (int)unitType) + 3) % 3];
//        var damage = Mathf.RoundToInt((attacker.AttackPower - Armor) * unitTypeBonus * (1f - attacker.Cell.ReduceRate));
//        return damage;
//    }

//    public void DestroyWithAnimate()
//    {
//        // GetComponent<Button>().enabled = false;

//        GameState.Instance.endTurnButton.GetComponent<CanvasGroup>().interactable = false;
//        destory = true;
//        transform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
//        {
//            Destroy(gameObject);
//        });
//        GameState.Instance.endTurnButton.GetComponent<CanvasGroup>().interactable = true;
//    }


//    public void PointerEnter()
//    {

//        StatusManager.Instance.Show(this);
//    }

//    public void EndButton()
//    {
//        this.isMoved = true;
//        this.Atacked = true;
//        map.ClearHighlight();

//    }

//    public void SkilButton()
//    {
//        foreach (string skilname in skils)
//        {
//            SkilManager.Instance.SkilRan.GetComponent<SkilScript>().SkilNameCreate(SkilManager.Instance.Getskil(skilname));
//        }
//    }

//    //public void SkilRan()
//    //{
//    //    foreach (SkilScript skil in skils)
//    //    {
//    //        skil.SkilNameCreate(skil);
//    //    }
//    //}

//    public string MyInfomation()
//    {
//        string Infomation;
//        return Infomation = string.Format("Hp:{0}/{1}\nStrength:{2}\nIntellect:{3}\nYoroi:{4}\nAgility:{5} ", life, lifeMax, Strength, Intellect, Armor, Agility);


//    }


//    public void LvUp()
//    {
//        switch (lvType)
//        {
//            case LvType.SOUZYUKU:

//                this.lifeMax += 1;
//                this.Strength += 1;
//                this.Armor += 1;
//                this.Intellect += 1;
//                this.Agility += 1;
//                this.lv += 1;
//                this.exp = 0;

//                break;
//            case LvType.HUTUU:

//                this.lifeMax += 2;
//                this.Strength += 2;
//                this.Armor += 2;
//                this.Intellect += 2;
//                this.Agility += 2;
//                this.lv += 1;
//                this.exp = 0;

//                break;
//            case LvType.TAIKIBANSEI:

//                this.lifeMax += 3;
//                this.Strength += 3;
//                this.Armor += 3;
//                this.Intellect += 3;
//                this.Agility += 3;
//                this.lv += 1;
//                this.exp = 0;

//                break;

//        }



//    }

//    //public void CestOpen()
//    //{
//    //    if (chest != null)
//    //    {
//    //        if (chest.canvasGroup.alpha == 0 || chest.canvasGroup.alpha == 1)
//    //        {
//    //            chest.Open();

//    //        }


//    //    }
//    //}

//    //private void OnTriggerEnter2D(Collider2D other)
//    //{

//    //    if (other.tag == "Chest" || other.tag == "Vendor" || other.tag == "Luot")
//    //    {
//    //        // helperText.gameObject.SetActive(true);
//    //        chest = other.GetComponent<InventoryLink>().linkedInventory;
//    //        Debug.Log(" OnTriggerEnter2D");
//    //    }



//    //}

//    //private void OnTriggerExit2D(Collider2D other)
//    //{
//    //    if (other.gameObject.tag == "Chest" || other.gameObject.tag == "CraftingBench" || other.gameObject.tag == "Vendor" || other.tag == "Luot")
//    //    {
//    //        if (chest.IsOpen)
//    //        {
//    //            chest.Open();
//    //        }

//    //        chest = null;
//    //    }
//    //}
//}
