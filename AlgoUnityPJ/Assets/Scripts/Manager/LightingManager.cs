using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class LightingManager : MonoBehaviour
{
    public static LightingManager instance;

    public Light2D globalLight;

    public Light2D[] corridorLights;
    public Image[] lightImgs;

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

    public bool SetCorridorLight(List<bool> onOff)
    {
        bool isAllTrue = true;
        for(int i = 0; i < corridorLights.Length; i++)
        {
            if(onOff[i])
            {
                corridorLights[i].gameObject.SetActive(!corridorLights[i].gameObject.activeSelf);
                lightImgs[i].color = corridorLights[i].gameObject.activeSelf ? Color.yellow : Color.black;
            }

            if(!corridorLights[i].gameObject.activeSelf)
            {
                isAllTrue = false;
            }
        }

        return isAllTrue;
    }
}
