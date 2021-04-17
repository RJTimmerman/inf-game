using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform player { get; private set; }
    private LayerMask groundLayer, playerLayer;
    private EnemyGun gun;

    private Vector3 destination;
    private bool destinationSet;
    [SerializeField] [Min(0)] private float destinationRange, keepPositionTime;
    private float lastDestinationReach;

    private bool alreadyAttacked;

    [SerializeField] [Min(0)] private float sightRange, attackRange;
    private bool playerInSightRange;
    public bool playerInAttackRange { get; private set; }

    [SerializeField] [Min(0)] private int health, maxHealth;
    [SerializeField] private bool invincible = false;
    [SerializeField] private Gradient colourOfHealthbar;

    private Slider healthBar;
    private Image barFiller;


    private void Start()
    {
        InitializeCanvas();
        
    }
    public void InitializeCanvas()
    {
        healthBar = GetComponentInChildren<Slider>();
        barFiller = healthBar.transform.Find("Data Bar").GetComponent<Image>();
        
        if (!invincible)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = health;
            barFiller.color = colourOfHealthbar.Evaluate((float)health / maxHealth);
        }
        else
        {
            healthBar.value = healthBar.maxValue;
            barFiller.color = Color.black;
        }
    }
    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        groundLayer = LayerMask.GetMask("Ground");
        playerLayer = LayerMask.GetMask("Player");
        gun = GetComponentInChildren<EnemyGun>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        else if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        else if (playerInAttackRange) AttackPlayer();
    }

    private void Patrolling()
    {
        if (!destinationSet) FindDestination();

        if (destinationSet) agent.SetDestination(destination);

        float distanceTodestination = (destination - transform.position).magnitude;
        if (distanceTodestination < 1 && Time.time - lastDestinationReach >= keepPositionTime) { destinationSet = false;  lastDestinationReach = Time.time; }
    }
    private void FindDestination()
    {
        float x = Random.Range(-destinationRange, destinationRange);
        float z = Random.Range(-destinationRange, destinationRange);
        destination = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if (Physics.Raycast(destination, -transform.up, 3, groundLayer)) destinationSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        // De code voor het echt aanvallen wordt geregeld in EnemyGun.cs
    }

    public void TakeDamage(int amount)
    {
        if (!invincible)
        {
            health -= amount; HUDUpdateHealth();
            if (health <= 0)
            {
                // Dood
                Debug.Log("Enemy dead");
                Destroy(gameObject);
            }
        }
    }


    public static event Action<Enemy> OnEnemyDeath;
    private void OnDestroy()
    {
        OnEnemyDeath?.Invoke(this);
    }
    
    
    private void HUDUpdateHealth() { healthBar.value = health; barFiller.color = colourOfHealthbar.Evaluate((float)health / maxHealth); }
}
