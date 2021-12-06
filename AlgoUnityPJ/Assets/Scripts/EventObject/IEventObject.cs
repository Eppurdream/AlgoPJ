using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventObject // 이벤트 키를 눌렀을 때 반응하는 오브젝트
{
    public List<Scenario> GetScenario();
}
