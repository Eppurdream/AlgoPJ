using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvent : MonoBehaviour
{
    public LayerMask whatIsEventObj;

    public Transform playerEventPoint;
    

    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.C))
        //{
        //    List<Scenario> list = ScenarioManager.instance.GetMessage("test");


        //    DialogManager.instance.StartDialogCorutine(list);

        //    list = ScenarioManager.instance.GetMessage("test2");


        //    DialogManager.instance.StartDialogCorutine(list);
        //}

        if (InputManager.instance.EventKeyDown)
        {
            Collider2D col = Physics2D.OverlapCircle(playerEventPoint.position, 0.3f, whatIsEventObj);

            if(col != null)
            {
                IEventObject iEvent = col.GetComponent<IEventObject>();

                List<Scenario> scenarioList = iEvent.GetScenario();

                DialogManager.instance.StartDialogCorutine(scenarioList);
            }
        }
    }
}
