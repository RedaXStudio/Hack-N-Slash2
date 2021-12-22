using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Modifier_RNG_Res : Item_Modifer_RNG
{
    private CombatSystemeData.DamageType resistanceType;

    public int resistanceValue;
    
    public Item_Modifier_RNG_Res(StatsHolder statsHolder, int relativeChanceToRollThisMod, CombatSystemeData.DamageType resistanceType) : 
        base(statsHolder, relativeChanceToRollThisMod)
    {
        this.resistanceType = resistanceType;
    }
    
    public Item_Modifier_RNG_Res(StatsHolder statsHolder, int relativeChanceToRollThisMod, CombatSystemeData.DamageType resistanceType, int resistanceValue) : 
        base(statsHolder, relativeChanceToRollThisMod)
    {
        this.resistanceType = resistanceType;
        this.resistanceValue = resistanceValue;
    }

    public override Item_Modifer_RNG RollValue(int itemLvl)
    {
        Item_Modifier_RNG_Res roll = new Item_Modifier_RNG_Res(null, relativeChanceToRollThisMod, resistanceType);
        
        roll.types = new[] {ModifiersType.Fire, ModifiersType.Resistance};
        
        int tierRange = Random.Range(1, 4);

        roll.tier = tierRange;

        switch (roll.tier)
        {
            case 3 :
                roll.lowRollRange = new []{3, 4};
                //roll.highRollRange = new []{}
                break;
            case 2 :
                roll.lowRollRange = new []{2, 3};
                //roll.highRollRange = new []{}
                break;
            case 1 :
                roll.lowRollRange = new []{1, 2};
                //roll.highRollRange = new []{}
                break;
        }

        roll.lowRoll = Random.Range(roll.lowRollRange[0], roll.lowRollRange[roll.lowRollRange.Length - 1] + 1);
        
        //roll.hightRoll = Random.Range(roll.hightRollRange[0], roll.hightRollRange[roll.hightRollRange.Length - 1]);

        roll.resistanceValue = roll.lowRoll;
        
        roll.modifierDescription = roll.lowRoll + "% " + resistanceType + " Resistance, Tier : " + roll.tier;
        
        return roll;
    }

    public override void ModiferEffect()
    {
        switch (resistanceType)
        {
            case CombatSystemeData.DamageType.FIRE :
                statsHolder.fireResistance += resistanceValue;
                break;
        }
    }
}
