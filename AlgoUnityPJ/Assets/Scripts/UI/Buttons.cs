using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public Button[] backButtons;
    public Button settingButton;
    public Button exitButton;


    private void Start()
    {
        foreach(Button btn in backButtons)
        {
            Button button = btn;
            button.onClick.AddListener(() =>
            {
                UIManager.instance.ClosePanel();
            });
        }
        //backButtons.onClick.AddListener(() =>
        //{
        //    UIManager.instance.ClosePanel();
        //});

        settingButton.onClick.AddListener(() =>
        {
            UIManager.instance.OpenPanel(UIManager.instance.settingPanel);
        });

        exitButton.onClick.AddListener(() =>
        {
            // 그 처음 씬으로 돌아가기
        });
    }
}
