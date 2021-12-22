using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasedMaximumLifePoint : PassivePoint
{
    public float increaseMaximumLife;

    public override void Awake()
    {
        base.Awake();
        
        modifiers.Add(new Item_Modifier_IncMaxLife(skillTreeLinkedTo.statsHolder, 0, 5));
    }
    
    /*public override void Effect()
    {
        //Debug.Log("Increase maximum life point effect");
        skillTreeLinkedTo.statsHolder.increasedMaximumLife += increaseMaximumLife;
        
        skillTreeLinkedTo.player.UpdateAll();
    }*/
}
