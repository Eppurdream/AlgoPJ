using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool inGame;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
            return;
            //Debug.Log("GameManager�� ������ �����Ǿ����ϴ�");
        }
        instance = this;

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        inGame = SceneManager.GetActiveScene().name == "GameScene";
    }

    public void Pause()
    {
        Time.timeScale = 0;
        InputManager.instance.BindingKey();
    }

    public void DePause()
    {
        Time.timeScale = 1;

        if(!DialogManager.instance.isStartDialog())
        {
            InputManager.instance.DeBindingKey();
        }
    }

}
