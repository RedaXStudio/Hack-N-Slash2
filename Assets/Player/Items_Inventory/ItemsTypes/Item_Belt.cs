using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "BaseEquipments/Belt")]
public class Item_Belt : Item_Equipment
{
    public Item_Belt(int xSlotTaken, int ySlotTaken, bool stackable, int ammount, int relativeChanceToLoot, Sprite sprite, ItemRarity rarity, int itemLvl, int implicitArmor) : 
        base(xSlotTaken, ySlotTaken, stackable, ammount, relativeChanceToLoot, sprite, rarity, itemLvl, implicitArmor)
    {
      
    }

    public override void AddRollableMods(List<Item_Modifier> allMods)
    {
        base.AddRollableMods(allMods);
        //allMods.AddRange(Items_Data.itemModifiersForBelts);
    }
}
