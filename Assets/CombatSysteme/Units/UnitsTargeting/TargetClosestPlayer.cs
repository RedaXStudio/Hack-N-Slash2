using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetClosestPlayer : UnitTargeting
{
    public TargetClosestPlayer(Enemies stateMachine) : base(stateMachine)
    {
        unit = stateMachine;
    }
    
    public override void OnStateEnter() { }

    public override void Tick()
    {
        Collider[] playersInRange = Physics.OverlapSphere(unit.transform.position, ((Enemies) unit).aggroRange, Gears.gears.playerLayer);
        
        //Debug.Log(playersInRange.Length);

        if (playersInRange.Length != 0)
        {
            float maxDistance = Mathf.Infinity;

            foreach (var player in playersInRange)
            {
                float d = Vector2.Distance(unit.transform.position, player.transform.position);
                
                if (d < maxDistance)
                {
                    target = player.gameObject;
                    maxDistance = d;
                }
            }
        }
        else
        {
            target = null;
        }
    }
    
    public override void OnStateExit() { }
}
