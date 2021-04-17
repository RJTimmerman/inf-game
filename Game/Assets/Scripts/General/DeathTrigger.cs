using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private PlayerHealth healthScript;
    
    
    private void Awake()
    {
        healthScript = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player") healthScript.Die();
        else if (other.CompareTag("Enemy")) Destroy(gameObject);
    }
}
