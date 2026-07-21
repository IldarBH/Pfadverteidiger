using System.Collections.Generic;
using UnityEngine;

public class ShipBase : MonoBehaviour
{
    private ShipData _shipData;
    [SerializeField] public List<GameObject> platformInstances = new List<GameObject>();
    
    public void Initialize(ShipData data)
    {
        _shipData = data;

        platformInstances = new List<GameObject>(GameObject.FindGameObjectsWithTag("TurretPlatform"));
        foreach (var platform in platformInstances)
        {
            if (_shipData.AddPlatform(platform.name))
            {
                Debug.Log($"Platform {platform.name} added to ship data.");
            }
            platform.SetActive(_shipData.PlatformIsActive(platform.name));
        }
    }

    public ShipData GetShipData()
    {
        return _shipData;
    }
}
