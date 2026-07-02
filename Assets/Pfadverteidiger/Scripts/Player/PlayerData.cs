public class PlayerData
{
    private string _name;
    private int _credits = 0;
    private int _experience = 0;
    private uint _level = 0;
    public void Initialize(string name, int experience = 0, uint level = 0u)
    {
        _name = name;
        _experience = experience;
        _level = level;
    }
    public string Name { get => _name; }
    public int Experience { get => _experience; }
    public int Credits { get => _credits; }
    public uint Level { get => _level; }
    public void AddCredits(int amount) { _credits += amount; }
    public void SubtractCredits(int amount) { _credits -= amount; }
    public void AddExperience(int amount) { _experience += amount; }
    public void SubtractExperience(int amount) { _experience -= amount; }
    public static PlayerData CreateInstance(string name, int credits = 1000, int experience = 0, uint level = 0u)
    {
        PlayerData playerData = new PlayerData();
        playerData.Initialize(name, experience, level);
        playerData.AddCredits(credits);
        return playerData;
    }
}
