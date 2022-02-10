using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontVehicle : MonoBehaviour
{
     [SerializeField]
    private float movementDuration;

    [SerializeField]
    private Transform targetPosition;

    [SerializeField]
    private bool isMainCar = false;

    private Vector3 initialPosition;
    private Vector3 initialScale;
    private bool isMoving = false;

    private void Start()
    {
        initialScale = transform.localScale;
        initialPosition = transform.position;
    }

    public void OnVehicleStart()
    {
        if(!isMoving)
        {
            StartCoroutine(StartEngine());
            StartCoroutine(GoToDestination());
        }
    }

    private IEnumerator StartEngine()
    {
        while(true)
        {
            float amount = Random.Range(initialScale.x, initialScale.x + 0.175f);
            // animate from small to large, and dont move on until its done
            yield return animateScale(amount * Vector3.one);
            // animate large to small, and dont move on until it's done
            yield return animateScale(initialScale.x * Vector3.one);
        }
    }

    private IEnumerator animateScale(Vector3 endSize)
    {
        bool done = false;
        Vector3 vel = Vector3.zero;
        while (!done)
        {
            // lerp the scale from current to end over time
            transform.localScale = Vector3.SmoothDamp(transform.localScale, endSize, ref vel, .075f);

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


    private IEnumerator MoveToTarget(Vector3 target, float duration)
    {
        isMoving = true;

        float timeElapsed = 0f;
        float t = 0f;
        Vector3 targetNorm = target;
        while (timeElapsed < duration)
        {
            t = timeElapsed / duration;
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            transform.position = Vector3.Lerp(initialPosition, targetNorm, t);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = target;
    }

    private IEnumerator GoToDestination()
    {
        yield return new WaitForSeconds(1f);
        yield return MoveToTarget(targetPosition.position, movementDuration);

        yield return OnArrival();
    }

    private IEnumerator OnArrival()
    {
        if (isMainCar)
        {
            FadeEffect.instance.FadeIn();
            yield return new WaitForSeconds(FadeEffect.instance.FadeTime + .5f);
            MainMenuManager.instance.StartGame();
        }
        else
        {
            //Reset
            isMoving = false;
            transform.position = initialPosition;
            transform.localScale = initialScale;
            StopAllCoroutines();
        }
        
    }
}
