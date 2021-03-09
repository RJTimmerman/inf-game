using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerCC : MonoBehaviour  // CC staat voor Character Controller, omdat dit bewegingssysteem gebruik maakt van het Character Controller component, in tegenstelling tot het Rigidbody component
{
    public CharacterController controller;
    private Transform groundCheck;
    public float groundDistance = 0.4f;
    private LayerMask groundMask;
    public LayerMask canStandOn;  // Zwaartekracht wordt tegengehouden door deze layers, maar je kan alleen springen en rennen op Ground
    public bool canMove = true;
    public bool doGravity = true;

    public float speed = 12;
    public float boostFactor = 1.5f;
    public float jumpHight = 3;
    public float gravity = Physics.gravity.y;

    public Vector3 velocity;
    public bool isGrounded;
    public bool standing;
    public Transform lastCheckpoint;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        groundCheck = transform.Find("GroundCheck");
        groundMask = LayerMask.GetMask("Ground");
        lastCheckpoint = GameObject.Find("Starting Point").transform;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        standing = Physics.CheckSphere(groundCheck.position, groundDistance, canStandOn);

        if (canMove)
        {
            float xInput = Input.GetAxis("Horizontal");
            float zInput = Input.GetAxis("Vertical");

            float boost = 1;
            if (Input.GetKey(KeyCode.LeftShift) && zInput > 0 && isGrounded) { boost = boostFactor; }  // Als de shift-knop ingedrukt is, activeer de boost; je kan alleen (schuin) vooruit rennen en als je op de grond staat
            Vector3 move = transform.right * xInput + transform.forward * zInput;  // Krijg de richting om te lopen
            if (move.magnitude > 1) { move = move.normalized; }  // Normaliseren wordt gedaan zodat je niet sneller loopt als je schuin gaat; normaliseer de vector alleen als de lengte meer is dan 1, anders blijf je langer doorlopen doordat de inputwaarde niet gelijk 0 is

            controller.Move(move * speed * boost * Time.deltaTime);  // Beweeg volgens de berekende richting, rekening houdend met de gekozen snelheid en eventuele boost (rennen)
            if (Input.GetButtonDown("Jump") && isGrounded) { velocity.y = Mathf.Sqrt(jumpHight * -2 * gravity); }
        }

        if (doGravity) { velocity.y += gravity * Time.deltaTime; }
        controller.Move(velocity * Time.deltaTime);
        
        if (standing && velocity.y < 0) { velocity.y = 0; }  // Verlaag deze waarde als de speler iets meer de grond in moet worden gedrukt
    }

    public void ReturnToCheckpoint()
    {
        transform.position = lastCheckpoint.position;
    }
}
