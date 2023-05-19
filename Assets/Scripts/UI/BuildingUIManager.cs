using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUIManager : Singleton<BuildingUIManager>
{
    [SerializeField] private Transform buildingMenu;
    [SerializeField] private Transform buildingMenuElementPrefab;
    private List<BuildingSO> buildingMainPageSOList;

    private void Awake()
    {
        LoadScriptableObjects();
    }
    
    private void LoadScriptableObjects()
    {
        buildingMainPageSOList = ((BuildingMainMenuSO)ScriptableObjectManager.Instance.LoadScriptableObjects("ScriptableObjects/Buildings/00_BuildingMainPage")).mainMenuBuildings;
        InitializeABuildingMenu(buildingMainPageSOList);
    }
    
    private void InitializeABuildingMenu(List<BuildingSO> buildingSOList)
    {
        for (int i = 0; i < buildingSOList.Count; i++)
        {
            var newBuildingMenuElement = Instantiate(buildingMenuElementPrefab, Vector3.zero, Quaternion.identity, buildingMenu);
            newBuildingMenuElement.GetChild(0).GetComponent<Image>().sprite = buildingSOList[i].sprite;
            
            var localIndex = i;
            newBuildingMenuElement.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (buildingSOList[localIndex].childObjects.Count > 0)
                {
                    ChangeBuildingMenuPage(buildingSOList[localIndex].childObjects);
                }
                else
                {
                    ChangeBuildingMenuPage(buildingMainPageSOList);
                    BuildingManager.Instance.CreateABuilding(buildingSOList[localIndex]);
                }
            });
        }
    }
    
    private void ChangeBuildingMenuPage(List<BuildingSO> buildingSOList)
    {
        var childCount = buildingMenu.childCount;
        for (var i = 0; i < childCount; i++)
                Destroy(buildingMenu.GetChild(i).gameObject);
            
        InitializeABuildingMenu(buildingSOList);
    }
}
