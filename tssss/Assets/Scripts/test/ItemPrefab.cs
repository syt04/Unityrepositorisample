using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPrefab : MonoBehaviour
{
    [SerializeField]
    private string itemName;

    public string ItemName
    {
        get { return itemName; }
        set { itemName = value; }
    }

}
