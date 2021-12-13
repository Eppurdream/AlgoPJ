using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Buttons : MonoBehaviour
{
    public Button[] backButtons;
    public Button settingButton;
    public Button exitButton;
    public Button startButton;
    public Button creditButton;
    public Button shotdownButton;
    public Button[] lightOnOffButtons;
    public List<List<bool>> lightOnOffs;


    private void Start()
    {
        lightOnOffs = new List<List<bool>>()
        {
            new List<bool>(){ true, false, true, false, true, false, true, false },
            new List<bool>(){ false, true, false, false, true, false, false, false },
            new List<bool>(){ false, false, false, false, false, true, false ,true},
            new List<bool>(){ true, false, false, false, false, false, true ,true},
            new List<bool>(){ false, false, true, true, true, true, false, false}
        };

        if(lightOnOffButtons != null)
        {
            for(int i = 0; i < lightOnOffButtons.Length; i++)
            {
                Button btn = lightOnOffButtons[i];

                List<bool> list = lightOnOffs[i];
                btn.onClick.AddListener(() =>
                {
                    if(LightingManager.instance.SetCorridorLight(list))
                    {
                        // Å¬¸®¾î
                        StartCoroutine(CorridorEventClear());

                    }
                });
            }
        }

        if(backButtons != null)
        {
            foreach (Button btn in backButtons)
            {
                Button button = btn;
                button.onClick.AddListener(() =>
                {
                    UIManager.instance.ClosePanel();
                });
            }
        }

        if(settingButton != null)
        {
            settingButton.onClick.AddListener(() =>
            {
                UIManager.instance.OpenPanel(UIManager.instance.settingPanel);
            });
        }

        if(startButton != null)
        {
            startButton.onClick.AddListener(() =>
            {
                GameManager.instance.inGame = true;
                SceneMoveManager.instance.SceneMove("GameScene");
            });
        }

        if(creditButton != null)
        {
            creditButton.onClick.AddListener(() =>
            {
                UIManager.instance.OpenPanel(UIManager.instance.creditPanel);
            });
        }

        if(exitButton != null)
        {
            exitButton.onClick.AddListener(() =>
            {
                GameManager.instance.inGame = false;
                SceneMoveManager.instance.SceneMove("TitleScene");
            });
        }

        if(shotdownButton != null)
        {
            shotdownButton.onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }
    }

    IEnumerator CorridorEventClear()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        UIManager.instance.ClosePanel();
    }
}

