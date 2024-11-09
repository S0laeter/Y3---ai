using UnityEngine;

public class StateMachine : MonoBehaviour
{
    //public string customName;
    private State mainStateType;

    public State currentState { get; private set; }
    private State nextState;

    // Update is called once per frame
    void Update()
    {
        if (nextState != null)
        {
            SetState(nextState);
            nextState = null;
        }

        if (currentState != null)
        {
            currentState.OnUpdate();
        }
    }

    //change state locally
    private void SetState(State _newState)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }

        currentState = _newState;
        currentState.OnEnter(this);
    }

    //change the next state from other scripts
    public void SetNextState(State _newState)
    {
        if (_newState != null)
        {
            nextState = _newState;
        }
    }

    private void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.OnFixedUpdate();
        }
    }
    private void LateUpdate()
    {
        if (currentState != null)
        {
            currentState.OnLateUpdate();
        }
    }

    //reset state
    private void Awake()
    {
        SetNextStateToMain();
    }
    public void SetNextStateToMain()
    {
        nextState = mainStateType;
    }
    
    private void OnValidate()
    {
        if (mainStateType == null)
        {
            //mainStateType = new CombatIdleState();
        }
    }

}
