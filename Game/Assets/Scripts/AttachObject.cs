using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachObject : MonoBehaviour  // Dit kan worden aangepast om alleen bepaalde opjecten vast te kunnen maken
{
    private GameObject attachHere;
    private Dictionary<Collider, Transform> oldParents = new Dictionary<Collider, Transform>();

    //public bool playerOnly = false;
    public LayerMask canAttach;

    
    void Start()
    {
        attachHere = new GameObject("Attached");
        attachHere.transform.parent = transform.parent;
        attachHere.transform.localPosition = Vector3.zero; attachHere.transform.localRotation = Quaternion.Euler(Vector3.zero);
        attachHere.transform.localScale = new Vector3(1 / transform.parent.localScale.x, 1 / transform.parent.localScale.y, 1 / transform.parent.localScale.z);
    }

    void OnTriggerEnter(Collider other)
    {
        //if (!playerOnly || other.name == "Player")
        if ((canAttach.value & (1 << other.gameObject.layer)) > 0)
        {
            oldParents.Add(other, other.transform.parent);
            other.gameObject.transform.parent = attachHere.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //if (!playerOnly || other.name == "Player")
        if ((canAttach.value & (1 << other.gameObject.layer)) > 0)
        {
            other.gameObject.transform.parent = oldParents[other];
            oldParents.Remove(other);
        }
    }
}
