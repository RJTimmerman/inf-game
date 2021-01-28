using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerRB : MonoBehaviour
{
    private Rigidbody playerRb;

    public float speed = 6;
    public float boostFactor = 1.5f;
    public Vector3 moveDirection = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        float boost = 1;
        if (Input.GetKey(KeyCode.LeftShift) && zInput > 0) { boost = boostFactor; }

        moveDirection = (transform.right * xInput + transform.forward * zInput).normalized * speed * boost;

        //playerRb.AddForce(direction * speed);
        // = new Vector3(x, playerRb.velocity.y, z);
    }

    private void FixedUpdate()
    {
        //playerRb.MovePosition(transform.position + moveDirection * speed);
        playerRb.velocity = moveDirection * Time.deltaTime + new Vector3(0, playerRb.velocity.y, 0);
    }
}
