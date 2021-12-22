using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTakenAsPoint : PassivePoint
{
    public CombatSystemeData.DamageType typeFrom_Point;
    
    public CombatSystemeData.DamageType typeTo_point;

    public int conversionValue;

    private int indexTrack;

    public override void Awake()
    {
        base.Awake();
        
        modifiers.Add(new Item_modifier_RNG_DamageConversion(skillTreeLinkedTo.statsHolder,0 , typeFrom_Point, 
            typeTo_point, conversionValue));
    }
    
    /*public override void Effect()
    {
        //Debug.Log("");
        skillTreeLinkedTo.statsHolder.damageTakenAs_Modifiers.Add(new DamageTakenAs(skillTreeLinkedTo.player, typeFrom_Point, typeTo_point, conversionValue));

        indexTrack = skillTreeLinkedTo.statsHolder.damageTakenAs_Modifiers.Count;
        
        skillTreeLinkedTo.player.UpdateAll();
    }*/
}
