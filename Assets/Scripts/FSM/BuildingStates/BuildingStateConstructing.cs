using UnityEngine;

public class BuildingStateConstructing : BuildingStateBase
{
    public BuildingStateConstructing(Building obj) : base(obj)
    {
        stateId = (int)BuildingStates.Constructing;
    }

    public override void Update()
    {
        base.Update();
        
        if (building.currentConstructionModel == 0 && building.currentProgressBarValue >= 20)
        {
            Utility.Instance.SentMessageToDeveloper("Building Progress Level 1 is success!");
            ChangeBuildingProgressState(1);
        }
        else if (building.currentConstructionModel == 1 && building.currentProgressBarValue >= 50)
        {
            Utility.Instance.SentMessageToDeveloper("Building Progress Level 2 is success!");
            ChangeBuildingProgressState(2);
        }
        else if (building.currentConstructionModel == 2 && building.currentProgressBarValue >= 100)
        {
            Utility.Instance.SentMessageToDeveloper("Building Progress Level 3 is success!");
            ChangeBuildingProgressState(3);
        }
        
        if (building.currentHealth < building.maxHealth)
        {
            building.ChangeState((int)BuildingTriggers.ToDamaging);
        }
        else if (building.currentProgressBarValue >= building.maxProgressBarValue)
        {
            building.ChangeState((int)BuildingTriggers.ToIdling);
        }
    }
    
    private void ChangeBuildingProgressState(int constructionModelNumber)
    {
        if (constructionModelNumber > building.constructionModels.Count)
            constructionModelNumber = building.constructionModels.Count - 1;

        building.constructionModels.ForEach(_ => _.gameObject.SetActive(false));
        var constructionModel = building.constructionModels[constructionModelNumber];
        constructionModel.gameObject.SetActive(true);
        building.GetComponent<Outliner>().renderersReferences = new []{ constructionModel.GetComponent<Renderer>() };
        building.GetComponent<Outliner>().meshFiltersReferences = new []{ constructionModel.GetComponent<MeshFilter>() };
        building.currentConstructionModel = constructionModelNumber;
    }
}