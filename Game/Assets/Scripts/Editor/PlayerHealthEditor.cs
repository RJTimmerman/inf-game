using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

using System;
using System.Reflection; // System en System.Reflection zijn voor SafeGradientValue

[CustomEditor(typeof(PlayerHealth))]
[CanEditMultipleObjects()]
public class PlayerHealthEditor : Editor
{
    SerializedObject obj;
    SerializedProperty invincible;
    SerializedProperty maxHealth;
    SerializedProperty health;
    SerializedProperty colourOfHealthbar;


    public void OnEnable()
    {
        obj = new SerializedObject(target);
        invincible = obj.FindProperty("invincible");
        maxHealth = obj.FindProperty("maxHealth");
        health = obj.FindProperty("health");
        colourOfHealthbar = obj.FindProperty("colourOfHealthbar");
    }

    public override void OnInspectorGUI()
    {
        obj.Update();

        EditorGUILayout.PropertyField(invincible);
        if (!invincible.boolValue)
        {
            EditorGUILayout.PropertyField(maxHealth);
            EditorGUILayout.PropertyField(health);
        }
        EditorGUILayout.PropertyField(colourOfHealthbar);

        obj.ApplyModifiedProperties();

        if (GUILayout.Button("Update HUD")) UpdateHUD();
    }


    private Transform playerHUD;
    private Transform healthHUD;
    private TextMeshProUGUI healthNumber;
    private Slider healthBar;
    private Image barFiller;


    void Awake()
    {
        playerHUD = GameObject.Find("Player HUD").transform;
        healthHUD = playerHUD.Find("Health HUD").transform;
        healthNumber = healthHUD.Find("Health Number").GetComponent<TextMeshProUGUI>();
        healthBar = healthHUD.GetComponent<Slider>();
        barFiller = healthBar.transform.Find("Data Bar").GetComponent<Image>();
    }

    public void UpdateHUD()
    {
        if (!invincible.boolValue)
        {
            healthNumber.text = health.floatValue.ToString();
            healthBar.maxValue = maxHealth.floatValue;
            healthBar.value = health.floatValue;
        }
        else
        {
            healthNumber.text = "∞";
            healthBar.value = healthBar.maxValue;
        }

        barFiller.color = SafeGradientValue(colourOfHealthbar).Evaluate(health.floatValue / maxHealth.floatValue);


        EditorApplication.QueuePlayerLoopUpdate();  // Dit is omdat de text van healthNumber niet direct veranderde in het beeld, ook al was de waarde in het TextMeshPro component wel gewoon veranderd. Deze functie dwingt Unity om de verandering direct te vinden in plaats van pas wanneer er weer wat anders gebeurt.
    }


    static Gradient SafeGradientValue(SerializedProperty sp)  // Deze code komt van https://gist.github.com/capnslipp/8516384 en is geschreven door Slipp Douglas Thompson. Het staat hier omdat een ingebouwde SerializedProperty.gradientValue niet bestaat.
    {
        BindingFlags instanceAnyPrivacyBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        PropertyInfo propertyInfo = typeof(SerializedProperty).GetProperty(
            "gradientValue",
            instanceAnyPrivacyBindingFlags,
            null,
            typeof(Gradient),
            new Type[0],
            null
        );
        if (propertyInfo == null)
            return null;

        Gradient gradientValue = propertyInfo.GetValue(sp, null) as Gradient;
        return gradientValue;
    }
}
