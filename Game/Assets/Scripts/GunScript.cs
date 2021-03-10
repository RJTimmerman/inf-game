using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunScript : MonoBehaviour  // De inspector voor dit script wordt geregeld in het GunScriptEditor script
{
    public float damage = 10;
    [Min(0)] public float range = 100;
    public bool automatic = false;
    [Min(0)] public float cooldown = 0.5f;
    public bool useMagazine = true;
    [Min(1)] public int magazineSize = 10;
    [Min(0)] public int bulletsInMag = 10;  // Deze waarde wordt alleen geruikt als er een magazijn wordt gebruikt
    [Min(0)] public float reloadTime = 3;  // Deze waarde wordt alleen geruikt als er een magazijn wordt gebruikt
    public bool infiniteBullets = false;
    [Min(0)] public int bulletPile = 100;  // De hoveelheid kogels die niet in het magazijn zitten

    public bool active = true;
    private bool reloading = false;
    public bool autoReload = true;  // Deze waarde wordt alleen geruikt als er een magazijn wordt gebruikt
    private float lastShotMoment;

    private Camera fpsCam;
    public GameObject hitEffect;
    private ParticleSystem muzzleFlash;
    private Animator reloadAnimation;
    private AudioSource audioPlayer;
    public AudioClip shootSound;  // Het geluid als je schiet
    public AudioClip emptySound;  // Het geluid als je schiet terwijl je geweer leeg is
    public AudioClip reloadSound;  // Het geluid bij het herladen

    private Transform gunHUD;
    private Transform ammoInfo;
    private TextMeshProUGUI magazineNumber;
    private Slider magazineBar;
    private TextMeshProUGUI totalAmmoCount;
    private GameObject crosshair;


    void Awake()
    {
        fpsCam = GetComponentInParent<Camera>();
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
        reloadAnimation = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();

        gunHUD = GameObject.Find("Gun HUD").transform;
        ammoInfo = gunHUD.Find("Ammo Info").transform;
        magazineNumber = ammoInfo.Find("Magazine Number").GetComponent<TextMeshProUGUI>();
        magazineBar = ammoInfo.GetComponent<Slider>();
        totalAmmoCount = ammoInfo.Find("Total Ammo Count").GetComponent<TextMeshProUGUI>();
        crosshair = gunHUD.Find("Crosshair Dot").gameObject;
    }

    void Start()
    {
        InitializeHUD();
    }
    public void InitializeHUD()
    {
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

        if (active) crosshair.SetActive(true);
        else crosshair.SetActive(false);
    }

    void Update()
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

    void Fire()
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
        Debug.Log(hit.transform.name);

        muzzleFlash.Play();  // TODO speel geluid af
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
}
