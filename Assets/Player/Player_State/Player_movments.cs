using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movments : State_Multi
{
    public Player player;

    public Vector3 targetPos;
    public Vector3 targetRot;

    public Quaternion playerRot;

    public Player_movments(Player stateMachine) : base (stateMachine)
    {
        player = stateMachine;
    }

    public override void OnStateEnter()
    {
        //Debug.Log("Player Movment");
    }

    public override void Tick()
    {
        if (Input.GetButton("Fire2") && !Gears.gears.managerMain.canvasMain.IsMouseOverUiIgnore() && 
            Gears.gears.managerMain.playerActionManager.currentState == null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, 1000, Gears.gears.SelectableLayer))
            {
                
            }
            else
            {
                if (Physics.Raycast(ray, out hit, 1000, Gears.gears.groundLayer))
                {
                    ((UnitTargeting) player.actionsState.stateMultis[player.targetingStateIndex]).target = null;
                    player.navMeshAgent.destination = hit.point;
                }
            }
        }
        
        if (player.navMeshAgent.remainingDistance <= player.navMeshAgent.stoppingDistance)
        {
            player.moving = false;
        }
        else player.moving = true;

        //SetTargetPos();

        /*if (player.moving)
        {
            Move();
        }*/

        GameObject target = ((UnitTargeting) player.actionsState.stateMultis[player.targetingStateIndex]).target;
        
        if (target)
        {
            player.navMeshAgent.SetDestination(target.transform.position);
            
            if (Vector3.Distance(player.navMeshAgent.transform.position, target.transform.position) <
                ((AttacksBeh_MultiState) player.actionsState.stateMultis[player.attackStateIndex]).range)
            {
                ((AttacksBeh_MultiState) player.actionsState.stateMultis[player.attackStateIndex]).AttackBeh();
                ((AttacksBeh_MultiState) player.actionsState.stateMultis[player.attackStateIndex]).AttackBeh(target.GetComponent<Units>());
                
                player.navMeshAgent.isStopped = true;
            }
        }
        else if (player.navMeshAgent.isStopped)
        {
            player.navMeshAgent.isStopped = false;
        }
    }

    public override void OnStateExit()
    {
        
    }

    #region Old(Without NavMesh)
    
    public void SetTargetPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 1000, Gears.gears.groundLayer))
        {
            targetPos = hit.point;
            targetRot = new Vector3(targetPos.x - player.transform.position.x, player.transform.position.y, targetPos.z - player.transform.position.z);


            //player.transform.LookAt(targetRot);
            playerRot = new Quaternion(playerRot.x, Quaternion.LookRotation(targetRot).y, playerRot.z, Quaternion.LookRotation(targetRot).w);

            player.moving = true;
        }
    }

    public void Move()
    {
        //player.transform.rotation = Quaternion.Slerp(player.transform.rotation, playerRot, player.rotSpeed * Time.deltaTime);

        player.transform.position = Vector3.MoveTowards(player.transform.position, targetPos, player.finalMovementSpeed * Time.deltaTime);
        
        //player.rigi.MovePosition((targetPos - player.transform.position).normalized * player.movmentSpeed * Time.deltaTime);

        if (Vector3.Distance(player.transform.position, targetPos) < 1f)
        {
            player.moving = false;
        }
    }
    #endregion
}
