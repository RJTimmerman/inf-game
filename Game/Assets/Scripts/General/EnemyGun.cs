using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    private Transform shootFromPoint;
    private Enemy enemy;
    private Transform playerTargetSpot;
    [SerializeField] private GameObject projectile;  // Snelheid en schade worden gekozen in de Projectile prefab
    private ParticleSystem muzzleFlash;

    [SerializeField] private float cooldown;

    private float lastShotMoment;


    private void Awake()
    {
        shootFromPoint = transform.Find("Body").Find("Shoot From Point");
        enemy = GetComponentInParent<Enemy>();
        playerTargetSpot = enemy.player.Find("Shoot Here");
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        if (enemy.playerInAttackRange)
        {
            transform.LookAt(playerTargetSpot);
            if (Time.time - lastShotMoment >= cooldown) Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(projectile, shootFromPoint.position, transform.rotation);
        bullet.GetComponent<Projectile>().shooter = transform.parent;

        lastShotMoment = Time.time;
    }
}
