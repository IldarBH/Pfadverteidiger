using UnityEngine;

public static class GameManager
{
    public static PlayerData Player { get; private set; }

    public static void StartNewCareer(string name)
    {
        Player = PlayerData.CreateInstance(name);
        SaveSystem.SavePlayerData(Player);
    }

    public static bool LoadCareer()
    {
        Player = SaveSystem.LoadPlayerData();
        return Player != null;
    }
}