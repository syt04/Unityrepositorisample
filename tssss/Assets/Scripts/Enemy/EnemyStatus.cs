using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Gata
{
    近接型,
    機動型,
    タンク型,

    遠距離型,
    魔法型,

    支援型,
    妨害型,

    ボス型,
}


[CreateAssetMenu(fileName = "EnemyStatus", menuName = "CreateEnemy")]
public class EnemyStatus : ScriptableObject
{
    [SerializeField]
    private Gata gata;

    [SerializeField]
    private int warPotential;

    [SerializeField]
    private int moveAmount;
    [SerializeField]
    private int attackRangeMin;
    [SerializeField]
    private int attackRangeMax;
    [SerializeField]
    int lifeMax;
    [SerializeField]
    private int maxSp;

    [SerializeField]
    private int strength;
    [SerializeField]
    private int intellect;
    [SerializeField]
    private int armor;
    [SerializeField]
    private int agility;


    [SerializeField]
    private List<Skil> skils = new List<Skil>();

    public List<Skil> Skils
    {
        get { return skils; }
    }

    [SerializeField]
    private string animation;
    public string Animation
    {
        get { return animation; }
    }



    [SerializeField]
    private string unitName;


    [SerializeField]
    private Sprite unitimage;


    [SerializeField]
    private string description;

    [SerializeField]
    private int hpGrowthRate;
    [SerializeField]
    private int spGrowthRate;

    [SerializeField]
    private int strengthGrowthRate;
    [SerializeField]
    private int armorGrowthRate;
    [SerializeField]
    private int intellectGrowthRate;
    [SerializeField]
    private int agilityGrowthRate;






    public Sprite UnitImage
    {
        get { return unitimage; }
    }

    public int LifeMax
    {
        get { return lifeMax; }
    }

    public int Intellect
    {
        get { return intellect; }
    }

    public int Agility
    {
        get { return agility; }
    }

    public int Strength
    {
        get { return strength; }
    }

    public int Armor
    {
        get { return armor; }
    }

    public int MoveAmount
    {
        get { return moveAmount; }
    }
    public int AttackRangeMax
    {
        get { return attackRangeMax; }
    }

    public string UnitName
    {
        get { return unitName; }
    }


    public int AttackRangeMin
    {
        get { return attackRangeMin; }
    }

    public int MaxSp
    {
        get { return maxSp; }
    }

    public string Description { get => description;}
    public int HpGrowthRate { get => hpGrowthRate;}
    public int SpGrowthRate { get => spGrowthRate;}
    public int IntellectGrowthRate { get => intellectGrowthRate;}
    public int AgilityGrowthRate { get => agilityGrowthRate;}
    public int StrengthGrowthRate { get => strengthGrowthRate; }
    public int ArmorGrowthRate { get => armorGrowthRate;}
    public Gata Gata { get => gata;}
    public int WarPotential { get => warPotential;}
}



