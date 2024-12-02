using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheckName : MonoBehaviour
{
    public Fade FadeScript;
    public InputField NameInputField;

    public void CheckInputAndProceed()
    {
        if (string.IsNullOrWhiteSpace(NameInputField.text) || NameInputField.text.Length < 5)
            FadeScript.ResetFade();
        else
        {
            PlayerPrefs.SetString("Username", NameInputField.text);
            SceneManager.LoadScene("Scene_A");
        }
    }

}
