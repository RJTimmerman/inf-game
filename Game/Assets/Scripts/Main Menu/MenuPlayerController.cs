using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayerController : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField] private float speed = 12;
    [SerializeField] private float boostFactor = 1.5f;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        float boost = 1;  // 1 betekent geen boost, want het is een vermenigvuldiging
        if (Input.GetKey(KeyCode.LeftShift)) { boost = boostFactor; }  // Als de shift-knop ingedrukt is, activeer de boost; je kan alleen rennen als je op de grond staat
        Vector3 move = transform.right * xInput + transform.forward * zInput;  // Krijg de richting om te lopen
        if (move.magnitude > 1) { move = move.normalized; }  // Normaliseren wordt gedaan zodat je niet sneller loopt als je schuin gaat; normaliseer de vector alleen als de lengte meer is dan 1, anders blijf je langer doorlopen doordat de inputwaarde niet gelijk 0 is

        controller.Move(move * speed * boost * Time.deltaTime);  // Beweeg volgens de berekende richting, rekening houdend met de gekozen snelheid en eventuele boost (rennen)
    }
}
