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
    public MouseRotate cameraScript;
    public List<GunScript> ownedGuns = new List<GunScript>();
    public List<GunScript> possibleGuns;
    public GunScript currentGun;
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

    private TextMeshProUGUI objectiveText;
    private TextMeshProUGUI savedText;
    private GameObject deathScreen;
    private GameObject victoryScreen;



    private void Awake()
    {
        objectiveText = GameObject.Find("Objective Text").GetComponent<TextMeshProUGUI>(); objectiveText.text = objective; objectiveText.CrossFadeAlpha(0, 0, true);
        savedText = GameObject.Find("Saved Text").GetComponent<TextMeshProUGUI>(); savedText.CrossFadeAlpha(0, 0, true);
        deathScreen = GameObject.Find("Death Screen"); deathScreen.SetActive(false);
        victoryScreen = GameObject.Find("Victory Screen"); victoryScreen.SetActive(false);
        

        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerControllerCC>();
        playerHealth = player.GetComponent<PlayerHealth>();
        cameraScript = player.GetComponentInChildren<MouseRotate>();

        foreach (Transform child in player.transform.Find("First Person Camera")) if (child.name.StartsWith("Gun ")) { 
                ownedGuns.Add(child.GetComponent<GunScript>()); if (child.gameObject.activeInHierarchy) currentGun = child.GetComponent<GunScript>(); }
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        livingEnemies = enemies.ToList();
    }
    private void Start()
    {
        Enemy.OnEnemyDeath += EnemyDied;
        Checkpoint.OnCheckpointReached += CheckpointReached;
        PlayerHealth.OnPlayerDeath += PlayerDied;
        
        LoadData();

        objectiveText.CrossFadeAlpha(1, 0, true);
        objectiveText.CrossFadeAlpha(10, 4, true);
        objectiveText.CrossFadeAlpha(0, 6, true);
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightControl) && Input.GetKeyDown(KeyCode.K)) SafeData();
        else if (Input.GetKey(KeyCode.RightControl) && Input.GetKeyDown(KeyCode.L)) SceneManager.LoadScene("Level " + Level);
        else if (Input.GetKey(KeyCode.RightControl) && Input.GetKeyDown(KeyCode.Semicolon)) SaveSystem.DeleteSave();
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
        playerController.canMove = false; cameraScript.locked = true; Cursor.lockState = CursorLockMode.Confined; Cursor.visible = true;
        currentGun.active = false;
        deathScreen.gameObject.SetActive(true);
    }


    private void ObjectiveCompleted()
    {
        playerController.canMove = false; cameraScript.locked = true; Cursor.lockState = CursorLockMode.Confined; Cursor.visible = true;
        victoryScreen.SetActive(true);
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
        if (data == null || data.Level != Level) { SafeData(); return; }

        foreach (int enemyID in data.deadEnemies) Destroy(livingEnemies.Find(x => x.name == "Enemy " + enemyID));

        playerHealth.SetHealth(data.health, data.maxHealth);
        
        ownedGuns = new List<GunScript>();
        foreach (Transform child in player.transform.Find("First Person Camera")) if (child.name.StartsWith("Gun ")) Destroy(child.gameObject);
        for (int i = 0; i < data.guns.Length; i++)
        {
            GunScript gun = Instantiate(GetGun(data.guns[i]), player.transform.Find("First Person Camera")); gun.name = gun.name.Replace("(Clone)", ""); ownedGuns.Add(gun);
            gun.SetAmmo(data.ammo[i]);
            if (data.gunsActive[i]) currentGun = gun; else gun.gameObject.SetActive(false);
        }
        
        playerController.lastCheckpoint = GameObject.Find("Checkpoint " + data.checkpoint).transform;
        playerController.ReturnToCheckpoint();

        Debug.Log("Loaded safe file at " + SaveSystem.path);
    }

    
    private void OnDestroy()
    {
        Enemy.OnEnemyDeath -= EnemyDied;
        Checkpoint.OnCheckpointReached -= CheckpointReached;
        PlayerHealth.OnPlayerDeath -= PlayerDied;
    }
}
