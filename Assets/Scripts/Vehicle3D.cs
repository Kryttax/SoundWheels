using UnityEngine;
using System.Collections;

public class Vehicle3D : MonoBehaviour
{
    [SerializeField]
    protected string vehicleName;
    [SerializeField]
    protected GameObject model;
    protected GameObject modelGenerated = null;
    [SerializeField]
    protected AudioClip vehicleSound;

    public void PlaySound()
    {
        GameManager.instance.SetSound(vehicleSound);
    }

    public void SetVehicleActive(bool status)
    {
        if (status)
        {
            GameManager.instance.SetSound(vehicleSound);
            if (modelGenerated == null)
                modelGenerated = Instantiate(model, this.transform);
      
                ShowVehicle(true);
        }
        else
            ShowVehicle(false);
    }

    private void ShowVehicle(bool state)
    {
        GetComponent<AnchorGameObject>().UpdateAnchor();
        gameObject.SetActive(state);
    }


    public string GetVehicleName() { return vehicleName; }
}
