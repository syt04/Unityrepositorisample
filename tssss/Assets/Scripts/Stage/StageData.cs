using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class StageData : MonoBehaviour
{
    #region Singleton
    private static StageData instance;
    public static StageData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (StageData)FindObjectOfType(typeof(StageData));

                if (instance == null)
                {
                    Debug.LogError(typeof(StageData
) + "is nothing");
                }
            }

            return instance;
        }
    }







    #endregion Singleton

    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    [SerializeField]
    private List<StageBase> stageDataContant = new List<StageBase>();

    public List<StageBase> StageDataContant { get => stageDataContant;}
    public StageBase UseStage1 { get => UseStage; set => UseStage = value; }

    [SerializeField]
    private StageBase UseStage;


    public StageBase GetStage(StageBase stage)
    {
        return stageDataContant.Find(x => x == stage);
    }

}