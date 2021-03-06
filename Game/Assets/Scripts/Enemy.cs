using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform player;
    private LayerMask groundLayer, playerLayer;
    private EnemyGun gun;

    public Vector3 destination;
    private bool destinationSet;
    [Min(0)] public float destinationRange;
    [Min(0)] public float keepPositionTime;
    public float lastDestinationReach;

    [Min(0)] public float attackCooldown;
    private bool alreadyAttacked;

    [Min(0)] public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        groundLayer = LayerMask.GetMask("Ground");
        playerLayer = LayerMask.GetMask("Player");
        gun = GetComponentInChildren<EnemyGun>();
    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        else if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        else if (playerInAttackRange) AttackPlayer();
    }

    void Patrolling()
    {
        if (!destinationSet) FindDestination();

        if (destinationSet) agent.SetDestination(destination);

        float distanceTodestination = (destination - transform.position).magnitude;
        if (distanceTodestination < 1 && Time.time - lastDestinationReach >= keepPositionTime) { destinationSet = false;  lastDestinationReach = Time.time; }
    }
    void FindDestination()
    {
        float x = Random.Range(-destinationRange, destinationRange);
        float z = Random.Range(-destinationRange, destinationRange);
        destination = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if (Physics.Raycast(destination, -transform.up, 3, groundLayer)) destinationSet = true;
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        // De code voor het echt aanvallen wordt geregeld in EnemyGun.cs
    }
}
