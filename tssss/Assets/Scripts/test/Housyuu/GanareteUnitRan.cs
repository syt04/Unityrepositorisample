using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Housyuu
{
    Shop,
    NewtStage,
    Kunren,
    Nakama,
    Item,
    Rest,
    Result
}

public class GanareteUnitRan : MonoBehaviour
{
    private static GanareteUnitRan instance;
    public static GanareteUnitRan Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GanareteUnitRan>();
            }
            return instance;
        }
    }



    public static List<UnitScript> unitlist = new List<UnitScript>();

    public GameObject Content;
    public GameObject unitran;
    public GameObject ItemPos;
    public GameObject Restunit;

    public Text text;


    [SerializeField]
    private  Housyuu housyuu;
    public  Housyuu Housyuu { get => housyuu; set => housyuu = value; }

    public GameObject Itemhousyuu;


    public  bool ON;

    //public void Start()
    //{
    //    switch (Housyuu)
    //    {
    //        case Housyuu.Kunren:
    //    foreach (UnitScript unit in unitlist)
    //    {
    //        AddUnit(unit);
    //    }
    //            ON = true;
    //            text.text = "レベルを上げるユニットを選んでください。";
    //            break;




    //        case Housyuu.Nakama:
    //            break;

    //        case Housyuu.Item:
    //            text.text = "拾うアイテムを選んでください。";


    //            for (int i = 0; i < 3; i++)
    //            {

    //                GameObject itemran;
    //                itemran = Instantiate(Itemhousyuu);
    //                itemran.transform.SetParent(Content.transform);
    //            }
    //            break;





    //        case Housyuu.Rest:
    //            text.text = "休息しました。";
    //            foreach (UnitScript unit in unitlist)
    //            {
    //                RestUnit(unit);
    //            }



    //            break;
    //    }

    //}

    public void Update()
    {

        switch (housyuu) { }

    }



    public void AddUnit(UnitScript unit)
    {        
            GameObject unitRan;

            unitRan = Instantiate(unitran);
            unitRan.transform.SetParent(Content.transform);
            unitRan.GetComponent<MyUnit>().unit = unit;

       
    }

    public void RestUnit(UnitScript unit)
    {
        GameObject restunit;

        restunit = Instantiate(Restunit);
        restunit.transform.transform.SetParent(Content.transform);
        restunit.GetComponent<MyUnit>().unit = unit;
        restunit.GetComponent<MyUnit>().button.interactable = false;
        restunit.GetComponent<MyUnit>().button.GetComponent<CanvasGroup>().alpha = 0 ;
    }


    public void Chengesecene()
    {

        if (Housyuu == Housyuu.Kunren || Housyuu == Housyuu.Rest)
        {
            List<UnitScript> units = new List<UnitScript>();
            foreach (MyUnit myunit in Content.GetComponentsInChildren<MyUnit>())
                units.Add(myunit.unit);

                UnitManager.Instance.SaveUnit(units.ToArray());
                           
        }
        SceneManager.LoadScene("SampleScene");
    }


    public void SaveItem()
    {
        string Itemcontent = string.Empty;

        for (int x = 0; x < ItemPos.GetComponentsInChildren<ItemPrefab>().Length; x++)
        {
            Itemcontent += ItemPos.GetComponentsInChildren<ItemPrefab>()[x].GetComponent<ItemPrefab>().ItemName.ToString()
    + ";";
        }
        PlayerPrefs.SetString("UnitRan" + "Itemcontent", Itemcontent);
    }
}
