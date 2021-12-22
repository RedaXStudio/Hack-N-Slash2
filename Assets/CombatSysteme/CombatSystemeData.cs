using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystemeData
{
    public enum DamageType {PHYSICAL, FIRE, COLD, LIGHTING, CHAOS}

    public static DamageType[] types_PhysicalResOnly = {DamageType.PHYSICAL};
    
    public static DamageType[] types_FireResOnly = {DamageType.FIRE};
    
    public static DamageType[] types_ColdResOnly = {DamageType.COLD};
    
    public static DamageType[] types_LightingResOnly = {DamageType.LIGHTING};
    
    public static DamageType[] types_ChaosResOnly = {DamageType.CHAOS};
    
    public static DamageType[] types_AllRes = {DamageType.PHYSICAL, DamageType.FIRE, DamageType.COLD, DamageType.LIGHTING, DamageType.CHAOS};

    public enum DamageForm {HIT, DAMAGE_OVER_TIME }
    
    public static DamageForm[] forms_HitOnly = {DamageForm.HIT};
    
    public static DamageForm[] forms_DamageOverTimeOnly = {DamageForm.DAMAGE_OVER_TIME};
    
    public static DamageForm[] forms_AllForm = {DamageForm.HIT, DamageForm.DAMAGE_OVER_TIME};
}
