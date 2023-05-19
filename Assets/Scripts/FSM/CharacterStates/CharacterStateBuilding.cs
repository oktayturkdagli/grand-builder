using System.Collections;
using DG.Tweening;
using UnityEngine;

public class CharacterStateBuilding : CharacterStateBase
{
    // Building
    private IEnumerator buildingCoroutine;

    public CharacterStateBuilding(Character obj) : base(obj)
    {
        stateId = (int)CharacterStates.Building;
    }
    
    public override void Enter()
    {
        base.Enter();
        character.ResetAnimationStates();
        AnimationManager.Instance.SetBool(character.AnimatorReference, character.animIsBuildingHash, true);
        
        var lookAtPosition = character.destinationObject.transform.position;
        character.transform.DOLookAt(new Vector3(lookAtPosition.x, character.transform.position.y, lookAtPosition.z), 1f);
        buildingCoroutine = BuildingCoroutine(character.destinationObject.GetComponent<Building>());
        character.StartCoroutine(buildingCoroutine);
    }
    
    public override void Update()
    {
        base.Update();
    }
    
    public override void Exit()
    {
        base.Exit();

        // Stop running coroutines
        if (buildingCoroutine != null)
            character.StopCoroutine(buildingCoroutine);
    }
    
    private IEnumerator BuildingCoroutine(Building building)
    {
        if (building.currentHealth < building.maxHealth)
        {
            if (building.currentState.stateId == building.buildingStateDamaging.stateId)
            {
                building.ChangeState((int)BuildingTriggers.ToRepairing);
            }
        }
        
        bool isWorking = true;
        while (isWorking)
        {
            // yield on a new YieldInstruction that waits for 1 seconds.
            yield return new WaitForSeconds(1);
            if (building.currentState.stateId == building.buildingStateIdling.stateId)
            {
                character.ChangeState((int)CharacterTriggers.ToIdling);
            }
            else if (building.currentState.stateId == building.buildingStateRepairing.stateId)
            {
                building.UpdateHealthBar(1);
            }
            else if (building.currentState.stateId == building.buildingStateConstructing.stateId)
            {
                building.UpdateProgressBarValue(1);
            }
            else
            {
                isWorking = false;
            }
        }
    }
}