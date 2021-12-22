using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Modifier_IncMaxLife : Item_Modifer_RNG
{
    public int incMaxLife;
    public Item_Modifier_IncMaxLife(StatsHolder statsHolder, int relativeChanceToRollThisMod) : 
        base(statsHolder, relativeChanceToRollThisMod)
    {
        
    }
    
    public Item_Modifier_IncMaxLife(StatsHolder statsHolder, int relativeChanceToRollThisMod, int incMaxLife) : 
        base(statsHolder, relativeChanceToRollThisMod)
    {
        this.incMaxLife = incMaxLife;
    }

    public override Item_Modifer_RNG RollValue(int itemLvl)
    {
        Item_Modifier_IncMaxLife roll = new Item_Modifier_IncMaxLife(null, relativeChanceToRollThisMod);
        
        roll.types = new[] {ModifiersType.Life};
        
        int tierRange = Random.Range(1, 4);

        roll.tier = tierRange;

        switch (roll.tier)
        {
            case 1 :
                roll.lowRollRange = new []{1, 2};
                break;
            case 2 :
                roll.lowRollRange = new []{2, 3};
                break;
            case 3 :
                roll.lowRollRange = new []{3, 4};
                break;
        }

        roll.lowRoll = Random.Range(roll.lowRollRange[0], roll.lowRollRange[roll.lowRollRange.Length - 1] + 1);
        
        roll.hightRoll = Random.Range(roll.highRollRange[0], roll.highRollRange[roll.highRollRange.Length - 1]);

        roll.incMaxLife = roll.lowRoll;
        
        roll.modifierDescription = roll.lowRoll + "% Inc maximum Life, Tier : " + roll.tier;
        
        return roll;
    }

    public override void ModiferEffect()
    {
        statsHolder.increasedMaximumLife += incMaxLife;
    }
}
