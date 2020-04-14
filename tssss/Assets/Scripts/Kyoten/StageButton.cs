using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageButton : MonoBehaviour
{
    [SerializeField]
    private StageBase stage;

    public void OnClick()
    {

        if (PlayerScript.Instance.Unitcount <= 0)
        {

        }
        else
        {
            List<UnitScript> unit = new List<UnitScript>();
            for (int x = 0; x < PlayerScript.Instance.Content.GetComponentsInChildren<UnitDelete>().Length; x++)
            {
                UnitDelete tmp = PlayerScript.Instance.Content.GetComponentsInChildren<UnitDelete>()[x].GetComponent<UnitDelete>();
                unit.Add(tmp.UnitObject);

            }
            UnitManager.Instance.SaveUnit(unit.ToArray());
            StageData.Instance.UseStage1 = StageData.Instance.GetStage(stage);
            Debug.Log(StageData.Instance.GetStage(stage));
            FadeManager.Instance.LoadScene("SampleScene", 1);
        }
    }
}
