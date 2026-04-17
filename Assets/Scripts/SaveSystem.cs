using UnityEngine;
using System.IO;

public static class SaveSystem
{
    static string path = Application.persistentDataPath + "/save.json";

    public static void Save(int points, Upgrades upgrades)
    {
        SaveData saveData = new SaveData { puntos = points, playerUpgrades = upgrades };

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(path, json);
    }

    public static SaveData Load()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<SaveData>(json);
        }

        return new SaveData();
    }
}
