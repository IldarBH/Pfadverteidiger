using System;
using UnityEngine;

[Serializable]
public class Property
{
    [field: SerializeField] public uint BaseValue { get; private set; } = 0;
    [field: SerializeField] public uint UpgradeValue { get; private set; } = 1;
    [field: SerializeField] public uint Level { get; private set; } = 1;
    [field: SerializeField] public uint MaxValue { get; private set; } = 0;
    [field: SerializeField] public uint Value { get; private set; } = 0;

    public Property(uint baseValue, uint upgradeValue)
    {
        BaseValue = baseValue;
        UpgradeValue = upgradeValue;
        UpdateMaxValue();
        RestoreValue();
    }

    public Property(uint baseValue, uint upgradeValue, uint level, uint value)
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
