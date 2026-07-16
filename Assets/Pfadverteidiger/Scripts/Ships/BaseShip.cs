using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BaseShip : MonoBehaviour
{
    private ShipData _shipData;
    [SerializeField] public List<GameObject> turretPlatforms = new List<GameObject>();
    [SerializeField] public List<GameObject> turretTowers = new List<GameObject>();
    
    public void Initialize(ShipData data)
    {
        _shipData = data;
        for (int i = 0; i < _shipData.Towers.Count; i++)
        {
            var tower = _shipData.Towers[i];
        }
        InitializeTowers();
    }

    private void InitializeTowers()
    {
        turretTowers.Clear();
        turretPlatforms.Clear();

        turretTowers = new List<GameObject>();
        foreach (Transform child in gameObject.transform)
        {
            if (child.CompareTag("TurretTower"))
            {
                turretTowers.Add(child.gameObject);
            }
        }

        foreach (var tower in turretTowers)
        {
            Assert.IsTrue(tower.transform.childCount > 0, $"{tower.name} has no child objects.");
            var platform = tower.transform.GetChild(0).gameObject;

            Assert.IsTrue(platform.CompareTag("TurretPlatform"), $"{platform.name} is not tagged as TurretPlatform.");
            turretPlatforms.Add(platform);

            var towerData = _shipData.GetTower(tower.name);
            if (towerData.platform.IsActive)
            {
                platform.SetActive(true);
            } else {
                platform.SetActive(false);
            }
        }
    }

    public void UpdateShipData(ShipData data)
    {
        _shipData = data;
        for (int i = 0; i < turretTowers.Count; i++)
        {
            var tower = turretTowers[i];
            var platform = turretPlatforms[i];

            if (_shipData.GetTower(tower.name) != null && _shipData.GetTower(tower.name).platform.IsActive)
            {
                platform.SetActive(true);
            } else {
                platform.SetActive(false);
            }
        }
        SaveSystem.SaveShipData(_shipData);
    }
}
