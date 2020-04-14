using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZukanButton : CanvasPanelScript
{
    public void PointerEnter()
    {
        KyotenManager.Instance.UnderInfomationNameText.text = "図鑑";
        KyotenManager.Instance.UnderInfomationText.text = "図鑑に移ります";

    }

}
