using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Items_Data
{
    //modifiers that all items can roll
    public static Item_Modifier[] modfiersForAllItems = new Item_Modifier[] {
       new Item_Modifier_RNG_Res(null, 5, CombatSystemeData.DamageType.PHYSICAL),
       new Item_Modifier_RNG_Res(null, 5, CombatSystemeData.DamageType.FIRE),
       new Item_Modifier_RNG_Res(null, 5, CombatSystemeData.DamageType.COLD),
       new Item_Modifier_RNG_Res(null, 5, CombatSystemeData.DamageType.LIGHTING),
       new Item_Modifier_RNG_Res(null, 2, CombatSystemeData.DamageType.CHAOS),
       new Item_Modifier_RNG_AddDmg(null, 5, CombatSystemeData.DamageType.PHYSICAL),
       new Item_Modifier_RNG_AddDmg(null, 5, CombatSystemeData.DamageType.FIRE),
       new Item_Modifier_RNG_AddDmg(null, 5, CombatSystemeData.DamageType.COLD),
       new Item_Modifier_RNG_AddDmg(null, 5, CombatSystemeData.DamageType.LIGHTING),
       new Item_Modifier_RNG_AddDmg(null, 2, CombatSystemeData.DamageType.CHAOS),
      new Item_Modifier_StunImmunity(null, 1), 
      new Item_modifier_RNG_DamageConversion(null, 2, CombatSystemeData.DamageType.PHYSICAL, 
          CombatSystemeData.DamageType.FIRE), };

   public static Item_Modifier[] itemModifiersForHelmets;// = new Item_Modifier[] {new Item_Modifier_RNG_AddPhysicalDmg(null), };
}
