using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    public static MainCanvas thisCvs;
    void Awake()
    {
        if(thisCvs != null)
        {
            Debug.Log("DUUUUUUU");
            Destroy(this);
        }

        Debug.Log("¿¿æ÷");

        DontDestroyOnLoad(this);
        thisCvs = this;
    }
}
