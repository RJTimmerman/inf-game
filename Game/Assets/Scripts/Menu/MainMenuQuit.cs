using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuQuit : MainMenuButton
{
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
        Application.Quit();
    }
}
