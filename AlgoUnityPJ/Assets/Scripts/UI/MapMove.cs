using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMove : MonoBehaviour, IEventObject
{
    public Transform point;
    public bool isLock;
    public List<Scenario> GetScenario()
    {
        if(isLock)
        {
            return ScenarioManager.instance.GetScenario("lockDoor");
        }
        else
        {
            PlayerManager.instance.playerObj.transform.position = point.position;
        }
        return null;
    }
}
