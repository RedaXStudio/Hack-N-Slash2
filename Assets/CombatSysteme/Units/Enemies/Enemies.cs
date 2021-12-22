using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemies : Units
{
    public NavMeshAgent navMeshAgent;
    
    public float aggroRange;

    public int chanceToNotLoot;

    public int xpValue;

    public override void Start()
    {
        base.Start();
        
        baseStats.DeathsEffects.Add(new LootOnDeath(this, chanceToNotLoot));
        baseStats.DeathsEffects.Add(new GiveXpOnDeath(this));
        
        //Moving enemies
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        UpdateAll();
    }

    /*void Update()
    {
        
    }*/

    #region Updates

    public override void UpdateMovementSpeed()
    {
        base.UpdateMovementSpeed();
        navMeshAgent.speed = finalMovementSpeed;
    }

    #endregion
}
