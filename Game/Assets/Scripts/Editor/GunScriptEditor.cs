using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

[CustomEditor(typeof(GunScript))]
[CanEditMultipleObjects()]
public class GunScriptEditor : Editor
{
    SerializedObject obj;
    SerializedProperty damage;
    SerializedProperty range;
    SerializedProperty automatic;
    SerializedProperty cooldown;
    SerializedProperty useMagazine;
    SerializedProperty magazineSize;
    SerializedProperty bulletsInMag;
    SerializedProperty infiniteBullets;
    SerializedProperty bulletPile;
    SerializedProperty reloadTime;
    SerializedProperty active;
    SerializedProperty autoReload;
    SerializedProperty hitEffect;
    SerializedProperty shootSound;
    SerializedProperty emptySound;
    SerializedProperty reloadSound;

    public void OnEnable()
    {
        obj = new SerializedObject(target);
        damage = obj.FindProperty("damage");
        range = obj.FindProperty("range");
        automatic = obj.FindProperty("automatic");
        cooldown = obj.FindProperty("cooldown");
        useMagazine = obj.FindProperty("useMagazine");
        magazineSize = obj.FindProperty("magazineSize");
        bulletsInMag = obj.FindProperty("bulletsInMag");
        infiniteBullets = obj.FindProperty("infiniteBullets");
        bulletPile = obj.FindProperty("bulletPile");
        reloadTime = obj.FindProperty("reloadTime");
        active = obj.FindProperty("active");
        autoReload = obj.FindProperty("autoReload");
        hitEffect = obj.FindProperty("hitEffect");
        shootSound = obj.FindProperty("shootSound");
        emptySound = obj.FindProperty("emptySound");
        reloadSound = obj.FindProperty("reloadSound");
    }

    public override void OnInspectorGUI()
    {
        obj.Update();

        EditorGUILayout.PropertyField(hitEffect);
        EditorGUILayout.PropertyField(shootSound);
        EditorGUILayout.PropertyField(active);

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(damage);
        EditorGUILayout.PropertyField(range);
        EditorGUILayout.PropertyField(automatic);
        EditorGUILayout.PropertyField(cooldown);
        EditorGUILayout.PropertyField(useMagazine);
        if (useMagazine.boolValue)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(magazineSize);
            EditorGUILayout.PropertyField(bulletsInMag);
            EditorGUILayout.PropertyField(reloadTime);
            EditorGUILayout.PropertyField(autoReload);
            EditorGUILayout.PropertyField(reloadSound);
            EditorGUI.indentLevel--;
        }
        if (!infiniteBullets.boolValue)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(bulletPile);
            EditorGUILayout.PropertyField(infiniteBullets);
            GUILayout.EndHorizontal();
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(emptySound);
            EditorGUI.indentLevel--;
        }
        else
        {
            EditorGUILayout.PropertyField(infiniteBullets);
        }

        obj.ApplyModifiedProperties();

        if (GUILayout.Button("Update HUD")) UpdateHUD();
    }


    private Transform gunHUD;
    private Transform ammoInfo;
    private TextMeshProUGUI magazineNumber;
    private Slider magazineBar;
    private TextMeshProUGUI totalAmmoCount;
    private GameObject crosshair;


    void Awake()
    {
        gunHUD = GameObject.Find("Gun HUD").transform;
        ammoInfo = gunHUD.Find("Ammo Info").transform;
        magazineNumber = ammoInfo.Find("Magazine Number").GetComponent<TextMeshProUGUI>();
        magazineBar = ammoInfo.GetComponent<Slider>();
        totalAmmoCount = ammoInfo.Find("Total Ammo Count").GetComponent<TextMeshProUGUI>();
        crosshair = gunHUD.Find("Crosshair Dot").gameObject;
    }

    private void UpdateHUD()
    {
        if (useMagazine.boolValue)
        {
            magazineNumber.text = bulletsInMag.intValue.ToString();
            magazineBar.maxValue = magazineSize.intValue;
            magazineBar.value = bulletsInMag.intValue;
        }
        else
        {
            magazineNumber.text = "-";
            magazineBar.value = magazineBar.maxValue;
        }
        if (!infiniteBullets.boolValue)
        {
            totalAmmoCount.text = bulletPile.intValue.ToString();
        }
        else
        {
            totalAmmoCount.text = "∞";
        }

        if (active.boolValue) crosshair.SetActive(true);
        else crosshair.SetActive(false);

        EditorApplication.QueuePlayerLoopUpdate();
    }
}
