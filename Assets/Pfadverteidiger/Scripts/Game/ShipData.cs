using System;
using System.Collections.Generic;
using UnityEngine;


public enum ShipType
{
    SSH_MK1,
}

public static class ShipDataConfig
{
    private static Dictionary<ShipType, string> ShipNameToPrefabPath = new Dictionary<ShipType, string>()
    {
        { ShipType.SSH_MK1, "Prefabs/Ships/SSH-MK1/SSH-MK1" },
    };

    public static string GetPrefabPath(ShipType shipType)
    {
        if (ShipNameToPrefabPath.ContainsKey(shipType))
        {
            return ShipNameToPrefabPath[shipType];
        }
        else
        {
            throw new ArgumentException($"Prefab path for ship type {shipType} not found.");
        }
    }
}


[Serializable]
public class ShipProperty
{
    [field: SerializeField] public uint BaseValue { get; private set; } = 0;
    [field: SerializeField] public uint UpgradeValue { get; private set; } = 1;
    [field: SerializeField] public uint Level { get; private set; } = 1;
    [field: SerializeField] public uint MaxValue { get; private set; } = 0;
    [field: SerializeField] public uint Value { get; private set; } = 0;
    
    public ShipProperty(uint baseValue, uint upgradeValue)
    {
        BaseValue = baseValue;
        UpgradeValue = upgradeValue;
        UpdateMaxValue();
        RestoreValue();
    }

    public ShipProperty(uint baseValue, uint upgradeValue, uint level, uint value)
    {
        BaseValue = baseValue;
        UpgradeValue = upgradeValue;
        Level = level;
        UpdateMaxValue();
        Value = Math.Min(value, MaxValue);
    }
    
    private void UpdateMaxValue() 
    { 
        MaxValue = BaseValue + UpgradeValue * (Level - 1);
    }

    public void RestoreValue()
    {
        Value = MaxValue;
    }

    public void UpgradeLevel()
    {
        Level++;
        UpdateMaxValue();
    }

    public uint SubtractValue(uint amount)
    {
        var actualAmount = Math.Min(amount, Value);
        Value -= actualAmount;
        return actualAmount;
    }
}

[Serializable]
public class PlatformData
{
    [field: SerializeField] public bool IsActive { get; private set; } = false;

    public PlatformData(bool isActive = false)
    {
        IsActive = isActive;
    }

    public void Activate()
    {
        IsActive = true;
    }
}

[Serializable]
public class TowerData
{
    [field: SerializeField] public PlatformData platform { get; private set; } = new PlatformData();
    [field: SerializeField] public string Name { get; private set; } = "DefaultTower";
    public TowerData(string towerName, bool isActive = false)
    {
        this.platform = new PlatformData(isActive);
        Name = towerName;
    }
}

[Serializable]
public class ShipData 
{
    [field: SerializeField] public ShipType ShipType { get; private set; } = ShipType.SSH_MK1;
    [field: SerializeField] public ShipProperty Health { get; private set; } = new ShipProperty(100, 10);
    [field: SerializeField] public ShipProperty Armor { get; private set; } = new ShipProperty(50, 5);
    [field: SerializeField] public List<TowerData> Towers { get; private set; } = new List<TowerData>();
    private Dictionary<string, TowerData> _towersDict = new Dictionary<string, TowerData>();

    public ShipData()
    {
        foreach (var tower in Towers)
        {
            _towersDict[tower.Name] = tower;
        }
    }

    public ShipData(ShipData other)
    {
        ShipType = other.ShipType;
        Health = new ShipProperty(other.Health.BaseValue, other.Health.UpgradeValue, other.Health.Level, other.Health.Value);
        Armor = new ShipProperty(other.Armor.BaseValue, other.Armor.UpgradeValue, other.Armor.Level, other.Armor.Value);
        Towers = new List<TowerData>();
        foreach (var tower in other.Towers)
        {
            var newTower = new TowerData(tower.Name, tower.platform.IsActive);
            Towers.Add(newTower);
            _towersDict[newTower.Name] = newTower;
        }
    }

    public string GetPrefabPath()
    {
        return ShipDataConfig.GetPrefabPath(ShipType);
    }

    public void Hit(uint damage)
    {
        var damage_to_armor = Armor.SubtractValue(damage);
        var damage_to_health = damage - damage_to_armor;
        Health.SubtractValue(damage_to_health);
    }

    public bool TowerExists(string towerName)
    {
        return _towersDict.ContainsKey(towerName);
    }

    public void AddTower(string towerName, bool isActive = false)
    {
        if (!_towersDict.ContainsKey(towerName))
        {
            var towerData = new TowerData(towerName, isActive);
            Towers.Add(towerData);
            _towersDict[towerName] = towerData;
        }
    }

    public TowerData GetTower(string towerName)
    {
        if (!_towersDict.ContainsKey(towerName))
        {
            AddTower(towerName);
        }
        return _towersDict[towerName];
    }

    public uint GetActiveTowerCount()
    {
        uint count = 0;
        foreach (var tower in Towers)
        {
            if (tower.platform.IsActive)
            {
                count++;
            }
        }
        return count;
    }
}
