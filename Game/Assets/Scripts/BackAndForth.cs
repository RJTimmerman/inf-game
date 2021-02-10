using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UIElements;

public class BackAndForth : MonoBehaviour
{
    public enum Axis { x, y, z };
    public Axis direction;
    private float startPos;
    public float distance;
    public float speed;
    public bool activated = true;

    // Start is called before the first frame update
    void Start()
    {
        switch (direction)
        {
            case Axis.x:
                startPos = transform.position.x;
                break;
            case Axis.y:
                startPos = transform.position.y;
                break;
            case Axis.z:
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
                case Axis.x:
                    transform.Translate(transform.right * speed * Time.deltaTime);
                    if (transform.position.x > startPos + distance && speed > 0 || transform.position.x < startPos && speed < 0) { speed = -speed; }
                    break;
                case Axis.y:
                    transform.Translate(transform.up * speed * Time.deltaTime);
                    if (transform.position.y > startPos + distance && speed > 0 || transform.position.y < startPos && speed < 0) { speed = -speed; }
                    break;
                case Axis.z:
                    transform.Translate(transform.forward * speed * Time.deltaTime);
                    if (transform.position.z > startPos + distance && speed > 0 || transform.position.z < startPos && speed < 0) { speed = -speed; }
                    break;
            }
        }
    }
}
