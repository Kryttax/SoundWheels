using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficRegulator : MonoBehaviour
{

    [SerializeField]
    private List<FrontVehicle> vehicles;

    private void Start()
    {
        StartCoroutine(StartTraffic());
    }

    private IEnumerator StartTraffic()
    {
        float timeStep;
        int randomCar;
        while (true)
        {
            timeStep = Random.Range(2f, 7f);
            yield return new WaitForSeconds(timeStep);
            randomCar = Random.Range(0, vehicles.Count);
            vehicles[randomCar].OnVehicleStart();
            yield return null;
        }

    }
}
