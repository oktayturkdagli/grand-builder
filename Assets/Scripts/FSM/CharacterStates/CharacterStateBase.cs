public abstract class CharacterStateBase : State
{
    public readonly Character character;

    protected CharacterStateBase(Character obj) : base()
    {
        character = obj as Character;
    }
    
    public override void Enter()
    {
        base.Enter();
        // Utility.Instance.SentMessageToDeveloper("Entered State: " + (CharacterStates)stateId);
    }
    
    public override void Update()
    {
        base.Update();
        // Utility.Instance.SentMessageToDeveloper("Update State: " + (CharacterStates)stateId);
    }
    
    public override void Exit()
    {
        base.Exit();
        // Utility.Instance.SentMessageToDeveloper("Exited State: " + (CharacterStates)stateId);
    }
}

public enum CharacterStates
{
    Idling,
    Moving,
    Building,
    Collecting,
    Defencing,
    Attacking,
    Destroying
}

public enum CharacterTriggers
{
    ToIdling,
    ToMoving,
    ToBuilding,
    ToCollecting,
    ToAttacking,
    ToDefencing,
    ToDestroying
}