using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Skil : ScriptableObject
{

    [SerializeField]
    private string skilName;
    [SerializeField]
    private string description;
    [SerializeField]
    private int useSp;
    [SerializeField]
    private int needlv;
    [SerializeField]
    private int attackRengeMin;
    [SerializeField]
    private int attackRengeMax;
    [SerializeField]
    private int maxAttackCount;
    [SerializeField]
    private GameObject efect;
    [SerializeField]
    private float strengethho;
    [SerializeField]
    private float intellectho;
    [SerializeField]
    private float koteiDame;
    [SerializeField]
    private bool needDirection;
    [SerializeField]
    private Abnormalcondition abnormalcondition;

    public Abnormalcondition Abnormalcondition{get { return abnormalcondition; }}

    public int Needlv {
        get { return needlv; }
        set { needlv = value; }
    }
    public bool NeedDirection { get { return needDirection; }}
    public int UseSp { get { return useSp; } set { useSp = value; } }

    public string SkilName { get { return skilName; } set { skilName = value; } }

    public int AttackRengeMin { get { return attackRengeMin; } set { attackRengeMin = value; }}

    public int AttackRengeMax { get { return attackRengeMax; } set { attackRengeMax = value; } }

    public int MaxAttackCount { get { return maxAttackCount; } set { maxAttackCount = value; } }

    public string Description { get { return description; } set { description = value; } }

    public GameObject Efect { get { return efect; } set { efect = value; } }

    public float Strengethho { get { return strengethho; } set { strengethho = value; } }
    public float Intellectho { get { return intellectho; } set { intellectho = value; } }
    public float KoteiDame { get { return koteiDame; } set { koteiDame = value; } }


    public Skil() { }

    public Skil(string SkilName,string Description,int UseMp,int Needlv,int AttackRengeMin,int AttackRengeMax,int MaxAttackCount,GameObject Efect
      , int Strengethho,int Intellectho )
    {
        this.SkilName = SkilName;
        this.Description = Description;
        this.UseSp = UseMp;
        this.Needlv = Needlv;
        this.AttackRengeMin = AttackRengeMin;
        this.AttackRengeMax = AttackRengeMax;
        this.MaxAttackCount = MaxAttackCount;
        this.Efect = Efect;
        this.Strengethho = Strengethho;
        this.Intellectho = Intellectho;
    }

    public abstract void Use();

    public virtual string GetTooltip()
    {
        return Description;
    }

    public abstract void SkilAble();

}
