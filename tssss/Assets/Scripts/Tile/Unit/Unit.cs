using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class Unit 
{
    private bool destory = false;
    public bool Destory
    {
        get { return destory; }
        set { destory = value; }
    }


    private LvType lvType;
    public LvType LvType
    {
        get { return lvType; }
        set { lvType = value; }
    }

    private UnitTypes unitType;
    public UnitTypes UnitType
    {
        get { return unitType; }
        set { unitType = value; }
    }

    private int x;
    public int X
    {
        get { return x; }
        set { x = value; }
    }
    private int y;
    public int Y
    {
        get { return y; }
        set { y = value; }
    }

    private int moveAmount;
    public int MoveAmount
    {
        get { return moveAmount; }
        set { moveAmount = value; }
    }

    public GameObject damegeui;

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



    public Teams team;

    public int lv;
    [SerializeField]
    protected Main_Stage map;


    [SerializeField]
    int lifeMax;


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

    public int Yoroi
    {
        get { return yoroi; }
        set { yoroi = value; }
    }

    private int intellect;

    private int agility;

    private int strength;

    private int yoroi;

    private int exp;

    [SerializeField]
    Skil[] skils;

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
        {
            status = value;
            this.LifeMax = status.LifeMax;
            this.moveAmount = status.MoveAmount;
            this.image = status.UnitImage;
            this.intellect = status.Intellect;
            this.agility = status.Agility;
            this.strength = status.Strength;
            this.yoroi = status.Armor;
            this.attackRangeMin = status.AttackRangeMin;
            this.attackRangeMax = status.AttackRangeMax;
            this.skils = status.Skils.ToArray();



            this.life = status.LifeMax;
            this.UnitName = status.UnitName;
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


    private bool atacked = false;
    public bool Atacked
    {
        set { atacked = value; }
        get { return atacked; }
    }

    public int life;
    public bool isFocusing = false;
    public bool isMoved = false;

    public int LifeMax
    {
        get { return lifeMax; }
        set { lifeMax = value; }
    }

    public int Life { get { return life; } set { life = value; } }

    public int AttackPower
    {
        get
        {
            return strength; //attackPowerBase;/* Mathf.RoundToInt(attackPowerBase * (Mathf.Ceil((float)life / (float)lifeMax * 10f) / 10f));*/
        }
    }

    public bool IsFocusing { get { return isFocusing; } set { isFocusing = value; } }


    //public bool IsMoved
    //{
    //    get { return isMoved; }
    //    set
    //    {
    //        isMoved = value;
    //        if (isMoved && IsFocusing)
    //        {
    //            OnClick();
    //        }
    //    }
    //}

    public Main_tile Cell
    {
        get
        {
            return map.GetCells(x, y);
        }
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
}
