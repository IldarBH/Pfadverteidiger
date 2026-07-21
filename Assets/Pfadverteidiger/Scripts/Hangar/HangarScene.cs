using UnityEngine;

public class HangarScene : MonoBehaviour
{
    public enum HangarState
    {
        Idle,
        BuildPlatform,
    }
    private HangarState _hangarState = HangarState.Idle;
    private HangarCanvas _hangarCanvas;
    private ShipData _currentShipData;
    private ShipData _upgradeShipData;
    private ShipBase _shipInstance;
    private HangarCameraControl _hangarCamera;
    
    private void Awake()
    {
        if (!GameManager.IsCareerLoaded)
        {
            Debug.LogWarning("No career loaded. Starting a new career.");
            GameManager.StartNewCareer();
        }
        _currentShipData = new ShipData(GameManager.GetShipData());
        var shipPrefab = Resources.Load<ShipBase>(_currentShipData.GetPrefabPath());
        _shipInstance = Instantiate<ShipBase>(shipPrefab, Vector3.zero, Quaternion.identity);
        _shipInstance.Initialize(_currentShipData);

        _hangarCanvas = GameObject.Find("HangarCanvas").GetComponent<HangarCanvas>();
        _hangarCanvas.Initialize(this);

        _hangarCamera = GameObject.Find("HangarCamera").GetComponent<HangarCameraControl>();
        _hangarCamera.Initialize(this);
    }

    public HangarState GetHangarState()
    {
        return _hangarState;
    }

    public ShipBase GetShipInstance()
    {
        return _shipInstance;
    }

    public ShipData GetCurrentShipData()
    {
        return _currentShipData;
    }

    public ShipData GetUpgradeShipData()
    {
        return _upgradeShipData;
    }

    public void GoToBuildPlatform()
    {
        _upgradeShipData = new ShipData(_currentShipData);
        _hangarState = HangarState.BuildPlatform;
    }

    public void GoToAcceptBuildPlatform()
    {
        _hangarState = HangarState.Idle;
        _currentShipData = new ShipData(_upgradeShipData);
        GameManager.UpdateShipData(_currentShipData);
    }
}
