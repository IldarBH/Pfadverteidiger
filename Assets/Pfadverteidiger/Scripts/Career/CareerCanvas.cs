using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CareerCanvas : MonoBehaviour
{
    private CareerScene _careerScene;
    private Button _contractMenu;
    private Button _hangarMenu;
    private Button _goBack;
    private RectTransform _careerPanel;

    void Awake()
    {
        _careerPanel = transform.Find("CareerPanel").GetComponent<RectTransform>();
        _contractMenu = _careerPanel.transform.Find("ContractMenu").GetComponent<Button>();
        UtilityHelpers.RegisterEvent<Button>(_contractMenu, EventTriggerType.PointerClick, (data) => OnContractMenuClicked(data));
        _hangarMenu = _careerPanel.transform.Find("HangarMenu").GetComponent<Button>();
        UtilityHelpers.RegisterEvent<Button>(_hangarMenu, EventTriggerType.PointerClick, (data) => OnHangarMenuClicked(data));
        _goBack = transform.Find("Back").GetComponent<Button>();
        UtilityHelpers.RegisterEvent<Button>(_goBack, EventTriggerType.PointerClick, (data) => OnGoBackClicked(data));
    }

    public void Initialize(CareerScene careerScene)
    {
        _careerScene = careerScene;
    }

    private void OnGoBackClicked(BaseEventData data)
    {
        _careerScene.GoToMainMenu();
    }

    private void OnContractMenuClicked(BaseEventData data)
    {
        _careerScene.GoToContract();
    }

    private void OnHangarMenuClicked(BaseEventData data)
    {
        _careerScene.GoToHangar();
    }
}
