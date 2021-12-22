using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTakenModifier : Units_Modifiers
{
    public Units me;

    public DamageTakenModifier(Units me)
    {
        this.me = me;
    }
   
    public virtual void Modifier(Damage incDamages, Damage outDamages)
    {
        Debug.Log("Base DamageModifier");
    }
}
