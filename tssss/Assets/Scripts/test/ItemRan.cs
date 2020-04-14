using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemRan : MonoBehaviour
{
    [SerializeField]
    private Button OkButton;
    [SerializeField]
    private Text ItemNameText;
    [SerializeField]
    private Text ItemDescription;
    [SerializeField]
    private Image ItemImage;
    [SerializeField]
    private GameObject Itemprefab;
    [SerializeField]
    private string Itemname;


    public void OKButton()
    {


    }

    public void AddButton()
    {
        GameObject addItem =  Instantiate(Itemprefab);
        addItem.transform.SetParent(GameObject.Find("ItemPos").transform);
        addItem.GetComponent<ItemPrefab>().ItemName = Itemname;

    }

}
