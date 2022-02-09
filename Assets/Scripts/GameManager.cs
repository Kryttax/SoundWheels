using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<GameObject> vehiclesPrefab;

    private List<Vehicle3D> vehicles;
    private int currentVehicleIndex;

    [SerializeField]
    private Transform leftPivot, rightPivot, centerPivot;

    [SerializeField]
    protected AudioSource globalAudio;

    private float globalVolume = 0.5f;
    private float moveDuration = .5f;

    private bool isPassingVehicle = false;
    private bool isDarkTheme = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        vehicles = new List<Vehicle3D>();

        foreach (GameObject vehicle in vehiclesPrefab)
        {
            GameObject newVehicle = Instantiate(vehicle);
            vehicles.Add(newVehicle.GetComponent<Vehicle3D>());
            newVehicle.GetComponent<Vehicle3D>().GenerateVehicle();
            newVehicle.GetComponent<Vehicle3D>().SetVehicleActive(false);
        }

        globalAudio.volume = globalVolume;

        currentVehicleIndex = 0;

        setUpVehicle();
    }

    public void setUpVehicle()
    {
        centerVehicle(vehicles[currentVehicleIndex]);
        EnableVehicle(vehicles[currentVehicleIndex], true);
        vehicles[currentVehicleIndex].StartMoving();
    }

    public void centerVehicle(Vehicle3D vehicle)
    {
        vehicle.transform.position = centerPivot.position;
    }

    public void onScreenClicked()
    {
        PlayCurrentSound();
    }

    public void OnUILoaded()
    {
        SetUpVehicleName(vehicles[currentVehicleIndex]);
    }

    private void SetUpVehicleName(Vehicle3D vehicle)
    {
        UIManager.instance.UpdateUI(vehicle.GetVehicleName());
    }

    private void EnableVehicle(Vehicle3D vehicle, bool state)
    {
        vehicle.SetVehicleActive(state);
    }

    public void PlayCurrentSound()
    {
        vehicles[currentVehicleIndex].ExpandVehicle();
        globalAudio.Play();
    }

    public void StopCurrentSound()
    {
        globalAudio.Stop();
    }

    public void SetSound(AudioClip vehicleSound)
    {
        globalAudio.clip = vehicleSound;
    }

    public void OnClickLeft()
    {
        if(!isPassingVehicle)
        {
            StopCurrentSound();
            isPassingVehicle = true;
            StartCoroutine(MoveToSide(vehicles[currentVehicleIndex], rightPivot.position));
            currentVehicleIndex--;
            if (currentVehicleIndex < 0)
                currentVehicleIndex = vehicles.Count - 1;
            EnableVehicle(vehicles[currentVehicleIndex], true);
            SetUpVehicleName(vehicles[currentVehicleIndex]);
            StartCoroutine(MoveFromSide(vehicles[currentVehicleIndex], leftPivot.position));
        }
    }

    public void OnClickRight()
    {
        if(!isPassingVehicle)
        {
            StopCurrentSound();
            isPassingVehicle = true;

            StartCoroutine(MoveToSide(vehicles[currentVehicleIndex], leftPivot.position));
            currentVehicleIndex++;
            if (currentVehicleIndex == vehicles.Count)
                currentVehicleIndex = 0;

            EnableVehicle(vehicles[currentVehicleIndex], true);
            SetUpVehicleName(vehicles[currentVehicleIndex]);
            StartCoroutine(MoveFromSide(vehicles[currentVehicleIndex], rightPivot.position));
        }
    }

    public IEnumerator MoveFromSide(Vehicle3D vehicle, Vector3 initSide)
    {
        float timeElapsed = 0f;
        float t = 0f;
        while (timeElapsed < moveDuration)
        {
            t = timeElapsed / moveDuration;
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            vehicle.transform.position = Vector3.Lerp(initSide, centerPivot.position, t) + vehicle.GetComponent<AnchorGameObject>().currentAnchor;
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        vehicle.transform.position = centerPivot.position;
        EnableVehicle(vehicle, true);
        vehicle.StartMoving();
        isPassingVehicle = false;
    }

    public IEnumerator MoveToSide(Vehicle3D vehicle, Vector3 targetSide)
    {
        float timeElapsed = 0f;
        float t = 0f;
        while (timeElapsed < moveDuration)
        {
            t = timeElapsed / moveDuration;
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            vehicle.transform.position = (Vector3.Lerp(centerPivot.position, targetSide, t) + vehicle.GetComponent<AnchorGameObject>().currentAnchor);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        vehicle.transform.position = targetSide;
        vehicle.StopMoving();
        EnableVehicle(vehicle, false);
    }

    public void onChangeTheme()
    {
        isDarkTheme = !isDarkTheme;

        if(isDarkTheme)
        {
            UIManager.instance.UpdateButtonsToDark();
        }
        else
        {
            UIManager.instance.UpdateButtonsToWhite();
        }
    }

    public void ExitGame() => Application.Quit();
}
