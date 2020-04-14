using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SerchButtonScript : MonoBehaviour
{
    [SerializeField]
    private GameObject UnitInfoPanel;
    [SerializeField]
    private Text UnitInfoName;
    [SerializeField]
    private Image UnitInfoImage;
    [SerializeField]
    private Text UnitInfoHptext;
    [SerializeField]
    private Text UnitInfomation;
    [SerializeField]
    private Text UnitInfoLvType;
    [SerializeField]
    private Image UnitInfoType;

    [SerializeField]
    private Text UnitInfoExp;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(SerchButton);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.Instance.currentTeam == Teams.Player1)
        {
            if (NewBehaviourScript.Instance.map.FocusingUnit != null)
            {
                gameObject.GetComponent<CanvasGroup>().interactable = true;
                gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
            else
            {
                gameObject.GetComponent<CanvasGroup>().interactable = false;
                gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }
    }


    public void SerchButton()
    {
        //SkilInfoPanel.GetComponent<CanvasGroup>().alpha = 0;
        Main_Stage map = NewBehaviourScript.Instance.map;
        if (UnitInfoPanel.GetComponent<CanvasGroup>().alpha == 0)
        {
            string maxexp =string.Empty;
            switch (map.FocusingUnit.Unit.LvType)
            {

                case LvType.SOUZYUKU:
                    maxexp = "3";
                    break;
                case LvType.HUTUU:
                    maxexp = "5";
                    break;
                case LvType.TAIKIBANSEI:
                    maxexp = "7";
                    break;


            }



            UnitInfoName.text = map.FocusingUnit.Unit.UnitName;
            UnitInfoImage.sprite = map.FocusingUnit.Unit.image;
            if (map.FocusingUnit.team == Teams.Player2) { UnitInfoImage.color = new Color(1f, 0.7f, 0.7f); } else { UnitInfoImage.color = Color.white; }



            UnitInfoHptext.text = "HP:" + map.FocusingUnit.Unit.Life.ToString() + "/" + map.FocusingUnit.Unit.LifeMax.ToString();
            UnitInfomation.text = MyInfomation(NewBehaviourScript.Instance.map.FocusingUnit.Unit);
            UnitInfoLvType.text = map.FocusingUnit.Unit.LvType.ToString();
            UnitInfoType.sprite = UnitManager.Instance.UnitZokusei(map.FocusingUnit.Unit);
            UnitInfoExp.text = "Exp:" + map.FocusingUnit.Unit.Exp.ToString() + "/" + maxexp;


            UnitInfoPanel.GetComponent<CanvasGroup>().alpha = 1;
            UnitInfoPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
            UnitInfoPanel.GetComponent<CanvasGroup>().interactable = true;
        }
        else
        {
            UnitInfoPanel.GetComponent<CanvasGroup>().alpha = 0;
            UnitInfoPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
            UnitInfoPanel.GetComponent<CanvasGroup>().interactable = false;
        }
        map.ClearHighlight();
    }

    //string SkilInfomation;
    public string MyInfomation(UnitScript unit)
    {
        string SkilInfomation;
        SkilInfomation = string.Empty;
        foreach (Skil skil in unit.Skils)
        {
            if (skil != null)
            {
                SkilInfomation += skil.SkilName + "\n";
            }
            else
            {
                SkilInfomation = "ない";
            }
        }


        return string.Format("Hp:{0}/{1}\nStrength:{2}\nIntellect:{3}\nArmor:{4}\nAgility:{5}\n覚えているスキル\n{6} ", unit.Life, unit.LifeMax, unit.Strength, unit.Intellect, unit.Armor, unit.Agility, SkilInfomation);


    }
}
