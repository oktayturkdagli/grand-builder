public class CharacterStateIdling : CharacterStateBase
{
    public CharacterStateIdling(Character obj) : base(obj)
    {
        stateId = (int)CharacterStates.Idling;
    }
    
    public override void Enter()
    {
        base.Enter();
        character.ResetAnimationStates();
    }
    
    public override void Update()
    {
        base.Update();
    }
    
    public override void Exit()
    {
        base.Exit();
    }
}