using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected StateMachine stateMachine;
    
    public State(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void OnStateEnter() { }
    public abstract void Tick();
    public virtual void OnStateExit() { }
}
