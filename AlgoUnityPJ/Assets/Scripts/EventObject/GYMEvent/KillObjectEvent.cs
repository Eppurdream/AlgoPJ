using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillObjectEvent : MonoBehaviour, IEventObject
{
    public bool isDead = false; // �׾��� ��ҳ�
    public List<Scenario> GetScenario()
    {
        isDead = true;

        return null;
    }
}
