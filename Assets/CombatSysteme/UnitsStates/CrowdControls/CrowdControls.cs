using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdControls : State_Multi_Modifier
{
    protected Units unitsItComeFrom;
    protected Units target;
    
    public float duration;
    
    public CrowdControls(MultiStateMachine stateMachine, float duration, Units unitsItComeFrom, Units target) : base(stateMachine)
    {
        this.duration = duration;
        this.unitsItComeFrom = unitsItComeFrom;
        this.target = target;
    }
    
    public override void OnStateEnter()
    {
        base.OnStateEnter();
    }

    public override void Tick()
    {
       base.Tick();
       
       duration -= Time.deltaTime;

       if (duration <= 0)
       {
           Clear();
       }
    }

    public override void OnStateExit()
    {
      base.OnStateExit();
    }

    public virtual void Clear()
    {
        
    }
}
