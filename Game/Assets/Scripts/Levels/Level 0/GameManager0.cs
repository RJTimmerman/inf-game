using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager0 : GameManager
{
    public int Level { get; } = 0;
    public GameObject player;
    public PlayerControllerCC playerController;
    public PlayerHealth playerHealth;
    public List<GunScript> ownedGuns = new List<GunScript>();
    public List<GunScript> possibleGuns;
    public string objective = "Find and kill all enemies";
    public List<GameObject> enemies;
    public List<GameObject> livingEnemies;
    public List<int> deadEnemies = new List<int>();
    public List<GameObject> healthPacks;
    public List<GameObject> remainingHealthPacks;
    public List<GameObject> takenHealthPacks;
    public List<GameObject> ammoPacks;
    public List<GameObject> remainingAmmoPacks;
    public List<GameObject> takenAmmoPacks;

    private TextMeshProUGUI savedText;


    private void Awake()
    {
        savedText = GameObject.Find("Saved Text").GetComponent<TextMeshProUGUI>();
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerControllerCC>();
        playerHealth = player.GetComponent<PlayerHealth>();
        
        foreach (Transform child in player.transform.Find("First Person Camera")) if (child.name.StartsWith("Gun ")) ownedGuns.Add(child.GetComponent<GunScript>());
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        livingEnemies = enemies.ToList();
    }
    private void Start()
    {
        Enemy.OnEnemyDeath += EnemyDied;
        Checkpoint.OnCheckpointReached += CheckpointReached;
        PlayerHealth.OnPlayerDeath += PlayerDied;
        
        LoadData();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) SafeData();
        else if (Input.GetKeyDown(KeyCode.L)) SceneManager.LoadScene("Level " + Level);
    }
    

    private void EnemyDied(Enemy enemy)
    {
        deadEnemies.Add(SomeFunctions.GetID(enemy.name));
        livingEnemies.Remove(enemy.gameObject);
        if (livingEnemies.Count == 0) ObjectiveCompleted();
    }

    private void CheckpointReached(Checkpoint checkpoint)
    {
        SafeData();
    }

    private void PlayerDied()
    {
        
    }


    private void ObjectiveCompleted()
    {
        // Level completed
    }


    private GunScript GetGun(string type)
    {
        return possibleGuns.Find(x => x.type == type);
    }

    private void SafeData()
    {
        LevelData0 data = new LevelData0(this);
        SaveSystem.SaveLevel(data);
        savedText.CrossFadeAlpha(60, 0.8f, true);
        savedText.CrossFadeAlpha(0, 4, true);
    }

    private void LoadData()
    {
        LevelData0 data = (LevelData0)SaveSystem.LoadLevel();
        if (data.Level != Level) { SafeData(); return; }

        foreach (int enemyID in data.deadEnemies) Destroy(livingEnemies.Find(x => x.name == "Enemy " + enemyID));

        playerHealth.SetHealth(data.health, data.maxHealth);
        
        ownedGuns = new List<GunScript>();
        foreach (Transform child in player.transform.Find("First Person Camera")) if (child.name.StartsWith("Gun ")) Destroy(child.gameObject);
        for (int i = 0; i < data.guns.Length; i++)
        {
            GunScript gun = Instantiate(GetGun(data.guns[i]), player.transform.Find("First Person Camera")); gun.name = gun.name.Replace("(Clone)", ""); ownedGuns.Add(gun);
            gun.SetAmmo(data.ammo[i]);
        }
        
        playerController.lastCheckpoint = GameObject.Find("Checkpoint " + data.checkpoint).transform;
        playerController.ReturnToCheckpoint();
    }

    
    private void OnDestroy()
    {
        Enemy.OnEnemyDeath -= EnemyDied;
        Checkpoint.OnCheckpointReached -= CheckpointReached;
        PlayerHealth.OnPlayerDeath -= PlayerDied;
    }
}
