using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance = null;

    [SerializeField]
    private Button startGameButton;

    [SerializeField]
    private Button creditsButton;

    [SerializeField]
    private FrontVehicle vehicle;

    [SerializeField]
    private AudioClip vehicleStartSound;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
        startGameButton.onClick.AddListener(() => { StartVehicle(); });
        creditsButton.onClick.AddListener(() => { StartCoroutine(GlobalUIManager.instance.FadeAndGo(SettingsManager.SCENE_CREDITS)); });

        GetComponent<AudioSource>().clip = vehicleStartSound;
    }

    private void StartVehicle()
    {
        startGameButton.onClick.RemoveAllListeners();
        vehicle.OnVehicleStart();
        GetComponent<AudioSource>().Play();
    }

    public void StartGame()
    {
        SettingsManager.instance.GoToScene(SettingsManager.SCENE_GAME);
    }
}
