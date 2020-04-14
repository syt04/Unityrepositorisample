using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayerScript : MonoBehaviour
{
    private static PlayerScript instance;
    public static PlayerScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PlayerScript>();
            }
            return instance;
        }

    }
    [SerializeField]
    private Text goldText;


    private int gold;
    public int Gold
    {
        get { return gold; }
        set
        {
            goldText.text = "Gold :" + value;
            gold = value;
        }
    }


    public GameObject Content;

    [SerializeField]
    private Image UnitInfoImage;
    [SerializeField]
    private Text UnitName;


    public int Unitcount;
    public int MaxUnitcount;

    public Text UnitcountText;

    [SerializeField]
    private GameObject ItemPos;


    [SerializeField]
    private GameObject AddUnitcon;
    [SerializeField]
    private GameObject AddItemcon;
    // Start is called before the first frame update
    void Start()
    {
        Gold = 100;
        //MaxUnitcount = 2;

        UnitcountText.text = string.Format("{0}/{1}",Unitcount,MaxUnitcount);

    }




    public void Delete(EnemyStatus unit)
    {
        Unitcount -= 1;
        //units.Remove(unit);
        UnitcountText.text = string.Format("{0}/{1}", Unitcount, MaxUnitcount);
    }


   


}
