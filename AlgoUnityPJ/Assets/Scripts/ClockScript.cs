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

    private float alpha = 0f;
    bool b = false;

    private void Start()
    {
        alpha = clock.color.a;
    }
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

        bgClock.color = b ? new Color(1, 1, 1, alpha) : new Color(0, 0, 0, alpha);
        clock.color = b ? new Color(0, 0, 0, alpha) : new Color(1, 1, 1, alpha);

        clockTime -= oneRotationTime;
        clock.fillAmount = 1;
    }
}
