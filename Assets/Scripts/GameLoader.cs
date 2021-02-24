using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameLoader : MonoBehaviour
{
    [SerializeField]
    private Button startGameButton;

    private void Awake()
    {
        startGameButton.onClick.AddListener(() => { StartGame(); });
    }

    void StartGame()
    {
        SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
    }
}
