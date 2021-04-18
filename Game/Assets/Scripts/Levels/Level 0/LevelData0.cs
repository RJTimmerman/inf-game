using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class LevelData0 : LevelData
{
    public int Level { get; }  = 0;
    public int checkpoint;
    public int[] deadEnemies;  // ID's van de al dode vijanden
    public int health;
    public int maxHealth;
    public string[] guns;
    public int[] ammo;
    public bool[] gunsActive;
    public int[] takenHealthPacks;
    public int[] takenAmmoPacks;
    
    
    public LevelData0 (GameManager0 gameManager)
    {
        PlayerControllerCC playerController = gameManager.playerController;
        PlayerHealth playerHealth = gameManager.playerHealth;
        List<GunScript> gunScripts = gameManager.ownedGuns;

        checkpoint = SomeFunctions.GetID(playerController.lastCheckpoint.name);

        deadEnemies = gameManager.deadEnemies.ToArray();

        int[] healthInfo = playerHealth.GetHealth();
        health = healthInfo[0];
        maxHealth = healthInfo[1];

        int gunCount = gunScripts.Count;
        guns = new string[gunCount];
        ammo = new int[gunCount];
        gunsActive = new bool[gunCount];
        for (int i = 0; i < gunCount; i++)
        {
            guns[i] = gunScripts[i].type;
            ammo[i] = gunScripts[i].GetAmmo();
            gunsActive[i] = gunScripts[i].gameObject.activeInHierarchy;
        }
        
        int takenHealthPacksCount = gameManager.takenHealthPacks.Count;
        takenHealthPacks = new int[takenHealthPacksCount];
        for (int i = 0; i < takenHealthPacksCount; i++)
        {
            string healthPackName = gameManager.takenHealthPacks[i].name;
            takenHealthPacks[i] = healthPackName[healthPackName.Length - 1];
        }
        
        int takenAmmoPacksCount = gameManager.takenAmmoPacks.Count;
        takenAmmoPacks = new int[takenAmmoPacksCount];
        for (int i = 0; i < takenAmmoPacksCount; i++)
        {
            string ammoPackName = gameManager.takenAmmoPacks[i].name;
            takenAmmoPacks[i] = ammoPackName[ammoPackName.Length - 1];
        }
    }
}
