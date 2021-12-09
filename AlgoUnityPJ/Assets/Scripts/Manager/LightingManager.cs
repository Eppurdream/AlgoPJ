using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightingManager : MonoBehaviour
{
    public static LightingManager instance;

    public Light2D globalLight;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void OnGlobalLight(float speed = 1)
    {
        StartCoroutine(OnToValue(1, speed));
    }

    public void OffGlobalLight(float speed = 1)
    {
        StartCoroutine(OnToValue(0, speed));
    }
    IEnumerator OnToValue(int value, float speed)
    {
        bool isMin = globalLight.intensity - value < 0 ? true : false;

        while(true)
        {

            if(isMin)
            {
                globalLight.intensity += Time.deltaTime * speed;
                if (globalLight.intensity >= value)
                {
                    globalLight.intensity = value;
                    break;
                }
            }
            else
            {
                globalLight.intensity -= Time.deltaTime * speed;
                if (globalLight.intensity <= value)
                {
                    globalLight.intensity = value;
                    break;
                }
            }

            yield return null;
        }
    }
}
