using System;

public static class GameManager
{
    public static PlayerData Player { get; private set; }
    public static ShipData Ship { get; private set; }

    public static void StartNewCareer(string name)
    {
        Player = new PlayerData();
        Ship = new ShipData();
        
        var playerSaveFile = SaveSystem.SavePlayerData(Player.ToSerializable());
        var shipSaveFile = SaveSystem.SaveShipData(Ship.ToSerializable());
        Console.WriteLine($"New career started.\n\tPlayer data saved to: {playerSaveFile}\n\tShip data saved to: {shipSaveFile}");
    }

    public static bool LoadCareer()
    {
        Player = SaveSystem.LoadPlayerData();
        Ship = SaveSystem.LoadShipData();
        return Player != null && Ship != null;
    }
}