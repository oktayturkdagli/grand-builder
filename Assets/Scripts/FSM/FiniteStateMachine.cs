using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine<T> : MonoBehaviour where T : MonoBehaviour
{
    public readonly Dictionary<int, State> states = new Dictionary<int, State>();
    public readonly Dictionary<int, Transition> transitions = new Dictionary<int, Transition>();
    public State rootState = null;
    public State currentState = null;
    
    public virtual void Update()
    {
        currentState?.Update();
    }
    
    public void AddState(State state, int trigger)
    {
        if (states.Count == 0)
            rootState = state;
        
        if (states.ContainsKey(state.stateId))
        {
            Utility.Instance.SentMessageToDeveloper("This state is already added: " + state.stateId);
            return;
        }
        
        if (transitions.ContainsKey(trigger))
        {
            Utility.Instance.SentMessageToDeveloper("This transition is already added for another states: " + trigger +  " --> " + state);
            return;
        }
        
        states.Add(state.stateId, state);
        transitions.Add(trigger, new Transition(state, trigger));
    }

    public void ChangeState(int trigger)
    {
        Transition transition = transitions[trigger];
        if (transition == null)
        {
            Utility.Instance.SentMessageToDeveloper("There is no such transition.");
            return;
        }
        currentState?.Exit(); // If it is not null, exit
        currentState = transition.targetState;
        currentState?.Enter();
    }
}
