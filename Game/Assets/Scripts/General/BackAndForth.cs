using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForth : MonoBehaviour
{
    private enum Axis { X, Y, Z };
    [SerializeField] private Axis direction;
    private float startPos;
    [SerializeField] private float distance;
    [SerializeField] private float speed;
    [SerializeField] private bool activated = true;

    
    void Start()
    {
        switch (direction)
        {
            case Axis.X:
                startPos = transform.position.x;
                break;
            case Axis.Y:
                startPos = transform.position.y;
                break;
            case Axis.Z:
                startPos = transform.position.z;
                break;
        }
    }

    void FixedUpdate()
    {
        if (activated)
        {
            switch (direction)
            {
                case Axis.X:
                    transform.Translate(transform.right * speed * Time.deltaTime);
                    if (transform.position.x > startPos + distance && speed > 0 || transform.position.x < startPos && speed < 0) speed = -speed;
                    break;
                case Axis.Y:
                    transform.Translate(transform.up * speed * Time.deltaTime);
                    if (transform.position.y > startPos + distance && speed > 0 || transform.position.y < startPos && speed < 0) speed = -speed;
                    break;
                case Axis.Z:
                    transform.Translate(transform.forward * speed * Time.deltaTime);
                    if (transform.position.z > startPos + distance && speed > 0 || transform.position.z < startPos && speed < 0) speed = -speed;
                    break;
            }
        }
    }
}
