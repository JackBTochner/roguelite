using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float speed;
    private bool restoreTime;

    Coroutine startTimeAgain;

    [Header("Broadcasting on")]
    public TimeManagerAnchor timeManagerAnchor;

    private void OnEnable()
    {
        timeManagerAnchor.Provide(this);
    }

    private void Start()
    {
        restoreTime = false;
    }

    private void Update()
    {
        if (restoreTime)
        {
            if (Time.timeScale < 1f)
            {
                Time.timeScale += Time.deltaTime * speed;
            }
            else
            {
                Time.timeScale = 1f;
                restoreTime = false;
            }
        }
    }

    public void SetTimeScale(float timeScale, float restoreSpeed, float delay)
    {
        speed = restoreSpeed;

        if (delay > 0)
        {
            if (startTimeAgain != null)
            { 
                StopCoroutine(startTimeAgain);
            }
            
            startTimeAgain = StartCoroutine(StartTimeAgain(delay));
        }
        else
        {
            restoreTime = true;
        }
        Time.timeScale = timeScale;
    }

    IEnumerator StartTimeAgain(float delay)
    {
        restoreTime = true;
        yield return new WaitForSecondsRealtime(delay);
    }
}
