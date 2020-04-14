using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{
    private static StatusManager instance;

    public static StatusManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<StatusManager>();

            }

            return instance;
        }
    }

    [SerializeField]
    public GameObject Skilcontent;
    [SerializeField]
    private GameObject ItemPrefab;
    [SerializeField]
    private Transform ItemPos;

    [SerializeField]
    private GameObject comandRan;
    public GameObject ComandRan
    {
        get { return comandRan; }
    }

    public GameObject SkilRan;



    private void Update()
    {
        if (GameState.Instance.currentTeam == Teams.Player1)
        {
            if (NewBehaviourScript.Instance.map.FocusingUnit != null)
            {
                comandRan.GetComponent<CanvasGroup>().alpha = 1;

            }
            else
            {
                comandRan.GetComponent<CanvasGroup>().alpha = 0;

            }
        }
    }


  

    public void Start()
    {
        ItemCriate();
    }

    public void ItemCriate()
    {
        string Itemcontent = PlayerPrefs.GetString("UnitRan" + "Itemcontent");


        if (Itemcontent != string.Empty)
        {
            string[] splitContent = Itemcontent.Split(';');
            for (int x = 0; x < splitContent.Length - 1; x++)
            {
                GameObject itemran;

               itemran = Instantiate(ItemPrefab);
               itemran.transform.SetParent(ItemPos);
                itemran.GetComponent<ItemPrefab>().ItemName = splitContent[x];

            }

        }


    }

    public void SkilNameCreate(Skil skil2)
    {
        GameObject skilran;

        skilran = Instantiate(SkilRan);
        skilran.transform.SetParent(Skilcontent.transform);
        skilran.GetComponent<SkilScript>().Skil = skil2;
        skilran.GetComponent<SkilScript>().Skil.SkilName = skil2.SkilName;
        skilran.name = skil2.SkilName;
        skilran.GetComponent<SkilScript>().SkilNameText.text = skilran.GetComponent<SkilScript>().Skil.SkilName;
    }


}

