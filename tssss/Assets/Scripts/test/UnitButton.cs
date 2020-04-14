using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitButton : MonoBehaviour
{
    [SerializeField]
    private EnemyStatus unit;
    public EnemyStatus Unit
    {
        get { return unit; }
        set { unit = value; }
    }

    [SerializeField]
    public Image image;
    [SerializeField]
    public Text text;




    public void Start()
    {
        image.sprite = unit.UnitImage;
        text.text = unit.UnitName;
       // UnitTypebutton.GetComponent<Image>().sprite = UnitManager.Instance.TypeZokusei(unitType);                      
    }




    public void Delite()
    {
        PlayerScript.Instance.Delete(this.unit);
        Destroy(gameObject);

    }



    public void Add()
    {
        //PlayerScript.Instance.AddUnit(gameObject);
        UnitInfo.Instance.InfoUnit(unit);
    }

}
