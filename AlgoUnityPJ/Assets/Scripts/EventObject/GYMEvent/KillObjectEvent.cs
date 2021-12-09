using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillObjectEvent : MonoBehaviour, IEventObject
{
    public bool isDead = false; // 죽었나 살았나
    public List<Scenario> GetScenario()
    {
        isDead = true;

        return null;
    }
}
