using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameUIManager : MonoBehaviour
{
    public static GameUIManager instance;

    public enum BUTTON_DIRECTION { LEFT, RIGHT }

    [SerializeField]
    private Button leftButton, rightButton;
    [SerializeField]
    private TextMeshProUGUI vehicleName;

    public void Init()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;

        if (leftButton)
            leftButton.onClick.AddListener(() => { GameManager.instance.OnClickLeft(); });
        if (rightButton)
            rightButton.onClick.AddListener(() => { GameManager.instance.OnClickRight(); });
        if (vehicleName)
            UpdateVehicleName(GameManager.instance.GetCurrentVehicle().GetVehicleName());
    }

    public void UpdateVehicleName(string newName)
    {
        vehicleName.text = newName;
    }
}