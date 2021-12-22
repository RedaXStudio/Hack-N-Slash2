using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootOnDeath : DeathsEffect
{
    public int chanceToNotLoot;
    
    public LootOnDeath(Units unit, int chanceToNotLoot) : base(unit)
    {
        this.chanceToNotLoot = chanceToNotLoot;
    }

    public override void DeathRattle()
    {
        //Debug.Log("Start Loot");
        
        int rangeNoLoot = Random.Range(0, 100);

        if (rangeNoLoot >= chanceToNotLoot)
        {
            Debug.Log("No Loot");
        }
        else
        {
            Gears.gears.managerMain.currentArea.LootTrashMobs(unit.gameObject.transform.position);
        }
    }

    public override void DeathRattle(Units killer)
    {
        
    }
}
