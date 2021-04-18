using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class MainMenuDeleteSave : MainMenuButton
{
    private TextMeshProUGUI deletedText;


    private new void Awake()
    {
        base.Awake();
        deletedText = GameObject.Find("Save Deleted Text").GetComponent<TextMeshProUGUI>();
        deletedText.CrossFadeAlpha(0, 0, true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            background.CrossFadeAlpha(3, 0.2f, true);
            ActivateButton();
        }
    }

    private void ActivateButton()
    {
        SaveSystem.DeleteSave();

        deletedText.CrossFadeAlpha(1, 1, true);
    }
}
