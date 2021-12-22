using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State currentState;

    private string startName;

    public virtual void Awake()
    {
        startName = gameObject.name;
    }
    
    public virtual void Start()
    {
        
    }

    public virtual void Update()
    {
        currentState?.Tick();
    }

    public void SetState(State state)
    {
        if (currentState != null)
            currentState.OnStateExit();

        currentState = state;
        if (currentState != null)
        {
            gameObject.name = startName + " - " + state.GetType().Name;
        }

        if (currentState != null)
            currentState.OnStateEnter();
    }

    public GameObject FuncInstantiate(GameObject go, Vector2 pos, Transform t)
    {
        return Instantiate(go, pos, t.rotation);
    }
    
    public GameObject FuncInstantiate(GameObject go, Vector3 pos, Transform t)
    {
        return Instantiate(go, pos, t.rotation);
    }
    
    public GameObject FuncInstantiate(GameObject go, Transform parent)
    {
        return Instantiate(go, parent);
    }

    public void DestroyObject(GameObject go)
    {
        Destroy(go);
    }
}
