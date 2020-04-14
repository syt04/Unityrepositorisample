using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
   //static List<Unit> units = new List<Unit>();

    private static PlayerStatus instance;
    public static PlayerStatus Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<PlayerStatus>();
            }
            return instance;
        }
    }

    //public void SetUnit(Unit unit)
    //{
    //    if(!(units.Contains(unit)))
    //    units.Add(unit);

    //}

    //public List<Unit> GetUnits()
    //{
    //    return units;
    //}


    //public void CheckPlayer()
    //{
    //    int x = 0;
    //    foreach(Unit unit in units)
    //    {
    //        if(unit.Enegy == 0)
    //        {
    //            x++;
    //            Debug.Log("Next" + GameState.Instance.Next);

    //        }

    //    }

    //    if(x == units.Count)
    //    {
    //        GameState.Instance.Next = true;
    //        Debug.Log("Next" + GameState.Instance.Next);

            
    //    }

    //}

    private void Update()
    {
     //if(GameState.Instance.Turn == eAct.PlayerTuen)
     //   {
     //       CheckPlayer();
     //       Debug.Log("aaaa");
     //   }   
    }

    public void Start()
    {


       

    }
}
