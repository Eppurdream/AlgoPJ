using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroadcastRoomEvent : MonoBehaviour, IEventObject
{
    public BoxCollider2D boxCol;
    public LayerMask whatIsPlayer;

    public List<Scenario> GetScenario()
    {
        StartCoroutine(StartBroadcastTimer());
        return null;
    }

    IEnumerator StartBroadcastTimer()
    {
        float currentTime = 0f;

        while(true)
        {
            Debug.Log("ing...");
            currentTime += Time.deltaTime;

            Collider2D col = Physics2D.OverlapBox(boxCol.transform.position, boxCol.size, 0, whatIsPlayer);

            if(col != null)
            {
                Debug.Log("Ŭ����");
                // Ŭ����
                yield break;
            }

            if (currentTime >= 60f)
            {
                Debug.Log("�� ������");
                // ü�� ����, ���� ����
                yield break;
            }

            yield return null;
        }
    }
}
