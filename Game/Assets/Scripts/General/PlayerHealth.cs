using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    private PlayerControllerCC playerController;

    [SerializeField] [Min(0)] private int health = 100, maxHealth = 100;  // Wordt natuurlijk alleen gebruikt als invincible false is
    [SerializeField] private bool invincible = false;

    private Transform playerHUD, healthHUD;
    private TextMeshProUGUI healthNumber;
    private Slider healthBar;
    private Image barFiller;
    [SerializeField] private Gradient colourOfHealthbar;


    private void Awake()
    {
        playerController = GetComponent<PlayerControllerCC>();
    }
    private void Start()
    {
        InitializeHUD();
    }
    public void InitializeHUD()
    {
        playerHUD = GameObject.Find("Player HUD").transform;
        healthHUD = playerHUD.Find("Health HUD").transform;
        healthNumber = healthHUD.Find("Health Number").GetComponent<TextMeshProUGUI>();
        healthBar = healthHUD.GetComponent<Slider>();
        barFiller = healthHUD.Find("Data Bar").GetComponent<Image>();
        
        if (!invincible)
        {
            healthNumber.text = health.ToString();
            healthBar.maxValue = maxHealth;
            healthBar.value = health;
        }
        else
        {
            healthNumber.text = "∞";
            healthBar.value = healthBar.maxValue;
        }
        barFiller.color = colourOfHealthbar.Evaluate((float)health / maxHealth);  // Zonder (float) wordt een deelsom tussen twee integers ook een integer (dus alles tussen 0 en 1 wordt 0)
    }

    public void TakeDamage(int amount)
    {
        if (!invincible)
        {
            health -= amount; HUDUpdateHealth();
            if (health <= 0)
            {
                Die();
            }
        }
    }

    public static event Action OnPlayerDeath;
    public void Die()
    {
        Debug.Log("You died...");
        OnPlayerDeath?.Invoke();
    }

    private void HUDUpdateHealth() { healthNumber.text = health.ToString(); healthBar.value = health; barFiller.color = colourOfHealthbar.Evaluate((float)health / maxHealth); }
    
    
    public int[] GetHealth() { return new int[] { health, maxHealth }; }
    public void SetHealth(int newHealth, int newMaxHealth) { health = newHealth; maxHealth = newMaxHealth; }
}
