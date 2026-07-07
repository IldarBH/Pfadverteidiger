using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContractItem : MonoBehaviour
{
    private enum HorizontalSide { Left, Right }
    private uint _enterCount = 0;
    private RectTransform _rectTransform;
    private RectTransform _contractPanel;
    private Button _accept;
    private TMPro.TextMeshProUGUI _nameText;
    private Contract _contract;
    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _contractPanel = transform.Find("Panel").GetComponent<RectTransform>();
        _nameText = _contractPanel.transform.Find("NameText").GetComponent<TMPro.TextMeshProUGUI>();
        _accept = _contractPanel.transform.Find("Accept").GetComponent<Button>();

        UtilityHelpers.RegisterEvent(_rectTransform, EventTriggerType.PointerEnter, (data) => OnRectEnter(data));
        UtilityHelpers.RegisterEvent(_rectTransform, EventTriggerType.PointerExit, (data) => OnRectExit(data));
        UtilityHelpers.RegisterEvent(_contractPanel, EventTriggerType.PointerEnter, (data) => OnPanelEnter(data));
        UtilityHelpers.RegisterEvent(_contractPanel, EventTriggerType.PointerExit, (data) => OnPanelExit(data));
        UtilityHelpers.RegisterEvent(_accept, EventTriggerType.PointerClick, (data) => OnAcceptClick(data));

        _contractPanel.gameObject.SetActive(false);
    }

    public void SetContract(Contract contract)
    {
        _contract = contract;
        _nameText.text = contract.Name;
    }

    public void SetPosition(Vector2 position)
    {
        _rectTransform.anchoredPosition = position;
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
        BattleManager.ActiveContract = _contract;
        SceneManager.LoadScene("Battle");
    }
}
