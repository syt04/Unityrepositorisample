using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Direction
{
    N,
    W,
    S,
    E,
}

[CreateAssetMenu]
public class HaniSkil : Skil
{
    [SerializeField]
    private int[] SkilHani;

    public HaniSkil()
    {

    }

    public HaniSkil(string SkilName, string Description, int UseMp, int Needlv, int AttackRengeMin, int AttackRengeMax, int MaxAttackCount, GameObject Animation
      , int Strengethho, int Intellectho) : base(SkilName, Description, UseMp, Needlv, AttackRengeMin, AttackRengeMax, MaxAttackCount, Animation
      , Strengethho, Intellectho)
    {
        this.SkilName = SkilName;
        this.Description = Description;
        this.UseSp = UseMp;
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
            //Vector3 vector = new Vector3(SkilAttacktile.Unit.transform.position.x, SkilAttacktile.Unit.transform.position.y);
            //var obj = Instantiate(Efect, vector, Quaternion.identity);
            Damageculcu(NewBehaviourScript.Instance.map.FocusingUnit, unit);
        }
    }


    public void Damageculcu(UnitObject fromUnit, UnitObject toUnit)
    {
        int life = toUnit.Unit.Life;
       // toUnit.SkilDamage(fromUnit, this);
        BattleManager.Instance.SkilDamage(fromUnit, this,toUnit);
        int x = life - toUnit.Unit.Life;

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
        SkilManager.Instance.UseSpell = null;
        //Debug.Log(skil.SkilName + "の範囲");
        map.ClearHighlight();//いったんクリア
        Main_tile cell = map.FocusingUnit.Cell; // 使用者の座標
        if (map.FocusingUnit != null && !map.FocusingUnit.Atacked)
        {
            for (int i = 0; i < SkilHani.Length; i++)
            {
                for (int j = 0; j < map.cells.Count; j++)
                {
                    var distancex = (cell.X - map.cells[j].X);
                    var distancey = (cell.Y - map.cells[j].Y);
                    var distancedx = Math.Abs(cell.X - map.cells[j].X);
                    var distancedy = Math.Abs(cell.Y - map.cells[j].Y);

                    switch (SkilManager.Instance.UseSkil.Direction)
                    {
                        case Direction.N:
                            if (distancey == -i - 1 && distancedx < SkilHani[i])
                            {
                                CalcurateSkilable(j);
                            }
                            break;
                        case Direction.W:
                            if (distancex == -i - 1 && distancedy < SkilHani[i])
                            {
                                CalcurateSkilable(j);
                            }
                            break;
                        case Direction.S:
                            if (distancey == i + 1 && distancedx < SkilHani[i])
                            {
                                CalcurateSkilable(j);
                            }
                            break;
                        case Direction.E:
                            if (distancex == i + 1 && distancedy < SkilHani[i])
                            {
                                CalcurateSkilable(j);
                            }
                            break;
                    }
                }
            }
        }
    }
    
    public void CalcurateSkilable(int j)
    {
        Main_Stage map = NewBehaviourScript.Instance.map;
        if (map.cells[j].Unit == null)
            map.cells[j].IsSkilable = true;
        else if (map.cells[j].Unit.team != map.FocusingUnit.team)
        {
            map.cells[j].IsSkilAttackable = true;
        }
        else
        {
            map.cells[j].IsSkilAttackable = false;
        }
    }
}