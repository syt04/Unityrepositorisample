using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item
{

    public int Intellect { get; set; }

    public int Agility { get; set; }

    public int Stamina { get; set; }

    public int Strength { get; set; }

    public Equipment()
    { }


    public Equipment(string itemName, string description, ItemType itemType, Quality quality, string spriteNeutral, string spriteHighlighted, int maxSize,int intellect,int agility,int stamina,int strength)
        :base(itemName,description,itemType,quality,spriteNeutral,spriteHighlighted,maxSize)
    {
        this.Intellect = intellect;
        this.Agility = agility;
        this.Stamina = stamina;
        this.Strength = strength;
    }


    public override void Use(Slot slot, ItemScript item,UnitScript unit)
    {
        CharacterPanel.Instance.EquipItem(slot, item);
    }

    public override void Use(Slot slot, ItemScript item)
    {

    }


    public override string GetTooltip(Inventory inv)
    {
        string stats = string.Empty;

        if (Strength > 0)
        {
            stats += "\n+" + Strength.ToString() + "Strength";
        }
        if (Intellect > 0)
        {
            stats += "\n+" + Intellect.ToString() + "Strength";
        }
        if (Agility > 0)
        {
            stats += "\n+" + Agility.ToString() + "Strength";
        }
        if (Stamina > 0)
        {
            stats += "\n+" + Stamina.ToString() + "Strength";
        }


        string itemTip = base.GetTooltip(inv);

        if (inv is VendorInventory && !(this is Weapon))
        {
            return string.Format("{0}" + "<size=14>{1}\n<color=yellow>Price: {2}</color></size>", itemTip, stats, BuyPrice);
        }
        else if (VendorInventory.Instance.IsOpen && !(this is Weapon))
        {
            return string.Format("{0}" + "<size=14>{1}\n<color=yellow>Price: {2}</color></size>", itemTip, stats, SellPrice);
        }
        else
        {
            return string.Format("{0}" + "<size=12>{1}</size>", itemTip, stats);
        }
       
    }
}
