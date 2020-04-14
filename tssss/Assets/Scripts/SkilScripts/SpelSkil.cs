using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class SpelSkil : Skil
{
    [SerializeField]
    private int min;
    [SerializeField]
    private int max;


    public SpelSkil()
    {

    }

    public SpelSkil(string SkilName, string Description, int UseSp, int Needlv, int AttackRengeMin, int AttackRengeMax, int MaxAttackCount, GameObject Animation
      , int Strengethho, int Intellectho) : base(SkilName, Description, UseSp, Needlv, AttackRengeMin, AttackRengeMax, MaxAttackCount, Animation
      , Strengethho, Intellectho)
    {
        this.SkilName = SkilName;
        this.Description = Description;
        this.UseSp = UseSp;
        this.Needlv = Needlv;
        this.AttackRengeMin = AttackRengeMin;
        this.AttackRengeMax = AttackRengeMax;
        this.MaxAttackCount = MaxAttackCount;
        this.Efect = Animation;
        this.Strengethho = Strengethho;
        this.Intellectho = Intellectho;
    }


    public override void Use()
    {

            Debug.Log(SkilName + "を詠唱");
        foreach (UnitObject unit in SkilManager.Instance.UsedUnits)
        {
            //Vector3 vector = new Vector3(SkilAttacktile.Unit.transform.position.x, SkilAttacktile.Unit.transform.position.y);
            //var obj = Instantiate(Efect, vector, Quaternion.identity);
            Damageculcu(NewBehaviourScript.Instance.map.FocusingUnit, unit);
        }

        NewBehaviourScript.Instance.map.FocusingUnit.IsMoved = true;
            NewBehaviourScript.Instance.map.FocusingUnit.Atacked = true;
            NewBehaviourScript.Instance.map.ClearHighlight();
            NewBehaviourScript.Instance.map.FocusingUnit = null;


        
    }

    public void Damageculcu(UnitObject fromUnit, UnitObject toUnit)
    {
        if (toUnit != null)
        {
            int life = toUnit.Unit.Life;
           // toUnit.SkilDamage(fromUnit, this);
            BattleManager.Instance.SkilDamage(fromUnit, this, toUnit);
            if (toUnit.Unit.Life <= 0)
            {
                toUnit.Destory = true;
                BattleManager.Instance.DestroyWithAnimate(toUnit.gameObject);
                UnitManager.Instance.GetExp(fromUnit.Unit);
            }
        }
    }


    public override void SkilAble()
    {
        Main_Stage map = NewBehaviourScript.Instance.map;
        //Debug.Log(skil.SkilName + "の範囲");
        map.ClearHighlight();
        SkilManager.Instance.UseSkil.Skil = null;
        SkilManager.Instance.UseSpell = null;
        Main_tile cell = map.FocusingUnit.Cell; // 使用者の座標
        foreach (Main_tile skilcell in map.GetCellsByDistance(cell, this.AttackRengeMin, this.AttackRengeMax))
        {                         
                skilcell.IsSpellable = true;            
        }
        SkilManager.Instance.UseSkil.Skil = this;
        SkilManager.Instance.UseSpell = this;
    }

    public void SkilAttacktile(Main_tile tile)
    {
        var map = NewBehaviourScript.Instance.map;
        Main_tile cell = tile; // 座標
        //Debug.Log(skil.SkilName + "の範囲");
        map.ClearHighlight();//いったんクリア
        if (map.FocusingUnit != null && !map.FocusingUnit.Atacked)
        {
            foreach (Main_tile speltile in map.GetCellsByDistance(cell, min, max))
            {
                speltile.IsSkilAttackable = true;
            }
            tile.IsSkilAttackable = true;
        }
    }
}


