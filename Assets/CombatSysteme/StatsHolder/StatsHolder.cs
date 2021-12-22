using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsHolder : TrueStatsHolder
{
    #region ResetStats
    public void ResetStats()
    {
        stunImmunity = false;

        baseMovementSpeed = 0;
        increaseMovementSpeed = 0;

        baseLife = 0;
        increasedMaximumLife = 0;
        lifeRegenPerSecond = 0;

        baseAttackRange = 0;

        addedPhysicalDamagesLow = 0;
        addedPhysicalDamagesHigh = 0;
        
        addedFireDamagesLow = 0;
        addedFireDamagesHigh = 0;

        addedColdDamageLow = 0;
        addedColdDamageHigh = 0;

        addedLightningDamageLow = 0;
        addedLightningDamageHigh = 0;

        addedChaosDamageLow = 0;
        addedChaosDamageHigh = 0;

        baseAttackSpeed = 0;
        increaseAttackSpeed = 0;

        //Capacity = 0?
        damageTakenModifiers.Clear();
        damageTakenAs_Modifiers.Clear();
        DeathsEffects.Clear();
        whenHitModifiers.Clear();
        onKillModifiers.Clear();

        PhysicalResistance = 0;
        fireResistance = 0;
        coldResistance = 0;
        lightningResistance = 0;
        chaosResistance = 0;
    }
    
    public void ResetStats(TrueStatsHolder trueStatsHolder)
    {
        stunImmunity = trueStatsHolder.stunImmunity;

        baseMovementSpeed = trueStatsHolder.baseMovementSpeed;
        increaseMovementSpeed = trueStatsHolder.increaseMovementSpeed;

        baseLife = trueStatsHolder.baseLife;
        increasedMaximumLife = trueStatsHolder.increasedMaximumLife;
        lifeRegenPerSecond = trueStatsHolder.lifeRegenPerSecond;

        baseAttackRange = trueStatsHolder.baseAttackRange;

        addedPhysicalDamagesLow = trueStatsHolder.addedPhysicalDamagesLow;
        addedPhysicalDamagesHigh = trueStatsHolder.addedPhysicalDamagesHigh;
        
        addedFireDamagesLow = trueStatsHolder.addedFireDamagesLow;
        addedFireDamagesHigh = trueStatsHolder.addedFireDamagesHigh;

        addedColdDamageLow = trueStatsHolder.addedColdDamageLow;
        addedColdDamageHigh = trueStatsHolder.addedColdDamageHigh;

        addedLightningDamageLow = trueStatsHolder.addedLightningDamageLow;
        addedLightningDamageHigh = trueStatsHolder.addedLightningDamageHigh;

        addedChaosDamageLow = trueStatsHolder.addedChaosDamageLow;
        addedChaosDamageHigh = trueStatsHolder.addedChaosDamageHigh;

        baseAttackSpeed = trueStatsHolder.baseAttackSpeed;
        increaseAttackSpeed = trueStatsHolder.increaseAttackSpeed;

        //Capacity = 0?
        damageTakenModifiers = trueStatsHolder.damageTakenModifiers;
        damageTakenAs_Modifiers = trueStatsHolder.damageTakenAs_Modifiers;
        DeathsEffects = trueStatsHolder.DeathsEffects;
        whenHitModifiers = trueStatsHolder.whenHitModifiers;
        onKillModifiers = trueStatsHolder.onKillModifiers;

        PhysicalResistance = trueStatsHolder.PhysicalResistance;
        fireResistance = trueStatsHolder.fireResistance;
        coldResistance = trueStatsHolder.coldResistance;
        lightningResistance = trueStatsHolder.lightningResistance;
        chaosResistance = trueStatsHolder.chaosResistance;
    }
    #endregion

    public void AddStats(TrueStatsHolder statsToAdd)
    { 
        //Debug.Log("Add stats");
        if (statsToAdd.stunImmunity && !stunImmunity)
        {
            stunImmunity = true;
        }

        baseMovementSpeed += statsToAdd.baseMovementSpeed;
        increaseMovementSpeed += statsToAdd.increaseMovementSpeed;
            
        baseLife += statsToAdd.baseLife;
        increasedMaximumLife += statsToAdd.increasedMaximumLife;
        lifeRegenPerSecond += statsToAdd.lifeRegenPerSecond;

        baseAttackRange += statsToAdd.baseAttackRange;

        addedPhysicalDamagesLow += statsToAdd.addedPhysicalDamagesLow;
        addedPhysicalDamagesHigh += statsToAdd.addedPhysicalDamagesHigh;
        
        addedFireDamagesLow += statsToAdd.addedFireDamagesLow;
        addedFireDamagesHigh += statsToAdd.addedFireDamagesHigh;

        addedColdDamageLow += statsToAdd.addedColdDamageLow;
        addedColdDamageHigh += statsToAdd.addedColdDamageHigh;

        addedLightningDamageLow += statsToAdd.addedLightningDamageLow;
        addedLightningDamageHigh += statsToAdd.addedLightningDamageHigh;

        addedChaosDamageLow += statsToAdd.addedChaosDamageLow;
        addedChaosDamageHigh += statsToAdd.addedChaosDamageHigh;

        baseAttackSpeed += statsToAdd.baseAttackSpeed;
        increaseAttackSpeed += statsToAdd.increaseAttackSpeed;
        
        damageTakenModifiers.AddRange(statsToAdd.damageTakenModifiers);
        damageTakenAs_Modifiers.AddRange(statsToAdd.damageTakenAs_Modifiers);
        DeathsEffects.AddRange(statsToAdd.DeathsEffects);
        whenHitModifiers.AddRange(statsToAdd.whenHitModifiers);
        onKillModifiers.AddRange(statsToAdd.onKillModifiers);

        PhysicalResistance += statsToAdd.PhysicalResistance;
        fireResistance += statsToAdd.fireResistance;
        coldResistance += statsToAdd.coldResistance;
        lightningResistance += statsToAdd.lightningResistance;
        chaosResistance += statsToAdd.chaosResistance;
        
        UpdateResistance();
    }

    public void UpdateResistance()
    {
        //remove all res
        damageTakenModifiers.Remove(damageTakenModifiers.Find(resistance => 
            ((DamagesResistance)resistance).TypesResisted == CombatSystemeData.types_PhysicalResOnly));
        
        damageTakenModifiers.Remove(damageTakenModifiers.Find(resistance => 
            ((DamagesResistance)resistance).TypesResisted == CombatSystemeData.types_FireResOnly));
        
        damageTakenModifiers.Remove(damageTakenModifiers.Find(resistance => 
            ((DamagesResistance)resistance).TypesResisted == CombatSystemeData.types_ColdResOnly));
        
        damageTakenModifiers.Remove(damageTakenModifiers.Find(resistance => 
            ((DamagesResistance)resistance).TypesResisted == CombatSystemeData.types_LightingResOnly));
        
        damageTakenModifiers.Remove(damageTakenModifiers.Find(resistance => 
            ((DamagesResistance)resistance).TypesResisted == CombatSystemeData.types_ChaosResOnly));

        //add all res with updated value
        damageTakenModifiers.Add(new DamagesResistance(null, CombatSystemeData.types_PhysicalResOnly,
            CombatSystemeData.forms_AllForm, PhysicalResistance));
        
        damageTakenModifiers.Add(new DamagesResistance(null,CombatSystemeData.types_FireResOnly,
            CombatSystemeData.forms_AllForm, fireResistance));
        
        damageTakenModifiers.Add(new DamagesResistance(null, CombatSystemeData.types_ColdResOnly,
            CombatSystemeData.forms_AllForm, coldResistance));
        
        damageTakenModifiers.Add(new DamagesResistance(null, CombatSystemeData.types_LightingResOnly,
            CombatSystemeData.forms_AllForm, lightningResistance));
        
        damageTakenModifiers.Add(new DamagesResistance(null, CombatSystemeData.types_ChaosResOnly,
            CombatSystemeData.forms_AllForm, chaosResistance));
        
        //Debug.Log("Resistances Updated");
    }
}
