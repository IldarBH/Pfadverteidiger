using UnityEngine;
using System.IO;
using System;

public class SaveSystem
{
    private static readonly string _saveFolder = Path.Combine(Application.persistentDataPath, "Saves");

    [Serializable]
    public class PlayerDataSave
    {
        public string name;
        public int credits;
        public int experience;
        public uint level;
    }

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

    public static PlayerDataSave LoadPlayerData(string saveFile)
    {
        if (string.IsNullOrWhiteSpace(saveFile) || !File.Exists(saveFile)) return null;
        try {
            var json = File.ReadAllText(saveFile);
            return JsonUtility.FromJson<PlayerDataSave>(json);
        } catch (Exception ex) {
            Debug.LogError($"Failed to load player data from {saveFile}: {ex.Message}");
            return null;
        }
    }

    public static PlayerData LoadPlayerData()
    {
        var latestSaveFile = FindLatestSaveFile(_saveFolder);
        var playerSaveData = LoadPlayerData(latestSaveFile);
        if (playerSaveData == null)
        {
            return null;
        }
        return PlayerData.CreateInstance(playerSaveData.name, playerSaveData.credits, playerSaveData.experience, playerSaveData.level);
    }

    public static string SavePlayerData(PlayerData playerData)
    {
        if (playerData == null) throw new ArgumentNullException(nameof(playerData));
        if (!Directory.Exists(_saveFolder)) Directory.CreateDirectory(_saveFolder);
        
        var fileName = string.IsNullOrWhiteSpace(playerData.Name) ? "Player" : playerData.Name;
        var saveFile = Path.Combine(_saveFolder, fileName + ".save");
        var saveData = new PlayerDataSave
        {
            name = playerData.Name,
            credits = playerData.Credits,
            experience = playerData.Experience,
            level = playerData.Level
        };
        File.WriteAllText(saveFile, JsonUtility.ToJson(saveData, true));
        return saveFile;
    }

}