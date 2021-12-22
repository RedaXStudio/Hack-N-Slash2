using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeToMagicItem_UseableItem : Item_Useable
{
    public UpgradeToMagicItem_UseableItem(int xSlotTaken, int ySlotTaken, bool stackable, int ammount, int relativeChanceToLoot, Sprite sprite) : 
        base(xSlotTaken, ySlotTaken, stackable, ammount, relativeChanceToLoot, sprite)
    {
        itemName = "Upgrade To Magic Item";
    }

    public override void UseItem()
    {
        
    }

    public override void UseItem(GameObject target)
    {
        if (target.GetComponent<ItemUI>().Item.GetType().IsSubclassOf(typeof(Item_Equipment)))
        {
            Item_Equipment equipment = (Item_Equipment) target.GetComponent<ItemUI>().Item;

            if (equipment.itemRarity == ItemRarity.normal)
            {
                equipment.UpgradeToMagicItem();
            
                Debug.Log("Upgrade to Magic item : " + target.GetComponent<ItemUI>().Item.itemName);
            }
        }
        else
        {
            Debug.Log("Not an equipment");
        }
    }
}
