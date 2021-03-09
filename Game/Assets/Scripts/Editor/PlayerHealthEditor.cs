using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerHealth))]
[CanEditMultipleObjects()]
public class PlayerHealthEditor : Editor
{
    SerializedObject obj;
    SerializedProperty invincible;
    SerializedProperty health;

    public void OnEnable()
    {
        obj = new SerializedObject(target);
        invincible = obj.FindProperty("invincible");
        health = obj.FindProperty("health");
    }

    public override void OnInspectorGUI()
    {
        obj.Update();

        EditorGUILayout.PropertyField(invincible);
        if (!invincible.boolValue)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(health);
            EditorGUI.indentLevel--;
        }

        obj.ApplyModifiedProperties();
    }
}
