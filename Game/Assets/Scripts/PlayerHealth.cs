using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    private PlayerControllerCC playerController;
    private Transform canvas;
    private TextMeshProUGUI healthText;

    [Min(0)] public float health = 100;  // Wordt natuurlijk alleen gebruikt als invincible false is
    public bool invincible = false;


    private void Awake()
    {
        playerController = GetComponent<PlayerControllerCC>();
        canvas = transform.Find("Player Info Canvas").transform;
        healthText = canvas.Find("Health").GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void TakeDamage(float amount)
    {
        if (!invincible)
        {
            health -= amount;
            healthText.text = "Health: " + health;
        }
    }
}
