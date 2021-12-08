using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvent : MonoBehaviour
{
    public LayerMask whatIsEventObj;

    public Transform playerEventPoint;
    

    void Update()
    {
        if (InputManager.instance.EventKeyDown)
        {
            Collider2D col = Physics2D.OverlapCircle(playerEventPoint.position, 0.3f, whatIsEventObj);

            if(col != null)
            {
                IEventObject iEvent = col.GetComponent<IEventObject>();

                List<Scenario> scenarioList = iEvent.GetScenario();

                if(scenarioList != null)
                {
                    DialogManager.instance.StartDialogCorutine(scenarioList);
                }
            }
        }
    }
}
