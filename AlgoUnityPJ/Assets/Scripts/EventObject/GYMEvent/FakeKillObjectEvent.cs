using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeKillObjectEvent : MonoBehaviour, IEventObject
{
    public bool isDead = false; // �׾��� ��ҳ�
    public List<Scenario> GetScenario()
    {
        isDead = true;

        // ��¥�� �׿��� �� �ƹ��ų� �ϰ���
        // 1. ���ʵ��� ����
        // 2. hp ����
        // ���� �Ѵ� ���ƺ��̴µ� �� �� �ұ�?
        // ���߿� �̷��� ���� ���ְ��� ����

        return null;
    }
}
