using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoEvent : MonoBehaviour, IEventObject
{
    public List<Scenario> GetScenario()
    {
        UIManager.instance.OpenPanel(UIManager.instance.pianoPanel);
        return null;
    }
}
