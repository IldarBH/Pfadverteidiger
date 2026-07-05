using System;

public static class GameManager
{
    public static PlayerData Player { get; private set; } = SaveSystem.LoadPlayerData() ?? new PlayerData();
    public static ShipData Ship { get; private set; } = SaveSystem.LoadShipData() ?? new ShipData();

    public static void StartNewCareer()
    {
        Player = new PlayerData();
        Ship = new ShipData();
        
        var playerSaveFile = SaveSystem.SavePlayerData(Player);
        var shipSaveFile = SaveSystem.SaveShipData(Ship);
        Console.WriteLine($"New career started.\n\tPlayer data saved to: {playerSaveFile}\n\tShip data saved to: {shipSaveFile}");
    }

    public static bool LoadCareer()
    {
        Player = SaveSystem.LoadPlayerData();
        Ship = SaveSystem.LoadShipData();
        return Player != null && Ship != null;
    }
}