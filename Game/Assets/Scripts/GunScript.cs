using UnityEngine;

public class GunScript : MonoBehaviour
{
    public float damage = 10;
    public float range = 100;

    public Camera fpsCam;
    public GameObject hitEffect;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            // Iets geraakt
            if (!hit.transform.CompareTag("Unshootable")) {
                Debug.Log(hit.transform.name);

                GameObject hEGO = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));  // (Hit Effect GameObject) activeer een particle effect als je iets raakt
                Destroy(hEGO, 0.5f);
            }
        }
    }
}
