using System;
using UnityEngine;

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
    
    public void UpdateMaxValue() 
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
}

[Serializable]
public class ShipData 
{
    [field: SerializeField] public string PrefabPath { get; set; } = "Prefabs/Ships/SSH-MK1/SSH-MK1";
    [field: SerializeField] public ShipProperty Health { get; private set; } = new ShipProperty(100, 10);
    [field: SerializeField] public ShipProperty Armor { get; private set; } = new ShipProperty(50, 5);
    
    public ShipData() {}

    public string ShipName() { return System.IO.Path.GetFileNameWithoutExtension(PrefabPath); }
}
