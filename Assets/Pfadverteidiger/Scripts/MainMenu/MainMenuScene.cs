using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuScene : MonoBehaviour
{
    private enum SceneState
    {
        Startup,
        Career,
        Contract
    }

    private StartupCanvas _startupCanvas;
    private CareerCanvas _careerCanvas;
    private ContractCanvas _contractCanvas;
    public MainMenuCamera MainCamera;
    private SceneState _state = SceneState.Startup;

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

        _startupCanvas = GameObject.Find("StartupCanvas").GetComponent<StartupCanvas>();
        _careerCanvas = GameObject.Find("CareerCanvas").GetComponent<CareerCanvas>();
        _contractCanvas = GameObject.Find("ContractCanvas").GetComponent<ContractCanvas>();
        _startupCanvas.gameObject.SetActive(true);
        _careerCanvas.gameObject.SetActive(false);
        _contractCanvas.gameObject.SetActive(false);
        
        if (GameManager.IsCareerLoaded)
        {
            GoToCareer();
        }
    }

    public void GoToCareer()
    {
        if (_state == SceneState.Career) return;
        switch (_state)
        {
            case SceneState.Startup:
                MainCamera.StartupToCareer();
                break;
            case SceneState.Contract:
                MainCamera.ContractToCareer();
                break;
        }
        _state = SceneState.Career;
        _careerCanvas.gameObject.SetActive(true);
        _startupCanvas.gameObject.SetActive(false);
        _contractCanvas.gameObject.SetActive(false);
    }

    public void GoToStartup()
    {
        if (_state == SceneState.Startup) return;
        switch (_state)
        {
            case SceneState.Career:
                MainCamera.CareerToStartup();
                break;
        }
        _state = SceneState.Startup;
        _startupCanvas.gameObject.SetActive(true);
        _careerCanvas.gameObject.SetActive(false);
        _contractCanvas.gameObject.SetActive(false);
    }

    public void GoToContract()
    {
        if (_state == SceneState.Contract) return;
        switch (_state)
        {
            case SceneState.Career:
                MainCamera.CareerToContract();
                break;
        }
        _state = SceneState.Contract;
        _startupCanvas.gameObject.SetActive(false);
        _careerCanvas.gameObject.SetActive(false);
        _contractCanvas.gameObject.SetActive(true);
    }
}
