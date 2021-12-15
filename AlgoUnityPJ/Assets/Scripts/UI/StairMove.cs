using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairMove : MonoBehaviour
{
    public Transform point;
    private BoxCollider2D boxCol;

    bool check = false;

    private void Start()
    {
        boxCol = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if(boxCol.isTrigger)
        {
            Collider2D col = Physics2D.OverlapBox(transform.position, boxCol.size, 0, PlayerManager.instance.whatIsPlayer);

            if (col != null)
            {
                PlayerManager.instance.playerObj.transform.position = point.position;
            }
        }
        else
        {
            Collider2D col = Physics2D.OverlapBox(transform.position, boxCol.size, 0, PlayerManager.instance.whatIsPlayer);

            if(col != null && !check)
            {
                DialogManager.instance.StartDialogCoroutine(ScenarioManager.instance.GetScenario("threeFloor"));
                check = true;
            }else if(col == null)
            {
                check = false;
            }

        }
    }
}
