using System;

public static class GameManager
{
    private static PlayerData _player;
    private static ShipData _ship;
    public static bool IsCareerLoaded { get; private set; } = false;

    public static bool StartNewCareer()
    {
        _player = new PlayerData();
        _ship = SaveSystem.LoadShipData(_player.CurrentShip);
        
        var playerSaveFile = SaveSystem.SavePlayerData(_player);
        var shipSaveFile = SaveSystem.SaveShipData(_ship);
        IsCareerLoaded = _player != null && _ship != null;
        return IsCareerLoaded;
    }

    public static bool LoadCareer()
    {
        _player = SaveSystem.LoadPlayerData();
        _ship = SaveSystem.LoadShipData(_player.CurrentShip);
        
        IsCareerLoaded = _player != null && _ship != null;
        return IsCareerLoaded;
    }

    public static void SaveCareer()
    {
        if (_player == null || _ship == null)
        {
            throw new InvalidOperationException("Cannot save career: Player or Ship data is null.");
        }
        SaveSystem.SavePlayerData(_player);
        SaveSystem.SaveShipData(_ship);
    }

    public static PlayerData GetPlayerData()
    {
        if (_player == null)
        {
            throw new InvalidOperationException("Player data is not loaded.");
        }
        return _player;
    }

    public static ShipData GetShipData()
    {
        if (_ship == null)
        {
            throw new InvalidOperationException("Ship data is not loaded.");
        }
        return _ship;
    }
}