using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class GYMEvent : MonoBehaviour, IEventObject
{
    // 19 * 7
    public GameObject killObjPrefab;
    public Transform killObjParent;
    public Transform killObjSpawnPoint;
    public int killObjCount = 7;
    public Text timeText;
    public CinemachineVirtualCamera GYMVirCam;

    public float timeCount = 30;

    public GameObject GYMbg;

    private int currentkillObjCount = 0;
    private float timer = 0;

    private List<KillObjectEvent> killobjList = new List<KillObjectEvent>();

    public List<Scenario> GetScenario()
    {
        Init();
        StartCoroutine(GYMMiniGame());

        return null;
    }

    public void Init()
    {
        for(int i = 0; i < killobjList.Count; i++)
        {
            Destroy(killobjList[i].gameObject);
        }

        killobjList.Clear();
        GYMbg.SetActive(true);
        currentkillObjCount = 0;
        timer = 0;

        Vector3 spawnPoint = killObjSpawnPoint.position;
        for (int i = 0; i < killObjCount; i++)
        {
            GameObject g = Instantiate(killObjPrefab, killObjParent);
            g.transform.position = new Vector3(spawnPoint.x + Random.Range(0, 19), spawnPoint.y + i);

            killobjList.Add(g.GetComponent<KillObjectEvent>());
        }

        PlayerManager.instance.playerObj.SetActive(false);
    }

    IEnumerator GYMMiniGame()
    {
        bool isbreak = false;

        //잠시 보는 시간, 타이머 3초
        GYMVirCam.gameObject.SetActive(true);
        InputManager.instance.BindingKey();
        yield return new WaitForSeconds(3f);
        InputManager.instance.DeBindingKey();
        GYMVirCam.gameObject.SetActive(false);

        PlayerManager.instance.playerObj.transform.position = new Vector3(killObjSpawnPoint.position.x + 1, killObjSpawnPoint.position.y + 5);
        PlayerManager.instance.playerObj.SetActive(true);
        LightingManager.instance.OffGlobalLight();

        // 잡는 시간, 타이머 30초
        while (true)
        {
            timer += Time.deltaTime;
            timeText.text = Mathf.RoundToInt(timeCount - timer).ToString();
            if (timer >= timeCount)
            {
                Fail();
                break;
                // 게임 오버..
            }

            for(int i = 0; i < killobjList.Count; i++)
            {
                if(killobjList[i].isDead)
                {
                    KillThisObject(killobjList[i]);

                    if(currentkillObjCount == killObjCount)
                    {
                        isbreak = true;
                        Success(); // 클리어..
                        break;
                    }
                }
            }

            if (isbreak) break;

            yield return null;
        }
    }

    void KillThisObject(KillObjectEvent obj)
    {
        Destroy(obj.gameObject);
        killobjList.Remove(obj);
        currentkillObjCount++;
    }

    void Success()
    {
        GYMbg.SetActive(false);
        killobjList.Clear();
        timeText.text = "";
        LightingManager.instance.OnGlobalLight();
    }

    void Fail()
    {
        GYMbg.SetActive(false);
        killobjList.Clear();
        SceneMoveManager.instance.SceneMove("TitleScene");
        timeText.text = "";
    }
}
