using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance = null;

    [SerializeField]
    private Button startGameButton;

    [SerializeField]
    private Button creditsButton;

    [SerializeField]
    private FrontVehicle vehicle;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
        startGameButton.onClick.AddListener(() => { StartVehicle(); });
        creditsButton.onClick.AddListener(() => { StartCoroutine(GlobalUIManager.instance.FadeAndGo(SettingsManager.SCENE_CREDITS)); });
    }

    private void StartVehicle()
    {
        startGameButton.onClick.RemoveAllListeners();
        //FadeEffect.instance.FadeOut(startGameButton.GetComponent<Image>());
        vehicle.OnVehicleStart();
    }

    public void StartGame()
    {
        SettingsManager.instance.GoToScene(SettingsManager.SCENE_GAME);
    }
}
