using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform shooter;
    [SerializeField] private float speed, maxDistance;  // Changing this value while the game is running will have no effect
    [SerializeField] private int damage;


    private void Start()
    {
        Destroy(gameObject, maxDistance / speed);  // afstand / snelheid = tijd tot die afstand
    }

    private void FixedUpdate()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player") other.GetComponent<PlayerHealth>().TakeDamage(damage);
        if (other.CompareTag("Enemy") && other.transform != shooter) other.GetComponent<Enemy>().TakeDamage(damage);
        if (other.transform != shooter) Destroy(gameObject);
    }
}
