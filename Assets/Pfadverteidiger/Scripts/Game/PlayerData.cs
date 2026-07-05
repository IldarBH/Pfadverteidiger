using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    [field: SerializeField] public string Name { get; private set; } = "Player";
    [field: SerializeField] public int Credits { get; private set; } = 1000;
    [field: SerializeField] public int Experience { get; private set; } = 0;
    [field: SerializeField] public uint Level { get; private set; } = 0;

    public PlayerData() {}

    public void AddCredits(int amount) { Credits += amount; }

    public void SubtractCredits(int amount) { Credits -= amount; }

    public void AddExperience(int amount) { Experience += amount; }

    public void SubtractExperience(int amount) { Experience -= amount; }

}
