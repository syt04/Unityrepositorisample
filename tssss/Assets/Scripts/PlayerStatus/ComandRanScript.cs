using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ComandRanScript : MonoBehaviour
{
    private static ComandRanScript instance;

    public static ComandRanScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ComandRanScript>();

            }

            return instance;
        }
    }


    [SerializeField]
    private Text hptext;
    [SerializeField]
    private Text unitName;

    //[SerializeField]
    //private Text weapontext;
    [SerializeField]
    private Image gazou;
    //[SerializeField]
    //private Text statsText;
    [SerializeField]
    private Slider hpder;
    //[SerializeField]
    //private Button nextbutton;
    [SerializeField]
    private Text Logturn;
    [SerializeField]
    private string logturn;
    [SerializeField]
    private GameObject CharainfoPanel;

    [SerializeField]
    private Text Idouryou;
    [SerializeField]
    private Text Syatei;
    [SerializeField]
    private Text Lvtext;

    public Image Zokusei;
    [SerializeField]
    private GameObject UnitInfoPanel;
    [SerializeField]
    private Text UnitInfoName;
    [SerializeField]
    private Image UnitInfoImage;
    [SerializeField]
    private Text UnitInfomation;
    [SerializeField]
    private Text UnitInfoHptext;
    [SerializeField]
    private Text UnitInfoLvType;
    [SerializeField]
    private Image UnitInfoType;

    [SerializeField]
    private Button idoubutton;
    [SerializeField]
    private Button skilbutton;
    [SerializeField]
    private Button endbutton;
    [SerializeField]
    private Button serchbutton;
    [SerializeField]
    private CanvasGroup skilcanvas;
    [SerializeField]
    public GameObject Skilcontent;

    public Main_Stage map;

    [SerializeField]
    private GameObject Pausepanel;
    [SerializeField]
    private GameObject ItemPrefab;
    [SerializeField]
    private Transform ItemPos;
    [SerializeField]
    private Text UnitInfoExp;

    [SerializeField]
    private GameObject SkilInfoPanel;
    [SerializeField]
    private Text SkilNameText;
    [SerializeField]
    private Text SkilDescriptionText;


    public GameObject SkilRan;

    public void Show(UnitObject unitObject)
    {
        var unit = unitObject.Unit;
        SkilInfoPanel.GetComponent<CanvasGroup>().alpha = 0;
        SkilInfoPanel.GetComponent<CanvasGroup>().interactable = false;
        SkilInfoPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;

        CharainfoPanel.GetComponent<CanvasGroup>().alpha = 1;
        unitName.text = unit.UnitName;
        hptext.text = string.Format("<size=14><color=red>HP:</color>{0}/{1} </size>", unit.Life, unit.LifeMax);
        this.hpder.maxValue = (float)unit.LifeMax;
        this.hpder.value = (float)unit.Life;
        Idouryou.text = unit.MoveAmount.ToString();
        Syatei.text = unit.AttackRangeMin.ToString() + "-" + unit.AttackRangeMax.ToString();
        Lvtext.text = "Lv." + unit.Lv.ToString();
        gazou.sprite = unit.image;
        if (unitObject.team == Teams.Player2) { gazou.color = new Color(1f, 0.7f, 0.7f); } else { gazou.color = Color.white; }
        Zokusei.sprite = UnitManager.Instance.UnitZokusei(unit);

    }

    public void ShowSkil(Skil skil)
    {
        if (skil != null)
        {

            CharainfoPanel.GetComponent<CanvasGroup>().alpha = 0;
            SkilInfoPanel.GetComponent<CanvasGroup>().alpha = 1;
            SkilInfoPanel.GetComponent<CanvasGroup>().interactable = true;
            SkilInfoPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;

            //Debug.Log(skil.SkilName);
            SkilNameText.text = skil.SkilName;
            SkilDescriptionText.text = string.Format(skil.Description + "\n{0}*{1}+{2}*{3}+{4} = {5}ダメージ", map.FocusingUnit.Unit.Strength, skil.Strengethho, map.FocusingUnit.Unit.Intellect, skil.Intellectho, skil.KoteiDame, (int)((map.FocusingUnit.Unit.Strength * skil.Strengethho) + (map.FocusingUnit.Unit.Intellect * skil.Intellectho) + skil.KoteiDame));
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

