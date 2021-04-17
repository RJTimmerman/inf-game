using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

using System;
using System.Reflection; // System en System.Reflection zijn voor SafeGradientValue

[CustomEditor(typeof(Enemy))]
[CanEditMultipleObjects()]
public class EnemyEditor : Editor
{
    SerializedObject obj;
    SerializedProperty invincible, maxHealth, health, colourOfHealthbar, destinationRange, keepPositionTime, sightRange, attackRange;


    public void OnEnable()
    {
        obj = new SerializedObject(target);
        invincible = obj.FindProperty("invincible");
        maxHealth = obj.FindProperty("maxHealth");
        health = obj.FindProperty("health");
        colourOfHealthbar = obj.FindProperty("colourOfHealthbar");
        destinationRange = obj.FindProperty("destinationRange");
        keepPositionTime = obj.FindProperty("keepPositionTime");
        sightRange = obj.FindProperty("sightRange");
        attackRange = obj.FindProperty("attackRange");
    }

    public override void OnInspectorGUI()
    {
        obj.Update();
        Enemy enemyScript = (Enemy)target;

        EditorGUILayout.PropertyField(invincible);
        if (!invincible.boolValue)
        {
            EditorGUILayout.PropertyField(maxHealth);
            EditorGUILayout.PropertyField(health);
        }
        EditorGUILayout.PropertyField(destinationRange);
        EditorGUILayout.PropertyField(keepPositionTime);
        EditorGUILayout.PropertyField(sightRange);
        EditorGUILayout.PropertyField(attackRange);
        EditorGUILayout.PropertyField(colourOfHealthbar);

        obj.ApplyModifiedProperties();

        if (GUILayout.Button("Update HUD")) enemyScript.InitializeCanvas();
    }
}
