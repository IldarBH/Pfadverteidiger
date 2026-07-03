using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CareerCanvas : MonoBehaviour
{
    private Button _goToStartup;
    private MainMenuScene _mainMenuScene;

    void Awake()
    {
        _goToStartup = GameObject.Find("GoToStartup").GetComponent<Button>();
        UtilityHelpers.RegisterEvent<Button>(_goToStartup, EventTriggerType.PointerClick, (data) => OnGoToStartupClicked(data));

        _mainMenuScene = GameObject.Find("MainMenuScene").GetComponent<MainMenuScene>();
    }

    private void OnGoToStartupClicked(BaseEventData data)
    {
        _mainMenuScene.GoToStartup();
    }
}
