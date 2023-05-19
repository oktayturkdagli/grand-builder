using UnityEngine.AI;

public class CharacterStateMoving : CharacterStateBase
{
    public CharacterStateMoving(Character obj) : base(obj)
    {
        stateId = (int)CharacterStates.Moving;
    }
    
    public override void Enter()
    {
        base.Enter();
        
        // Open require components
        character.isMoving = true;
        character.NavMeshAgentReference.enabled = true;

        // Play animation
        character.ResetAnimationStates();
        AnimationManager.Instance.SetBool(character.AnimatorReference, character.animIsRunningHash, true);
        
        // Set Destination
        character.NavMeshAgentReference.SetDestination(character.destinationPoint);
    }
    
    public override void Update()
    {
        base.Update();
        if (IsReachDestination(character.NavMeshAgentReference))
        {
            if (!character.destinationObject)
            {
                character.ChangeState((int)CharacterTriggers.ToIdling);
            }
            else if (character.destinationObject.GetType() == typeof(Building))
            {
                character.ChangeState((int)CharacterTriggers.ToBuilding);
            }
            else
            {
                character.ChangeState((int)CharacterTriggers.ToIdling);
            }
        }
    }
    
    public override void Exit()
    {
        base.Exit();
        character.isMoving = false;
        character.NavMeshAgentReference.enabled = false;
    }
    
    private bool IsReachDestination(NavMeshAgent dataNavMeshAgent)
    {
        // Check if we've reached the destination
        if (!dataNavMeshAgent.pathPending)
        {
            if (dataNavMeshAgent.remainingDistance <= dataNavMeshAgent.stoppingDistance)
            {
                if (!dataNavMeshAgent.hasPath || dataNavMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        
        return false;
    }
}