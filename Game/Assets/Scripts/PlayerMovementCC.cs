using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementCC : MonoBehaviour  // CC staat voor Character Controller, omdat dit bewegingssysteem gebruik maakt van het Character Controller component, in tegenstelling tot het Rigidbody component
{
    public CharacterController controller;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float speed = 12;
    public float boostFactor = 1.5f;
    public float jumpHight = 3;
    public float gravity = Physics.gravity.y;

    [SerializeField] private Vector3 velocity;
    [SerializeField] private bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        float boost = 1;
        if (Input.GetKey(KeyCode.LeftShift) && zInput > 0) { boost = boostFactor; }  // Als de shift-knop ingedrukt is, activeer de boost; je kan alleen (schuin) naar voren rennen
        Vector3 move = (transform.right * xInput + transform.forward * zInput).normalized;  // Krijg de richting om te lopen

        controller.Move(move * speed * boost * Time.deltaTime);  // Beweeg volgens de berekende richting, rekening houdend met de gekozen snelheid en eventuele boost (rennen)
        if (Input.GetButtonDown("Jump") && isGrounded) { velocity.y = Mathf.Sqrt(jumpHight * -2 * gravity); }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        
        if (isGrounded && velocity.y < 0) { velocity.y = 0; }  // Verlaag deze waarde als de speler iets meer de grond in moet worden gedrukt
    }
}
