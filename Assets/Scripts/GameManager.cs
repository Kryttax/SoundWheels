using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<GameObject> vehiclesPrefab;

    private List<Vehicle> vehicles;
    private int currentVehicleIndex;

    [SerializeField]
    private Transform leftPivot, rightPivot, centerPivot;

    private float moveDuration = .5f;

    private bool isPassingVehicle = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        vehicles = new List<Vehicle>();

        foreach (GameObject vehicle in vehiclesPrefab)
        {
            GameObject newVehicle = Instantiate(vehicle);
            vehicles.Add(newVehicle.GetComponent<Vehicle>());
            newVehicle.GetComponent<Vehicle>().ActivateVehicle(false);
            newVehicle.GetComponent<Vehicle>().ShowVehicle(false);
        }

        currentVehicleIndex = 0;

        vehicles[currentVehicleIndex].transform.position = centerPivot.position;
        vehicles[currentVehicleIndex].ActivateVehicle(true);
        vehicles[currentVehicleIndex].ShowVehicle(true);
    }

    public void OnUILoaded()
    {
        SetUpVehicleName(vehicles[currentVehicleIndex]);
    }

    private void ShowVehicle(Vehicle vehicle, bool state)
    {
        vehicle.ShowVehicle(state);
    }

    private void SetUpVehicleName(Vehicle vehicle)
    {
        UIManager.instance.UpdateUI(vehicle.GetVehicleName());
    }

    private void EnableVehicle(Vehicle vehicle, bool state)
    {
        vehicle.ActivateVehicle(state);
    }

    public void OnClickLeft()
    {
        if(!isPassingVehicle)
        {
            isPassingVehicle = true;
            EnableVehicle(vehicles[currentVehicleIndex], false);
            StartCoroutine(MoveToSide(vehicles[currentVehicleIndex], rightPivot.position));
            currentVehicleIndex--;
            if (currentVehicleIndex < 0)
                currentVehicleIndex = vehicles.Count - 1;
            ShowVehicle(vehicles[currentVehicleIndex], true);
            SetUpVehicleName(vehicles[currentVehicleIndex]);
            StartCoroutine(MoveFromSide(vehicles[currentVehicleIndex], leftPivot.position));
        }
    }

    public void OnClickRight()
    {
        if(!isPassingVehicle)
        {
            isPassingVehicle = true;
            EnableVehicle(vehicles[currentVehicleIndex], false);
            StartCoroutine(MoveToSide(vehicles[currentVehicleIndex], leftPivot.position));
            currentVehicleIndex++;
            if (currentVehicleIndex == vehicles.Count)
                currentVehicleIndex = 0;
            ShowVehicle(vehicles[currentVehicleIndex], true);
            SetUpVehicleName(vehicles[currentVehicleIndex]);
            StartCoroutine(MoveFromSide(vehicles[currentVehicleIndex], rightPivot.position));
        }
    }

    public IEnumerator MoveFromSide(Vehicle vehicle, Vector3 initSide)
    {
        float timeElapsed = 0f;
        float t = 0f;
        while (timeElapsed < moveDuration)
        {
            t = timeElapsed / moveDuration;
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            vehicle.transform.position = Vector3.Lerp(initSide, centerPivot.position, t);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        vehicle.transform.position = centerPivot.position;
        EnableVehicle(vehicle, true);
        isPassingVehicle = false;
    }

    public IEnumerator MoveToSide(Vehicle vehicle, Vector3 targetSide)
    {
        float timeElapsed = 0f;
        float t = 0f;
        while (timeElapsed < moveDuration)
        {
            t = timeElapsed / moveDuration;
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            vehicle.transform.position = Vector3.Lerp(centerPivot.position, targetSide, t);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        vehicle.transform.position = targetSide;
        ShowVehicle(vehicle, false);
    }

    public void ExitGame() => Application.Quit();
}
