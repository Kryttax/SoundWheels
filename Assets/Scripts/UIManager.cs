using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public enum BUTTON_DIRECTION { LEFT, RIGHT }

    [SerializeField]
    private TextMeshProUGUI vehicleName;
    [SerializeField]
    private Button leftButton, rightButton;
    [SerializeField]
    private Button changeThemeButton;
    [SerializeField]
    private Button exitGameButton;

    [SerializeField]
    private Color darkColor;
    [SerializeField]
    private Color whiteColor;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        leftButton.onClick.AddListener(() => { GameManager.instance.OnClickLeft(); });
        rightButton.onClick.AddListener(() => { GameManager.instance.OnClickRight(); });
        changeThemeButton.onClick.AddListener(() => { GameManager.instance.onChangeTheme(); });
        exitGameButton.onClick.AddListener(() => { GameManager.instance.ExitGame(); });

        GameManager.instance.OnUILoaded();
    }

    public void UpdateUI(string newName)
    {
        vehicleName.text = newName;
    }

    public void UpdateButtonsToWhite()
    {
        StartCoroutine(UpdateUITo(darkColor, whiteColor));
    }

    public void UpdateButtonsToDark()
    {
        StartCoroutine(UpdateUITo(whiteColor, darkColor));
    }

    private IEnumerator UpdateUITo(Color initColor, Color endColor)
    {
        float timeElapsed = 0f;
        float t = 0f;
        float totalTime = 1.4f;
        while (timeElapsed < totalTime)
        {
            t = timeElapsed / totalTime;
            t = t * t * t * (t * (6f * t - 15f) + 10f);

            vehicleName.color = Color.Lerp(endColor, initColor, t);

            leftButton.transform.GetComponentsInChildren<Image>()[1].color = Color.Lerp(endColor, initColor, t);
            rightButton.transform.GetComponentsInChildren<Image>()[1].color = Color.Lerp(endColor, initColor, t);
            changeThemeButton.transform.GetComponent<Image>().color = Color.Lerp(endColor, initColor, t);
            exitGameButton.GetComponent<Image>().color = Color.Lerp(endColor, initColor, t);

            Camera.main.backgroundColor = Color.Lerp(initColor, endColor, t);

            timeElapsed += Time.deltaTime;

            yield return null;
        }
        
    }

    //public void OnClickButton(BUTTON_DIRECTION dir)
    //{

    //    switch (dir)
    //    {
    //        case BUTTON_DIRECTION.LEFT:
    //            GameManager.OnClickLeft();
    //            break;
    //        case BUTTON_DIRECTION.RIGHT:
    //            GameManager.OnClickRight();
    //            break;
    //        default:
    //            Debug.LogError("Button Direction not properly defined!");
    //            break;
    //    }
    //}
}