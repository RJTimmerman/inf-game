using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private GameObject playerObject;
    private PlayerControllerCC playerController;
    [SerializeField] private float speed = 2;

    private bool onLadder = false;


    private void Awake()
    {
        playerObject = GameObject.Find("Player");
        playerController = playerObject.GetComponent<PlayerControllerCC>();
    }

    private void Update()
    {
        if (onLadder)
        {
            playerController.velocity.y = 0;
            Vector3 move = transform.parent.up;
            if (Input.GetKey(KeyCode.W))
            {
                move = move * speed * Time.deltaTime;
                
            }
            else
            {
                move = move * -speed * Time.deltaTime;
            }
            playerController.controller.Move(move);

            if (playerController.isGrounded)
            {
                playerController.canMove = true;
            }
            else
            {
                playerController.canMove = false;
            }

            if (Input.GetKey(KeyCode.S))
            {
                Exit();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            onLadder = true;
            playerController.doGravity = false;
            playerController.canMove = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            Exit();
        }
    }
    private void Exit()
    {
        onLadder = false;
        playerController.doGravity = true;
        playerController.canMove = true;
    }
}
