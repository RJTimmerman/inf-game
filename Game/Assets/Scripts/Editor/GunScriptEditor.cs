using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

[CustomEditor(typeof(GunScript))]
[CanEditMultipleObjects()]
public class GunScriptEditor : Editor
{
    SerializedObject obj;
    SerializedProperty type, damage, range, automatic, cooldown, useMagazine, magazineSize, bulletsInMag, infiniteBullets, bulletPile, reloadTime, active, autoReload, hitEffect, shootSound, emptySound, reloadSound, relativePosition;

    public void OnEnable()
    {
        obj = new SerializedObject(target);
        type = obj.FindProperty("type");
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
        relativePosition = obj.FindProperty("relativePosition");
    }

    public override void OnInspectorGUI()
    {
        obj.Update();
        GunScript gunScript = (GunScript)target;

        EditorGUILayout.PropertyField(relativePosition);
        EditorGUILayout.PropertyField(type);
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

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(hitEffect);
        EditorGUILayout.PropertyField(shootSound);
        EditorGUILayout.PropertyField(active);

        obj.ApplyModifiedProperties();

        if (GUILayout.Button("Update HUD")) { gunScript.InitializeHUD();EditorApplication.QueuePlayerLoopUpdate(); } //UpdateHUD();
    }


    /*private Transform gunHUD, ammoInfo;
    private TextMeshProUGUI magazineNumber, totalAmmoCount;
    private Slider magazineBar;
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
    }*/
}
