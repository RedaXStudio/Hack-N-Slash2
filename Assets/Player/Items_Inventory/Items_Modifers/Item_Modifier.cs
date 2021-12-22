using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item_Modifier
{
    public StatsHolder statsHolder;

    public string modifierDescription;

    public int relativeChanceToRollThisMod;

    public enum ModifiersType {Physical, Fire, Cold, Lighting, Chaos, Resistance, Life, Damage }
    public Item_Modifier(StatsHolder statsHolder, int relativeChanceToRollThisMod)
    {
        this.statsHolder = statsHolder;
        this.relativeChanceToRollThisMod = relativeChanceToRollThisMod;
    }
    
    public virtual void ModiferEffect()
    {
        
    }

    public Item_Modifier ShallowCopy()
    {
        return (Item_Modifier) MemberwiseClone();
    }
}
