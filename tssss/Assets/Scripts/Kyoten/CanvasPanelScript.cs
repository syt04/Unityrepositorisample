using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPanelScript : MonoBehaviour
{
    CanvasPanel canvasPanel = new CanvasPanel();

    [SerializeField]
    CanvasGroup canvasGroup;


    protected virtual void Start()
    {
        canvasPanel.Criate(canvasGroup);

    }



    public virtual void OnClick()
    {
        KyotenManager.Instance.OnClick(canvasPanel);
    }

    public virtual void ModoruButton()
    {
        KyotenManager.Instance.UnderInfomationNameText.text = "";
        KyotenManager.Instance.UnderInfomationText.text = "";


        KyotenManager.Instance.OpenObject(KyotenManager.Instance.MenyuPanel);
        KyotenManager.Instance.OpenObject(canvasPanel);

        KyotenManager.Instance.PageButton.alpha = 0;
        KyotenManager.Instance.PageButton.blocksRaycasts = false;
        KyotenManager.Instance.PageButton.interactable = false;
        KyotenManager.Instance.PagePanel.alpha = 0;
    }

}
