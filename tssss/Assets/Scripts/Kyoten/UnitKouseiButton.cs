using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitKouseiButton : CanvasPanelScript
{
    public void PointerEnter()
    {
        KyotenManager.Instance.UnderInfomationNameText.text = "拠点強化";
        KyotenManager.Instance.UnderInfomationText.text = "拠点の強化を行う画面に移ります";

    }
}
