using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HousyuuManager : MonoBehaviour
{
    #region Singleton

    private static HousyuuManager instance;

    public static HousyuuManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (HousyuuManager)FindObjectOfType(typeof(HousyuuManager));

                if (instance == null)
                {
                    Debug.LogError(typeof(HousyuuManager) + "is nothing");
                }
            }

            return instance;
        }
    }


    #endregion Singleton


    public CanvasPanel CanvasPanel { get => canvasPanel; set => canvasPanel = value; }

    [SerializeField]
    private float fadeTime;

    public bool instantClose = false;

    [SerializeField]
    private CanvasGroup Menyuu;

    private CanvasPanel canvasPanel = new CanvasPanel();

    private void Start()
    {
        canvasPanel.Criate(Menyuu);
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
}
