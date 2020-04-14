using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType { CONSUMEABLE,MAINHAND,TWOHAND,OFFHAND,HEAD,NECK,CHEST,RING,LEGS,BRACERS,BOOTS,TRINKET, SHOULDER,BELT,GENERIC,GENERICWEAPON,MATERIAL,UNIT};
public enum Quality { COMMON,UNCOMMON,RARE,EPIC,LEGENDARY,ARTIFACT}


public class ItemScript : MonoBehaviour
{
    Main_Stage map;

    /// <summary>
    /// The item's neutral sprite
    /// </summary>
    public Sprite spriteNeutral;

    /// <summary>
    /// The item's highlighted sprite
    /// </summary>
    public Sprite spriteHighlighted;

    private Item item;
    public Item Item
    {
        get { return item; }
        set
        {
            item = value;
            spriteHighlighted = Resources.Load<Sprite>(value.SpriteHighlighted);
            spriteNeutral = Resources.Load<Sprite>(value.SpriteNeutral);
        }
    }

    /// <summary>
    /// Uses the item
    /// </summary>
    public void Use(Slot slot)
    {
        //if (SceneCon.Instance.scene == SceneCon.Scene.B) {
        //    map = NewBehaviourScript.Instance.map;
        //    item.Use(slot, this, map.FocusingUnit);
        //}
        //else if (SceneCon.Instance.scene == SceneCon.Scene.Kyoten)
        //{
        //    item.Use(slot, this);
        //}
    }


    public string GetTooltip(Inventory inv)
    {
        return item.GetTooltip(inv);       
    }

}
