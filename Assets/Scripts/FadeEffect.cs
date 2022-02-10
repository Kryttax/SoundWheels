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
        //StartCoroutine(Fade(0f, 1f, fadeImage));
    }

    public void FadeIn(Image customImage)
    {
        Color fixedColor = customImage.color;
        fixedColor.a = 1;
        customImage.color = fixedColor;
        customImage.CrossFadeAlpha(0f, 0f, true);

        customImage.CrossFadeAlpha(1f, fadeTime, false);
        //StartCoroutine(Fade(0f, 1f, customImage));
    }

    public void FadeOut()
    {
        Color fixedColor = fadeImage.color;
        fixedColor.a = 1;
        fadeImage.color = fixedColor;
        fadeImage.CrossFadeAlpha(1f, 0f, true);
        //fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1f);
        fadeImage.CrossFadeAlpha(0f, fadeTime, false);
        //StartCoroutine(Fade(1f, 0f, fadeImage));
    }

    public void FadeOut(Image customImage)
    {
        customImage.CrossFadeAlpha(0f, fadeTime, false);
        //StartCoroutine(Fade(1f, 0f, customImage));
    }

    private IEnumerator Fade(float from, float to, Image target)
    {
        float timeElapsed = 0f;
        float t = 0f;
        float totalTime = 1f;
        while (timeElapsed < totalTime)
        {
            t = timeElapsed / totalTime;
            t = t * t * t * (t * (6f * t - 15f) + 10f);

            target.CrossFadeAlpha(to, 1f, false);

            timeElapsed += Time.deltaTime;

            yield return null;
        }
        yield return null;
    }


}
