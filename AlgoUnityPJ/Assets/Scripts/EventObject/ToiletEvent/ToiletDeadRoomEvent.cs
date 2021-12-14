using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletDeadRoomEvent : MonoBehaviour, IEventObject
{
    public string eventName;
    public List<Scenario> GetScenario()
    {
        // ���⼭ ���� ���
        return ScenarioManager.instance.GetScenario(eventName);
    }
}
