using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    public State curentState { get; private set; }
    public void Initialize(State startingState)
    {
        curentState = startingState;
        curentState.Enter();
    }
    public void ChangeState(State newState)
    {
        curentState.Exit();
        curentState = newState;
        curentState.Enter();
    }
}
