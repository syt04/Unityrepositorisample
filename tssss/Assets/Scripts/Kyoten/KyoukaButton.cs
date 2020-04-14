using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KyoukaButton : CanvasPanelScript
{
    public void PointerEnter()
    {
        KyotenManager.Instance.UnderInfomationNameText.text = "ユニット構成";
        KyotenManager.Instance.UnderInfomationText.text = "ユニットを構成します";

    }
}
