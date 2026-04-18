using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic;

// Las mejoras que el player ha cogido. Cada una tiene varias posibles, por lo que todas las mejoras son int representando
// asi el numero por el que voy de la mejora
[Serializable]
public struct Upgrades
{
    public int speed;
    public int health;
    public int energy;
    public int powerUps;
    public int chanceOfPowerUps;
    public int shootRate;
    public int betterEnemies;
    public int revive;
    public int enemiesRatingSpawn;
    public int enemiesRatingSpawnMin;
    public int earnPointsOfEnemy1;
    public int earnPointsOfEnemy2;
    public int earnPointsOfEnemy3;
    public int damage;

    public int swordWeapon;
    public int missileWeapon;
    public int vampirism; // Energy
}

public class Mejoras : MonoBehaviour
{
    public static Mejoras instance;

    public Upgrades playerUpgrades;

    public List<UpgradeObject> upgradesInfo;
    public Upgrades initUpgrades;

    public bool mainMenuScript = false;

    private int points = 3;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        // Coger las mejoras actuales de la BD y los puntos
        SaveData data = SaveSystem.Load();
        playerUpgrades = data.playerUpgrades;
        points = data.puntos;
    }

    private void Start()
    {
        if (!mainMenuScript) PointsUI.instance.ChangePoints(points);
    }

    public int GetCostNextUpgrade(UpgradeObject upgrade)
    {
        int upgradeLevel = GetUpgradeLevel(upgrade.id);
        if (upgradeLevel >= upgrade.costs.Length - 1) return -1;

        int cost = upgrade.costs[upgradeLevel + 1];
        return cost;
    }

    public bool CanTakeUpgrade(UpgradeObject upgrade)
    {
        int upgradeLevel = GetUpgradeLevel(upgrade.id);
        if (upgradeLevel >= upgrade.costs.Length - 1) return false;

        int cost = upgrade.costs[upgradeLevel + 1];
        if (points - cost < 0) return false;

        return true;
    }

    public bool TakeUpgrade(UpgradeObject upgrade)
    {
        if (!CanTakeUpgrade(upgrade)) return false;
        //int upgradeLevel = GetUpgradeLevel(upgrade.id);
        //if (upgradeLevel >= upgrade.costs.Length - 1) return false;

        //int cost = upgrade.costs[upgradeLevel + 1];
        //if (points - cost < 0) return false;

        int cost = GetCostNextUpgrade(upgrade);
        points -= cost;
        UpgradeLevel(upgrade.id);

        // GUARDAR EN BD
        SaveToBd();

        PointsUI.instance.ChangePoints(points);

        return true;
    }

    public float CheckUpgrade(string id)
    {
        foreach (UpgradeObject upgradeObject in upgradesInfo)
        {
            if (upgradeObject.id == id)
            {
                int valueOfUpgrade = GetUpgradeLevel(id);
                return upgradeObject.values[valueOfUpgrade];
            }
        }

        return 0;
    }

    public void SaveToBd()
    {
        SaveSystem.Save(points, playerUpgrades);
    }

    public void AddPoints(int pts)
    {
        points += pts;
        PointsUI.instance.ChangePoints(points);
    }

    public List<UpgradeObject> GetUpgradesInfo() { return upgradesInfo; }

    private ref int GetRefUpgrade(string id)
    {
        switch (id)
        {
            case "speed": return ref playerUpgrades.speed;
            case "health": return ref playerUpgrades.health;
            case "energy": return ref playerUpgrades.energy;
            case "powerUps": return ref playerUpgrades.powerUps;
            case "chanceOfPowerUps": return ref playerUpgrades.chanceOfPowerUps;
            case "shootRate": return ref playerUpgrades.shootRate;
            case "betterEnemies": return ref playerUpgrades.betterEnemies;
            case "revive": return ref playerUpgrades.revive;
            case "enemiesRatingSpawn": return ref playerUpgrades.enemiesRatingSpawn;
            case "enemiesRatingSpawnMin": return ref playerUpgrades.enemiesRatingSpawnMin;
            case "earnPointsOfEnemy1": return ref playerUpgrades.earnPointsOfEnemy1;
            case "earnPointsOfEnemy2": return ref playerUpgrades.earnPointsOfEnemy2;
            case "earnPointsOfEnemy3": return ref playerUpgrades.earnPointsOfEnemy3;
            case "swordWeapon": return ref playerUpgrades.swordWeapon;
            case "missileWeapon": return ref playerUpgrades.missileWeapon;
            case "vampirism": return ref playerUpgrades.vampirism;
            case "damage": return ref playerUpgrades.damage;
        }

        throw new Exception("NO HAY UPGRADE CON ESE ID " + id);
    }

    private void UpgradeLevel(string id)
    {
        GetRefUpgrade(id)++;
    }

    private int GetUpgradeLevel(string id)
    {
        return GetRefUpgrade(id);
    }

    public void RestartAll()
    {
        SaveSystem.Save(0, initUpgrades);
        PointsUI.instance.ChangePoints(0);
    }
}
