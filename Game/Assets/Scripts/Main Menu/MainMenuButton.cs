using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour
{
    protected Image background;


    private void Awake()
    {
        background = GetComponentInChildren<Image>();
    }

    private void OnTriggerEnter(Collider other)
    {
        background.CrossFadeAlpha(2, 0.5f, true);
    }

    private void OnTriggerExit(Collider other)
    {
        background.CrossFadeAlpha(1, 0.5f, true);
    }
}
