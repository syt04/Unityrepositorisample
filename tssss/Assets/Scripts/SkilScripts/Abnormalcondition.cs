using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Abnormalcondition : ScriptableObject
{
    [SerializeField]
    private string conditionName;
    [SerializeField]
    private string description;

    [SerializeField]
    private float lifeDame;
    [SerializeField]
    private float conditionintellect;
    [SerializeField]
    private float conditionagility;
    [SerializeField]
    private float conditionstrength;
    [SerializeField]
    private float conditionyoroi;
    [SerializeField]
    private float conditionmoveAmount;
    [SerializeField]
    private bool canAttack;
    [SerializeField]
    private bool canMove;
    [SerializeField]
    private float conditionF;
    [SerializeField]
    private float conditionW;
    [SerializeField]
    private float conditionR;

    [SerializeField]
    private bool statuscondition;
    [SerializeField]
    private bool turnEndcondition;

    [SerializeField]
    private int zanzonTurn;

    [SerializeField]
    private bool movementcondition;


    public string ConditionName{get{return conditionName;}}

    public string Description { get => description; }
    public float LifeDame { get => lifeDame; set =>lifeDame= value; }
    public float Conditionintellect { get => conditionintellect; }
    public float Conditionagility { get => conditionagility; }
    public float Conditionstrength { get => conditionstrength; }
    public float Conditionyoroi { get => conditionyoroi; }
    public float ConditionmoveAmount { get => conditionmoveAmount; }
    public bool CanAttack { get => canAttack; }
    public bool CanMove { get => canMove; }
    public bool TurnEndcondition { get => turnEndcondition; }
    public bool Statuscondition { get => statuscondition; }
    public bool Movementcondition { get => movementcondition; }
    public float ConditionF { get => conditionF; }
    public float ConditionW { get => conditionW; }
    public float ConditionR { get => conditionR; }
    public int ZanzonTurn { get => zanzonTurn; set => zanzonTurn = value; }
}
