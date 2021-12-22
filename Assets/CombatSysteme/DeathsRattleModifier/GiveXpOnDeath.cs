using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveXpOnDeath : DeathsEffect
{
    public Enemies enemie;
    
    public GiveXpOnDeath(Enemies unit) : base(unit)
    {
        enemie = unit;
    }

    public override void DeathRattle(Units killer)
    {
        //TODO : Improve
        if (killer.GetType().IsSubclassOf(typeof(Player)) || killer.GetType() == typeof(Player))
        {
            ((Player) killer).mySkillTree.GetXp(enemie.xpValue);
        }
    }
    
    public override void DeathRattle()
    {
        
    }
}
