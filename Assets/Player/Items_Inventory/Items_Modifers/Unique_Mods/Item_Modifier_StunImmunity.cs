using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Modifier_StunImmunity : Item_Modifier
{
    public Item_Modifier_StunImmunity(StatsHolder statsHolder, int relativeChanceToRollThisMod) : base(statsHolder, relativeChanceToRollThisMod)
    {
        modifierDescription = "Stun Immunity";
    }

    public override void ModiferEffect()
    {
        statsHolder.stunImmunity = true;
    }
}
