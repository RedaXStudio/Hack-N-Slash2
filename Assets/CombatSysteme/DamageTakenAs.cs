using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTakenAs : DamageTakenModifier
{
    //All From of damages can be taken as another type
    public CombatSystemeData.DamageType typeFrom;

    public CombatSystemeData.DamageType typeTo;

    public float conversionValue;

    public DamageTakenAs(Units me, CombatSystemeData.DamageType typeFrom, CombatSystemeData.DamageType typeTo, float conversionValue) : base(me)
    {
        this.typeFrom = typeFrom;
        this.typeTo = typeTo;
        this.conversionValue = conversionValue;
    }
   
    /*public override void Modifier(DamageTypes incDamages, out DamageTypes outDamages)
    {
        outDamages = incDamages;;
        
        if (incDamages.type == typeFrom)
        {
            float dmg = incDamages.damages;
            
            dmg *= (100f - conversionValue) / 100f;

            outDamages.damages = dmg;
        }
    }*/

    public void Conversion(Damage incDamages, float incReducingDamage, out float reducingDamage, List<Damage> damageConvertedList)
    {
        reducingDamage = incReducingDamage;
        
        if (incDamages.myType == typeFrom)
        {
            //storing the reduced damage(we don't reduce damage here cuz ex : 100 phys dmg with 2 10% phys to fire mod : 100 * 0.9 = 90, 90 * 0.9 = 0.81 not equal 100 * 0.8 = 80
            //1 - (1 - 0.9) + (1 - 0.9) = 0.8 utility calc
            
            float baseDamage = incDamages.damages;
            
            float dmg = incDamages.damages;
            
            dmg *= (100f - conversionValue) / 100f;

            reducingDamage += baseDamage - dmg;

            //Converting dmg (adding reduced damage as an other type)

            float damageConverted = baseDamage - dmg;

            damageConvertedList.Add(new Damage(typeTo, incDamages.formOfDamages, damageConverted));

            if (me != null)
            {
                Debug.Log(me.gameObject.name + " : " + baseDamage + incDamages.myType + " Taken as : " 
                          + dmg + incDamages.myType + " + " + damageConverted + typeTo + " dmgs");
            }
        }
    }
}
