using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordManager : MonoBehaviour
{
    public static PasswordManager instance;

    public InputField pwIF;

    public string computerPW = "TWELVE";

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void OpenPW()
    {
        UIManager.instance.OpenPanel(UIManager.instance.pwPanel);
    }

    public string GetPW()
    {
        return pwIF.text;
    }

    public void FailPW()
    {

    }
}
