using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class Tanitu : Skil
{
    //[SerializeField]
    //private string SkilName;
    //[SerializeField]
    //private string Description;
    //[SerializeField]
    //private int UseMp;
    //[SerializeField]
    //private int Needlv;
    //[SerializeField]
    //private int AttackRengeMin;
    //[SerializeField]
    //private int AttackRengeMax;
    //[SerializeField]
    //private int MaxAttackCount;
    //[SerializeField]
    //private Animation Animation;
    //[SerializeField]
    //private int Strengethho;
    //[SerializeField]
    //private int Intellectho;
    public Tanitu()
    {

    }

    public Tanitu(string SkilName, string Description, int UseSp, int Needlv, int AttackRengeMin, int AttackRengeMax, int MaxAttackCount, GameObject Animation
      , int Strengethho, int Intellectho) : base(SkilName,Description,UseSp,Needlv,AttackRengeMin,AttackRengeMax,MaxAttackCount,Animation
      ,Strengethho,Intellectho)
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
        
        
            Debug.Log(SkilName + "を実行");
        foreach (UnitObject unit in SkilManager.Instance.UsedUnits)
        {
            Damageculcu(NewBehaviourScript.Instance.map.FocusingUnit, unit);
            if(Abnormalcondition != null &&!unit.Unit.Abnormalcondtion.Contains(Abnormalcondition))
            {
                unit.Unit.Abnormalcondtion.Add(Abnormalcondition);
            }
        }
    }


    public void Damageculcu(UnitObject fromUnit, UnitObject toUnit)
    {
        int life = toUnit.Unit.Life;
        //toUnit.SkilDamage(fromUnit,this);   
        BattleManager.Instance.SkilDamage(fromUnit, this, toUnit);
        if (toUnit.Unit.Life <= 0)
        {
            toUnit.Destory = true;
            BattleManager.Instance.DestroyWithAnimate(toUnit.gameObject);
            UnitManager.Instance.GetExp(fromUnit.Unit);
        }

    }


    public override void SkilAble()
    {
        Main_Stage map = NewBehaviourScript.Instance.map;
        //Debug.Log(skil.SkilName + "の範囲");
        map.ClearHighlight();
        SkilManager.Instance.UseSpell = null;
        Main_tile cell = map.FocusingUnit.Cell; // 使用者の座標
        foreach (Main_tile skilcell in map.GetCellsByDistance(cell, this.AttackRengeMin, this.AttackRengeMax))
        {
            if (skilcell.Unit != null)
            {
                if (skilcell.Unit.team != map.FocusingUnit.team)
                {
                    skilcell.IsSecondSkilable = true;
                }
                else
                {
                    skilcell.IsSecondSkilable = false;
                }

            }
            else
            {
                skilcell.IsSkilable = true;
            }
        }
    }


}
