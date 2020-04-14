using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum eAct
{
    //KeyInput, // キー入力待ち。もしくは待機中
    Start,
    TurnEnd,   // ターン終了

    PlayerTuenStart,
    PlayerTurnAct,
    PlayerTurnEnd,
    EnemyTuenStart,
    EnemyTuenAct,
    EnemyTuenEnd,

    End,
};

public class GameState : MonoBehaviour
{

    private GameObject[] enemyObjects;
    private GameObject[] playerObjects;
    [SerializeField]
    private GameObject StageClea;
    [SerializeField]
    private Transform StageCleaPos;

    [SerializeField]
    public CanvasGroup noclick;
    [SerializeField]
    private GameObject Turnobject;
    [SerializeField]
    private Text logturn;
    [SerializeField]
    private float step_time;

    private static GameState instance;
    public static GameState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameState>();

            }

            return instance;

        }
    }
    private eAct turn;
    public eAct Turn
    {
        get { return turn; }
        set { turn = value; }
    }

    //static MyState[] tmp;

    //public MyState[] zyun;
    [SerializeField]
    private CanvasGroup endStage;


    [SerializeField]
    private Button endTurnButton;

    private void Start()
    {
        Turn = eAct.Start;
       // tmp = new MyState[states.Count];
        endTurnButton.onClick.AddListener(delegate { Plturnend(); });
        unitContainer = GameObject.Find("UnitContainer").transform;
    }

    //private void Update()
    //{
    //    if (tmp.Length != states.Count || tmp.Length == 0)
    //    {
    //        tmp = Speedzyun();

    //        zyun = tmp;
    //    }
    //    else
    //    {

    //        switch (Turn)
    //        {
    //            case eAct.Start:

    //                Turn = eAct.ActBegin;
    //                break;

    //            case eAct.ActBegin:
    //                tmp[i].Myturn = true;


    //                Debug.Log(tmp[i].gameobject.name + "Start");
    //                for (int x = 0; x < 4; x++)
    //                    image[x].sprite = zyun[x].image;
    //                Turn = eAct.Act;
    //                break;

    //            case eAct.Act:
    //                // Debug.Log(tmp[i].gameobject.name + "Act");
    //                if (tmp[i].MyturnEnd)
    //                {
    //                    tmp[i].MyturnEnd = false;
    //                    Turn = eAct.ActEnd;
    //                }
    //                break;

    //            case eAct.ActEnd:
    //                Debug.Log(tmp[i].gameobject.name + "End");
    //                tmp[i].MyturnEnd = false;
    //                tmp[i].Myturn = false;
    //                if (i >= tmp.Length - 1)
    //                {
    //                    Turn = eAct.End;
    //                }
    //                else
    //                {
    //                    zyun = chenge(zyun);
    //                    i++;
    //                    Turn = eAct.ActBegin;
    //                }
    //                break;

    //            case eAct.End:
    //                Array.Clear(tmp, 0, tmp.Length);
    //                tmp = Speedzyun();
    //                i = 0;
    //                for (int x = 0; x >= tmp.Length; x++)
    //                {
    //                    tmp[x].Myturn = false;
    //                    tmp[x].MyturnEnd = false;
    //                }
    //                zyun = chenge(zyun);
    //                Turn = eAct.Start;

    //                break;
    //            case eAct.TurnEnd:




    //                break;
    //        }
    //    }
    //}

    //public void StageUnit()
    //{
    //    //units = PlayerStatus.Instance.GetUnits();
    //   // enemys = EnemyGanareter.Instance.GetEnemylist();

    //}

    //public void SetState(MyState mystate)
    //{
    //    if (!states.Contains(mystate))
    //        states.Add(mystate);
    //    // Debug.Log(mystate.gameobject.name);
    //}

    //public MyState[] Speedzyun()
    //{


    //    MyState[] tmp = new MyState[states.Count];
    //    int i = 0;
    //    foreach (MyState state in states)
    //    {
    //        tmp[i] = state;

    //        i++;
    //    }


    //    bubble_sort(tmp, tmp.Length);
    //    return tmp;

    //}



    //int bubble_sort(MyState[] a, int n)
    //{
    //    int i, data;
    //    MyState tmp;
    //    /* data: 整列する必要があるデータ数。 */
    //    for (data = n; data > 1; --data)
    //    {
    //        /* i: 現在比較中のデータペアの左側を示す */
    //        for (i = 0; i < data - 1; ++i)
    //        {
    //            /* 比較 */
    //            if (a[i].Speed > a[i + 1].Speed)
    //            {
    //                /* 間違っていたので交換 */
    //                tmp = a[i];
    //                a[i] = a[i + 1];
    //                a[i + 1] = tmp;
    //            }
    //        }
    //    }
    //    return 0;
    //}

    //public MyState[] chenge(MyState[] myStates)
    //{

    //    MyState[] tmp = new MyState[myStates.Length];

    //    for (int x = 0; x < tmp.Length - 1; x++)
    //    {
    //        tmp[x] = myStates[x + 1];

    //    }
    //    tmp[3] = myStates[0];

    //    return tmp;

    //}

    public void Update()
    {
        switch (currentTeam)
        {
            case Teams.Player1:
                logturn.text = "<size=20>Turn :<color=blue>Player</color></size>";
                break;
            case Teams.Player2:
                logturn.text = "<size=20>Turn : <color=red>Enemy</color></size>";
                break;

        }

        //StageUnit();
        switch (Turn)
        {
            case eAct.PlayerTuenStart:
              NewBehaviourScript.Instance.map.ClearHighlight();
                NewBehaviourScript.Instance.map.FocusingUnit = null;

                //SkilManager.Instance.PLSpelUse();


                noclick.blocksRaycasts = false;
                currentTeam = Teams.Player1;
                foreach (var unit in unitContainer.GetComponentsInChildren<UnitObject>())
                {
                    unit.IsMoved = Teams.Player1 != unit.team;
                    unit.Atacked = Teams.Player1 != unit.team;

                }
                Turn = eAct.PlayerTurnAct;

                endTurnButton.GetComponent<CanvasGroup>().alpha = 1;
                endTurnButton.GetComponent<CanvasGroup>().interactable = true;
                endTurnButton.GetComponent<CanvasGroup>().blocksRaycasts = true;

                               break;



            case eAct.PlayerTurnAct:
              
                // Enemyというタグが付いているオブジェクトのデータを箱の中に入れる。
                enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");

                // データの入った箱のデータが０に等しくなった時（Enemyというタグが付いているオブジェクトが全滅したとき）
                if (enemyObjects.Length == 0)
                {
                    Instantiate(StageClea, StageCleaPos);
                    Turn = eAct.End;
                }
                noclick.blocksRaycasts = false;
                break;



            case eAct.PlayerTurnEnd:

                endTurnButton.GetComponent<CanvasGroup>().alpha = 0;
                endTurnButton.GetComponent<CanvasGroup>().interactable = false;
                endTurnButton.GetComponent<CanvasGroup>().blocksRaycasts = false;
                playerObjects = GameObject.FindGameObjectsWithTag("Player");
                AbnormalconditionTurn(playerObjects);


                break;
            //case eAct.EnemyTuenStart:
            //    currentTeam = UnitScript.Teams.Player2;
            //    Turn = eAct.EnemyTuenAct;
            //break;

            //        case eAct.EnemyTuenAct:

            //    if (ais.ContainsKey(UnitScript.Teams.Player2))
            //    {
            //        //touchBlocker.SetActive(true);
            //        var ai = ais[UnitScript.Teams.Player2];
            //        bool on = true;
            //        if (on)
            //        {
            //            on = false;
            //            ai.Run();
            //        }
            //        Debug.Log("ai.Run");
            //    }                
            //    break;

            case eAct.EnemyTuenEnd:
                enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
                AbnormalconditionTurn(enemyObjects);

                Debug.Log("Enemyturnend");
                



                Turn = eAct.PlayerTuenStart;
                break;

            case eAct.End:
                Turnobject.GetComponent<CanvasGroup>().alpha = 0;



                endTurnButton.GetComponent<CanvasGroup>().alpha = 0;
                endTurnButton.GetComponent<CanvasGroup>().interactable = false;
                endTurnButton.GetComponent<CanvasGroup>().blocksRaycasts = false;

                step_time += Time.deltaTime;



                // 3秒後に画面遷移（scene2へ移動）
                if (step_time >= 2.0f)
                {


                    // ゲームクリアーシーンに遷移する。
                    //endStage.alpha = 1;
                    //endStage.blocksRaycasts = true;
                    //endStage.interactable = true;
                    FadeManager.Instance.LoadScene("Housyu",2f);

                }



                break;

        }
        //Debug.Log(Turn);
    }




    public void SetAI(Teams team, Main_AI ai)
    {
        ai.Initialize(this);
        ais[team] = ai;
    }

    /// <summary>
    /// ターンを開始します
    /// </summary>
    /// <param name = "team" > Team.</ param >
    public void StartTurn(Teams team)
    {

        currentTeam = team;
        //    foreach (var unit in unitContainer.GetComponentsInChildren<UnitScript>())
        //    {
        //        unit.IsMoved = team != unit.team;

        //    }



        if (team == Teams.Player1)
        {
            Turn = eAct.PlayerTuenStart;
        }
        else if (team == Teams.Player2)
        {
            Turn = eAct.EnemyTuenStart;
        }

        //    if (ais.ContainsKey(team))
        //    {
        //        //touchBlocker.SetActive(true);
        //        var ai = ais[team];
        //        ai.Run();
        //        Debug.Log("ai.Run");
        //    }
        //    else
        //    {
        //        //touchBlocker.SetActive(false);
        //    }
    }

    ///// <summary>
    ///// 次のターンに移ります
    ///// </summary>
    //public void NextTurn()
    //{
    //    var nextTeam = currentTeam == UnitScript.Teams.Player1 ? UnitScript.Teams.Player2 : UnitScript.Teams.Player1;
    //    StartTurn(nextTeam);
    //    Debug.Log("nextTurn" +nextTeam);
    //}

    Dictionary<Teams, Main_AI> ais = new Dictionary<Teams, Main_AI>();



    /// <summary>
    /// 自軍のユニットを取得します
    /// </summary>
    /// <returns>The own units.</returns>
    public UnitObject[] GetOwnUnits()
    {
        unitContainer = GameObject.Find("UnitContainer").transform;

        return unitContainer.GetComponentsInChildren<UnitObject>().Where(x => x.team == currentTeam).ToArray();
    }

    /// <summary>
    /// 敵軍のユニットを取得します
    /// </summary>
    /// <returns>The enemy units.</returns>
    public UnitObject[] GetEnemyUnits()
    {
     
            unitContainer = GameObject.Find("UnitContainer").transform;
        
        return unitContainer.GetComponentsInChildren<UnitObject>().Where(x => x.team != currentTeam).ToArray();
    }


   public Teams currentTeam;

    [SerializeField]
    Transform unitContainer;


    public void Plturnend()
    {

        Turn = eAct.PlayerTurnEnd;

        foreach(UnitObject unit in unitContainer.GetComponentsInChildren<UnitObject>())
        {
            unit.IsFocusing = false;

        }
        if (NewBehaviourScript.Instance.map.FocusingUnit != null)
            NewBehaviourScript.Instance.map.FocusingUnit.IsFocusing = false;
        NewBehaviourScript.Instance.map.FocusingUnit= null;
        NewBehaviourScript.Instance.map.ClearHighlight();

        endTurnButton.GetComponent<CanvasGroup>().alpha = 0;
        endTurnButton.GetComponent<CanvasGroup>().interactable = false;
        endTurnButton.GetComponent<CanvasGroup>().blocksRaycasts = false;
        StatusManager.Instance.ComandRan.GetComponent<CanvasGroup>().alpha = 0;

        noclick.blocksRaycasts = true;
       

        Turn = eAct.EnemyTuenStart;


        currentTeam = Teams.Player2;
        foreach (var unit in unitContainer.GetComponentsInChildren<UnitObject>())
        {
            unit.IsMoved = Teams.Player2 != unit.team;
            unit.Atacked = Teams.Player2 != unit.team;

        }

        Turn = eAct.EnemyTuenAct;

        var ai = ais[currentTeam];
            ai.Run();
           


                if (NewBehaviourScript.Instance.map.FocusingUnit != null)
            NewBehaviourScript.Instance.map.FocusingUnit.IsFocusing = false;
        NewBehaviourScript.Instance.map.FocusingUnit= null;
    }


    public void AbnormalconditionTurn(GameObject[] enemyObjects)
    {
        foreach (GameObject Enemy in enemyObjects)
        {
            if (Enemy.GetComponent<UnitObject>().Unit.Abnormalcondtion.Count > 0)
            {
                foreach (Abnormalcondition condition in Enemy.GetComponent<UnitObject>().Unit.Abnormalcondtion)
                {
                    condition.ZanzonTurn -= 1;
                }
            }

            Enemy.GetComponent<UnitObject>().Unit.Abnormalcondtion.RemoveAll(x => x.ZanzonTurn <= 0);
        }
    }

}
