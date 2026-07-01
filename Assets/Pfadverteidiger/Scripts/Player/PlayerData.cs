using Codice.CM.SEIDInfo;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    private string _name;
    private uint _credits = 0;
    private uint _level = 0;
    private uint _experience = 0;

    public void Initialize(string name, uint level = 0, uint experience = 0)
    {
        _name = name;
        _level = level;
        _experience = experience;
    }
    public string Name { get => _name; }
    public uint Experience { get => _experience; }
    public uint Credits { get => _credits; }
    public uint Level { get => _level; }
    public void AddCredits(uint amount) { _credits += amount; }
    public void SubtractCredits(uint amount) { _credits -= amount; }
    public void AddExperience(uint amount) { _experience += amount; }
    public void SubtractExperience(uint amount) { _experience -= amount; }
    public static PlayerData CreateInstance(string name, uint credits = 1000, uint level = 0, uint experience = 0)
    {
        PlayerData playerData = ScriptableObject.CreateInstance<PlayerData>();
        playerData.Initialize(name, level, experience);
        playerData.AddCredits(credits);
        return playerData;
    }
}
