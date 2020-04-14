using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AbnormalconditionData : ScriptableObject
{
    [SerializeField]
    private List<Abnormalcondition> abnormalconditionlist = new List<Abnormalcondition>();

    public List<Abnormalcondition> AbnormalconditionList
    {
        get { return abnormalconditionlist; }
    }

}
