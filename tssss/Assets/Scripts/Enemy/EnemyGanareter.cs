using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public enum AIActionType
{
    ターン待機型,
    行動範囲待機型,
    遊撃型,
    金魚の糞型,

    ボス型,
}

public enum AIAargetType
{
    無差別,
    相性,
    弱防御力,
    最小被害,
}

public enum Corps
{
    ランダム,
    バランス,
    近接,
    遠距離,
    魔法,
    支援,
    妨害,
}

public class EnemyGanareter : MonoBehaviour
{

    #region Singleton
    private static EnemyGanareter instance;
    public static EnemyGanareter Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (EnemyGanareter)FindObjectOfType(typeof(EnemyGanareter));

                if (instance == null)
                {
                    Debug.LogError(typeof(EnemyGanareter) + "is nothing");
                }
            }

            return instance;
        }
    }

   



    

    #endregion Singleton

   private static int kai = 0;


    public int Kai { get => kai; }

    private EnemyStatus[] enemys;
    private float dificultRate;

    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    int unitcount;

    /// <summary>
    /// ユニットの作成
    /// </summary>
    public void Createunit()
    {
        kai += 1;
        unitcount = 0;
        // LoadUnit();
        CriateUnit(UnitManager.Instance.LoadUnit());

        //for (int x = 0; x < 3; x++)
        //{
            int randomType = UnityEngine.Random.Range(0, StageData.Instance.UseStage1.PopEnemys.Count);
         PutUnit(1, 2, StageData.Instance.UseStage1.PopEnemys[randomType], RandomType(), Teams.Player2, kai);
           
        //}



    }


    public void CriateUnit(GameObject[] units)
    {
        for (int x = 0; x < units.Length; x++)
        {
            units[x].gameObject.SetActive(true);
            units[x].transform.position = NewBehaviourScript.Instance.map.cells.First(c => c.X == 4 + unitcount && c.Y == 4).transform.position;
            units[x].GetComponent<UnitObject>().X = 4 + unitcount;
            units[x].GetComponent<UnitObject>().Y = 4;
            units[x].GetComponent<UnitObject>().team = Teams.Player1;
            unitcount += 1;
            units[x].tag = "Player";
            units[x].name = units[x].name;
        }
    }

    public UnitTypes RandomType()
    {
        UnitTypes unittype = new UnitTypes();

        int randomType = UnityEngine.Random.Range(0, 3);
        //instantiates for adding to the inventory



        switch (randomType) //selects an item
        {
            case 0:
                unittype = UnitTypes.F;
                break;
            case 1:
                unittype = UnitTypes.W;
                break;
            case 2:
                unittype = UnitTypes.R;
                break;

        }


        return unittype;
    }


    /// <summary>
    /// ユニット配置
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="enemy"></param>
    /// <param name="type"></param>
    /// <param name="team"></param>
    /// <param name="lv"></param>
    public void PutUnit(int x, int y, EnemyStatus enemy, UnitTypes type, Teams team ,int lv)
    {
        Transform unitContainer = GameObject.Find("UnitContainer").transform;
        var unitobject = Instantiate((GameObject)Resources.Load("Prefabs/UnitObject"), Vector3.zero, Quaternion.identity, unitContainer);
        var unit = unitobject.GetComponent<UnitObject>();

        unit.team = team;
        switch (unit.team)
        {
            case Teams.Player2:
                // 敵ユニットはちょっと色を変えて反転
                var image = unit.GetComponent<SpriteRenderer>();
                image.color = new Color(1f, 0.7f, 0.7f);
                var scale = image.transform.localScale;
                scale.x *= -1f;
                image.transform.localScale = scale;
                break;
        }

        unit.gameObject.SetActive(true);
        unit.transform.position = NewBehaviourScript.Instance.map.cells.First(c => c.X == x && c.Y == y).transform.position;
        unit.X = x;
        unit.Y = y;
        unit.Unit.UnitType = type;



        unit.Unit.Strength = enemy.Strength;
        unit.Unit.Armor = enemy.Armor;
        unit.Unit.Intellect = enemy.Intellect;
        unit.Unit.Agility = enemy.Agility;
        unit.Unit.UnitName = enemy.UnitName;
        unit.name = enemy.UnitName;
        unit.Unit.Life = enemy.LifeMax;
        unit.Unit.LifeMax = enemy.LifeMax;
        unit.Unit.MoveAmount = enemy.MoveAmount;
        unit.Unit.AttackRangeMax = enemy.AttackRangeMax;
        unit.Unit.AttackRangeMin = enemy.AttackRangeMin;
        unit.Unit.image = enemy.UnitImage;
        unit.GetComponent<SpriteRenderer>().sprite = enemy.UnitImage;
        unit.Unit.LvType = LvType.TAIKIBANSEI;
        unit.Unit.Skils = enemy.Skils.ToArray();
        unit.Unit.MaxSp = enemy.MaxSp;
        unit.Unit.Sp = enemy.MaxSp;
        unit.Unit.AnimationController = enemy.Animation;
        unit.Unit.Status = enemy;

        unit.Unit.Lv = 1;
        //Debug.Log(lv);
        for (int i = 1; i < lv; i++)
        {
           UnitManager.Instance.LvUp(unit.Unit);
        }

        unit.Unit.Life = enemy.LifeMax;
        if (team == Teams.Player2)
        {
            unit.name += "敵";
            unit.tag = "Enemy";
        }


        //Debug.Log("putunit"+unit.name);
    }



}

