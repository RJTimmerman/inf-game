using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    private PlayerControllerCC playerController;

    [Min(0)] public float health = 100;  // Wordt natuurlijk alleen gebruikt als invincible false is
    [Min(0)] public float maxHealth = 100;
    public bool invincible = false;

    private Transform playerHUD;
    private Transform healthHUD;
    private TextMeshProUGUI healthNumber;
    private Slider healthBar;
    private Image barFiller;
    public Gradient colourOfHealthbar;


    void Awake()
    {
        playerController = GetComponent<PlayerControllerCC>();

        playerHUD = GameObject.Find("Player HUD").transform;
        healthHUD = playerHUD.Find("Health HUD").transform;
        healthNumber = healthHUD.Find("Health Number").GetComponent<TextMeshProUGUI>();
        healthBar = healthHUD.GetComponent<Slider>();
        barFiller = healthBar.transform.Find("Data Bar").GetComponent<Image>();
    }

    void Start()
    {
        InitializeHUD();
    }
    private void InitializeHUD()
    {
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
        barFiller.color = colourOfHealthbar.Evaluate(health / maxHealth);
    }

    void Update()
    {
        
    }

    public void TakeDamage(float amount)
    {
        if (!invincible)
        {
            health -= amount; HUDUpdateHealth();
            if (health <= 0)
            {
                // Dood
                Debug.Log("You died...");
            }
        }
    }

    private void HUDUpdateHealth() { healthNumber.text = health.ToString(); healthBar.value = health; barFiller.color = colourOfHealthbar.Evaluate(health / maxHealth); }
}
