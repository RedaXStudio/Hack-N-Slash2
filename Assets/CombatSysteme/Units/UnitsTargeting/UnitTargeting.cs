using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTargeting : State_Multi
{
    public Units unit;

    public GameObject target;
    
    public UnitTargeting(Units stateMachine) : base(stateMachine)
    {
        unit = stateMachine;
    }
    
    public override void OnStateEnter() { }

    public override void Tick()
    {
        
    }
    
    public override void OnStateExit() { }
}
