using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CC_RootState : Hard_CrowdControls
{
    public State_Multi movementStateReplaced;

    protected NavMeshAgent navMeshAgentToStop;
    
    public CC_RootState(MultiStateMachine stateMachine, NavMeshAgent navMeshAgentToStop, State_Multi movementStateReplaced, float duration, Units unitsItComeFrom, Units target) :
        base(stateMachine, duration, unitsItComeFrom, target)
    {
        this.navMeshAgentToStop = navMeshAgentToStop;
        this.movementStateReplaced = movementStateReplaced;
    }
    
    public override void OnStateEnter()
    {
        base.OnStateEnter();
        
        Debug.Log(target.gameObject.name + " Rooted");

        if (navMeshAgentToStop.isActiveAndEnabled)
        {
            navMeshAgentToStop.isStopped = true;
        }
    }

    public override void Tick()
    {
        base.Tick();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override void Clear()
    {
        navMeshAgentToStop.isStopped = false;
        
        target.actionsState.SetStateAtIndex(movementStateReplaced, target.movementStateIndex);
    }
}
