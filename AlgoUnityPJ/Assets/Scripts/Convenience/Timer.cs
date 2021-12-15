using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer Instance;

    public float time = 0;

    bool isStartTimer = false;

    private void Awake()
    {
        if(Instance == null) Instance = this;
    }

    private void Update()
    {
        if(isStartTimer)
        {
            time += Time.deltaTime;
        }
    }

    public void StartTimer()
    {
        isStartTimer = true;
    }
    public void StopTimer()
    {
        isStartTimer = false;
    }
    public void ReStartTimer()
    {
        time = 0;
        isStartTimer = true;
    }
}
