using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkilManager : MonoBehaviour
{

    private static SkilManager instance;
    public static SkilManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SkilManager>();

            }

            return instance;

        }
    }

    [SerializeField]
    private SkilData skilData;
    [SerializeField]
    private AbnormalconditionData abnormaldonditionData;

    [SerializeField]
    private SkilScript useSkil;
    public SkilScript UseSkil
    {
        get { return useSkil; }
    }

    [SerializeField]
    private SpelSkil useSpell;
    public SpelSkil UseSpell
    {
        get { return useSpell; }
        set { useSpell = value; }
    }

    [SerializeField]
    private List<UnitObject> usedUnits = new List<UnitObject>();
    public List<UnitObject> UsedUnits
    {
        get { return usedUnits; }
        
    }
    

    public List<Skil> Skillist()
    {
        return skilData.GetSkillist();
    }

    public List<Abnormalcondition> Abnormalcondtionlist()
    {
        return abnormaldonditionData.AbnormalconditionList;
    }

    public Skil Getskil(string skilname)
    {

            return skilData.GetSkillist().Find(Skil => Skil.SkilName == skilname);
        

    }
    
}
