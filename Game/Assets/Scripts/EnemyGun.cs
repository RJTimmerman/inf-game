using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    private Enemy enemy;
    private Transform playerTargetSpot;
    public GameObject projectile;
    private ParticleSystem muzzleFlash;

    public float cooldown;

    private float lastShotMoment;


    void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        playerTargetSpot = enemy.player.Find("Shoot Here");
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (enemy.playerInAttackRange)
        {
            transform.LookAt(playerTargetSpot);
            if (Time.time - lastShotMoment >= cooldown) Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(projectile, transform.position, transform.rotation);

        lastShotMoment = Time.time;
    }
}
