using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunScript : MonoBehaviour  // De inspector voor dit script wordt geregeld in het GunScriptEditor script
{
    public string type = "Normal";
    [SerializeField] private int damage = 10;
    [SerializeField] [Min(0)] private float range = 100, cooldown = 0.5f, reloadTime = 3;
    [SerializeField] private bool useMagazine = true, automatic = false, autoReload = true, infiniteBullets = false;
    [SerializeField] [Min(1)] private int magazineSize = 10;
    [SerializeField] [Min(0)] private int bulletsInMag = 10, bulletPile = 100;  // ...; De hoveelheid kogels die niet in het magazijn zitten
    public bool active = true;

    private bool reloading = false;
    private float lastShotMoment;

    private Camera fpsCam;
    [SerializeField] private GameObject hitEffect;
    private ParticleSystem muzzleFlash;
    private Animator reloadAnimation;
    private AudioSource audioPlayer;
    [SerializeField] private AudioClip shootSound, emptySound, reloadSound;  // Het geluid als je schiet; Het geluid als je schiet terwijl je geweer leeg is; Het geluid bij het herladen

    private Transform gunHUD, ammoInfo;
    private TextMeshProUGUI magazineNumber, totalAmmoCount;
    private Slider magazineBar;
    private GameObject crosshair;

    public Vector3 relativePosition;  // Zo weet elk geweer precies waar het moet zijn ten opzichte van de camera


    private void Awake()
    {
        fpsCam = GetComponentInParent<Camera>();
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
        reloadAnimation = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();
    }
    private void Start()
    {
        transform.localPosition = relativePosition;
        InitializeHUD();
    }
    public void InitializeHUD()
    {
        gunHUD = GameObject.Find("Gun HUD").transform;
        ammoInfo = gunHUD.Find("Ammo Info").transform;
        magazineNumber = ammoInfo.Find("Magazine Number").GetComponent<TextMeshProUGUI>();
        magazineBar = ammoInfo.GetComponent<Slider>();
        totalAmmoCount = ammoInfo.Find("Total Ammo Count").GetComponent<TextMeshProUGUI>();
        crosshair = gunHUD.Find("Crosshair Dot").gameObject;
        
        if (useMagazine)
        {
            magazineNumber.text = bulletsInMag.ToString();
            magazineBar.maxValue = magazineSize;
            magazineBar.value = bulletsInMag;
        }
        else
        {
            magazineNumber.text = "-";
            magazineBar.value = magazineBar.maxValue;
        }
        if (!infiniteBullets)
        {
            totalAmmoCount.text = bulletPile.ToString();
        }
        else
        {
            totalAmmoCount.text = "∞";
        }

        crosshair.SetActive(active);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && useMagazine && !reloading) StartCoroutine(Reload());

        if (active)
        {
            if (automatic) { if (Input.GetButton("Fire1")) 
                {
                    Fire();
            }   }
            else { if (Input.GetButtonDown("Fire1"))
                {
                    Fire();
            }   }
        }

        if (reloading)
        {
            magazineBar.value += magazineSize / reloadTime * Time.deltaTime;
        }
    }

    private void Fire()
    {
        if (!reloading && Time.time - lastShotMoment >= cooldown)
        {
            if ((useMagazine && bulletsInMag > 0) || (!useMagazine && (bulletPile > 0 || infiniteBullets)))
            {
                RaycastHit hit;
                if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
                {
                    // Raycast heeft iets opgeleverd
                    if (!hit.transform.CompareTag("Unshootable")) Shoot(hit);
                }
            }
            else
            {
                Debug.Log("Out of bullets!");
                //audioPlayer.PlayOneShot(emptySound);
            }
        }
    }
    private void Shoot(RaycastHit hit)
    {
        // Iets geraakt, doe damage
        //audioPlayer.PlayOneShot(shootSound);
        Debug.Log("Player shot " + hit.transform.name);
        if (hit.transform.CompareTag("Enemy")) DamageEnemy(hit.transform);

        muzzleFlash.Play();
        GameObject hEGO = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));  // (Hit Effect GameObject) activeer een particle effect als je iets raakt
        Destroy(hEGO, 0.5f);

        lastShotMoment = Time.time;
        if (useMagazine)
        {
            bulletsInMag--; HUDUpdateMag();
            if (bulletsInMag == 0 && autoReload) StartCoroutine(Reload());
        }
        else if (!infiniteBullets)
        {
            bulletPile--; HUDUpdatePile();
        }
    }

    private void DamageEnemy(Transform enemy)
    {
        enemy.GetComponent<Enemy>().TakeDamage(damage);
    }

    private IEnumerator Reload()
    {
        if (bulletPile > 0 || infiniteBullets)
        {
            Debug.Log("Reloading...");
            reloading = true;
            magazineNumber.text = "x";
            magazineBar.wholeNumbers = false; magazineBar.value = 0;
            // audioPlayer.PlayOneShot(reloadSound);  // TODO herlaad animatie

            yield return new WaitForSeconds(reloadTime);
            if (!infiniteBullets)
            {
                int bulletsBefore = bulletsInMag;
                bulletsInMag = Mathf.Min(magazineSize, bulletsBefore + bulletPile); HUDUpdateMag();
                bulletPile = Mathf.Max(bulletPile - (bulletsInMag - bulletsBefore), 0); HUDUpdatePile();
            }
            else
            {
                bulletsInMag = magazineSize; HUDUpdateMag();
            }
            reloading = false;
            magazineBar.wholeNumbers = true; //magazineBar.value = bulletsInMag;
        }
        else
        {
            Debug.Log("There are no more bullets!");
        }
    }

    private void HUDUpdateMag() { magazineNumber.text = bulletsInMag.ToString(); magazineBar.value = bulletsInMag; }
    private void HUDUpdatePile() { totalAmmoCount.text = bulletPile.ToString(); }


    public int GetAmmo()
    {
        return bulletPile + bulletsInMag;
    }
    public void SetAmmo(int amount)
    {
        bulletsInMag = Mathf.Min(magazineSize, amount);
        bulletPile = Mathf.Max(amount - bulletsInMag, 0);
    }
}
