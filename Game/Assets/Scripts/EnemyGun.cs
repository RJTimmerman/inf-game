using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    private Enemy enemy;
    public GameObject projectile;
    private ParticleSystem muzzleFlash;

    public float cooldown;

    private float lastShotMoment;


    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.playerInAttackRange && Time.time - lastShotMoment >= cooldown)
        {
            //transform.LookAt(enemy.player);
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(projectile, transform.position, transform.rotation);
        bullet.GetComponent<Projectile>().direction = (enemy.player.position - transform.position).normalized;

        lastShotMoment = Time.time;
    }
}
