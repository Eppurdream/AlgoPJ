using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class GYMEvent : MonoBehaviour, IEventObject
{
    // 19 * 7
    public GameObject killObjPrefab;
    public GameObject fakeKillObjPrefab;
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
    private List<FakeKillObjectEvent> fakeKillobjList = new List<FakeKillObjectEvent>();

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
        int rend;
        for (int i = 0; i < 7; i++)
        {
            GameObject g = Instantiate(killObjPrefab, killObjParent);
            rend = Random.Range(0, 19);
            g.transform.position = new Vector3(spawnPoint.x + rend, spawnPoint.y + i);

            killobjList.Add(g.GetComponent<KillObjectEvent>());

            for(int n = 0; n < 2; n++)
            {
                Vector3 fakeSpawnPoint = new Vector3(spawnPoint.x + Random.Range(0, 19), spawnPoint.y + i);

                if(spawnPoint.x + rend != fakeSpawnPoint.x)
                {
                    g = Instantiate(fakeKillObjPrefab, killObjParent);

                    g.transform.position = fakeSpawnPoint;

                    fakeKillobjList.Add(g.GetComponent<FakeKillObjectEvent>());

                    g.SetActive(false);
                }
            }
        }

        PlayerManager.instance.playerObj.SetActive(false);
    }

    IEnumerator GYMMiniGame() // 더러운 코드.... 나중에 다시 찾아올 일은 없을 듯
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

        yield return new WaitForSeconds(1.2f);

        ActiveFakeObjectList();

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

            for (int i = 0; i < fakeKillobjList.Count; i++)
            {
                if (fakeKillobjList[i].isDead)
                {
                    KillThisFakeObject(fakeKillobjList[i]);
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

    void KillThisFakeObject(FakeKillObjectEvent obj)
    {
        Destroy(obj.gameObject);
        fakeKillobjList.Remove(obj);
        // 여기서 뭔갈 해야한다..............
        Debug.Log("님 가짜 죽임..");
    }

    void ActiveFakeObjectList()
    {
        for(int i = 0; i < fakeKillobjList.Count; i++)
        {
            fakeKillobjList[i].gameObject.SetActive(true);
        }
    }

    void Success()
    {
        GYMbg.SetActive(false);
        killobjList.Clear();
        
        for(int i = 0; i < fakeKillobjList.Count; i++)
        {
            Destroy(fakeKillobjList[i].gameObject);
        }

        fakeKillobjList.Clear();

        timeText.text = "";
        LightingManager.instance.OnGlobalLight();
    }

    void Fail()
    {
        GYMbg.SetActive(false);
        killobjList.Clear();

        for (int i = 0; i < fakeKillobjList.Count; i++)
        {
            Destroy(fakeKillobjList[i].gameObject);
        }

        fakeKillobjList.Clear();
        SceneMoveManager.instance.SceneMove("TitleScene");
        timeText.text = "";
    }
}
