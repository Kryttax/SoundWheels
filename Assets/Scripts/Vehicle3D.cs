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

    private Vector3 dummyInitialPosition = new Vector3(50, 50);
    private Quaternion initialRotation;

    private void Awake()
    {
        initialRotation = transform.rotation;
    }

    public void PlaySound()
    {
        GameManager.instance.SetSound(vehicleSound);
    }

    public void ExpandVehicle()
    {
        StartCoroutine(ExpandAndShrink());
    }

    IEnumerator ExpandAndShrink()
    {
        // animate from small to large, and dont move on until its done
        yield return animateScale(1.35f * Vector3.one);
        // animate large to small, and dont move on until it's done
        yield return animateScale(1f * Vector3.one);
    }

    private IEnumerator animateScale(Vector3 endSize)
    {
        bool done = false;
        Vector3 vel = Vector3.zero;
        while (!done)
        {
            // lerp the scale from current to end over time
            transform.localScale = Vector3.SmoothDamp(transform.localScale, endSize, ref vel, .1f);

            // get the distance between the current scale and the end scale
            float distance = Vector3.Distance(transform.localScale, endSize);

            // if the current scale is within 0.1 of the end scale... close enough!
            if (distance <= 0.1)
            {
                // now were done
                done = true;
            }

            // wait a frame then continue from here
            yield return null;
        }
    }


    public void GenerateVehicle()
    {
        gameObject.transform.position = dummyInitialPosition;
        modelGenerated = Instantiate(model, this.transform);
    }

    public void SetVehicleActive(bool status)
    {
        if (status)
        {
            GameManager.instance.SetSound(vehicleSound);
            if (modelGenerated == null)
                GenerateVehicle();

            ShowVehicle(true);
        }
        else
            ShowVehicle(false);
    }

    private void ShowVehicle(bool state)
    {
        GetComponent<AnchorGameObject>().UpdateAnchor();
        gameObject.transform.rotation = initialRotation;
        gameObject.SetActive(state);
    }

    public void StartMoving()
    {
        StartCoroutine(ContinuousRotation());
    }

    IEnumerator ContinuousRotation()
    {
        while (true)
        {
            gameObject.transform.RotateAround(transform.position, Vector3.up, 2);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void StopMoving()
    {
        StopCoroutine(ContinuousRotation());
    }


    public string GetVehicleName() { return vehicleName; }
}
