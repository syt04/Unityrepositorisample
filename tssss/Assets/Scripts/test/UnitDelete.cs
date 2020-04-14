using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitDelete : MonoBehaviour
{


    public Image image;
    public Text text;

    public Text buttuntext;
    public Image UnitTypebutton;

    private EnemyStatus unit;
    public EnemyStatus Unit
    {
        get { return unit; }
        set { unit = value; }
    }

    public Text UnitLvType;
    private UnitTypes unitType;
    public UnitTypes UnitType
    {
        get { return unitType; }
        set { unitType = value; }
    }
    private LvType lvType = LvType.SOUZYUKU;
    public LvType LvType
    {
        get { return lvType; }
        set { lvType = value; }
    }

    public UnitScript UnitObject { get => unitObject; set => unitObject = value; }

    public GameObject UnitRan;

    private UnitScript unitObject = new UnitScript(); 

    public void Start()
    {
        
        image.sprite = unit.UnitImage;
        text.text = unit.UnitName;

        switch (lvType)
        {
            case LvType.SOUZYUKU:
                UnitLvType.text = "S";
                break;
            case LvType.HUTUU:
                UnitLvType.text = "H";
                break;
            case LvType.TAIKIBANSEI:
                UnitLvType.text = "T";
                break;
        }





        UnitTypebutton.sprite = UnitManager.Instance.TypeZokusei(unitType);


        unitObject.image = unit.UnitImage;
        unitObject.LifeMax = unit.LifeMax;
        unitObject.MoveAmount = unit.MoveAmount;
        unitObject.AttackRangeMax= unit.AttackRangeMax;
        unitObject.AttackRangeMin = unit.AttackRangeMin;
        unitObject.image = unit.UnitImage;
        unitObject.Intellect = unit.Intellect;
        unitObject.Agility = unit.Agility;
        unitObject.Strength = unit.Strength;
        unitObject.Armor = unit.Armor;
        unitObject.AttackRangeMin = unit.AttackRangeMin;
        unitObject.AttackRangeMax = unit.AttackRangeMax;
        unitObject.Skils = unit.Skils.ToArray();
        unitObject.Life = unit.LifeMax;
        unitObject.UnitName = unit.UnitName;
        unitObject.LvType = lvType;
        unitObject.UnitType = unitType;
        unitObject.MaxSp = unit.MaxSp;
        unitObject.Sp = unit.MaxSp;
        unitObject.Lv = 1;
        unitObject.Exp = 0;
    }

    public void Delite()
    {
        PlayerScript.Instance.Delete(this.unit);
        Destroy(gameObject);

    }




}
