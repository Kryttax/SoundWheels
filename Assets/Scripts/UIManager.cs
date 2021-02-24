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
    private Button exitGameButton;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        leftButton.onClick.AddListener(() => { GameManager.instance.OnClickLeft(); });
        rightButton.onClick.AddListener(() => { GameManager.instance.OnClickRight(); });
        exitGameButton.onClick.AddListener(() => { GameManager.instance.ExitGame(); });

        GameManager.instance.OnUILoaded();
    }

    public void UpdateUI(string newName)
    {
        vehicleName.text = newName;
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