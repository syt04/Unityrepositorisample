using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyUnit : MonoBehaviour
{

    public UnitScript unit;
    public Image Unitimage;
    public Text Unittext;
    public Button button;
    public Text buttuntext;
    public Text UnitStatusText;
    public Image zokusei;

    public Text Lvtext;


    //public void Start()
    //{
    //    Unitimage.sprite = unit.image;
    //    Unittext.text = unit.UnitName;
    //    Lvtext.text = "Lv:" + unit.Lv.ToString();

    //    switch (GanareteUnitRan.Housyuu)
    //    {
    //        case Housyuu.Kunren:
               
    //            UnitStatusText.text = string.Format("Hp:{0}\nString:{1}\nIntellect:{2}\nAgility:{3}\n",unit.Life,unit.Strength,unit.Intellect,unit.Agility);
    //           button.onClick.AddListener(LvupButtun);

    //            break;


    //        case Housyuu.Rest:
    //            if (unit.LifeMax < unit.Life + 10)
    //            {
    //                UnitStatusText.text = string.Format("<color=red><size=20>Hp:{0} →{1}</size></color>", unit.Life, unit.LifeMax);
    //                unit.Life = unit.LifeMax;
    //            }
    //            else
    //            {
    //                UnitStatusText.text = string.Format("<color=red><size=20>Hp:{0} →{1}</size></color>", unit.Life, unit.Life + 10);
    //                unit.Life = unit.Life + 10;
    //            }
               
    //            break;
    //    }
    //    //switch(unit.UnitType)
    //    //{
    //    //    case UnitTypes.F:
    //    //        zokusei.sprite = F;
    //    //        break;
    //    //    case UnitTypes.W:
    //    //        zokusei.sprite = W;
    //    //        break;
    //    //    case UnitTypes.R:
    //    //        zokusei.sprite = R;
    //    //        break;
    //    //}

    //    zokusei.sprite = UnitManager.Instance.UnitZokusei(unit);

    //}



    public void LvupButtun()
    {
        if (GanareteUnitRan.Instance.ON)
        {

            UnitManager.Instance.LvUp(unit);        
            Lvtext.text = "Lv:"+unit.Lv.ToString();
            UnitStatusText.text = string.Format("Hp:{0}\nString:{1}\nIntellect:{2}\nAgility:{3}\n", unit.Life, unit.Strength, unit.Intellect, unit.Agility);
            GanareteUnitRan.Instance.ON = false;
            GanareteUnitRan.Instance.text.text = "訓練をしました";

        }


    }
}
