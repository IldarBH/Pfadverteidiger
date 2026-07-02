using System;

public class ShipProperty
{
    private uint _level;
    private uint _maxValue;
    private uint _value;
    
    public ShipProperty(uint level)
    {
        _level = level;
        UpdateMaxValue();
        _value = _maxValue;
    }

    public ShipProperty(uint level, uint value)
    {
        _level = level;
        UpdateMaxValue();
        _value = Math.Min(value, _maxValue);
    }
    
    private void UpdateMaxValue() 
    { 
        _maxValue = _level * 10; 
    }
    
    public uint Level { get => _level; }
    public uint MaxValue { get => _maxValue; }
    public uint Value { get => _value; }
}

public class ShipData 
{
    public string Name { get; set; } = "Default Ship";
    private ShipProperty _health = new ShipProperty(1);
    private ShipProperty _armor = new ShipProperty(0);

    [Serializable]
    public class SerializableShipData
    {
        public string Name;
        public uint HealthLevel;
        public uint HealthValue;
        public uint ArmorLevel;
        public uint ArmorValue;
    }
    
    public SerializableShipData ToSerializable()
    {
        return new SerializableShipData
        {
            Name = this.Name,
            HealthLevel = this.Health.Level,
            HealthValue = this.Health.Value,
            ArmorLevel = this.Armor.Level,
            ArmorValue = this.Armor.Value
        };
    }
    
    public ShipData() {}

    public ShipData(SerializableShipData serializableData)
    {
        Name = serializableData.Name;
        _health = new ShipProperty(serializableData.HealthLevel, serializableData.HealthValue);
        _armor = new ShipProperty(serializableData.ArmorLevel, serializableData.ArmorValue);
    }

    public ShipProperty Health { get => _health; }
    public ShipProperty Armor { get => _armor; }
}
