using UnityEngine;
using System.IO;
using System;

public class SaveSystem
{
    private static readonly string _saveFolder = Path.Combine(Application.persistentDataPath, "Saves");

    public static string FindLatestSaveFile(string searchFolder)
    {
        // Check if the save folder exists and if there are any save files in it
        if (!Directory.Exists(searchFolder)) return null;
        var saveFiles = Directory.GetFiles(searchFolder, "*.save");
        if (saveFiles.Length == 0) return null;

        // Find the latest save file based on the last write time
        var latestSaveFile = saveFiles[0];
        var latestWriteTime = File.GetLastWriteTimeUtc(latestSaveFile);
        foreach (var candidateFile in saveFiles)
        {
            var candidateWriteTime = File.GetLastWriteTimeUtc(candidateFile);
            if (candidateWriteTime > latestWriteTime)
            {
                latestSaveFile = candidateFile;
                latestWriteTime = candidateWriteTime;
            }
        }
        return latestSaveFile;
    }

    public static PlayerData LoadPlayerData()
    {
        var latestSaveFile = FindLatestSaveFile(_saveFolder);
        if (string.IsNullOrWhiteSpace(latestSaveFile) || !File.Exists(latestSaveFile)) return null;
        try {
            var json = File.ReadAllText(latestSaveFile);
            return JsonUtility.FromJson<PlayerData>(json);
        } catch (Exception ex) {
            Debug.LogError($"Failed to load player data from {latestSaveFile}: {ex.Message}");
            return null;
        }
    }

    public static string SavePlayerData(PlayerData playerData)
    {
        if (playerData == null) throw new ArgumentNullException(nameof(playerData));
        if (!Directory.Exists(_saveFolder)) Directory.CreateDirectory(_saveFolder);
        
        var fileName = string.IsNullOrWhiteSpace(playerData.Name) ? "Player" : playerData.Name;
        var saveFile = Path.Combine(_saveFolder, fileName + ".save");
        File.WriteAllText(saveFile, JsonUtility.ToJson(playerData, true));
        return saveFile;
    }

    public static ShipData LoadShipData(string shipName)
    {
        var shipSaveFile = Path.Combine(_saveFolder, shipName + ".save");
        if (string.IsNullOrWhiteSpace(shipSaveFile))
        {
            throw new ArgumentException("Ship name cannot be null or whitespace.", nameof(shipName));
        } else if (!File.Exists(shipSaveFile)) 
        {
            return new ShipData();
        }
        try {
            var json = File.ReadAllText(shipSaveFile);
            return JsonUtility.FromJson<ShipData>(json);
        } catch (Exception ex) {
            throw new Exception($"Failed to load ship data from {shipSaveFile}: {ex.Message}", ex);
        }
    }

    public static string SaveShipData(ShipData shipData)
    {
        if (shipData == null) throw new ArgumentNullException(nameof(shipData));
        if (!Directory.Exists(_saveFolder)) Directory.CreateDirectory(_saveFolder);
        
        var fileName = shipData.ShipName();
        var saveFile = Path.Combine(_saveFolder, fileName + ".save");
        File.WriteAllText(saveFile, JsonUtility.ToJson(shipData, true));
        return saveFile;
    }
}