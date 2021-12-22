using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemie : Enemies
{
    public override void Start()
    {
        base.Start();
        
        actionsState.AddState(new TargetClosestPlayer(this), out targetingStateIndex);
        actionsState.AddState(new BaseAttack(this), out attackStateIndex);
        actionsState.AddState(new GoToTargetAndAttack(this, navMeshAgent), out movementStateIndex);
    }

    public override void Update()
    {
        base.Update();
    }
}
