using UnityEngine;
using UnityEngine.EventSystems;

public class ContractItem : MonoBehaviour
{
    private enum Quarter { TopLeft, TopRight, BottomLeft, BottomRight }
    private uint _enterCount = 0;
    private RectTransform _rectTransform;
    private RectTransform _contractPanel;
    private TMPro.TextMeshProUGUI _nameText;
    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _contractPanel = transform.Find("Panel").GetComponent<RectTransform>();
        _nameText = _contractPanel.transform.Find("NameText").GetComponent<TMPro.TextMeshProUGUI>();

        UtilityHelpers.RegisterEvent(_rectTransform, EventTriggerType.PointerEnter, (data) => OnRectEnter(data));
        UtilityHelpers.RegisterEvent(_rectTransform, EventTriggerType.PointerExit, (data) => OnRectExit(data));
        UtilityHelpers.RegisterEvent(_contractPanel, EventTriggerType.PointerEnter, (data) => OnPanelEnter(data));
        UtilityHelpers.RegisterEvent(_contractPanel, EventTriggerType.PointerExit, (data) => OnPanelExit(data));

        _contractPanel.gameObject.SetActive(false);
    }

    public void SetContract(Contract contract)
    {
        _nameText.text = contract.Name;
    }

    public void SetPosition(Vector2 position)
    {
        _rectTransform.anchoredPosition = position;
        var quarter = position.x >= 0
            ? (position.y >= 0 ? Quarter.TopRight : Quarter.BottomRight)
            : (position.y >= 0 ? Quarter.TopLeft : Quarter.BottomLeft);
        SetPanelAnchor(quarter);
        Debug.Log($"ContractItem position set to: {position}, quarter: {quarter}");
    }

    private void SetPanelAnchor(Quarter quarter)
    {
        var anchor = quarter switch
        {
            Quarter.TopRight    => new Vector2(1f, 1f),
            Quarter.TopLeft     => new Vector2(0f, 1f),
            Quarter.BottomRight => new Vector2(1f, 0f),
            Quarter.BottomLeft  => new Vector2(0f, 0f),
            _                   => new Vector2(0f, 0f)
        };
        _contractPanel.pivot = anchor;
    }

    void OnRectEnter(BaseEventData data)
    {
        _enterCount++;
        _contractPanel.gameObject.SetActive(true);
    }
    void OnRectExit(BaseEventData data)
    {
        _enterCount--;
        if (_enterCount == 0)
            _contractPanel.gameObject.SetActive(false);
    }
    void OnPanelEnter(BaseEventData data)
    {
        _enterCount++;
        _contractPanel.gameObject.SetActive(true);
    }
    void OnPanelExit(BaseEventData data)
    {
        _enterCount--;
        if (_enterCount == 0)
            _contractPanel.gameObject.SetActive(false);
    }
}
