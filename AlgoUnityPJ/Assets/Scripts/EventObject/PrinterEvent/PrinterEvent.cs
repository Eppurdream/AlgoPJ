using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrinterEvent : MonoBehaviour, IEventObject
{
    public List<Scenario> GetScenario()
    {
        // 출력된 사진 활성화
        UIManager.instance.OpenPanel(UIManager.instance.printerPanel);
        return null;
    }
}
