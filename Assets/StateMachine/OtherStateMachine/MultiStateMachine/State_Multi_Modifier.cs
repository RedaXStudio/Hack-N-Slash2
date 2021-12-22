using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Multi_Modifier : State_Multi
{
    public List<State_Multi> stateMultis = new List<State_Multi>();
    
    public State_Multi_Modifier(MultiStateMachine stateMachine) : base(stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    
    public State_Multi_Modifier(MultiStateMachine stateMachine, List<State_Multi> stateMultis) : base(stateMachine)
    {
        this.stateMachine = stateMachine;

        this.stateMultis = stateMultis;
    }

    public override void OnStateEnter()
    {
        foreach (var stateMulti in stateMultis)
        {
            stateMulti.OnStateEnter();
        }
    }

    public override void Tick()
    {
        for (int i = 0; i < stateMultis.Count; i++)
        {
            stateMultis[i].Tick();
        }
    }

    public override void OnStateExit()
    {
        foreach (var stateMulti in stateMultis)
        {
            stateMulti.OnStateExit();
        }
    }
    
    public void SetState(State_Multi newState, State_Multi definedState, out State_Multi outState)
    {
        if (definedState != null)
            definedState.OnStateExit();

        outState = newState;
        
        outState.OnStateEnter();
    }

    public void AddState(State_Multi newState)
    {
        int i = stateMultis.Count;

        stateMultis.Insert(i, newState);
        
        stateMultis[i].OnStateEnter();
    }
    
    public void AddState(State_Multi newState, out int index)
    {
        int i = stateMultis.Count;

        stateMultis.Insert(i, newState);
        
        stateMultis[i].OnStateEnter();

        index = i;
    }

    public void InsertStateList(State_Multi newState, int index)
    {
        stateMultis.Insert(index, newState);
    }

    public void SetStateAtIndex(State_Multi newState, int index)
    {
        if (stateMultis[index] != null)
        {
            stateMultis[index].OnStateExit();
        }
        
        stateMultis[index] = newState;
        
        stateMultis[index].OnStateEnter();
    }

    public void RemoveStateFromList(State_Multi stateToRemove)
    {
        stateMultis.Remove(stateMultis.Find(state => state.GetType() == stateToRemove.GetType()));
    }

    public void RemoveAllStateFromList(State_Multi statesToRemove)
    {
        foreach (var state in stateMultis.FindAll(state => state.GetType() == statesToRemove.GetType()))
        {
            stateMultis.Remove(state);
        }
    }
}
