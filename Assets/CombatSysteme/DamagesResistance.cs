using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagesResistance : DamageTakenModifier
{
   public CombatSystemeData.DamageType[] TypesResisted;

   public CombatSystemeData.DamageForm[] formsResisted;

   public int resistanceValue;
   
   public DamagesResistance(Units me, CombatSystemeData.DamageType[] typesResisted, CombatSystemeData.DamageForm[] formsResisted, int resistanceValue) : base(me)
   {
      TypesResisted = typesResisted;
      this.formsResisted = formsResisted;
      this.resistanceValue = resistanceValue;
   }

   public override void Modifier(Damage incDamages, Damage outDamages)
   {
      if (incDamages.myType == Array.Find(TypesResisted, s => s == incDamages.myType) && 
          incDamages.formOfDamages == Array.Find(formsResisted, s => s == incDamages.formOfDamages)) 
      {
         float dmg = incDamages.damages;
         
         dmg *= (100f - resistanceValue) / 100f;

         outDamages.damages = dmg;
         
         if (me != null && (me.GetType().IsSubclassOf(typeof(Player)) || me.GetType() == typeof(Player)) ) {
            Debug.Log(incDamages.damages + incDamages.myType.ToString() + " damage Reduced by : " 
                      + resistanceValue + " %" + "Final : " + dmg + incDamages.myType + " dmg");
         }
      }
   }

   public void UpdateResistanceValue(int i)
   {
      resistanceValue = i;
   }
}
