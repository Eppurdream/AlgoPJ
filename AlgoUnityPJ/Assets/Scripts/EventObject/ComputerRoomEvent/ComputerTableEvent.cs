using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerTableEvent : MonoBehaviour, IEventObject
{
    public List<Scenario> GetScenario()
    {
        PasswordManager.instance.OpenPW();
        return null;
    }
}
