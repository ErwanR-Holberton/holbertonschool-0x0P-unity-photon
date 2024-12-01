using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    private Text FadingText;

    void Start()
    {
        FadingText = GetComponent<Text>();
        Color originalColor = FadingText.color;
        FadingText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
    }

    void Update()
    {
        Color originalColor = FadingText.color;
        FadingText.color = new Color(originalColor.r, originalColor.g, originalColor.b, originalColor.a - 0.001f);
    }

    public void ResetFade()
    {
        Color originalColor = FadingText.color;
        FadingText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);
    }
}
