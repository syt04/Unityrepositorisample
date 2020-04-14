using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public ItemType ItemType { get; set; }

    public Quality Quality { get; set; }

    public string SpriteNeutral { get; set; }
    public string SpriteHighlighted { get; set; }

    public int MaxSize { get; set; }
    public string ItemName { get; set; }
    public string Description { get; set; }

    public int BuyPrice { get; set; }
    public int SellPrice { get; set; }

    public Item()
    {


    }

    public Item(string itemName,string description,ItemType itemType,Quality quality,string spriteNeutral,string spriteHighlighted,int maxSize) {

        this.ItemName = itemName;
        this.Description = description;
        this.ItemType = itemType;
        this.Quality = quality;
        this.SpriteNeutral = spriteNeutral;
        this.SpriteHighlighted = spriteHighlighted;
        this.MaxSize = maxSize;

    }


    public abstract void Use(Slot slot,ItemScript item,UnitScript unit);

    public abstract void Use(Slot slot, ItemScript item);

    public virtual string GetTooltip(Inventory inv)
    {

        string stats = string.Empty;
        string color = string.Empty;
        string newLine = string.Empty;

        if (Description != string.Empty)
        {
            newLine = "\n";

        }

        switch (Quality)
        {
            case Quality.COMMON:
                color = "white";
                break;
            case Quality.UNCOMMON:
                color = "lime";
                break;
            case Quality.RARE:
                color = "navy";
                break;
            case Quality.EPIC:
                color = "magenta";
                break;
            case Quality.LEGENDARY:
                color = "orange";
                break;
            case Quality.ARTIFACT:
                color = "red";
                break;
        }
        return string.Format("<color=" + color + "><size=12>{0}</size></color><size=12><i><color=lime>" + newLine + "{1}</color></i>\n{2}</size>", ItemName, Description,ItemType.ToString().ToLower());

    }

}
