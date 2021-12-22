using System.Collections;
using System.Collections.Generic;
using Assets.Player.Items_Inventory;
using UnityEngine;

public class Reforge_Useable_Item : Item_Useable
{
    public Reforge_Useable_Item(int xSlotTaken, int ySlotTaken, bool stackable, int ammount, int relativeChanceToLoot, Sprite sprite) : 
        base(xSlotTaken, ySlotTaken, stackable, ammount, relativeChanceToLoot, sprite)
    {
        itemName = "Reforge Item";
    }

    public override void UseItem()
    {
        
    }

    public override void UseItem(GameObject target)
    {
        if (target.GetComponent<ItemUI>().Item.GetType().IsSubclassOf(typeof(Item_Equipment)))
        {
            Item_Equipment equipment = (Item_Equipment) target.GetComponent<ItemUI>().Item;
            
            equipment.RollNewModifier(equipment.itemModifiers, out equipment.itemModifiers, equipment.minimalNbrOfModifiers);
            
            Debug.Log("Roll new Modifier : " + target.GetComponent<ItemUI>().Item.itemName);
        }
        else
        {
            Debug.Log("Not an equipment");
        }
    }
}
