using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitPrefab
{
    public Teams Team { get; set; }
    public UnitTypes UnitType { get; set; }
    public LvType LvType { get; set; }

    public string UnitName { get; set; }

    public int x;
    public int y;

    public int LifeMax { get; set; }
    public int Life { get; set; }
    public int MoveAmount { get; set; }
    public int AttackRangeMin { get; set; }
    public int AttackRangeMax { get; set; }
    public int AttackPowerBase { get; set; }/* Mathf.RoundToInt(attackPowerBase * (Mathf.Ceil((float)life / (float)lifeMax * 10f) / 10f));*/ 
    public int BaseIntellect { get; set; }
    public int BaseAgility { get; set; }
    public int BaseStrength { get; set; }
    public int BaseStamina { get; set; }

    public int Intellect { get; set; }
    public int Agility { get; set; }
    public int Strength { get; set; }
    public int Stamina { get; set; }

    public int BuyPrice { get; set; }

    //protected Inventory chest;

    public Sprite image;

    public bool Atacked{get;set;}

    protected bool isFocusing = false;
    protected bool isMoved = false;




    public bool IsFocusing { get { return isFocusing; } set { isFocusing = value; } }


    public UnitPrefab() { }
    public UnitPrefab(UnitTypes unitTypes, Teams teams, LvType lvType,int LifeMax, int Life,int MoveAmount, int AttackRangeMin, int AttackRangeMax)
    {
        this.UnitType = unitTypes;
        this.Team = teams;
        this.LvType = lvType;
        this.LifeMax = LifeMax;
        this.Life = Life;
        this.MoveAmount = MoveAmount;
        this.AttackRangeMin = AttackRangeMin;
        this.AttackRangeMax = AttackRangeMax;

    }
    public abstract void Unitsousa();
    public abstract void Start();
    public abstract void Update();

    public abstract void Isfocusing();
    public abstract void Damage(UnitScript attacker);
    public abstract void CalcurateDamageValue(UnitScript attacker);
    public abstract void DestroyWithAnimate();
    public abstract void PointerEnter();
    public abstract void EndButton();
    public abstract void TaikiButton();


    public virtual string GetTypytip()
    {

        string stats = string.Empty;
        string color = string.Empty;


        switch (UnitType)
        {
            case UnitTypes.R:
                color = "lime";
                stats = "草";
                break;
            case UnitTypes.W:
                color = "navy";
                stats = "水";
                break;
            case UnitTypes.F:
                color = "red";
                stats = "火";
                break;
        }
        return string.Format("<color=" + color + "><size=12>{0}</size></color>",stats);

    }


}
