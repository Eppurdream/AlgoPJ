using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletDeadRoomEvent : MonoBehaviour, IEventObject
{
    public string eventName;
    public List<Scenario> GetScenario()
    {
        // 여기서 음기 깎기
        StartCoroutine(Waiter());
        return ScenarioManager.instance.GetScenario(eventName);
    }

    IEnumerator Waiter()
    {
        UIManager.instance.OpenPanel(UIManager.instance.toiletPanel);
        yield return new WaitForSecondsRealtime(2f);
        UIManager.instance.ClosePanel();
    }
}
