using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class BuildPanel : MonoBehaviour
{
    private Button _buildButton;
    private Button _acceptButton;
    private Button _cancelButton;
    private RectTransform _turretsPanel;
    private List<TurretPlatformData> _platformData = new List<TurretPlatformData>();
    private List<GameObject> _platformInstances = new List<GameObject>();
    private List<PlatformPanel> _platformPanelsInstances = new List<PlatformPanel>();
    public GameObject _platformPanelPrefab;

    public void Initialize(List<TurretPlatformData> platformData, List<GameObject> platformInstances)
    {
        _platformData = platformData;
        _platformInstances = platformInstances;

        _buildButton = transform.Find("Build").GetComponent<Button>();
        _acceptButton = transform.Find("Accept").GetComponent<Button>();
        _cancelButton = transform.Find("Cancel").GetComponent<Button>();
        _turretsPanel = transform.Find("TurretsPanel").GetComponent<RectTransform>();

        UtilityHelpers.RegisterEvent(_buildButton, EventTriggerType.PointerClick, (data) => OnBuildClicked(data));
        UtilityHelpers.RegisterEvent(_acceptButton, EventTriggerType.PointerClick, (data) => OnBuildAcceptClicked(data));
        UtilityHelpers.RegisterEvent(_cancelButton, EventTriggerType.PointerClick, (data) => OnBuildCancelClicked(data));

        _buildButton.gameObject.SetActive(true);
        _acceptButton.gameObject.SetActive(false);
        _cancelButton.gameObject.SetActive(false);
        _turretsPanel.gameObject.SetActive(false);
    }

    private void OnBuildClicked(BaseEventData data)
    {
        CreateTurretPanels();
        _buildButton.gameObject.SetActive(false);
        _acceptButton.gameObject.SetActive(true);
        _cancelButton.gameObject.SetActive(true);
        _turretsPanel.gameObject.SetActive(true);
    }

    private void OnBuildAcceptClicked(BaseEventData data)
    {
        DestroyTurretPanels();
        _buildButton.gameObject.SetActive(true);
        _acceptButton.gameObject.SetActive(false);
        _cancelButton.gameObject.SetActive(false);
        _turretsPanel.gameObject.SetActive(false);
    }

    private void OnBuildCancelClicked(BaseEventData data)
    {
        DestroyTurretPanels();
        _buildButton.gameObject.SetActive(true);
        _acceptButton.gameObject.SetActive(false);
        _cancelButton.gameObject.SetActive(false);
        _turretsPanel.gameObject.SetActive(false);
    }

    private void CreateTurretPanels()
    {
        if (_platformData.Count != _platformInstances.Count)
        {
            Debug.LogError("Mismatch between platform data and platform instances count.");
            return;
        }
        
        for (int i = 0; i < _platformData.Count; i++)
        {
            var platformData = _platformData[i];
            var platformInstance = _platformInstances[i];
            if (!platformData.IsActive)
            {
                Debug.Log($"Creating panel for {platformData.Name}, {platformInstance.name}");
                var platformPanel = Instantiate(_platformPanelPrefab, transform).GetComponent<PlatformPanel>();
                platformPanel.Initialize(platformInstance.transform);
                _platformPanelsInstances.Add(platformPanel);
            }
        }
    }

    private void DestroyTurretPanels()
    {
        foreach (var platformPanel in _platformPanelsInstances)
        {
            Destroy(platformPanel.gameObject);
        }
        _platformPanelsInstances.Clear();
    }
}
