using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GunScript))]
public class GunScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorUtility.SetDirty(target);
        GunScript gunScript = (GunScript)target;

        gunScript.active = EditorGUILayout.Toggle("Enabled", gunScript.active);
        gunScript.hitEffect = (GameObject)EditorGUILayout.ObjectField("Hit Particle Effect", gunScript.hitEffect, typeof(GameObject), false);
        gunScript.shootSound = (AudioClip)EditorGUILayout.ObjectField("Sound of Shot", gunScript.shootSound, typeof(AudioClip), false);
        gunScript.emptySound = (AudioClip)EditorGUILayout.ObjectField("Sound of Empty Shot", gunScript.emptySound, typeof(AudioClip), false);
        gunScript.reloadSound = (AudioClip)EditorGUILayout.ObjectField("Sound of Reload", gunScript.reloadSound, typeof(AudioClip), false);

        EditorGUILayout.Space();
        gunScript.damage = EditorGUILayout.FloatField("Damage", gunScript.damage);
        gunScript.range = Mathf.Max(EditorGUILayout.FloatField("Bullet Range", gunScript.range), 0);
        gunScript.automatic = EditorGUILayout.Toggle("Automatic Firing", gunScript.automatic);
        gunScript.cooldown = Mathf.Max(EditorGUILayout.FloatField("Shot Cooldown (s)", gunScript.cooldown), 0);
        gunScript.useMagazine = EditorGUILayout.Toggle("Use Magazines", gunScript.useMagazine);
        if (gunScript.useMagazine)
        {
            EditorGUI.indentLevel++;
            gunScript.magazineSize = Mathf.Max(EditorGUILayout.IntField("Magazine Size", gunScript.magazineSize), 1);
            gunScript.bulletsInMag = Mathf.Max(EditorGUILayout.IntField("Bullets in Magazine", gunScript.bulletsInMag), 0);
            gunScript.reloadTime = Mathf.Max(EditorGUILayout.FloatField("Reload Time (s)", gunScript.reloadTime), 0);
            gunScript.autoReload = EditorGUILayout.Toggle("Automatic Reloading", gunScript.autoReload);
            EditorGUI.indentLevel--;
        }
        GUILayout.BeginHorizontal();
        if (!gunScript.infiniteBullets)
        {
            gunScript.bulletPile = Mathf.Max(EditorGUILayout.IntField("Other Bullets", gunScript.bulletPile), 0);
        }
        gunScript.infiniteBullets = EditorGUILayout.Toggle("Infinite Bullets", gunScript.infiniteBullets);
        GUILayout.EndHorizontal();
    }
}
