using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State_Multi
{
    protected MultiStateMachine stateMachine;

    public State_Multi(MultiStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void OnStateEnter() { }

    public abstract void Tick();
    
    public virtual void OnStateExit() { }
}

/*public DamagesResistance(MultiStateMachine stateMachine) : base(stateMachine)
{

}

public override void OnStateEnter() { }

public override void Tick()
{
        
}
    
public override void OnStateExit() { }*/