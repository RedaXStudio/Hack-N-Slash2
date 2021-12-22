using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttack : AttacksBeh_MultiState
{
    public BaseAttack(Units stateMachine) : base(stateMachine)
    {
        unit = stateMachine;
    }

    public override void OnStateEnter()
    {
        range = unit.statsHolder.baseAttackRange;
    }

    public override void Tick()
    {
        cooldown -= Time.deltaTime;

        Mathf.Clamp(cooldown, 0, cooldown);
    }
    
    public override void OnStateExit() { }

    public override void AttackBeh()
    {
        
    }

    public override void AttackBeh(Units target)
    {
        //TODO : add a timer for animation
        if (cooldown <= 0)
        {
            List<Damage> damagesRoll = new List<Damage>();
        
            if (unit.finalAttackPhysicalHigh > 0) {
                damagesRoll.Add(new Damage(CombatSystemeData.DamageType.PHYSICAL, CombatSystemeData.DamageForm.HIT, 
                    Random.Range(unit.finalAttackPhysicalLow, unit.finalAttackPhysicalHigh)));
            }

            target.TakeDamage(unit.GetDamages(1), unit, target.nbrOfDamageConversion);

            cooldown = unit.finalAttackSpeed;
        }
    }

    public override void AttackBeh(List<Units> targets)
    {
        
    }
}
