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

    public void OnGlobalLight()
    {
        StartCoroutine(OnToValue(1));
    }

    public void OffGlobalLight()
    {
        StartCoroutine(OnToValue(0));
    }
    IEnumerator OnToValue(int value)
    {
        bool isMin = globalLight.intensity - value < 0 ? true : false;

        while(true)
        {

            if(isMin)
            {
                globalLight.intensity += Time.deltaTime;
                if (globalLight.intensity >= value)
                {
                    globalLight.intensity = value;
                    break;
                }
            }
            else
            {
                globalLight.intensity -= Time.deltaTime;
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
