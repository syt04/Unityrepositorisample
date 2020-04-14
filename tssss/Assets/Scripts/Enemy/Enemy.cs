//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using DG.Tweening;
//using UnityEngine.UI;

//public class Enemy : UnitScript
//{
//    public bool turn;
//    public bool end;

//    [SerializeField]
//    private EnemyStatusData EnemyDataBase;

//    public MyState mystate;


//    // Use this for initialization
//    public override void  Start()
//    {

//        //if (GetEnemy(UnitName) == null)
//        //    Debug.Log("ないよ");
//        //else {
//            // Debug.Log(GetEnemy(Enemyname).GetInformation);
//            //Debug.Log(numEnemy[GetEnemy("B")]);
//        //}

//        //SpriteRenderer sprite = GetComponent<SpriteRenderer>();

//        //sprite.sprite = GetEnemy(UnitName).GetSprite;
//        //moveAmount = GetEnemy(UnitName).GetMoveAmount;


//    }
    



//    //　名前でアイテムを取得
//    public EnemyStatus GetEnemy(string searchName)
//    {
//        return EnemyDataBase.GetEnemyLists().Find(itemName => itemName.GetName == searchName);
//    }

//    public override void OnClick()
//    {
//        base.OnClick();
//    }



//    public Enemy ActiveUnit
//    {
//        get
//        {

//            return this;

//        }
//    }

//}



