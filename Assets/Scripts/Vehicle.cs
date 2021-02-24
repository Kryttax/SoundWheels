using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [SerializeField]
    protected string vehicleName;
    [SerializeField]
    protected SpriteRenderer vehicleImg;
    [SerializeField]
    protected AudioSource vehicleSound;
    [SerializeField]
    protected Color backgroundColor;

    public float volume = 0.5f;

    public void PlaySound()
    {
        vehicleSound.volume = volume;
        vehicleSound.Play();
    }

    public void ActivateVehicle(bool state)
    {
        GetComponent<BoxCollider>().enabled = state;
    }

    public void ShowVehicle(bool state)
    {
        gameObject.SetActive(state);
    }


    public string GetVehicleName() { return vehicleName; }
}
