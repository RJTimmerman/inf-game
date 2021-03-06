using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetToCheckpoint : MonoBehaviour
{
    private PlayerControllerCC playerController;


    void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerControllerCC>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player") playerController.ReturnToCheckpoint();
    }
}
