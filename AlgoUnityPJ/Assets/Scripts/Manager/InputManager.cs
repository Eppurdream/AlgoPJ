using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public float horizontal { get; private set; }
    public float vertical { get; private set; }

    public bool EventKeyDown { get; private set; }
    public bool isMoving { get; private set; }

    public bool dialogSkipKey { get; set; }
    public bool bindingKeys;


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
        if(bindingKeys)
        {
            isMoving = false;
            horizontal = 0;
            vertical = 0;
            EventKeyDown = false;
            dialogSkipKey = dialogSkipKey == true ? true : Input.anyKeyDown;
            return;
        }
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        EventKeyDown = Input.GetKeyDown(KeyCode.Space);
        isMoving = horizontal != 0 || vertical != 0;
        dialogSkipKey = false;
    }
}
