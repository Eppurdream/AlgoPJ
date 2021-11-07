using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockScript : MonoBehaviour
{
    public Image clock;
    public Image bgClock;
    public Text timeTxt;
    public float oneRotationTime;

    public float clockTime = 0f;
    bool b = false;

    // Update is called once per frame
    void Update()
    {
        SetClock();

        clockTime += Time.deltaTime;
    }

    private void SetClock()
    {
        timeTxt.text = clockTime.ToString("0.00");
        clock.fillAmount = 1 - (clockTime / oneRotationTime);

        if(clock.fillAmount <= 0)
        {
            ResetClock();
        }
    }

    private void ResetClock()
    {
        b = !b;

        bgClock.color = b ? Color.white : Color.black;
        clock.color = b ? Color.black : Color.white;

        clockTime -= oneRotationTime;
        clock.fillAmount = 1;
    }
}
