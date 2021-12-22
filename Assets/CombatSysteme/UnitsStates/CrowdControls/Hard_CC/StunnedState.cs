using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StunnedState : Hard_CrowdControls
{
    protected Units stunnedUnits;
    
    public State_Multi_Modifier actionStateReplaced;

    protected NavMeshAgent navMeshAgentToStop;

    public StunnedState(Units stateMachine, NavMeshAgent navMeshAgentToStop, State_Multi_Modifier actionStateReplaced, float stunDuration, Units unitsItComeFrom, Units target) 
        : base(stateMachine, stunDuration, unitsItComeFrom, target)
    {
        this.stateMachine = stateMachine;

        this.actionStateReplaced = actionStateReplaced;

        this.navMeshAgentToStop = navMeshAgentToStop;

        stunnedUnits = stateMachine;
    }

    public override void OnStateEnter()
    { 
        if (stunnedUnits.statsHolder.stunImmunity)
        {
            Clear();
        }
        else
        {
            base.OnStateEnter();
       
            Debug.Log(target.gameObject.name + " stunned");

            if (navMeshAgentToStop.isActiveAndEnabled)
            {
                navMeshAgentToStop.isStopped = true;
            }
            //clalculate stun reduced duration from unit
            //Stun feedBack
        }
    }

    public override void Tick()
    {
       base.Tick();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        //enlever une spirale de stun
    }

    public override void Clear()
    {
        navMeshAgentToStop.isStopped = false;

        target.SetState(actionStateReplaced, target.actionsState, out target.actionsState);
    }
}
