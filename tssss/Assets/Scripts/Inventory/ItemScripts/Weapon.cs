using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Equipment
{
    public float AttackSpeed { get; set; }



    public Weapon()
    { }


    public Weapon(string itemName, string description, ItemType itemType, Quality quality, string spriteNeutral, string spriteHighlighted, int maxSize, int intellect, int agility, int stamina, int strength,float attackSpeed)
        : base(itemName,description,itemType,quality,spriteNeutral,spriteHighlighted,maxSize,intellect,agility,stamina,strength)
    {

        this.AttackSpeed = attackSpeed;


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
        string equipmentTip = base.GetTooltip(inv);

        if (inv is VendorInventory)
        {
            return string.Format("{0}\n<size=14>AttackSpeed{1}\n<color=yellow>Price: {2}</color></size>", equipmentTip, AttackSpeed, BuyPrice);
        }
        else if (VendorInventory.Instance.IsOpen)
        {
            return string.Format("{0}\n<size=14>AttackSpeed{1}\n<color=yellow>Price: {2}</color></size>", equipmentTip, AttackSpeed, SellPrice);
        }
        else
        {
            return string.Format("{0}\n<size=12>AttackSpeed{1}</size>", equipmentTip, AttackSpeed);
        }


        
    }
}
