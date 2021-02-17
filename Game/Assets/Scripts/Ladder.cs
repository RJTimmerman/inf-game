using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public GameObject playerObject;
    private PlayerMovementCC playerController;
    private AttachObject attachScript;
    public float speed = 6;

    private bool onLadder = false;

    // Start is called before the first frame update
    void Start()
    {
        playerController = playerObject.GetComponent<PlayerMovementCC>();
        attachScript = GetComponent<AttachObject>();
    }

    // Update is called once per frame
    void Update()
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onLadder = true;
            playerController.doGravity = false;
            playerController.canMove = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Exit();
        }
    }
    void Exit()
    {
        onLadder = false;
        playerController.doGravity = true;
        playerController.canMove = true;
    }
}
