using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public Button[] backButtons;
    public Button settingButton;
    public Button exitButton;
    public Button startButton;
    public Button creditButton;
    public Button shotdownButton;


    private void Start()
    {
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
}
