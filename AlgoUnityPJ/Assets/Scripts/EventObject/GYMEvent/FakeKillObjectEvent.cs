using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeKillObjectEvent : MonoBehaviour, IEventObject
{
    public bool isDead = false; // 죽었나 살았나
    public List<Scenario> GetScenario()
    {
        isDead = true;

        // 가짜를 죽였을 때 아무거나 하겠지
        // 1. 몇초동안 스턴
        // 2. hp 감소
        // 몰라 둘다 좋아보이는데 둘 다 할까?
        // 나중에 미래의 내가 해주겠지 ㅎㅎ

        return null;
    }
}
