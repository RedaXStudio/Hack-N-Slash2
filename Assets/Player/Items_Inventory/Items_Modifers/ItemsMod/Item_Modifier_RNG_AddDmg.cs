using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Modifier_RNG_AddDmg : Item_Modifer_RNG
{
    private CombatSystemeData.DamageType _damageType;

    public int damageLow, damageHigh;

    public Item_Modifier_RNG_AddDmg(StatsHolder statsHolder, int relativeChanceToRollThisMod, CombatSystemeData.DamageType damageType) 
        : base(statsHolder, relativeChanceToRollThisMod)
    {
        _damageType = damageType;
    }
    
    public Item_Modifier_RNG_AddDmg(StatsHolder statsHolder, int relativeChanceToRollThisMod, CombatSystemeData.DamageType damageType, int damageLow, int damageHigh) 
        : base(statsHolder, relativeChanceToRollThisMod)
    {
        _damageType = damageType;
        this.damageLow = damageLow;
        this.damageHigh = damageHigh;
    }

    public override Item_Modifer_RNG RollValue(int itemLvl)
    {
        Item_Modifier_RNG_AddDmg roll = new Item_Modifier_RNG_AddDmg(null, relativeChanceToRollThisMod, _damageType);
        
        roll.types = new[] {ModifiersType.Physical, ModifiersType.Damage};

        int tierRange = Random.Range(1, 4);

        roll.tier = tierRange;

        switch (roll.tier)
        {
            case 3 :
                roll.lowRollRange = new []{3, 4};
                roll.highRollRange = new[] {5, 6};
                break;
            case 2 :
                roll.lowRollRange = new []{2, 3};
                roll.highRollRange = new[] {4, 5};
                break;
            case 1 :
                roll.lowRollRange = new []{1, 2};
                roll.highRollRange = new[] {3, 4};
                break;
        }

        roll.lowRoll = Random.Range(roll.lowRollRange[0], roll.lowRollRange[roll.lowRollRange.Length - 1] + 1);
        
        roll.hightRoll = Random.Range(roll.highRollRange[0], roll.highRollRange[roll.highRollRange.Length - 1] + 1);

        roll.damageLow = roll.lowRoll;
        roll.damageHigh = roll.hightRoll;
        
        roll.modifierDescription = roll.lowRoll + " to " + roll.hightRoll + " added " + _damageType + " Damage, Tier : " + roll.tier;
        
        return roll;
    }

    public override void ModiferEffect()
    {
        switch (_damageType)
        {
            case CombatSystemeData.DamageType.PHYSICAL :
                statsHolder.addedPhysicalDamagesLow += damageLow;
                statsHolder.addedPhysicalDamagesHigh += damageHigh;
                //Debug.Log("Add phys damage mod proc : " + statsHolder.addedPhysicalDamagesLow + " Damage low : " + damageLow);
                Debug.Log(Gears.gears.managerMain.playerActionManager.player.statsHolder.addedPhysicalDamagesHigh);
                break;
            case CombatSystemeData.DamageType.FIRE :
                statsHolder.addedFireDamagesLow += damageLow;
                statsHolder.addedFireDamagesHigh += damageHigh;
                Debug.Log("Add Fire Damage addedFire dmg high : " + statsHolder.addedFireDamagesHigh);
                break;
            case CombatSystemeData.DamageType.COLD :
                statsHolder.addedColdDamageLow += damageLow;
                statsHolder.addedColdDamageHigh += damageHigh;
                Debug.Log("Add Cold Damage addedCold dmg high : " + statsHolder.addedColdDamageHigh);
                break;
            case CombatSystemeData.DamageType.LIGHTING :
                statsHolder.addedLightningDamageLow += damageLow;
                statsHolder.addedLightningDamageHigh += damageHigh;
                Debug.Log("Add Lightning Damage addedLightning dmg high : " + statsHolder.addedLightningDamageHigh);
                Debug.Log(Gears.gears.managerMain.playerActionManager.player.statsHolder.addedLightningDamageHigh);
                break;
            case CombatSystemeData.DamageType.CHAOS :
                statsHolder.addedChaosDamageLow += damageLow;
                statsHolder.addedChaosDamageHigh += damageHigh;
                break;
        }
    }
}
