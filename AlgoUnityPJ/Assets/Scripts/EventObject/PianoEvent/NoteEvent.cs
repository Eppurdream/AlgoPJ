using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteEvent : MonoBehaviour, IEventObject
{
    public string pianoEventName;
    public List<Scenario> GetScenario()
    {
        return ScenarioManager.instance.GetScenario(pianoEventName);
    }
}
