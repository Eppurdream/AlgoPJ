using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrinterEvent : MonoBehaviour, IEventObject
{
    public List<Scenario> GetScenario()
    {
        // ��µ� ���� Ȱ��ȭ
        UIManager.instance.OpenPanel(UIManager.instance.printerPanel);
        return null;
    }
}
