using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public float horizontal { get; private set; }
    public float vertical { get; private set; }

    public bool W { get; private set; }
    public bool A { get; private set; }
    public bool S { get; private set; }
    public bool D { get; private set; }

    public bool EventKeyDown { get; private set; }
    public bool isMoving { get; private set; }
    public bool ESC { get; private set; }

    public bool dialogSkipKey { get; set; }
    public bool bindkey { get; set; }


    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("InputManager가 여러개 생성되었습니다");
        }
        instance = this;
    }

    private void Update()
    {
        if (GameManager.instance.inGame)
        {
            ESC = Input.GetKeyDown(KeyCode.Escape); // esc는 언제 어디서나 작동 되야하기에 bindKey에 영향 받지 않는다
        }

        if (bindkey)
        {
            isMoving = false;
            horizontal = 0;
            vertical = 0;
            EventKeyDown = false;
            dialogSkipKey = dialogSkipKey == true ? true : Input.anyKeyDown;
            W = false;
            A = false;
            S = false;
            D = false;
            return;
        }
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        EventKeyDown = Input.GetKeyDown(KeyCode.Space);
        isMoving = horizontal != 0 || vertical != 0;
        dialogSkipKey = false;

        W = Input.GetKeyDown(KeyCode.W);
        A = Input.GetKeyDown(KeyCode.A);
        S = Input.GetKeyDown(KeyCode.S);
        D = Input.GetKeyDown(KeyCode.D);
    }

    public void BindingKey()
    {
        bindkey = true;
    }

    public void DeBindingKey()
    {
        bindkey = false;
    }
}
