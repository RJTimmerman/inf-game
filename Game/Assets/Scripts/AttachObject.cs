using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachObject : MonoBehaviour  // Dit kan worden aangepast om alleen bepaalde opjecten vast te kunnen maken
{
    private Dictionary<Collider, Transform> oldParents = new Dictionary<Collider, Transform>();

    void OnTriggerEnter(Collider other)
    {
        oldParents.Add(other, other.transform.parent);
        other.gameObject.transform.parent = transform.parent;
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.transform.parent = oldParents[other];
        oldParents.Remove(other);
    }
}
