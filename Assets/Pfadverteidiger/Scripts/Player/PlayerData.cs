using System;

public class PlayerData
{
    public string Name { get; private set; } = "Player";
    private int _credits = 1000;
    private int _experience = 0;
    private uint _level = 0;

    [Serializable]
    public class SerializablePlayerData
    {
        public string Name;
        public int Credits;
        public int Experience;
        public uint Level;
    }

    public SerializablePlayerData ToSerializable()
    {
        return new SerializablePlayerData
        {
            Name = this.Name,
            Credits = this.Credits,
            Experience = this.Experience,
            Level = this.Level
        };
    }

    public PlayerData() {}

    public PlayerData(SerializablePlayerData serializableData)
    {
        Name = serializableData.Name;
        _credits = serializableData.Credits;
        _experience = serializableData.Experience;
        _level = serializableData.Level;
    }
    public int Experience { get => _experience; }

    public int Credits { get => _credits; }

    public uint Level { get => _level; }

    public void AddCredits(int amount) { _credits += amount; }

    public void SubtractCredits(int amount) { _credits -= amount; }

    public void AddExperience(int amount) { _experience += amount; }

    public void SubtractExperience(int amount) { _experience -= amount; }

}
