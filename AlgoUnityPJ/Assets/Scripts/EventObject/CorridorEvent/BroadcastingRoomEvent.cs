using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroadcastingRoomEvent : MonoBehaviour, IEventObject
{
    public List<Scenario> GetScenario()
    {
        Event();

        return null;
    }

    public void Event()
    {
        UIManager.instance.OpenPanel(UIManager.instance.bcpuzzlePanel);
    }
}
