using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextInputExample : MonoBehaviour
{
    private TMP_InputField inputField;
    public UIOnClick inputUI;


    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onSubmit.AddListener(DoSomething);
    }

    void DoSomething(string text)
    {
        if (inputField.wasCanceled) { return; }  // Stop uitvoering als het inputscherm was gesloten met escape
        // Doe iets wanneer tekst is ingevoerd
        Debug.Log(text);

        inputUI.CloseUI();
    }
}
