using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuScene : MonoBehaviour
{
    private StartupCanvas _startupCanvas;
    private CareerCanvas _careerCanvas;
    public MainMenuCamera MainCamera;

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
    }

    void OnEnable()
    {
        _startupCanvas.gameObject.SetActive(true);
        _careerCanvas.gameObject.SetActive(false);
    }

    public void GoToCareer()
    {
        MainCamera.GoToCareer();
        _startupCanvas.gameObject.SetActive(false);
        _careerCanvas.gameObject.SetActive(true);
        
    }

    public void GoToStartup()
    {
        MainCamera.GoToStartup();
        _startupCanvas.gameObject.SetActive(true);
        _careerCanvas.gameObject.SetActive(false);
    }
}
