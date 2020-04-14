using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPanel
{
    CanvasGroup canvasGroup;
    bool fadingIn;
    bool fadingOut;

    public bool FadingIn { get => fadingIn; set => fadingIn = value; }
    public bool FadingOut { get => fadingOut; set => fadingOut = value; }
    public CanvasGroup CanvasGroup { get => canvasGroup; set => canvasGroup = value; }

    public void Criate(CanvasGroup canvasGroup)
    {
        this.canvasGroup = new CanvasGroup();
        this.canvasGroup = canvasGroup;
    }

}

public class KyotenManager : MonoBehaviour
{
    private static KyotenManager instance;
    public static KyotenManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<KyotenManager>();
            }
            return instance;
        }

    }

    [SerializeField]
    private CanvasGroup Menyu;
    private CanvasPanel menyuPanel = new CanvasPanel();


    [SerializeField]
    private CanvasGroup UnitRan;
    [SerializeField]
    private CanvasGroup Kyouka;
    [SerializeField]
    private CanvasGroup Zukan;

    [SerializeField]
    private CanvasGroup StageSelect;

    [SerializeField]
    private Text underInfomationNameText;
    [SerializeField]
    private CanvasGroup underPanelPage1;
    [SerializeField]
    private CanvasGroup underPanelPage2;
    [SerializeField]
    private CanvasGroup underPanelPage3;

    [SerializeField]
    private CanvasGroup pagePanel;
    [SerializeField]
    private Text pageText;

    [SerializeField]
    private int maxpage;
    private int page;

    [SerializeField]
    private CanvasGroup pageButton;


    [SerializeField]
    private Text underInfomationText;

    public Text UnderInfomationNameText { get => underInfomationNameText; set => underInfomationNameText = value; }
    public Text UnderInfomationText { get => underInfomationText; set => underInfomationText = value; }
    public CanvasGroup PagePanel { get => pagePanel; set => pagePanel = value; }
    public Text PageText { get => pageText; set => pageText = value; }
    public CanvasGroup PageButton { get => pageButton; set => pageButton = value; }
    public CanvasPanel MenyuPanel { get => menyuPanel; set => menyuPanel = value; }
    public CanvasGroup UnitRan1 { get => UnitRan;}

    [SerializeField]
    private float fadeTime;

    public bool instantClose = false;

    private void Start()
    {
        Close(UnitRan1);
        Close(StageSelect);
        Open(Menyu);

        Close(underPanelPage2);
        Close(underPanelPage3);
        Open(underPanelPage1);
        Close(PagePanel);
        page = 1;

        MenyuPanel.Criate(Menyu);

    }



    public void Open(CanvasGroup UnitKousei)
    {
        UnitKousei.alpha = 1;
        UnitKousei.interactable = true;
        UnitKousei.blocksRaycasts = true;

    }

    public void Close(CanvasGroup UnitKousei)
    {
        UnitKousei.alpha = 0;
        UnitKousei.interactable = false;
        UnitKousei.blocksRaycasts = false;

    }






    public void OnPageButton()
    {
        page += 1;
        if (page > maxpage)
        {
            page = 1;
        }

        switch (page)
        {
            case 1:
                PageText.text = string.Format("({0}/{1})", page, maxpage);
                Close(underPanelPage2);
                Close(underPanelPage3);
                Open(underPanelPage1);
                break;
            case 2:
                Close(underPanelPage1);
                Close(underPanelPage3);
                Open(underPanelPage2);
                PageText.text = string.Format("({0}/{1})", page, maxpage);
                break;
            case 3:
                Close(underPanelPage2);
                Close(underPanelPage1);
                Open(underPanelPage3);
                PageText.text = string.Format("({0}/{1})", page, maxpage);
                break;

        }
    }



    public IEnumerator FadeOut(CanvasPanel canvasPanel)
    {
        if (!canvasPanel.FadingOut)  //checks if we are alredy fading out
        {
            //sets the current state
            canvasPanel.FadingOut = true;
            canvasPanel.FadingIn = false;

            canvasPanel.CanvasGroup.blocksRaycasts = false;
            canvasPanel.CanvasGroup.interactable = false;

            //makes sure that we are not fading out the at same time
            StopCoroutine("FadeIn");

            //sets the values for fading
            float startAlpha = canvasPanel.CanvasGroup.alpha;

            //calculates the rate so that we can fade over * amount of seconds
            float rate = 1.0f / fadeTime;

            //progresses over the set time
            float progress = 0.0f;

            //progresses over the set time
            while (progress < 1.0)
            {
                //lerps from the start alpha to 0 to make the inventory
                canvasPanel.CanvasGroup.alpha = Mathf.Lerp(startAlpha, 0, progress);

                //adds to the progress so that we will get c;pse tp pit goal
                progress += rate * Time.deltaTime;

                if (instantClose)
                {
                    break;
                }


                yield return null;
            }
            //sets the end codition to make sure we are 100% invisble
            canvasPanel.CanvasGroup.alpha = 0;
            //set the status
            instantClose = false;
            canvasPanel.FadingOut = false;
        }

    }

    public IEnumerator FadeIn(CanvasPanel canvasPanel)
    {
        if (!canvasPanel.FadingIn)
        {

            canvasPanel.FadingOut = false;
            canvasPanel.FadingIn = true;
            StopCoroutine("FadeOut");

            float startAlpha = canvasPanel.CanvasGroup.alpha;

            float rate = 1.0f / fadeTime;

            float progress = 0.0f;

            while (progress < 1.0)
            {
                canvasPanel.CanvasGroup.alpha = Mathf.Lerp(startAlpha, 1, progress);

                progress += rate * Time.deltaTime;

                yield return null;
            }

            canvasPanel.CanvasGroup.alpha = 1;
            canvasPanel.CanvasGroup.blocksRaycasts = true;
            canvasPanel.CanvasGroup.interactable = true;
            canvasPanel.FadingIn = false;
        }

    }

    public void OpenObject(CanvasPanel gameobject)
    {
        if (gameobject.CanvasGroup.alpha > 0)
        {
            StartCoroutine(FadeOut(gameobject));
        }
        else
        {            
            StartCoroutine(FadeIn(gameobject));
        }
    }

    public void OnClick(CanvasPanel canvasPanel)
    {
        OpenObject(MenyuPanel);
        UnderInfomationNameText.text = "";
        UnderInfomationText.text = "";
        OpenObject(canvasPanel);
    }


}
