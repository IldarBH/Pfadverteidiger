using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class HangarCanvas : MonoBehaviour
{
    private HangarScene _hangarScene;
    private Button _buildPlatform;
    private Button _buildAccept;
    private Button _buildCancel;
    private BuildPlatformPanel _updateTowerPanelPrefab;
    [SerializeField] public List<RectTransform> towerPanelInstances;

    public void Initialize(HangarScene hangarScene)
    {
        _hangarScene = hangarScene;
        _updateTowerPanelPrefab = Resources.Load<BuildPlatformPanel>("Prefabs/Hangar/BuildPlatformPanel");

        _buildPlatform = transform.Find("BuildPlatform").GetComponent<Button>();
        UtilityHelpers.RegisterEvent<Button>(_buildPlatform, EventTriggerType.PointerClick, (data) => OnBuildPlatformClicked(data));
        _buildAccept = transform.Find("BuildAccept").GetComponent<Button>();
        UtilityHelpers.RegisterEvent<Button>(_buildAccept, EventTriggerType.PointerClick, (data) => OnBuildAcceptClicked(data));
        _buildCancel = transform.Find("BuildCancel").GetComponent<Button>();
        UtilityHelpers.RegisterEvent<Button>(_buildCancel, EventTriggerType.PointerClick, (data) => OnBuildCancelClicked(data));

        _buildPlatform.gameObject.SetActive(true);
        _buildAccept.gameObject.SetActive(false);
        _buildCancel.gameObject.SetActive(false);
    }

    private void OnBuildPlatformClicked(BaseEventData data)
    {
        _hangarScene.GoToBuildPlatform();
        _buildPlatform.gameObject.SetActive(false);
        _buildAccept.gameObject.SetActive(true);
        _buildCancel.gameObject.SetActive(true);
        CreateTowerPanels();
    }

    private void OnBuildAcceptClicked(BaseEventData data)
    {
        DestroyTowerPanels();
        _buildPlatform.gameObject.SetActive(true);
        _buildAccept.gameObject.SetActive(false);
        _buildCancel.gameObject.SetActive(false);
        _hangarScene.GoToAcceptBuildPlatform();
    }

    private void OnBuildCancelClicked(BaseEventData data)
    {
        DestroyTowerPanels();
        _buildPlatform.gameObject.SetActive(true);
        _buildAccept.gameObject.SetActive(false);
        _buildCancel.gameObject.SetActive(false);
        _hangarScene.GoToCancelBuildPlatform();
    }

    private void CreateTowerPanels()
    {
        var shipInstance = _hangarScene.GetShipInstance();
        for (int id = 0; id < shipInstance.turretTowers.Count; id++)
        {
            var tower = shipInstance.turretTowers[id];
            var platform = shipInstance.turretPlatforms[id];

            var towerData = _hangarScene.GetCurrentShipData().GetTower(tower.name);
            if (towerData.platform.IsActive)
            {
                
            } else {
                var towerPanel = Instantiate<BuildPlatformPanel>(_updateTowerPanelPrefab, transform);
                towerPanel.Initialize(_hangarScene, tower);
                var towerPanelRect = towerPanel.GetComponent<RectTransform>();
                towerPanelInstances.Add(towerPanelRect);
            }
        }
    }

    private void DestroyTowerPanels()
    {
        foreach (var towerPanel in towerPanelInstances)
        {
            Destroy(towerPanel.gameObject);
        }
        towerPanelInstances.Clear();
    }
}
