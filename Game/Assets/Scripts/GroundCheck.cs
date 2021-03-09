using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GroundCheck : MonoBehaviour
{
    private PlayerControllerCC controller;
    public LayerMask[] exceptions;


    private void Awake()
    {
        controller = GetComponentInParent<PlayerControllerCC>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Toggle(other.gameObject.layer);
    }

    private void OnTriggerExit(Collider other)
    {
        Toggle(other.gameObject.layer);
    }
    
    private void Toggle(LayerMask colliderLayer)
    {
        //if (!exceptions.Contains<LayerMask>(colliderLayer)) {
            controller.isGrounded = !controller.isGrounded;
        //}
    }
}
