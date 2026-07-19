using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class HangarCanvas : MonoBehaviour
{
    private HangarScene _hangarScene;
    private PropertyPanel _hullPanel;
    private PropertyPanel _armorPanel;
    private BuildPanel _buildPanel;
    private Button _exitButton;
    private RectTransform _playerPanel;
    private RectTransform _shipPanel;

    public void Initialize(HangarScene hangarScene)
    {
        _hangarScene = hangarScene;
        InitializeHullPanel();
        InitializeArmorPanel();
        InitializeBuildPanel();
    }

    private void InitializeHullPanel()
    {
        _hullPanel = transform.Find("Hull").GetComponent<PropertyPanel>();
        _hullPanel.Initialize(GameManager.GetShipData().Health);
    }

    private void InitializeArmorPanel()
    {
        _armorPanel = transform.Find("Armor").GetComponent<PropertyPanel>();
        _armorPanel.Initialize(GameManager.GetShipData().Armor);
    }

    private void InitializeBuildPanel()
    {
        _buildPanel = transform.Find("BuildPanel").GetComponent<BuildPanel>();
        var platformsInstances = _hangarScene.GetShipInstance().platformInstances;
        var platforms =_hangarScene.GetShipInstance().GetShipData().Platforms;
        _buildPanel.Initialize(platforms, platformsInstances);
    }
}
