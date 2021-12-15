using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteEvent : MonoBehaviour, IEventObject
{
    //public string pianoEventName;
    public List<Scenario> GetScenario()
    {
        UIManager.instance.OpenPanel(UIManager.instance.pianoNotePanel);
        return null;
        //return ScenarioManager.instance.GetScenario(pianoEventName);
    }
}
