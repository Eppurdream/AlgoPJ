using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Transform rootPausePanel;
    public Transform selectPanel;
    public Transform settingPanel;
    public Transform creditPanel;
    public Transform bcpuzzlePanel;

    private Stack<Transform> pauseStack = new Stack<Transform>(); // panel들을 queue에서 관리

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("UIManager가 여러개 생성되었습니다");
        }
        instance = this;
    }

    private void Update()
    {
        if(InputManager.instance.ESC)
        {
            if(pauseStack.Count == 0)
            {
                OpenPanel(selectPanel);
            }
            else
            {
                ClosePanel();
            }
        }
    }

    public void OpenPanel(Transform panel)
    {
        if(panel.gameObject.activeSelf)
        {
            return;
        }

        if(pauseStack.Count >= 1)
        {
            pauseStack.Peek().gameObject.SetActive(false);
        }
        else
        {
            GameManager.instance.Pause();
            if(rootPausePanel != null)
            {
                rootPausePanel.gameObject.SetActive(true);
            }
        }

        panel.gameObject.SetActive(true);
        pauseStack.Push(panel);
    }

    public void ClosePanel()
    {
        if(pauseStack.Count >= 1)
        {
            pauseStack.Pop().gameObject.SetActive(false);

            if(pauseStack.Count != 0)
            {
                pauseStack.Peek().gameObject.SetActive(true);
            }
            else
            {
                GameManager.instance.DePause();
                if(rootPausePanel != null)
                {
                    rootPausePanel.gameObject.SetActive(false);
                }
            }
        }
    }
}
