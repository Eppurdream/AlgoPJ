using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public GameObject playerObj;

    public float lightPower;

    public Light2D playerLight;

    public LayerMask whatIsPlayer;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        playerLight.intensity = lightPower;
    }
}
