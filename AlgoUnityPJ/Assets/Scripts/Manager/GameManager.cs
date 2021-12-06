using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool inGame;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("GameManager가 여러개 생성되었습니다");
        }
        instance = this;

        DontDestroyOnLoad(this);
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
