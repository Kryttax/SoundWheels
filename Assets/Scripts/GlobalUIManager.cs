using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GlobalUIManager : MonoBehaviour
{
    public static GlobalUIManager instance;

    public enum BUTTON_DIRECTION { LEFT, RIGHT }

    [SerializeField]
    private Button changeThemeButton;
    [SerializeField]
    private Button backButton;
    [SerializeField]
    private Button exitGameButton;

    [SerializeField]
    private Color darkColor;
    [SerializeField]
    private Color whiteColor;

    private const float updateTime = 1.5f;

    public Color DarkColor { get => darkColor; set => darkColor = value; }
    public Color WhiteColor { get => whiteColor; set => whiteColor = value; }

    private void Awake()
    {
        if (instance == null)
            instance = this;

        changeThemeButton.onClick.AddListener(() => { SettingsManager.instance.ChangeTheme(); });
        if(backButton)
            backButton.onClick.AddListener(() => { StartCoroutine(FadeAndGo(SettingsManager.SCENE_MAIN_MENU)); });
        exitGameButton.onClick.AddListener(() => { SettingsManager.instance.ExitGame(); });
    }

    public IEnumerator FadeAndGo(string scene)
    {
        FadeEffect.instance.FadeIn();
        yield return new WaitForSeconds(FadeEffect.instance.FadeTime + 0.5f);
        SettingsManager.instance.GoToScene(scene);
        StopAllCoroutines();
    }

    public void UpdateUIToWhite(float customTime = updateTime)
    {
        if (customTime <= .0f)
        {
            FadeEffect.instance.FadeImage.color = new Color(whiteColor.r, whiteColor.g, whiteColor.b, FadeEffect.instance.FadeImage.color.a);
            Camera.main.backgroundColor = whiteColor;

            GameObject[] customUIElements = GameObject.FindGameObjectsWithTag("Colorable");
            for (int i = 0; i < customUIElements.Length; i++)
            {
                if (customUIElements[i].GetComponent<Image>() != null)
                    customUIElements[i].GetComponent<Image>().color = darkColor;
                else if (customUIElements[i].GetComponent<TextMeshProUGUI>() != null)
                    customUIElements[i].GetComponent<TextMeshProUGUI>().color = darkColor;
            }
        }
        else
            StartCoroutine(UpdateUIColor(darkColor, whiteColor, customTime));
    }

    public void UpdateUIToDark(float customTime = updateTime)
    {
        if(customTime <= .0f)
        {
            FadeEffect.instance.FadeImage.color = new Color(darkColor.r, darkColor.g, darkColor.b, FadeEffect.instance.FadeImage.color.a);
            Camera.main.backgroundColor = darkColor;

            GameObject[] customUIElements = GameObject.FindGameObjectsWithTag("Colorable");
            for (int i = 0; i < customUIElements.Length; i++)
            {
                if (customUIElements[i].GetComponent<Image>() != null)
                    customUIElements[i].GetComponent<Image>().color = whiteColor;
                else if (customUIElements[i].GetComponent<TextMeshProUGUI>() != null)
                    customUIElements[i].GetComponent<TextMeshProUGUI>().color = whiteColor;
            }
        }
        else
        {
            StartCoroutine(UpdateUIColor(whiteColor, darkColor, customTime));
        }      
    }

    private IEnumerator UpdateUIColor(Color from, Color to, float totalTime)
    {
        // Inverted colors (white background -- dark icons / dark background -- white icons)

        if (totalTime <= 0.00f)
            totalTime = 0.1f;

        FadeEffect.instance.FadeImage.color = new Color(to.r, to.g, to.b, FadeEffect.instance.FadeImage.color.a);

        float timeElapsed = 0f;
        float t = 0f;
        while (timeElapsed < totalTime)
        {
            t = timeElapsed / totalTime;
            t = t * t * t * (t * (6f * t - 15f) + 10f);

            Camera.main.backgroundColor = Color.Lerp(from, to, t);          

            GameObject[] customUIElements = GameObject.FindGameObjectsWithTag("Colorable");
            for (int i = 0; i < customUIElements.Length; i++)
            {
                if (customUIElements[i].GetComponent<Image>() != null)
                    customUIElements[i].GetComponent<Image>().color = Color.Lerp(to, from, t);
                else if (customUIElements[i].GetComponent<TextMeshProUGUI>() != null)
                    customUIElements[i].GetComponent<TextMeshProUGUI>().color = Color.Lerp(to, from, t);
            }

            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}