using UnityEngine;
using UnityEngine.AI;

public class Character : Element, ISelectable
{
    public CapsuleCollider colliderReference => GetComponent<CapsuleCollider>();
    public Animator AnimatorReference => GetComponent<Animator>();
    public NavMeshAgent NavMeshAgentReference => GetComponent<NavMeshAgent>();
    
    // Animation
    [HideInInspector] public string animIsRunningHash = "isRunning";
    [HideInInspector] public string animIsBuildingHash = "isBuilding";

    // Movement
    [HideInInspector] public bool isMoving = false;
    [HideInInspector] public Vector3 destinationPoint = Vector3.zero;
    [HideInInspector] public Element destinationObject = null;
    
    // Selection
    [field: SerializeField] public GameObject CircleBottom { get; set; }
    
    // States
    public readonly CharacterStateIdling characterStateIdling;
    public readonly CharacterStateMoving characterStateMoving;
    public readonly CharacterStateBuilding characterStateBuilding;
    public readonly CharacterStateCollecting characterStateCollecting;
    public readonly CharacterStateAttacking characterStateAttacking;
    public readonly CharacterStateDefencing characterStateDefencing;
    public readonly CharacterStateDestroying characterStateDestroying;
    
    public Character() 
    {
        characterStateIdling = new CharacterStateIdling(this);
        characterStateMoving = new CharacterStateMoving(this);
        characterStateBuilding = new CharacterStateBuilding(this);
        characterStateCollecting = new CharacterStateCollecting(this);
        characterStateAttacking = new CharacterStateAttacking(this);
        characterStateDefencing = new CharacterStateDefencing(this);
        characterStateDestroying = new CharacterStateDestroying(this);
        
        AddState(characterStateIdling, (int)CharacterTriggers.ToIdling);
        AddState(characterStateMoving, (int)CharacterTriggers.ToMoving);
        AddState(characterStateBuilding, (int)CharacterTriggers.ToBuilding);
        AddState(characterStateCollecting, (int)CharacterTriggers.ToCollecting);
        AddState(characterStateAttacking, (int)CharacterTriggers.ToAttacking);
        AddState(characterStateDefencing, (int)CharacterTriggers.ToDefencing);
        AddState(characterStateDestroying, (int)CharacterTriggers.ToDestroying);
        
        currentState = characterStateIdling;
    }

    public override void Start()
    {
        base.Start();
        ObjectType = AllObjects.Character;
    }

    public void ResetAnimationStates()
    {
        AnimationManager.Instance.SetBool(AnimatorReference, animIsRunningHash, false);
        AnimationManager.Instance.SetBool(AnimatorReference, animIsBuildingHash, false);
    }
    
    public void Move(Vector3 destinationPointValue, Element destinationObjectValue = null)
    {
        destinationPoint = destinationPointValue;
        destinationObject = destinationObjectValue;
        
        if (currentState == characterStateMoving)
        {
            characterStateMoving.Enter();
        }
        else
        {
            ChangeState((int)CharacterTriggers.ToMoving);
        }
    }

    // Selection
    public void OnHover()
    {
        
    }
    
    public void OnHoverEnd()
    {
        
    }
    
    public void OnSelect()
    {
        
    }
    
    public void OnDeSelect()
    {
        
    }
}