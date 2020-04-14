using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public enum Teams
{
    Player1,
    Player2
}

public enum UnitTypes
{
    F,
    W,
    R,
}

public enum LvType
{
    SOUZYUKU,
    HUTUU,
    TAIKIBANSEI
}

public class UnitScript
{



    private LvType lvType;
    public LvType LvType
    {
        get {return lvType; }
        set {lvType = value; }
    }

    private UnitTypes unitType;
    public UnitTypes UnitType
    {
        get { return unitType; }
        set { unitType = value; }
    }


    //private int x;
    //public int X
    //{
    //    get { return x; }
    //    set { x = value; }
    //}
    //private int y;
    //public int Y
    //{
    //    get { return y; }
    //    set { y = value; }
    //}

    private int moveAmount;
    public int MoveAmount
    {
        get {

            int tmp = moveAmount;
            foreach(Abnormalcondition abnormal in Abnormalcondtion)
            {
                if (abnormal.CanMove == false)
                {
                    return 0;
                }
                else if(abnormal.Movementcondition == true)
                {
                    tmp  +=  (int)abnormal.ConditionmoveAmount;
                }
            }

            return tmp;


        }
        set { moveAmount = value; }
    }


    private int maxSp;
    public int MaxSp
    {
        get { return maxSp; }
        set { maxSp = value; }
    }


    private int sp;
    public int Sp
    {
        get { return sp; }
        set { sp = value; }
    }

    private int attackRangeMin;

    public int AttackRangeMin
    {
        get
        {
            return attackRangeMin;
        }
        set { attackRangeMin = value; }
    }

    private int attackRangeMax;

    public int AttackRangeMax
    {
        get { return attackRangeMax; }
        set { attackRangeMax = value; }
    }



    //public Teams team;

    private int lv;
   // [SerializeField]
   //protected Main_Stage map;



   private int lifeMax;


    public int Intellect
    {
        get { return intellect; }
        set { intellect = value; }
    }

    public int Agility
    {
        get { return agility; }
        set { agility = value; }
    }

    public int Strength
    {
        get { return strength; }
        set { strength = value; }
    }
    protected Inventory chest;

    public int Armor
    {
        get { return armor; }
        set { armor = value; }
    }
    
    private int intellect;
    
    private int agility;
   
    private int strength;
   
    private int armor;
    
    private int exp;

    [SerializeField]
    Skil[] skils;

    [SerializeField]
    List<Abnormalcondition>  abnormalcondition = new List<Abnormalcondition>();

    public List<Abnormalcondition> Abnormalcondtion
    {
        get { return abnormalcondition; }
    }


    public Skil[] Skils
    {
        get { return skils; }
        set { skils = value; }
    }

    private EnemyStatus status;
    public EnemyStatus Status
    {
        get { return status; }
        set
        { status = value;
            //this.LifeMax = status.LifeMax;
            //this.moveAmount = status.MoveAmount;
            //this.image = status.UnitImage;
            //this.intellect = status.Intellect;
            //this.agility = status.Agility;
            //this.strength = status.Strength;
            //this.armor = status.Armor;
            //this.attackRangeMin = status.AttackRangeMin;
            //this.attackRangeMax = status.AttackRangeMax;
            //this.skils = status.Skils.ToArray();



            //this.life = status.LifeMax;
            //this.UnitName = status.UnitName;
        }
    }

    private string animationcontroller;
    public string AnimationController
    {
        get { return animationcontroller; }
        set { animationcontroller = value; }
    }


    public int Exp
    {
        get { return exp; }
        set { exp = value; }
    }

    private string unitName;
    public string UnitName
    {
        get { return unitName; }
        set { unitName = value; }
    }

    public Sprite image;


   //private bool atacked = false;
   // public bool Atacked
   // {
   //     set
   //     {
   //         bforeCell = null;
   //         atacked = value;
   //     }
   //     get { return atacked; }
   // }

   private int life;
   //private  bool isFocusing = false;
   //private bool isMoved = false;

    public int LifeMax { get { return lifeMax; }
        set { lifeMax = value; }
    }

    public int Life { get { return life; }  set { life = value; } }

    public int AttackPower {
        get
        {
            return strength; /* Mathf.RoundToInt(strength * (Mathf.Ceil((float)life / (float)lifeMax * 10f) / 10f));*/
        }
    }

    //public bool IsFocusing { get { return isFocusing; } set { isFocusing = value; } }


    //public bool IsMoved
    //{
    //    get { return isMoved; }
    //    set
    //    {
    //        isMoved = value;
    //            if (isMoved && IsFocusing)
    //            {
    //                OnClick();
    //            }         
    //    }
    //}

    //public Main_tile Cell { get {
    //        return map.GetCells(x, y);
    //    }
    //}

    //private Main_tile bforeCell;
    //public Main_tile BforeCell
    //{
    //    get
    //    {
    //        return bforeCell;
    //    }
    //    set { bforeCell = value; }
    //}

    public int Lv
    {
        get { return lv; }
        set { lv = value; }
    }

    //public void SetStatusFromData()
    //{
    //  this.LifeMax = UnitManager.Instance.GetEnemyStatus(unitName).LifeMax;
    //  this.moveAmount = UnitManager.Instance.GetEnemyStatus(unitName).MoveAmount;
    //  this.image = UnitManager.Instance.GetEnemyStatus(unitName).UnitImage;
    //  this.intellect = UnitManager.Instance.GetEnemyStatus(unitName).Intellect;
    //  this.agility = UnitManager.Instance.GetEnemyStatus(unitName).Agility;
    //  this.strength = UnitManager.Instance.GetEnemyStatus(unitName).Strength;
    //  this.armor = UnitManager.Instance.GetEnemyStatus(unitName).Armor;
    //  this.attackRangeMin = UnitManager.Instance.GetEnemyStatus(unitName).AttackRangeMin;
    //  this.attackRangeMax = UnitManager.Instance.GetEnemyStatus(unitName).AttackRangeMax;
    //  this.skils = UnitManager.Instance.GetEnemyStatus(unitName).Skils.ToArray();


    //}

    //private void Start()
    //{

    //    if (Resources.Load("Animation/" + animationcontroller) != null)
    //    {
    //        Animator animator = GetComponent<Animator>();
    //        animator.runtimeAnimatorController = (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load("Animation/" + animationcontroller));
    //    }

    //}

    //void Update()
    //{
    //    if (team == Teams.Player1)
    //        Unitsousa();
    //}




    //public void Unitsousa()
    //{
    //    if (isMoved && Atacked)
    //    {
    //        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;            
    //    }
    //    else
    //    {
    //        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    //    }
    //}

    //public  void OnClick()
    //{
    //   if (!destory)
    //    {
    //        if (SkilManager.Instance.UseSpell != null && Cell.IsSpellable)
    //        {
    //            SkilManager.Instance.UseSpell.SkilAttacktile(Cell);
    //            return;
    //        }


    //        if (Cell.IsSecondSkilable)
    //        {
    //            map.AttackSkilFocusingcells(x, y);     
    //            return;
    //        }        
            

    //        else if(Cell.IsSkilAttackable)
    //        {
    //            if(SkilManager.Instance.UseSpell ==null)
    //            Cell.IsSecondSkilable = true;
    //            return;
    //        }


    //        // 攻撃対象の選択中であれば攻撃アクション実行
    //        if (map.GetCells(x, y).IsAttackable && !map.FocusingUnit.Atacked)
    //        {
    //            //map.AttackTo(map.FocusingUnit, this);
    //            StartCoroutine(BattleManager.Instance.AttackTo(map.FocusingUnit, this));
    //            map.FocusingUnit.Atacked = true;
    //            map.FocusingUnit.IsMoved = true;
    //            return;
    //        }
    //        Isfocusing();
    //    }
    //}


    //public void Isfocusing()
    //{
    //    SkilManager.Instance.UseSkil.Skil = null;
    //    // 自分以外のユニットが選択状態であれば、そのユニットの選択を解除
    //    if (null != map.FocusingUnit && this != map.FocusingUnit)
    //    {
    //        map.FocusingUnit.isFocusing = false;
    //        map.ClearHighlight();
    //    }
    //    isFocusing = !isFocusing;
    //    //map.FocusingUnit = this;
    //    if (isFocusing)
    //    {
    //        map.FocusingUnit = this;

            
    //        map.HighlightMovableCells(x, y, MoveAmount);
    //        map.FirstFocusingcells(this);
    //      //  map.HighlightAttackableCells(this);
    //    }
    //    else
    //    {               
    //        map.ClearHighlight();
    //    }
    //}
}
