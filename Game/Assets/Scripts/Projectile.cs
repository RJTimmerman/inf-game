using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    //public Vector3 direction;
    public float maxDistance;  // Changing this value while the game is running will have no effect


    void Start()
    {
        Destroy(gameObject, maxDistance / speed);
    }

    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }
}
