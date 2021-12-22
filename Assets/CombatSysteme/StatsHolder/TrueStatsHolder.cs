using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrueStatsHolder
{
    //Misc
    public bool stunImmunity;

    public float baseMovementSpeed;
    public int increaseMovementSpeed;
    
    //Life
    public float baseLife;
    
    public float increasedMaximumLife;

    public float lifeRegenPerSecond;
    
    //Damages
    public float baseAttackRange;

    public int addedPhysicalDamagesLow;
    public int addedPhysicalDamagesHigh;

    public int addedFireDamagesLow;
    public int addedFireDamagesHigh;

    public int addedColdDamageLow;
    public int addedColdDamageHigh;

    public int addedLightningDamageLow;
    public int addedLightningDamageHigh;

    public int addedChaosDamageLow;
    public int addedChaosDamageHigh;

    public float baseAttackSpeed;
    public float increaseAttackSpeed;

    //List of modifiers
    public List<DamageTakenModifier> damageTakenModifiers = new List<DamageTakenModifier>();
    public List<DamageTakenAs> damageTakenAs_Modifiers = new List<DamageTakenAs>();
    
    public List<DeathsEffect> DeathsEffects = new List<DeathsEffect>();
    public List<WhenHitModifier> whenHitModifiers = new List<WhenHitModifier>();
    public List<OnKillModifier> onKillModifiers = new List<OnKillModifier>();

    //Resistances
    public int PhysicalResistance;
    public int fireResistance;
    public int coldResistance;
    public int lightningResistance;
    public int chaosResistance;
}
