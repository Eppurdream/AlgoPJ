using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtEvent : MonoBehaviour, IEventObject
{
    public float timeCount = 12;
    public Text timeText;
    public BoxCollider2D boxCol;
    public LayerMask whatIsPlayer;

    private float currentTime = 0;

    public List<Scenario> GetScenario()
    {
        StartCoroutine(ArtEventCo());
        return null;
    }

    IEnumerator ArtEventCo()
    {
        UIManager.instance.OpenPanel(UIManager.instance.artPanel);
        yield return new WaitForSecondsRealtime(3f);
        UIManager.instance.ClosePanel();
        LightingManager.instance.OffGlobalLight();
        PlayerManager.instance.lightPower = 0.1f;
        currentTime = 0;

        while (true)
        {
            currentTime += Time.deltaTime;
            timeText.text = Mathf.RoundToInt(timeCount - currentTime).ToString();

            if (currentTime >= timeCount)
            {
                Fail();
                break;
                // 게임 오버..
            }
            else if(Physics2D.OverlapBox(boxCol.transform.position, boxCol.size, 0, whatIsPlayer) != null)
            {
                Success();
                break;
            }


            yield return null;
        }
    }

    void Success()
    {
        timeText.text = "";
        LightingManager.instance.OnGlobalLight();
        PlayerManager.instance.lightPower = 0.45f;
    }

    void Fail()
    {
        SceneMoveManager.instance.SceneMove("TitleScene");
        timeText.text = "";
    }
}
