using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    public const string SCENE_MAIN_MENU = "MainMenu";
    public const string SCENE_GAME = "MainGame";
    public const string SCENE_CREDITS = "Credits";


    public static SettingsManager instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
            Destroy(this);
    }

    private void Start()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private bool isDarkTheme = false;

    public void ChangeTheme()
    {
        isDarkTheme = !isDarkTheme;

        if (isDarkTheme)
        {
            GlobalUIManager.instance.UpdateUIToDark();
        }
        else
        {
            GlobalUIManager.instance.UpdateUIToWhite();
        }
    }

    public void SyncTheme()
    {  
        if (isDarkTheme)
        {
            GlobalUIManager.instance.UpdateUIToDark(.0f);
        }
        else
        {
            GlobalUIManager.instance.UpdateUIToWhite(.0f);
        }
    }
    
    private void OnSceneChanged(Scene arg0, Scene arg1)
    {
        if(arg0.buildIndex != SceneManager.GetActiveScene().buildIndex)
        {
            SyncTheme();
            FadeEffect.instance.FadeOut();
        }
    }

    public void GoToScene(string sceneName = SCENE_MAIN_MENU) => SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

    public void ExitGame() => Application.Quit();
}
