using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuScene : MonoBehaviour
{
    public GameObject StartupCanvas;
    public GameObject CareerCanvas;
    public MainMenuCamera MainCamera;

    public Button CareerMenu;
    public Button CareerNew;
    public Button CareerContinue;
    public RectTransform CareerPanel;
    public Button GoToStartup;
    void Awake()
    {
        if (MainCamera == null) MainCamera = GameObject.Find("MainCamera").GetComponent<MainMenuCamera>();

        var shipPrefab = Resources.Load<GameObject>(GameManager.Ship.PrefabPath);
        if (shipPrefab == null) {
            Debug.LogError($"Ship prefab not found at path: {GameManager.Ship.PrefabPath}");
        } else {
            var shipInstance = Instantiate(shipPrefab, this.transform);
            shipInstance.transform.localPosition = Vector3.zero;
            shipInstance.transform.localRotation = Quaternion.identity;    
        }

        if (StartupCanvas == null) StartupCanvas = GameObject.Find("StartupCanvas");
        if (CareerCanvas == null) CareerCanvas = GameObject.Find("CareerCanvas");
        if (CareerMenu == null) CareerMenu = GameObject.Find("CareerMenu").GetComponent<Button>();
        UtilityHelpers.RegisterEvent<Button>(CareerMenu, EventTriggerType.PointerEnter, (data) => OnCareerMenuHover(data));
        if (CareerNew == null) CareerNew = GameObject.Find("CareerNew").GetComponent<Button>();
        UtilityHelpers.RegisterEvent<Button>(CareerNew, EventTriggerType.PointerClick, (data) => OnCareerNewClicked(data));
        if (CareerContinue == null) CareerContinue = GameObject.Find("CareerContinue").GetComponent<Button>();
        UtilityHelpers.RegisterEvent<Button>(CareerContinue, EventTriggerType.PointerClick, (data) => OnCareerContinueClicked(data));
        if (CareerPanel == null) CareerPanel = GameObject.Find("CareerPanel").GetComponent<RectTransform>();
        UtilityHelpers.RegisterEvent<RectTransform>(CareerPanel, EventTriggerType.PointerExit, (data) => OnCareerPanelExit(data));
        if (GoToStartup == null) GoToStartup = GameObject.Find("GoToStartup").GetComponent<Button>();
        UtilityHelpers.RegisterEvent<Button>(GoToStartup, EventTriggerType.PointerClick, (data) => OnBackToStartupClicked(data));
        StartupCanvas.SetActive(true);
        CareerCanvas.SetActive(false);
        CareerPanel.gameObject.SetActive(false);
    }

    private void OnCareerMenuHover(BaseEventData data)
    {
        CareerPanel.gameObject.SetActive(true);
    }

    private void OnCareerPanelExit(BaseEventData data)
    {
        CareerPanel.gameObject.SetActive(false);
    }
    
    private void OnCareerNewClicked(BaseEventData data)
    {
        GameManager.StartNewCareer();
        GoToCareer();
    }

    private void OnCareerContinueClicked(BaseEventData data)
    {
        GameManager.LoadCareer();
        GoToCareer();
    }

    private void OnBackToStartupClicked(BaseEventData data)
    {
        MainCamera.GoToStartup();
        StartupCanvas.SetActive(true);
        CareerCanvas.SetActive(false);
    }

    private void GoToCareer()
    {
        MainCamera.GoToCareer();
        StartupCanvas.SetActive(false);
        CareerCanvas.SetActive(true);
        CareerPanel.gameObject.SetActive(false);
    }
}
