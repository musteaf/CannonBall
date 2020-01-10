using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private static TimeManager instance;

    private float fixedDeltaTime;
    private bool slowDowned = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        this.fixedDeltaTime = Time.fixedDeltaTime;
    }

    public void SlowDownTime()
    {
        if (!slowDowned)
        {
            Time.timeScale = 0.2f;
            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
            slowDowned = true;
            StartCoroutine(WaitAndSpeedUpTime(0.3f));
        }
    }
    
    IEnumerator WaitAndSpeedUpTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SpeedUpTime();
    }

    public void SpeedUpTime()
    {
        if (slowDowned)
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
            slowDowned = false;
        }
    }
}