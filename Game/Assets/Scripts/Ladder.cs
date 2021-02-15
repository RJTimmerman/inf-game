using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public PlayerMovementCC playerController;
    public float speed = 6;

    private bool onLadder = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (onLadder)
        {
            playerController.velocity.y = 0;
            Vector3 move = transform.parent.up;
            if (Input.GetKey(KeyCode.Space))
            {
                //playerController.velocity.y += speed * Time.deltaTime;
                move = move * speed * Time.deltaTime;
                
            }
            else //if (!playerController.isGrounded)
            {
                //playerController.velocity.y -= speed * Time.deltaTime;
                move = move * -speed * Time.deltaTime;
            }
            playerController.controller.Move(move);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onLadder = true;
            playerController.doGravity = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onLadder = false;
            playerController.doGravity = true;
        }
    }
}
