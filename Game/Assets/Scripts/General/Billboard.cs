using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private Transform theCamera;

    
    private void Awake()
    {
        theCamera = GameObject.Find("First Person Camera").transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(theCamera);
        transform.Rotate(0, 180, 0);  // Een canvas moet je in de richting van de positieve Z-as bekijken, dan moet deze dus niet recht naar de camera wijzen
    }
}
