using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuLoadScene : MainMenuButton
{
    [SerializeField] private string scene;
    

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
        SceneManager.LoadScene(scene);
    }
}
