using System.Collections;
using System.Collections.Generic;
using Assets.Player.Items_Inventory;
using UnityEngine;

public class Item_Useable : Item
{
    public Item_Useable(int xSlotTaken, int ySlotTaken, bool stackable, int ammount, int relativeChanceToLoot, Sprite sprite) : 
        base(xSlotTaken, ySlotTaken, stackable, ammount, relativeChanceToLoot, sprite)
    {
        
    }
    
    public virtual void UseItem()
    {
        Debug.Log("Base effect Item");
    }
    
    public virtual void UseItem(GameObject target)
    {
        Debug.Log("Base effect Item");
    }
}
