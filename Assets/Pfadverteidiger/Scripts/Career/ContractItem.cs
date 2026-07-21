using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContractItem : MonoBehaviour
{
    private enum HorizontalSide { Left, Right }
    private uint _enterCount = 0;
    private RectTransform _contractMarker;
    private RectTransform _contractPanel;
    private Button _accept;
    private TMPro.TextMeshProUGUI _nameText;
    private ContractData _contract;
    void Awake()
    {
        _contractMarker = transform.Find("Marker").GetComponent<RectTransform>();
        UtilityHelpers.RegisterEvent(_contractMarker, EventTriggerType.PointerEnter, (data) => OnRectEnter(data));
        UtilityHelpers.RegisterEvent(_contractMarker, EventTriggerType.PointerExit, (data) => OnRectExit(data));

        _contractPanel = transform.Find("Panel").GetComponent<RectTransform>();
        UtilityHelpers.RegisterEvent(_contractPanel, EventTriggerType.PointerEnter, (data) => OnPanelEnter(data));
        UtilityHelpers.RegisterEvent(_contractPanel, EventTriggerType.PointerExit, (data) => OnPanelExit(data));

        _accept = _contractPanel.transform.Find("Accept").GetComponent<Button>();
        UtilityHelpers.RegisterEvent(_accept, EventTriggerType.PointerClick, (data) => OnAcceptClick(data));

        _nameText = _contractPanel.transform.Find("NameText").GetComponent<TMPro.TextMeshProUGUI>();
        _contractPanel.gameObject.SetActive(false);
    }

    public void Initialize(ContractData contract, Vector2 position)
    {
        _contract = contract;
        _nameText.text = contract.Name;
        
        _contractMarker.anchoredPosition = position;
        var side = position.x >= 0 ? HorizontalSide.Right : HorizontalSide.Left;
        SetPanelAnchor(side);
    }

    private void SetPanelAnchor(HorizontalSide side)
    {
        var anchor = side switch
        {
            HorizontalSide.Right => new Vector2(1f, 1f),
            HorizontalSide.Left  => new Vector2(0f, 1f),
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

    void OnAcceptClick(BaseEventData data)
    {
        // SceneManager.LoadScene("Battle");
        // TODO: Implement contract acceptance logic
    }
}
