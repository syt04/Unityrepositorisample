using System.Collections.Generic;
using UnityEngine;

//ScriptableObjectを継承したクラス
[CreateAssetMenu]
public class EnemyStatusData : ScriptableObject
{

    //ListステータスのList
    public List<EnemyStatus> EnemyStatusList = new List<EnemyStatus>();



    public List<EnemyStatus> GetEnemyLists()
    {
        return EnemyStatusList;
    }


}
