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
public class ShipProperty : Property
{
    public ShipProperty(uint baseValue, uint upgradeValue) : base(baseValue, upgradeValue) { }
    public ShipProperty(uint baseValue, uint upgradeValue, uint level, uint value) : base(baseValue, upgradeValue, level, value) { }
}


[Serializable]
public class TurretPlatformData
{
    [field: SerializeField] public string Name { get; private set; } = "DefaultTurretPlatform";
    [field: SerializeField] public bool IsActive { get; private set; } = false;

    public TurretPlatformData(string name = "DefaultTurretPlatform", bool isActive = false)
    {
        Name = name;
        IsActive = isActive;
    }

    public void Activate()
    {
        IsActive = true;
    }
}

[Serializable]
public class ShipData 
{
    [field: SerializeField] public ShipType ShipType { get; private set; } = ShipType.SSH_MK1;
    [field: SerializeField] public ShipProperty Health { get; private set; } = new ShipProperty(100, 10);
    [field: SerializeField] public ShipProperty Armor { get; private set; } = new ShipProperty(50, 5);
    [field: SerializeField] public List<TurretPlatformData> Platforms { get; private set; } = new List<TurretPlatformData>();
    private Dictionary<string, TurretPlatformData> _platformsDict;

    public ShipData()
    {
        _platformsDict = new Dictionary<string, TurretPlatformData>();
        foreach (var platform in Platforms)
        {
            _platformsDict[platform.Name] = platform;
        }
    }

    public ShipData(ShipData other)
    {
        ShipType = other.ShipType;
        Health = new ShipProperty(other.Health.BaseValue, other.Health.UpgradeValue, other.Health.Level, other.Health.Value);
        Armor = new ShipProperty(other.Armor.BaseValue, other.Armor.UpgradeValue, other.Armor.Level, other.Armor.Value);
        Platforms = new List<TurretPlatformData>();
        _platformsDict = new Dictionary<string, TurretPlatformData>();
        foreach (var platform in other.Platforms)
        {
            var newPlatform = new TurretPlatformData(platform.Name, platform.IsActive);
            Platforms.Add(newPlatform);
            _platformsDict[newPlatform.Name] = newPlatform;
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

    public bool PlatformExists(string platformName)
    {
        return _platformsDict.ContainsKey(platformName);
    }

    public bool AddPlatform(string platformName, bool isActive = false)
    {
        if (PlatformExists(platformName))
        {
            return false; // Platform already exists
        }
        var newPlatform = new TurretPlatformData(platformName, isActive);
        Platforms.Add(newPlatform);
        _platformsDict[platformName] = newPlatform;
        return true;
    }

    public bool PlatformIsActive(string platformName)
    {
        if (PlatformExists(platformName))
        {
            return _platformsDict[platformName].IsActive;
        }
        return false; // Platform does not exist
    }
}
