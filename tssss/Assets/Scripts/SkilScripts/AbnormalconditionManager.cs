using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbnormalconditionManager : MonoBehaviour
{
    private static AbnormalconditionManager instance;

    public static AbnormalconditionManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AbnormalconditionManager>();

            }

            return instance;
        }
    }

    [SerializeField]
    private AbnormalconditionData abnormalconditionData;

    public List<Abnormalcondition> AbnormalconditionData()
    {
        return abnormalconditionData.AbnormalconditionList;
    }

    public Abnormalcondition Abnormalcondition(string abnormalname)
    {
        return AbnormalconditionData().Find(x => x.ConditionName == abnormalname);
    }

}
