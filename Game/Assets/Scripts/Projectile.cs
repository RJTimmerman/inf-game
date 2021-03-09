using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float maxDistance;  // Changing this value while the game is running will have no effect
    public float damage;


    void Start()
    {
        Destroy(gameObject, maxDistance / speed);
    }

    void FixedUpdate()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player") HitPlayer(other.gameObject);
        //Destroy(gameObject);
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player") HitPlayer(collision.gameObject);
        Destroy(gameObject);
    }*/

    private void HitPlayer(GameObject player)
    {
        player.GetComponent<PlayerHealth>().TakeDamage(damage);
        Destroy(gameObject);
    }
}
