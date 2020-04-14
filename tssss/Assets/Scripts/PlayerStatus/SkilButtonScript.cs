using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkilButtonScript : MonoBehaviour
{
    private static SkilButtonScript instance;

    public static SkilButtonScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SkilButtonScript>();

            }

            return instance;
        }
    }


    [SerializeField]
    private CanvasGroup skilcanvas;
    public CanvasGroup Skilcanvas
    {
        get { return skilcanvas; }
    }


    [SerializeField]
    private GameObject Skilcontent;
    [SerializeField]
    private CanvasGroup SkilInfoPanel;
    [SerializeField]
    private Text SkilNameText;
    [SerializeField]
    private Text SkilDescriptionText;



    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(Skilbutton);
    }


   private void Update()
    {
        if (GameState.Instance.Turn != eAct.PlayerTuenStart)
        {
            if (GameState.Instance.currentTeam == Teams.Player1)
            {
                if (NewBehaviourScript.Instance.map.FocusingUnit != null)
                {
                    if (NewBehaviourScript.Instance.map.FocusingUnit.team == Teams.Player1 && !NewBehaviourScript.Instance.map.FocusingUnit.Atacked)
                    {
                        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
                        gameObject.GetComponent<CanvasGroup>().interactable = true;
                    }
                    else
                    {
                        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
                        gameObject.GetComponent<CanvasGroup>().interactable = false;
                        CloseSkilRan();
                    }

                }
                else
                {
                    gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
                    gameObject.GetComponent<CanvasGroup>().interactable = false;
                    CloseSkilRan();
                }
            }
            else
            {
                gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
                gameObject.GetComponent<CanvasGroup>().interactable = false;
                CloseSkilRan();
            }

            if (SkilManager.Instance.UseSkil.Skil == null)
            {
                SkilInfoPanel.alpha = 0;
            }
        }
    }
    private void Skilbutton()
    {
        if (skilcanvas.alpha == 0)
        {
            SkilButton(NewBehaviourScript.Instance.map.FocusingUnit);            
            skilcanvas.alpha = 1;
            skilcanvas.interactable = true;
            skilcanvas.blocksRaycasts = true;            
        }
        else
        {
            CloseSkilRan();
        }

    }


    public void CloseSkilRan()
    {
        for (int i = 0; i < Skilcontent.GetComponentsInChildren<SkilScript>().Length; i++)
        {
            Skilcontent.GetComponentsInChildren<SkilScript>()[i].Destroy();
        }
        skilcanvas.alpha = 0;
        skilcanvas.interactable = false;
        skilcanvas.blocksRaycasts = false;

        if (NewBehaviourScript.Instance.map.cells.Any(x => x.IsSecondSkilable == true)) { }
        else 
        {
            SkilInfoPanel.alpha = 0;
            SkilInfoPanel.GetComponent<CanvasGroup>().interactable = false;
            SkilInfoPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void ShowSkil(Skil skil)
    {
        if (skil != null && GameState.Instance.Turn == eAct.PlayerTurnAct)
        {

           // CharainfoPanel.GetComponent<CanvasGroup>().alpha = 0;
            SkilInfoPanel.GetComponent<CanvasGroup>().alpha = 1;
            SkilInfoPanel.GetComponent<CanvasGroup>().interactable = true;
            SkilInfoPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;

            //Debug.Log(skil.SkilName);
            SkilNameText.text = skil.SkilName;
            SkilDescriptionText.text = string.Format(skil.Description + "\n{0}*{1}+{2}*{3}+{4} = {5}ダメージ", NewBehaviourScript.Instance.map.FocusingUnit.Unit.Strength, skil.Strengethho, NewBehaviourScript.Instance.map.FocusingUnit.Unit.Intellect, skil.Intellectho, skil.KoteiDame, (int)((NewBehaviourScript.Instance.map.FocusingUnit.Unit.Strength * skil.Strengethho) + (NewBehaviourScript.Instance.map.FocusingUnit.Unit.Intellect * skil.Intellectho) + skil.KoteiDame));
        }
    }


    public void SkilButton(UnitObject unit)
    {

        foreach (Skil skilname in unit.Unit.Skils)
        {
            if (skilname != null)
                StatusManager.Instance.SkilNameCreate(SkilManager.Instance.Getskil(skilname.SkilName));
        }


    }
}
