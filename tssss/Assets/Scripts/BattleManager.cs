using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class BattleManager : MonoBehaviour
{

    private static BattleManager instance;
    public static BattleManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BattleManager>();

            }

            return instance;

        }
    }


    /// <summary>
    /// 対象ユニットに攻撃します
    /// </summary>
    /// <param name="fromUnit">From unit.</param>
    /// <param name="toUnit">To unit.</param>
    public IEnumerator AttackTo(UnitObject fromUnit, UnitObject toUnit)
    {
        int life = toUnit.Unit.Life;
        fromUnit.transform.DOLocalMoveX(fromUnit.transform.localPosition.x - 0.2f, 0.2f)
                .SetLoops(2, LoopType.Yoyo);
        yield return new WaitForSeconds(0.5f);
        //toUnit.Damage(fromUnit);
        Damage(fromUnit,toUnit);
        int x = life - toUnit.Unit.Life;



        if (toUnit.Unit.Life <= 0)
        {
            DestroyWithAnimate(toUnit.gameObject);
            UnitManager.Instance.GetExp(fromUnit.Unit);

        }


       NewBehaviourScript.Instance.map.ClearHighlight();
       NewBehaviourScript.Instance.map.FocusingUnit.IsMoved = true;
    }



    public void DestroyWithAnimate(GameObject unit)
    {
        // GetComponent<Button>().enabled = false;

        GameState.Instance.noclick.blocksRaycasts = true;
        unit.transform.DOScale(Vector3.zero, 1.0f).OnComplete(() =>
        {
            Destroy(unit.gameObject);
        });
        GameState.Instance.noclick.blocksRaycasts = false;
    }


    /// <summary>
    /// ダメージを与えます
    /// </summary>
    /// <param name="attacker">Attacker.</param>
    public void Damage(UnitObject attacker, UnitObject defender)
    {
        int x = defender.Unit.Life;
        defender.Unit.Life = Mathf.Max(0, defender.Unit.Life - CalcurateDamageValue(attacker, defender));
        DamageUICreate(x - defender.Unit.Life,defender);
    }

    /// <summary>
    /// スキルダメージを与えます
    /// </summary>
    /// <param name="attacker"></param>
    /// <param name="skil"></param>
    public void SkilDamage(UnitObject attacker, Skil skil, UnitObject defender)
    {
        int x = defender.Unit.Life;
        defender.Unit.Life = Mathf.Max(0, defender.Unit.Life - CalcurateSkilDamageValue(attacker, skil, defender));
        DamageUICreate(x - defender.Unit.Life,defender);
    }

    /// <summary>
    /// ダメージ値を計算します
    /// </summary>
    /// <param name="attacker">Attacker.</param>
    public int CalcurateDamageValue(UnitObject attacker, UnitObject defender)
    {
        // 三つ巴的な相性ダメージ Kuro < Shiro < Tora < Kuro ...
        var unitTypeBonus = new float[] { 1f, 1f, 1f }[(((int)attacker.Unit.UnitType - (int)defender.Unit.UnitType) + 3) % 3];
        var damage = Mathf.RoundToInt(((attacker.Unit.AttackPower*Random.Range(90,111)/100) - defender.Unit.Armor* Random.Range(45,60)/100) * unitTypeBonus * (1f - attacker.Cell.ReduceRate));
        return damage;
    }

    public int CalcurateSkilDamageValue(UnitObject attacker, Skil skil, UnitObject defender)
    {
        // 三つ巴的な相性ダメージ Kuro < Shiro < Tora < Kuro ...
        var unitTypeBonus = new float[] { 1f, 1f, 1f }[(((int)attacker.Unit.UnitType - (int)defender.Unit.UnitType) + 3) % 3];
        var damage = Mathf.RoundToInt((attacker.Unit.Strength * skil.Strengethho - defender.Unit.Armor) + (attacker.Unit.Intellect * skil.Intellectho - defender.Unit.Agility) * unitTypeBonus * (1f - attacker.Cell.ReduceRate) + skil.KoteiDame);
        return damage;
    }

    public void DamageUICreate(int damege,UnitObject unit)
    {
        Vector3 vector = new Vector3(unit.transform.position.x, unit.transform.position.y);
        var obj = Instantiate<GameObject>((GameObject)Resources.Load("Prefabs/DamageText"), vector, Quaternion.identity);
        //damegeui.GetComponent<DamageUI>().text = "-" + damege.ToString();
        obj.GetComponent<DamageUI>().text = "-" + damege.ToString();


        //  var obj = Instantiate<GameObject>(damegeui, vector, Quaternion.identity);
        //var obj = Instantiate<GameObject>((GameObject)Resources.Load("Prefabs/DamageText"), vector, Quaternion.identity);
        //var obj = Instantiate<GameObject>(damegeui);
        //obj.GetComponent<Transform>().position = this.transform.position;
    }

}
