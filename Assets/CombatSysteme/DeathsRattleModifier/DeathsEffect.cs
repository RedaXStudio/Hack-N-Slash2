using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathsEffect
{
    public Units unit;

    public DeathsEffect(Units unit)
    {
        this.unit = unit;
    }
    public virtual void DeathRattle(Units killer)
    {
        Debug.Log("Base Death Rattle");
    }
    
    public virtual void DeathRattle()
    {
        Debug.Log("Base Death Rattle");
    }
}
