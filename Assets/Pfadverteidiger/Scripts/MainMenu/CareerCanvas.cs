using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CareerCanvas : MonoBehaviour
{
    private Button _contractMenu;
    private Button _goBack;
    private MainMenuScene _mainMenuScene;
    private RectTransform _careerPanel;

    void Awake()
    {
        _mainMenuScene = GameObject.Find("MainMenuScene").GetComponent<MainMenuScene>();

        _goBack = transform.Find("Back").GetComponent<Button>();
        UtilityHelpers.RegisterEvent<Button>(_goBack, EventTriggerType.PointerClick, (data) => OnGoBackClicked(data));
        
        _careerPanel = transform.Find("CareerPanel").GetComponent<RectTransform>();
        _contractMenu = _careerPanel.transform.Find("ContractMenu").GetComponent<Button>();
        UtilityHelpers.RegisterEvent<Button>(_contractMenu, EventTriggerType.PointerClick, (data) => OnContractMenuClicked(data));
    }

    private void OnGoBackClicked(BaseEventData data)
    {
        _mainMenuScene.GoToStartup();
    }

    private void OnContractMenuClicked(BaseEventData data)
    {
        _mainMenuScene.GoToContract();
    }
}
