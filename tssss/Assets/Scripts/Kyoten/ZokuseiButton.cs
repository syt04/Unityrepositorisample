using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZokuseiButton : MonoBehaviour
{
    [SerializeField]
    private Image ZokuseiImage;
    [SerializeField]
    private Button zokuseiButton;

    [SerializeField]
    private UnitTypes types;
    public UnitTypes Types
    {
        get { return types; }
    }

    private void Start()
    {
        types = UnitTypes.F;
        ZokuseiImage.sprite = UnitManager.Instance.F;

        zokuseiButton.interactable = false;

    }
    private void Update()
    {
        if (UnitInfo.Instance.Unit != null)
        {
            zokuseiButton.interactable = true;
        }
        else
        {
            zokuseiButton.interactable = false;
        }
    }

    public void Chenge()
    {
        switch (types)
        {
            case UnitTypes.F:
                types = UnitTypes.W;
                ZokuseiImage.sprite = UnitManager.Instance.W;
                break;
            case UnitTypes.W:
                types = UnitTypes.R;
                ZokuseiImage.sprite = UnitManager.Instance.R;
                break;
            case UnitTypes.R:
                types = UnitTypes.F;
                ZokuseiImage.sprite = UnitManager.Instance.F;
                break;
        }

    }

}
