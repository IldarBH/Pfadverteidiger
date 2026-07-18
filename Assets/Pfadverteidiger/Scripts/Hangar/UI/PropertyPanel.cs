using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PropertyPanel : MonoBehaviour
{
    public string PropertyName;
    private TMPro.TextMeshProUGUI _nameText;
    private TMPro.TextMeshProUGUI _currentValueText;
    private TMPro.TextMeshProUGUI _maxValueText;
    private TMPro.TextMeshProUGUI _restoreCostText;
    private TMPro.TextMeshProUGUI _upgradeCostText;
    private Image _indicatorImage;
    private Button _restoreButton;
    private Button _upgradeButton;
    public Sprite[] IndicatorSprites;
    private SortedDictionary<float, int> _indicatorLevels;
    private ShipProperty _property;

    public void Initialize(ShipProperty property)
    {
        _property = property;

        _nameText = transform.Find("Name").GetComponent<TMPro.TextMeshProUGUI>();
        _currentValueText = transform.Find("CurrentValue").GetComponent<TMPro.TextMeshProUGUI>();
        _maxValueText = transform.Find("MaxValue").GetComponent<TMPro.TextMeshProUGUI>();
        _restoreButton = transform.Find("Restore").GetComponent<Button>();
        _restoreCostText = _restoreButton.transform.Find("Price").GetComponent<TMPro.TextMeshProUGUI>();
        _upgradeButton = transform.Find("Upgrade").GetComponent<Button>();
        _upgradeCostText = _upgradeButton.transform.Find("Price").GetComponent<TMPro.TextMeshProUGUI>();
        _indicatorImage = transform.Find("Indicator").GetComponent<Image>();
        
        _nameText.text = PropertyName;
        _indicatorLevels = new SortedDictionary<float, int>();
        for (int i = 0; i < IndicatorSprites.Length; i++) {
            _indicatorLevels[(float)i / (IndicatorSprites.Length - 1)] = i;
        }
        UtilityHelpers.RegisterEvent(_restoreButton, EventTriggerType.PointerClick, data => OnRestoreButtonClicked());
        UtilityHelpers.RegisterEvent(_upgradeButton, EventTriggerType.PointerClick, data => OnUpgradeButtonClicked());
        UpdateUI();
    }

    private void UpdateUI()
    {
        _currentValueText.text = _property.Value.ToString();
        _maxValueText.text = _property.MaxValue.ToString();
        
        var currentLevel = (float)_property.Value / _property.MaxValue;
        var upperBound = _indicatorLevels.Keys.FirstOrDefault(level => level >= currentLevel);
        _indicatorImage.sprite = IndicatorSprites[_indicatorLevels[upperBound]];
    }

    public void OnRestoreButtonClicked()
    {
        _property.RestoreValue();
        UpdateUI();
    }

    public void OnUpgradeButtonClicked()
    {
        _property.UpgradeLevel();
        UpdateUI();
    }
}
