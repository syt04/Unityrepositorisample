using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvTypeButton : MonoBehaviour
{
    private static LvTypeButton instance;
    public static LvTypeButton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<LvTypeButton>();
            }
            return instance;
        }

    }



    [SerializeField]
    private Text LvTypeText;

    [SerializeField]
    private Button lvButton;


    [SerializeField]
    private LvType types;
    public LvType Types
    {
        get { return types; }
    }

    private void Start()
    {
        types = LvType.SOUZYUKU;
        LvTypeText.text = types.ToString();
        lvButton.interactable = false;
    }


    private void Update()
    {
        if(UnitInfo.Instance.Unit != null)
        {
            lvButton.interactable = true;
        }
        else
        {
            lvButton.interactable = false;
        }
    }

    public void Chenge()
    {
        switch (types)
        {
            case LvType.SOUZYUKU:
                types = LvType.HUTUU;
                LvTypeText.text = types.ToString();
                UnitInfo.Instance.GrowRate();
                break;
            case LvType.HUTUU:
                types = LvType.TAIKIBANSEI;
                LvTypeText.text = types.ToString();
                UnitInfo.Instance.GrowRate();
                break;
            case LvType.TAIKIBANSEI:
                types = LvType.SOUZYUKU;
                LvTypeText.text = types.ToString();
                UnitInfo.Instance.GrowRate();
                break;
        }

    }
}
