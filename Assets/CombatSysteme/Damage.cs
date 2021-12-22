using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    public CombatSystemeData.DamageType myType;

    public CombatSystemeData.DamageForm formOfDamages;

    public float damages;

    public Damage(CombatSystemeData.DamageType type, CombatSystemeData.DamageForm formOfDamages, float dmg)
    {
        myType = type;

        this.formOfDamages = formOfDamages;

        damages = dmg;
    }
    
    public Damage ShallowCopy()
    {
        return (Damage) MemberwiseClone();
    }
}
