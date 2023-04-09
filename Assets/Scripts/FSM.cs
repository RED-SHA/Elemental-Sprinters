using System;
using System.Collections.Generic;

public class FSM
{
    private Dictionary<PlayerEnum, FSMState> states = new Dictionary<PlayerEnum, FSMState>();

    private FSMState currentState = null;

    public void AddState(PlayerEnum name, Action enterAction, Action exitAction, Action updateAction)
    {
        FSMState newState = new FSMState(name, enterAction, exitAction, updateAction);
        states.Add(name, newState);
    }

    public void SetInitialState(PlayerEnum name)
    {
        if (states.ContainsKey(name))
        {
            currentState = states[name];
            currentState.Enter();
        }
        else
        {
            throw new ArgumentException("Invalid state name: " + name);
        }
    }

    public void Update()
    {
        if (currentState != null && currentState.Update != null)
        {
            currentState.Update();
        }
    }

    public void ChangeState(PlayerEnum name)
    {
        if (currentState != null && states.ContainsKey(name))
        {
            currentState.Exit();
            currentState = states[name];
            currentState.Enter();
            UnitController.crnEnum = name;
        }
        else
        {
            throw new ArgumentException("Invalid state name: " + name);
        }
    }

    public PlayerEnum GetCurrentState()
    {
        return currentState.Name;
    }
}

// A single state in the FSM:
public class FSMState
{
    // Name of the state:
    public PlayerEnum Name { get; private set; }

    // Action to perform when entering the state:
    public Action Enter { get; private set; }

    // Action to perform when exiting the state:
    public Action Exit { get; private set; }

    // Action to perform each frame while in the state:
    public Action Update { get; private set; }

    // Constructor:
    public FSMState(PlayerEnum name, Action enterAction, Action exitAction, Action updateAction)
    {
        Name = name;
        Enter = enterAction;
        Exit = exitAction;
        Update = updateAction;
    }
}
