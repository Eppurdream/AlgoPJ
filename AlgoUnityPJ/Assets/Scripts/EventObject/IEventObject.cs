using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventObject // 이벤트가 있는 오브젝트들에게 붙이는 interface
{
    public List<Scenario> GetScenario();
}
