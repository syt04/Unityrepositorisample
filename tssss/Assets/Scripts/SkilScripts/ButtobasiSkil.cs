using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu]
public class ButtobasiSkil : Skil
{

    [SerializeField]
    private int[] SkilHani;

    [SerializeField]
    private int Buttobasidintance;

    public override void Use()
    {
        Debug.Log(SkilName + "を実行");

        foreach (UnitObject unit in SkilManager.Instance.UsedUnits)
        {
                    Damageculcu(NewBehaviourScript.Instance.map.FocusingUnit, unit);

            if (ButtobasiTileable(unit.Cell).Unit ==null) 
            MoveTo(unit, ButtobasiTileable(unit.Cell));

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


    public void Damageculcu(UnitObject fromUnit, UnitObject toUnit)
    {
        int life = toUnit.Unit.Life;
        // toUnit.SkilDamage(fromUnit, this);
        BattleManager.Instance.SkilDamage(fromUnit, this, toUnit);
        int x = life - toUnit.Unit.Life;

        if (toUnit.Unit.Life <= 0)
        {
            toUnit.Destory = true;
            BattleManager.Instance.DestroyWithAnimate(toUnit.gameObject);
            UnitManager.Instance.GetExp(fromUnit.Unit);
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
            ButtobasiTileable(map.cells[j]).IsButtobasimovable = true;
        }
        else
        {
            map.cells[j].IsSkilAttackable = false;
        }
    }


    public Main_tile ButtobasiTileable(Main_tile tile)
    {
        Main_Stage map = NewBehaviourScript.Instance.map;


        switch (SkilManager.Instance.UseSkil.Direction)
        {
            case Direction.N:
                    return map.cells.First(x => x.X == tile.X && x.Y == tile.Y + Buttobasidintance);

            case Direction.W:
                    return map.cells.First(x => x.X == tile.X + Buttobasidintance && x.Y == tile.Y);


            case Direction.S:
                    return map.cells.First(x => x.X == tile.X && x.Y == tile.Y - Buttobasidintance);


            case Direction.E:
                    return map.cells.First(x => x.X == tile.X - Buttobasidintance && x.Y == tile.Y);

        }
        return null;
    }


    /// <summary>
    /// ユニットを対象のマスに移動させます
    /// </summary>
    /// <param name="cell">Cell.</param>
    public void MoveTo(UnitObject unit, Main_tile cell)
    {
        Main_Stage map = NewBehaviourScript.Instance.map;
        map.ClearHighlight();
        unit.X = cell.X;
        unit.Y = cell.Y;

        unit.transform.position = cell.transform.position;

    }

}
