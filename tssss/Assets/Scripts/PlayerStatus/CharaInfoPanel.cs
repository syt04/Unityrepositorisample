using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaInfoPanel : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup CharainfoPanel;
    [SerializeField]
    private CanvasGroup CharainfoPanel2;
    [SerializeField]
    private CanvasGroup CharainfoPanel3;
    [SerializeField]
    private Text unitName;
    [SerializeField]
    private Text hptext;
    [SerializeField]
    private Slider hpder;

    [SerializeField]
    private Text sptext;
    [SerializeField]
    private Slider spder;

    [SerializeField]
    private Text idouryou;
    [SerializeField]
    private Text syatei;
    [SerializeField]
    private Text lvtext;
    [SerializeField]
    private Image unitimage;
    [SerializeField]
    private Image zokusei;

    private int page =1;
    [SerializeField]
    private int maxpage;
    [SerializeField]
    private Text PageText;
    [SerializeField]
    private Text PageText2;
    [SerializeField]
    private Text PageText3;

    [SerializeField]
    private Text StatusText;
    [SerializeField]
    private Text SkilText;
    [SerializeField]
    private Text DesctiptionText;


    private void Update()
    {
        if(NewBehaviourScript.Instance.map.FocusingUnit != null &&SkilManager.Instance.UseSkil.Skil ==null)
        {
            Show(NewBehaviourScript.Instance.map.FocusingUnit);
        }
        else
        {
            CharainfoPanel.alpha = 0;
            CharainfoPanel.blocksRaycasts = false;
            CharainfoPanel.interactable = false;
        }
    }

    public void Show(UnitObject unitObject)
    {
        var unit = unitObject.Unit;

        switch (page)
        {
            case 1:
                Close(CharainfoPanel3);
                Open(CharainfoPanel);
                break;
            case 2:
                Close(CharainfoPanel);
                Open(CharainfoPanel2);
                break;
            case 3:
                Close(CharainfoPanel2);
                Open(CharainfoPanel3);
                break;
        }

        unitName.text = unit.UnitName;
        hptext.text = string.Format("<size=14>{0}/{1} </size>", unit.Life, unit.LifeMax);
        hpder.maxValue = (float)unit.LifeMax;
        hpder.value = (float)unit.Life;

        sptext.text = string.Format("<size=14>{0}/{1} </size>", unit.Sp, unit.MaxSp);
        spder.maxValue = (float)unit.MaxSp;
        spder.value = (float)unit.Sp;


        idouryou.text = unit.MoveAmount.ToString();
        syatei.text = unit.AttackRangeMin.ToString() + "-" + unit.AttackRangeMax.ToString();
        lvtext.text = "Lv." + unit.Lv.ToString();
        unitimage.sprite = unit.image;
        if (unitObject.team == Teams.Player2) { unitimage.color = new Color(1f, 0.7f, 0.7f); } else { unitimage.color = Color.white; }
        zokusei.sprite = UnitManager.Instance.UnitZokusei(unit);

        StatusText.text = string.Format("Strength:{0}  Armor:{1}\nIntellect:{2}  Agility:{3}\n", unit.Strength,unit.Armor,unit.Intellect,unit.Agility);

        string skil = string.Empty;
        foreach(Skil tmp in unit.Skils)
        {
            skil += tmp.SkilName + "\n";
        }
        SkilText.text = string.Format(skil);
        
    }

    public void OnClickAndChenge()
    {
        if(page +1 > maxpage)
        {
            page = 1;
        } else
        {
            page += 1;
        }

        PageText.text = string.Format("({0}/{1})",page,maxpage);
        PageText2.text = string.Format("({0}/{1})",page,maxpage);
        PageText3.text = string.Format("({0}/{1})",page,maxpage);

    }

    private void Open(CanvasGroup Panel)
    {        
            Panel.alpha = 1;
            Panel.blocksRaycasts = true;
            Panel.interactable = true;
    }

    private void Close(CanvasGroup Panel)
    {
        Panel.alpha = 0;
        Panel.blocksRaycasts = false;
        Panel.interactable = false;
    }

}
