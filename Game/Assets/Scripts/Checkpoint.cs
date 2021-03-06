using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private PlayerControllerCC playerController;

    public bool singleUse;  // Als deze waarde true heeft, dan kun je maar één keer de checkpoint hiernaartoe veranderen. Je kan wel oneindig vaak hiernaartoe gezet worden.


    void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerControllerCC>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            playerController.lastCheckpoint = transform;

            if (singleUse) gameObject.SetActive(false);
        }
    }
}
