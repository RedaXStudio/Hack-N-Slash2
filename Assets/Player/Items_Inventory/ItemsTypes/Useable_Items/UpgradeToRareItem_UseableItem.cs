using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeToRareItem_UseableItem : Item_Useable
{
    public UpgradeToRareItem_UseableItem(int xSlotTaken, int ySlotTaken, bool stackable, int ammount, int relativeChanceToLoot, Sprite sprite) : 
        base(xSlotTaken, ySlotTaken, stackable, ammount, relativeChanceToLoot, sprite)
    {
        itemName = "Upgarde to Rare Item";
    }

    public override void UseItem()
    {
        
    }

    public override void UseItem(GameObject target)
    {
        if (target.GetComponent<ItemUI>().Item.GetType().IsSubclassOf(typeof(Item_Equipment)))
        {
            Item_Equipment equipment = (Item_Equipment) target.GetComponent<ItemUI>().Item;
            
            Debug.Log("Upgrade To rare Item : " + target.GetComponent<ItemUI>().Item.itemName);
            
            equipment.UpgradeToRareItem();
        }
        else
        {
            Debug.Log("Not an equipment");
        }
    }
}
