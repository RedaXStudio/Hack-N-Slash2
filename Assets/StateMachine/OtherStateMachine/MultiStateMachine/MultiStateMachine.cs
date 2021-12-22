using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiStateMachine : MonoBehaviour
{
    public List<State_Multi> stateMultis = new List<State_Multi>();

    protected bool statesTicking;
    
    private string startName;

    public void Awake()
    {
        startName = gameObject.name;
    }
    
    public virtual void Start()
    {
        
    }

    public virtual void Update()
    {
        if (statesTicking)
        {
            foreach (var stateMulti in stateMultis)
            {
                stateMulti.Tick();
            }
        }
    }

    public GameObject FuncInstantiate(GameObject go, Vector2 pos, Transform t)
    {
        return Instantiate(go, pos, t.rotation);
    }
    
    public GameObject FuncInstantiate(GameObject go, Vector3 pos, Transform t)
    {
        return Instantiate(go, pos, t.rotation);
    }

    public void DestroyObject(GameObject go)
    {
        Destroy(go);
    }
    
    public void SetState(State_Multi newState, State_Multi definedState, out State_Multi outState)
    {
        if (definedState != null)
            definedState.OnStateExit();

        outState = newState;

        if (definedState != null)
        {
            gameObject.name = startName + " - " + newState.GetType().Name;
        }
        
        outState.OnStateEnter();
    }

    public void SetStateList(State_Multi newState)
    {
        int i = stateMultis.Count;

        stateMultis.Insert(i, newState);

        stateMultis[i].OnStateEnter();
        
        statesTicking = true;
    }
    
    public void SetStateList(State_Multi newState, out int indexTracking)
    {
        int i = stateMultis.Count;
        
        indexTracking = i;

        stateMultis.Insert(i, newState);

        stateMultis[i].OnStateEnter();
        
        statesTicking = true;
    }

    public void RemoveStateFromList(State_Multi stateToRemove)
    {
        stateMultis.Remove(stateMultis.Find(state => state.GetType() == stateToRemove.GetType()));

        if (stateMultis.Count <= 0)
        {
            statesTicking = false;
        }
    }
}
