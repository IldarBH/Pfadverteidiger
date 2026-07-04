using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CareerCanvas : MonoBehaviour
{
    private Button _contractMenu;
    private Button _goToStartup;
    private MainMenuScene _mainMenuScene;
    private RectTransform _careerPanel;

    void Awake()
    {
        _goToStartup = GameObject.Find("GoToStartup").GetComponent<Button>();
        UtilityHelpers.RegisterEvent<Button>(_goToStartup, EventTriggerType.PointerClick, (data) => OnGoToStartupClicked(data));
        
        _careerPanel = transform.Find("CareerPanel").GetComponent<RectTransform>();
        _contractMenu = _careerPanel.transform.Find("ContractMenu").GetComponent<Button>();
        UtilityHelpers.RegisterEvent<Button>(_contractMenu, EventTriggerType.PointerClick, (data) => OnContractMenuClicked(data));

        _mainMenuScene = GameObject.Find("MainMenuScene").GetComponent<MainMenuScene>();
    }

    private void OnGoToStartupClicked(BaseEventData data)
    {
        _mainMenuScene.GoToStartup();
    }

    private void OnContractMenuClicked(BaseEventData data)
    {
        _mainMenuScene.GoToContract();
    }
}
