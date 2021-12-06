using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneMoveManager : MonoBehaviour
{
    public static SceneMoveManager instance;

    public string sceneName;

    public Canvas fadeCvs;
    public Image fadeImg;


    private void Awake()
    {
        if(instance != null)
        {
            //Debug.LogError("SceneMoveManager가 여러개 생성되었습니다");
            Destroy(this);
            return;
        }
        instance = this;

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        GameObject g = Instantiate(fadeCvs.gameObject);
        DontDestroyOnLoad(g);
        fadeCvs = g.GetComponent<Canvas>();
        fadeImg = g.GetComponentInChildren<Image>();

        fadeCvs.gameObject.SetActive(false);
    }

    public void SceneMove(string sceneName)
    {
        StartCoroutine(SceneMoveCo(sceneName));
    }

    IEnumerator SceneMoveCo(string sceneName)
    {
        float alpha = 0;
        fadeCvs.gameObject.SetActive(true);

        while (alpha < 1)
        {
            alpha += Time.fixedDeltaTime;
            fadeImg.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        
        SceneManager.LoadScene(sceneName);

        while(alpha > 0)
        {
            alpha -= Time.fixedDeltaTime;
            fadeImg.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadeCvs.gameObject.SetActive(false);
        InputManager.instance.bindkey = false;
        Time.timeScale = 1;
    }
}
