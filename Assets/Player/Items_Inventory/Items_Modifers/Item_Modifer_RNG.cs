using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Modifer_RNG : Item_Modifier
{
    public Item_Modifer_RNG(StatsHolder statsHolder, int relativeChanceToRollThisMod) : base(statsHolder, relativeChanceToRollThisMod)
    {
        
    }

    public ModifiersType[] types;
    
    public int tier;

    public int[] lowRollRange;

    public int[] highRollRange;

    public int lowRoll;

    public int hightRoll;

    public virtual Item_Modifer_RNG RollValue(int itemLvl)
    {
        Debug.Log("Base Roll");
        
        Item_Modifer_RNG roll = (Item_Modifer_RNG) MemberwiseClone();

        int tierRange = Random.Range(1, 3);

        roll.tier = tierRange;
        
        //change rollsRange value

        roll.lowRoll = Random.Range(roll.lowRollRange[0], roll.lowRollRange[roll.lowRollRange.Length]);
        
        roll.hightRoll = Random.Range(roll.highRollRange[0], roll.highRollRange[roll.highRollRange.Length]);
        
        return roll;
    }
}
