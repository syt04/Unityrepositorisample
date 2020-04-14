using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ConditionsForCompletion
{
    全滅,
    救援,
    回収,
    防衛,
    お店,
    強敵,
    ボス,
}

[CreateAssetMenu]
public class StageBase : ScriptableObject
{

    [SerializeField]
    private string StageName;

    [SerializeField]
    private int maxstratum;

    [SerializeField]
    private List<EnemyStatus> popEnemys = new List<EnemyStatus>();

    [SerializeField]
    private float difficultyRate;

    //[SerializeField]
    //private List<Item> popItems = new List<Item>();

    //[SerializeField]
    //private List<Item> shopItems = new List<Item>();

    public List<EnemyStatus> PopEnemys { get => popEnemys;}
    public int Maxstratum { get => maxstratum;}
    public string StageName1 { get => StageName;}
    public float DifficultyRate { get => difficultyRate;}
}