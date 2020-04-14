using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyUnitButton : MonoBehaviour
{

    [SerializeField]
    private Button BuyButton;



    private void Start()
    {


        BuyButton.interactable = false;

    }
    private void Update()
    {
        if (UnitInfo.Instance.Unit != null)
        {
            BuyButton.interactable = true;
        }
        else
        {
            BuyButton.interactable = false;
        }
    }

    public void AddUnit()
    {
        if (PlayerScript.Instance.Unitcount < PlayerScript.Instance.MaxUnitcount)
        {
            GameObject unitDelete;

            unitDelete = Instantiate(UnitInfo.Instance.Unitbutton);
            unitDelete.transform.SetParent(PlayerScript.Instance.Content.transform);
            unitDelete.GetComponent<UnitDelete>().UnitType = UnitInfo.Instance.ZokuseiButton.Types;
            unitDelete.GetComponent<UnitDelete>().Unit = UnitInfo.Instance.Unit;
            unitDelete.GetComponent<UnitDelete>().LvType = UnitInfo.Instance.LvTypeButton.Types;
            PlayerScript.Instance.Unitcount += 1;
            PlayerScript.Instance.UnitcountText.text = string.Format("{0}/{1}", PlayerScript.Instance.Unitcount, PlayerScript.Instance.MaxUnitcount);
        }

    }
}
