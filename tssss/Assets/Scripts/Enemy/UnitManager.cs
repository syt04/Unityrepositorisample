using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{

    [SerializeField]
    private Transform unitcontainer;
    public Transform Unitcontainer
    {
        get { return unitcontainer; }
    }

    private static UnitManager instance;

    public static UnitManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UnitManager>();

            }

            return instance;
        }
    }


    [SerializeField]
    private EnemyStatusData enemystatusdata;
    [SerializeField]
    private Sprite f;
    public Sprite F
    {
        get { return f; }
    }

    [SerializeField]
    private Sprite w;
    public Sprite W
    {
        get { return w; }
    }
    [SerializeField]
    private Sprite r;
    public Sprite R
    {
        get { return r; }
    }


    public List<EnemyStatus> EnemyStatuseList()
    {
        return enemystatusdata.GetEnemyLists();
    }

    public EnemyStatus GetEnemyStatus(string unitname)
    {

            return enemystatusdata.GetEnemyLists().Find(unit => unit.UnitName == unitname);

    }

    public EnemyStatus GetAtRandom()
    {

        if (EnemyStatuseList().Count == 0)
        {

            Debug.LogError("リストが空です！");

        }



        return EnemyStatuseList()[Random.Range(0, EnemyStatuseList().Count)];

    }





    public void GetExp(UnitScript unit)
    {
        unit.Exp += 1;        
        switch (unit.LvType)
        {
            case LvType.SOUZYUKU:
                if (unit.Exp >= 3)
                {
                    LvUp(unit);
                }
                break;
            case LvType.HUTUU:
                if (unit.Exp >= 5)
                {
                    LvUp(unit);
                }
                break;
            case LvType.TAIKIBANSEI:
                if (unit.Exp >= 7)
                {
                    LvUp(unit);
                }
                break;
        }
    }


    public void LvUp(UnitScript unit)
    {
        switch (unit.LvType)
        {
            case LvType.SOUZYUKU:
                UnitStatus(unit, -10);
                break;
            case LvType.HUTUU:
                UnitStatus(unit, 0);
                break;
            case LvType.TAIKIBANSEI:
                UnitStatus(unit, 10);
                break;

        }
    }

    public void UnitStatus(UnitScript unit ,int up)
    {

        if (unit.Status.HpGrowthRate + up <= Random.Range(0, 100))
        {
            unit.LifeMax += 1;
            unit.Life += 1;
        }
        if (unit.Status.SpGrowthRate + up <= Random.Range(0, 100))
        {
            unit.MaxSp += 1;
            unit.Sp += 1;
        }

        if (unit.Status.StrengthGrowthRate + up <= Random.Range(0, 100))
            unit.Strength += 1;
        if (unit.Status.ArmorGrowthRate + up <= Random.Range(0, 100))
            unit.Armor += 1;
        if (unit.Status.IntellectGrowthRate + up <= Random.Range(0, 100))
            unit.Intellect += 1;
        if (unit.Status.AgilityGrowthRate + up <= Random.Range(0, 100))
            unit.Agility += 1;      


        unit.Lv += 1;
        unit.Exp = 0;
    }




    public Sprite UnitZokusei(UnitScript unit)
    {
        switch (unit.UnitType)
        {
            case UnitTypes.F:
                return F;//(Sprite)Resources.Load("Sprites/F");

            case UnitTypes.W:
                return W;//(Sprite)Resources.Load("Sprites/W");

            case UnitTypes.R:
                return R;//(Sprite)Resources.Load("Sprites/R");


        }

        return null;

    }

    public Sprite TypeZokusei(UnitTypes unit)
    {
        switch (unit)
        {
            case UnitTypes.F:
                return F;//(Sprite)Resources.Load("Sprites/F");

            case UnitTypes.W:
                return W;//(Sprite)Resources.Load("Sprites/W");

            case UnitTypes.R:
                return R;//(Sprite)Resources.Load("Sprites/R");


        }

        return null;

    }


    public void TestLvup()
    {
        if(NewBehaviourScript.Instance.map.FocusingUnit != null)
        LvUp(NewBehaviourScript.Instance.map.FocusingUnit.Unit);
    }


    /// <summary>
    /// コンテナの中のユニットをセーブ
    /// </summary>
    public void SaveUnit(UnitScript[] unitContainer)
    {
        string content = string.Empty;

        for (int x = 0; x < unitContainer.Length; x++)
        {
            UnitScript unit = unitContainer[x];
            if (unit.UnitType == UnitTypes.F)
            {
                content += "F" + "-";
            }
            else
            if (unit.UnitType == UnitTypes.W)
            {
                content += "W" + "-";
            }
            else
            if (unit.UnitType == UnitTypes.R)
            {
                content += "R" + "-";
            }

            content += unit.Life.ToString() + "-";
            content += unit.Lv.ToString() + "-";
            content += unit.LvType.ToString() + "-";
            content += unit.Exp.ToString() + "-";
            content += unit.Strength.ToString() + "-";
            content += unit.Armor.ToString() + "-";
            content += unit.Intellect.ToString() + "-";
            content += unit.Agility.ToString() + "-";
            content += unit.LifeMax.ToString() + "-";
            content += unit.MaxSp.ToString() + "-";
            content += unit.Sp.ToString() + "-";
            content += unit.UnitName.ToString() + ";";


        }


        //Stores all the info in the playerPrefs
        Debug.Log(content);

        PlayerPrefs.SetString("UnitRan" + "content", content);
        //PlayerPrefs.SetInt("UnitRan" + "Unitcount", unitContainer.GetComponentsInChildren<UnitScript>().Length);
        PlayerPrefs.Save();

    }

    /// <summary>
    /// ユニットのロード
    /// /// </summary>
    public GameObject[] LoadUnit()
    {
        List<GameObject> tmp = new List<GameObject>();

        //Loads all the inventory's data from the playerprefs
        string content = PlayerPrefs.GetString("UnitRan" + "content");
        if (content != string.Empty)
        {


            string[] splitContent = content.Split(';'); // 0-Mana-3;

            for (int j = 0; j < splitContent.Length - 1; j++)
            {
                string[] splitValues = splitContent[j].Split('-');

                //string unit = splitContent[x];
                string unitname = splitValues[12];

                Transform unitContainer = GameObject.Find("UnitContainer").transform;
                //Debug.Log((GameObject)Resources.Load("Prefabs/UnitPrefab"));
                GameObject unit = Instantiate((GameObject)Resources.Load("Prefabs/UnitObject"), Vector3.zero, Quaternion.identity, unitContainer);

                UnitScript unitscript = unit.GetComponent<UnitObject>().Unit;
                //EnemyStatus tmp = new EnemyStatus();


                //UnitScript tmp = new UnitScript();
                //Debug.Log(UnitManager.Instance.GetEnemyStatus(unit));

                //if (tmp == null)
                //{
                //    tmp = unitList.GetUnitLists().Find(unitname => unitname.UnitName == unit);

                //}

                unitscript.image = UnitManager.Instance.GetEnemyStatus(unitname).UnitImage;
                unitscript.AttackRangeMin = UnitManager.Instance.GetEnemyStatus(unitname).AttackRangeMin;
                unitscript.AttackRangeMax = UnitManager.Instance.GetEnemyStatus(unitname).AttackRangeMax;
                unitscript.MoveAmount = UnitManager.Instance.GetEnemyStatus(unitname).MoveAmount;
                unitscript.UnitName = UnitManager.Instance.GetEnemyStatus(unitname).UnitName;
                unitscript.Skils = UnitManager.Instance.GetEnemyStatus(unitname).Skils.ToArray();
                unitscript.AnimationController = UnitManager.Instance.GetEnemyStatus(unitname).Animation;
                unitscript.Status = UnitManager.Instance.GetEnemyStatus(unitname);

                unit.GetComponent<SpriteRenderer>().sprite = UnitManager.Instance.GetEnemyStatus(unitname).UnitImage;



                if (splitValues[0] == "F")
                {
                    //tmp.UnitType = UnitTypes.F;
                    unitscript.UnitType = UnitTypes.F;
                }
                else if (splitValues[0] == "W")
                {
                    //tmp.UnitType = UnitTypes.W;
                    unitscript.UnitType = UnitTypes.W;
                }
                else if (splitValues[0] == "R")
                {
                    //tmp.UnitType = UnitTypes.R;
                    unitscript.UnitType = UnitTypes.R;
                }

                //Debug.Log(splitValues[0]);

                //Debug.Log(unit.GetComponent<UnitScript>().UnitType);

                switch (splitValues[3])
                {

                    case "SOUZYUKU":
                        unitscript.LvType = LvType.SOUZYUKU;
                        break;
                    case "HUTUU":
                        unitscript.LvType = LvType.HUTUU;
                        break;
                    case "TAIKIBANSEI":
                        unitscript.LvType = LvType.TAIKIBANSEI;
                        break;
                }
                unitscript.Life = int.Parse(splitValues[1]);
                unitscript.Lv = int.Parse(splitValues[2]);
                unitscript.Exp = int.Parse(splitValues[4]);
                unitscript.Strength = int.Parse(splitValues[5]);
                unitscript.Armor = int.Parse(splitValues[6]);
                unitscript.Intellect = int.Parse(splitValues[7]);
                unitscript.Agility = int.Parse(splitValues[8]);
                unitscript.LifeMax = int.Parse(splitValues[9]);
                unitscript.MaxSp = int.Parse(splitValues[10]);
                unitscript.Sp = int.Parse(splitValues[11]);


                //Debug.Log(splitValues[3]);

                //Debug.Log(tmp.UnitName+tmp.UnitType+tmp.life+tmp.LvType);
                //NewBehaviourScript.Instance.map.PutUnit(4 + unitcount, 4, tmp, tmp.UnitType, Teams.Player1);




                //unit.gameObject.SetActive(true);
                //// unit.transform.SetParent(unitContainer);
                //unit.transform.position = NewBehaviourScript.Instance.map.cells.First(c => c.X == 4 + unitcount && c.Y == 4).transform.position;
                //unit.GetComponent<UnitObject>().X = 4 + unitcount;
                //unit.GetComponent<UnitObject>().Y = 4;
                //unit.GetComponent<UnitObject>().team = Teams.Player1;
                //unitcount += 1;
                //unit.tag = "Player";
                //unit.name = unitname;

                tmp.Add(unit);

            }
        }
        return tmp.ToArray();
    }

}
