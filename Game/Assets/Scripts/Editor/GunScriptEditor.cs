using UnityEngine;
using UnityEditor;

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
    }
}
