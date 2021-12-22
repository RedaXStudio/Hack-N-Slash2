using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierMachine : StateMachine
{
    //not finished
    public List<State> modifiers = new List<State>();
    
    new void Start()
    {
        
    }
    
    new void Update()
    {
        currentState.Tick();

        foreach (var mod in modifiers)
        {
            mod.Tick();
        }

        for (int i = modifiers.Count - 1; i >= 0; i--)
        {
            modifiers[i].Tick();
        }
    }

    public void SetDebuff(State debuff)
    {
        debuff.OnStateEnter();
        
        modifiers.Add(debuff);
    }

    public void RemoveDebuff(State debuff)
    {
        debuff.OnStateExit();

        for (int i = modifiers.Count - 1; i >= 0; i--)
        {
            if (modifiers[i] == debuff)
            {
                modifiers.RemoveAt(i);
            }
        }
    }
}