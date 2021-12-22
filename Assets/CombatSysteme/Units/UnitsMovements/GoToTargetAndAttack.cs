using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToTargetAndAttack : State_Multi
{
    public Units unit;

    public NavMeshAgent unitNavMesh;

    public GoToTargetAndAttack(Units stateMachine, NavMeshAgent navMeshAgent) : base(stateMachine)
    {
        unit = stateMachine;
        unitNavMesh = navMeshAgent;
    }
    
    public override void OnStateEnter() { }

    public override void Tick()
    {
        GameObject target = ((UnitTargeting) unit.actionsState.stateMultis[unit.targetingStateIndex]).target;
        
        if (target)
        {
            unitNavMesh.SetDestination(target.transform.position);
            
            if (Vector3.Distance(unit.transform.position, target.transform.position) <
                ((AttacksBeh_MultiState) unit.actionsState.stateMultis[unit.attackStateIndex]).range)
            {
                ((AttacksBeh_MultiState) unit.actionsState.stateMultis[unit.attackStateIndex]).AttackBeh();
                ((AttacksBeh_MultiState) unit.actionsState.stateMultis[unit.attackStateIndex]).AttackBeh(target.GetComponent<Units>());
                
                unitNavMesh.isStopped = true;
            }
            else if (unitNavMesh.isStopped)
            {
                unitNavMesh.isStopped = false;
            }
        }
        else if (unitNavMesh.isStopped || unitNavMesh.destination != unit.transform.position)
        {
            unitNavMesh.isStopped = false;
            unitNavMesh.SetDestination(unit.transform.position);
        }
    }
    
    public override void OnStateExit() { }
}
