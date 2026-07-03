using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartupCanvas : MonoBehaviour
{
    private Button _careerMenu;
    private Button _careerNew;
    private Button _careerContinue;
    private RectTransform _careerPanel;
    private MainMenuScene _mainMenuScene;

    void Awake()
    {
        _careerMenu = GameObject.Find("CareerMenu").GetComponent<Button>();
        UtilityHelpers.RegisterEvent<Button>(_careerMenu, EventTriggerType.PointerEnter, (data) => OnCareerMenuHover(data));
        _careerNew = GameObject.Find("CareerNew").GetComponent<Button>();
        UtilityHelpers.RegisterEvent<Button>(_careerNew, EventTriggerType.PointerClick, (data) => OnCareerNewClicked(data));
        _careerContinue = GameObject.Find("CareerContinue").GetComponent<Button>();
        UtilityHelpers.RegisterEvent<Button>(_careerContinue, EventTriggerType.PointerClick, (data) => OnCareerContinueClicked(data));
        _careerPanel = GameObject.Find("CareerPanel").GetComponent<RectTransform>();
        UtilityHelpers.RegisterEvent<RectTransform>(_careerPanel, EventTriggerType.PointerExit, (data) => OnCareerPanelExit(data));
        
        _mainMenuScene = GameObject.Find("MainMenuScene").GetComponent<MainMenuScene>();
    }

    void OnEnable()
    {
        _careerPanel.gameObject.SetActive(false);
    }

    private void OnCareerMenuHover(BaseEventData data)
    {
        _careerPanel.gameObject.SetActive(true);
    }

    private void OnCareerPanelExit(BaseEventData data)
    {
        _careerPanel.gameObject.SetActive(false);
    }
    
    private void OnCareerNewClicked(BaseEventData data)
    {
        GameManager.StartNewCareer();
        _careerPanel.gameObject.SetActive(false);
        _mainMenuScene.GoToCareer();
    }

    private void OnCareerContinueClicked(BaseEventData data)
    {
        GameManager.LoadCareer();
        _careerPanel.gameObject.SetActive(false);
        _mainMenuScene.GoToCareer();
    }
}
