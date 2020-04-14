using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopRan : MonoBehaviour
{

    [SerializeField]
    GameObject SellUnitObject;

    [SerializeField]
    EnemyStatus[] SellUnits;

    [SerializeField]
    Transform ShopPosition;

    void Start()
    {
        for(int x = 0; x < SellUnits.Length; x++)
        {
           var Sellobject = Instantiate(SellUnitObject, ShopPosition);
           var unitobject = Sellobject.GetComponent<UnitButton>();
            Sellobject.name = SellUnits[x].UnitName+"(商品)";
            unitobject.Unit = SellUnits[x];

            if (x % 2 == 0)
            {
                Debug.Log("0");
                Sellobject.GetComponent<Image>().color = new Color(65.0f/255f, 65.0f/255f, 65.0f/255f);
            }
            else
            {
                Debug.Log("1");
                Sellobject.GetComponent<Image>().color = new Color(108.0f/255f, 108.0f/255f, 108.0f/255f);
            }
        }

        //for (int x = 0; x < ShopPosition.GetComponentsInChildren<Image>().Length; x++)
        //{

        //    if (x % 2 == 0)
        //    {
        //        Debug.Log("0");
        //        ShopPosition.GetComponentsInChildren<Image>()[x].GetComponent<Image>().color = new Color(20.0f, 20.0f, 20.0f);
        //    }
        //    else
        //    {
        //        Debug.Log("1");
        //        ShopPosition.GetComponentsInChildren<Image>()[x].GetComponent<Image>().color = new Color(108.0f, 108.0f, 108.0f);
        //    }
        //}

    }




}
