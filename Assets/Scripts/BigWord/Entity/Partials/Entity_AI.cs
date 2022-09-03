using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Entity
{
    public FSM_StateMachine<int> stateMachine;

    public void AddStateMachine()
    {
        stateMachine = new FSM_StateMachine<int>();
    }

    public void ChangeState(int stateId)
    {
        stateMachine.ChangeState(stateId);
    }

    public FSM_Status<int> AddState(int stateId)
    {
        FSM_Status<int> state = new FSM_Status<int>();
        stateMachine.AddStatus(stateId, state);
        return state;
    }

    private void UpdateStateMachine()
    {
        if (stateMachine != null)
        {
            stateMachine.OnAction();
        }
    }
}
