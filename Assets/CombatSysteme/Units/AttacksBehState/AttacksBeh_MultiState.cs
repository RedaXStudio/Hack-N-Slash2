using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttacksBeh_MultiState : State_Multi
{
    public Units unit;

    public float range;

    public float cooldown;
    
    public AttacksBeh_MultiState(Units stateMachine) : base(stateMachine)
    {
        unit = stateMachine;
    }
    
    public override void OnStateEnter() { }

    public override void Tick()
    {
        
    }
    
    public override void OnStateExit() { }

    public virtual void AttackBeh()
    {
        
    }

    public virtual void AttackBeh(Units taget)
    {
        
    }

    public virtual void AttackBeh(List<Units> targets)
    {
        
    }
}
