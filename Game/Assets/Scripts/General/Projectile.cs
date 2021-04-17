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
        if (other.name == "Player") HitPlayer(other.gameObject);
        if (other.transform != shooter) Destroy(gameObject);
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player") HitPlayer(collision.gameObject);
        Destroy(gameObject);
    }*/

    private void HitPlayer(GameObject player)
    {
        player.GetComponent<PlayerHealth>().TakeDamage(damage);
        //Destroy(gameObject);
    }
}
