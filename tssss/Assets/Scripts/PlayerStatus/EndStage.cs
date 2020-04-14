using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndStage : MonoBehaviour
{


    public void UnitKunrenButton()
    {
        Transform unitContainer = GameObject.Find("UnitContainer").transform;
        GanareteUnitRan.unitlist.Clear();
        foreach (UnitObject unit in unitContainer.GetComponentsInChildren<UnitObject>())
        {
            GanareteUnitRan.unitlist.Add(unit.Unit);
        }
        //GanareteUnitRan.Housyuu = Housyuu.Kunren;
        SceneManager.LoadScene("Housyuu", LoadSceneMode.Additive);
    }


    public void RestButton()
    {
        Transform unitContainer = GameObject.Find("UnitContainer").transform;
        GanareteUnitRan.unitlist.Clear();
        foreach (UnitObject unitobject in unitContainer.GetComponentsInChildren<UnitObject>())
        {
            GanareteUnitRan.unitlist.Add(unitobject.Unit);
        }

        //GanareteUnitRan.Housyuu = Housyuu.Rest;
        SceneManager.LoadScene("Housyuu", LoadSceneMode.Additive);
    }

    public void NakamaButton()
    {
        Transform unitContainer = GameObject.Find("UnitContainer").transform;
        GanareteUnitRan.unitlist.Clear();
        foreach (UnitObject unit in unitContainer.GetComponentsInChildren<UnitObject>())
        {
            GanareteUnitRan.unitlist.Add(unit.Unit);
        }

        //GanareteUnitRan.Housyuu = Housyuu.Nakama;
        SceneManager.LoadScene("Housyuu", LoadSceneMode.Additive);
    }

    public void ItemButton()
    {
        Transform unitContainer = GameObject.Find("UnitContainer").transform;
        UnitObject[] units = unitContainer.GetComponentsInChildren<UnitObject>();
        UnitScript[] saveunit = new UnitScript[units.Length];
        for (int x = 0; x < units.Length; x++)
        {
            var tmp = units[x].Unit;
            saveunit[x] = tmp;
        }


        UnitManager.Instance.SaveUnit(saveunit);

        //GanareteUnitRan.Housyuu = Housyuu.Item;
        SceneManager.LoadScene("Housyuu", LoadSceneMode.Additive);

    }
}
