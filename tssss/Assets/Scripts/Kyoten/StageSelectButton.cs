using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectButton : CanvasPanelScript
{
    public void PointerEnter()
    {
        KyotenManager.Instance.UnderInfomationNameText.text = "ステージ選択";
        KyotenManager.Instance.UnderInfomationText.text = "ステージ選択画面に移ります";

    }

}
