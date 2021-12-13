using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEventObj : MonoBehaviour, IEventObject
{
    public List<string> eventNames;
    public List<Scenario> GetScenario()
    {
        for(int i = 0; i < eventNames.Count; i++)
        {
            DialogManager.instance.StartDialogCoroutine(ScenarioManager.instance.GetScenario(eventNames[i]));
        }

        return null;
    }
}
