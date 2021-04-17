using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ScaleFloor : MonoBehaviour
{
    private new RectTransform transform;
    private BoxCollider floorCollider;


    private void Awake()
    {
        transform = GetComponent<RectTransform>();
        floorCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        floorCollider.size = new Vector3(transform.rect.width, transform.rect.height, floorCollider.size.z);
    }
}
