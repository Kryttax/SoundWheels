using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    public static FadeEffect instance = null;
    [SerializeField]
    private Image fadeImage;

    [SerializeField]
    private float fadeTime;

    public float FadeTime { get => fadeTime; set => fadeTime = value; }
    public Image FadeImage { get => fadeImage; set => fadeImage = value; }

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;

        if (!fadeImage.gameObject.activeSelf)
            fadeImage.gameObject.SetActive(true);
    }

    public void FadeIn()
    {
        fadeImage.raycastTarget = true;
        Color fixedColor = fadeImage.color;
        fixedColor.a = 1;
        fadeImage.color = fixedColor;
        fadeImage.CrossFadeAlpha(0f, 0f, true);
        fadeImage.CrossFadeAlpha(1f, fadeTime, false);
    }

    public void FadeIn(Image customImage)
    {
        Color fixedColor = customImage.color;
        fixedColor.a = 1;
        customImage.color = fixedColor;
        customImage.CrossFadeAlpha(0f, 0f, true);

        customImage.CrossFadeAlpha(1f, fadeTime, false);
    }

    public void FadeOut()
    {
        Color fixedColor = fadeImage.color;
        fixedColor.a = 1;
        fadeImage.color = fixedColor;
        fadeImage.CrossFadeAlpha(1f, 0f, true);
        fadeImage.CrossFadeAlpha(0f, fadeTime, false);
    }

    public void FadeOut(Image customImage)
    {
        customImage.CrossFadeAlpha(0f, fadeTime, false);
    }
}
