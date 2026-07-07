using System;

public static class GameManager
{
    public static PlayerData Player { get; private set; } = SaveSystem.LoadPlayerData() ?? new PlayerData();
    public static ShipData Ship { get; private set; } = SaveSystem.LoadShipData() ?? new ShipData();
    public static bool IsCareerLoaded { get; private set; } = false;

    public static bool StartNewCareer()
    {
        Player = new PlayerData();
        Ship = new ShipData();
        
        var playerSaveFile = SaveSystem.SavePlayerData(Player);
        var shipSaveFile = SaveSystem.SaveShipData(Ship);
        IsCareerLoaded = Player != null && Ship != null;
        return IsCareerLoaded;
    }

    public static bool LoadCareer()
    {
        Player = SaveSystem.LoadPlayerData();
        Ship = SaveSystem.LoadShipData();
        IsCareerLoaded = Player != null && Ship != null;
        return IsCareerLoaded;
    }
}