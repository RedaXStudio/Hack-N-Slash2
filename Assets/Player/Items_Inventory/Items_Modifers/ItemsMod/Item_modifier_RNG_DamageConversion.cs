using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_modifier_RNG_DamageConversion : Item_Modifer_RNG
{
    public CombatSystemeData.DamageType damageTypeFrom;
    
    public CombatSystemeData.DamageType damageTypeTo;

    public int conversionValue;
    
    private int indexTrack;
    
    public Item_modifier_RNG_DamageConversion(StatsHolder statsHolder, int relativeChanceToRollThisMod, CombatSystemeData.DamageType damageTypeFrom, 
        CombatSystemeData.DamageType damageTypeTo) : base(statsHolder, relativeChanceToRollThisMod)
    {
        this.damageTypeFrom = damageTypeFrom;
        this.damageTypeTo = damageTypeTo;
    }
    
    public Item_modifier_RNG_DamageConversion(StatsHolder statsHolder, int relativeChanceToRollThisMod, CombatSystemeData.DamageType damageTypeFrom, 
        CombatSystemeData.DamageType damageTypeTo, int conversionValue) : base(statsHolder, relativeChanceToRollThisMod)
    {
        this.damageTypeFrom = damageTypeFrom;
        this.damageTypeTo = damageTypeTo;
        this.conversionValue = conversionValue;
    }
    
    public override Item_Modifer_RNG RollValue(int itemLvl)
    {
        Item_modifier_RNG_DamageConversion roll = new Item_modifier_RNG_DamageConversion(null, relativeChanceToRollThisMod, damageTypeFrom, damageTypeTo);
        
        roll.types = new[] {ModifiersType.Fire, ModifiersType.Resistance};

        int tierRange = Random.Range(1, 4);

        roll.tier = tierRange;

        switch (roll.tier)
        {
            case 3 :
                roll.lowRollRange = new []{8, 11};
                break;
            case 2 :
                roll.lowRollRange = new []{5, 8};
                break;
            case 1 :
                roll.lowRollRange = new []{3, 5};
                break;
        }

        roll.lowRoll = Random.Range(roll.lowRollRange[0], roll.lowRollRange[roll.lowRollRange.Length - 1] + 1);

        roll.modifierDescription = roll.lowRoll + "% of " + damageTypeFrom + " Damages converted To " + damageTypeTo + " Damages, Tier : " + roll.tier;

        roll.conversionValue = roll.lowRoll;
        
        return roll;
    }

    public override void ModiferEffect()
    {
        statsHolder.damageTakenAs_Modifiers.Add(new DamageTakenAs(null,
            damageTypeFrom, damageTypeTo, conversionValue));
        
        indexTrack = statsHolder.damageTakenAs_Modifiers.Count;
    }
}
