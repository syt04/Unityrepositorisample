using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfo : MonoBehaviour
{

    private static UnitInfo instance;
    public static UnitInfo Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<UnitInfo>();
            }
            return instance;
        }

    }
    [SerializeField]
    private GameObject unitbutton;

    [SerializeField]
    private Text UnitNameText;
    [SerializeField]
    private Image UnitImage;
    [SerializeField]
    private Text UnitInfoText;
    [SerializeField]
    private ZokuseiButton zokuseiButton;
    [SerializeField]
    private LvTypeButton  lvTypeButton;
    [SerializeField]
    private CanvasGroup InfoPanel;
    [SerializeField]
    private Transform SkilInfoPanel;

    [SerializeField]
    private GameObject SkilButton;

    [SerializeField]
    private Text Page2CharaDescriptionText;
    [SerializeField]
    private Text Page2CharaText;


    [SerializeField]
    private CanvasGroup SkilInfocanvasGroup;


    [SerializeField]
    private EnemyStatus unit;
    public EnemyStatus Unit
    {
        get { return unit; }
        set { unit = value; }
    }

    public ZokuseiButton ZokuseiButton { get => zokuseiButton;}
    public LvTypeButton LvTypeButton { get => lvTypeButton;}
    public GameObject Unitbutton { get => unitbutton;}


    private void Update()
    {
        if (KyotenManager.Instance.UnitRan1.alpha<1)
        {
            unit = null;

            KyotenManager.Instance.Close(InfoPanel);
        }
    }

    public void InfoUnit(EnemyStatus unit)
    {

        this.unit = unit;
        UnitImage.sprite = unit.UnitImage;
        UnitNameText.text = unit.UnitName;
        UnitInfoText.text = unit.Description;

        KyotenManager.Instance.UnderInfomationNameText.text = unit.UnitName;
        KyotenManager.Instance.UnderInfomationText.text = unit.Description;

        GrowRate();

        foreach (var skil in SkilInfoPanel.GetComponentsInChildren<SKilButton>())
        {
            
            Destroy(skil.gameObject);
        }

        if (unit.Skils.Count >= 0)
        {
            for (int x = 0; x < unit.Skils.Count; x++)
            {
                GameObject SkilRan = Instantiate(SkilButton, SkilInfoPanel);
                
                SkilRan.GetComponent<SKilButton>().Skil = unit.Skils[x];
            }
        }



        KyotenManager.Instance.Open(InfoPanel);
        KyotenManager.Instance.Open(KyotenManager.Instance.PagePanel);
        KyotenManager.Instance.Open(KyotenManager.Instance.PageButton);

    }

    //public void AddUnit()
    //{
    //    if (PlayerScript.Instance.Unitcount < PlayerScript.Instance.MaxUnitcount)
    //    {
    //        GameObject unitDelete;

    //        unitDelete = Instantiate(unitbutton);
    //        unitDelete.transform.SetParent(PlayerScript.Instance.Content.transform);
    //        unitDelete.GetComponent<UnitDelete>().UnitType = ZokuseiButton.Types;
    //        unitDelete.GetComponent<UnitDelete>().Unit = unit;
    //        unitDelete.GetComponent<UnitDelete>().LvType = LvTypeButton.Types;
    //        PlayerScript.Instance.Unitcount += 1;
    //        PlayerScript.Instance.UnitcountText.text = string.Format("{0}/{1}", PlayerScript.Instance.Unitcount, PlayerScript.Instance.MaxUnitcount);
    //    }

    //}

    private int[] tmp = new int[6];

    public void GrowRate()
    {
        

        switch (LvTypeButton.Instance.Types)
        {
            case LvType.SOUZYUKU:                
                tmp[0] = unit.HpGrowthRate - 10;
                tmp[1] = unit.SpGrowthRate - 10;
                tmp[2] = unit.StrengthGrowthRate - 10;
                tmp[3] = unit.IntellectGrowthRate - 10;
                tmp[4] = unit.ArmorGrowthRate - 10;
                tmp[5] = unit.AgilityGrowthRate - 10;
                break;
            case LvType.HUTUU:
                tmp[0] = unit.HpGrowthRate;
                tmp[1] = unit.SpGrowthRate;
                tmp[2] = unit.StrengthGrowthRate;
                tmp[3] = unit.IntellectGrowthRate;
                tmp[4] = unit.ArmorGrowthRate;
                tmp[5] = unit.AgilityGrowthRate;
                break;
            case LvType.TAIKIBANSEI:
                tmp[0] = unit.HpGrowthRate + 10;
                tmp[1] = unit.SpGrowthRate + 10;
                tmp[2] = unit.StrengthGrowthRate + 10;
                tmp[3] = unit.IntellectGrowthRate + 10;
                tmp[4] = unit.ArmorGrowthRate + 10;
                tmp[5] = unit.AgilityGrowthRate + 10;
                break;
        }


        Page2CharaText.text = unit.UnitName;
        Page2CharaDescriptionText.text = string.Format("基礎ステータス\nHP:{0}  SP:{1}  STR:{2}  INT:{3}  ARM:{4}  AGI:{5}\n成長率\nHP:{6}% SP:{7}% STR:{8}% INT:{9}% ARM:{10}% AGI:{11}%", unit.LifeMax, unit.MaxSp, unit.Strength, unit.Intellect, unit.Armor, unit.Agility, tmp[0], tmp[1], tmp[2], tmp[3], tmp[4], tmp[5]);


    }



}
