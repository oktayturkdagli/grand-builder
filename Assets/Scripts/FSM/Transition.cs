public class Transition
{
    public State targetState;
    public int trigger;
    
    public Transition(State targetState, int trigger)
    {
        this.targetState = targetState;
        this.trigger = trigger;
    }
}