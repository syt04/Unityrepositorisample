//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Material : Item
//{
//    public Material(string itemName,string description,ItemType itemType,Quality quality,string spriteNeutral,string spriteHighlighted,int maxSize)
//        : base(itemName,description,itemType,quality,spriteNeutral,spriteHighlighted,maxSize)
//    {

//    }

//    public Material()
//    { }

//    public override string GetTooltip(Inventory inv)
//    {
//         string materialTip = base.GetTooltip(inv);

//        if (inv is VendorInventory)
//        {
//            return string.Format("{0}\n<size=14><color=yellow>Price: {1}</color></size>", materialTip, BuyPrice);
//        }
//        else if (VendorInventory.Instance.IsOpen)
//        {
//            return string.Format("{0}\n<size=14><color=yellow>Price: {1}</color></size>", materialTip, SellPrice);
//        }
//        else
//        {
//            return materialTip;
//        }
//    }

//    public override void Use(Slot slot, ItemScript item, UnitScript unit)
//    {
        
//    }

//    public override void Use(Slot slot, ItemScript item)
//    {

//    }
//}
