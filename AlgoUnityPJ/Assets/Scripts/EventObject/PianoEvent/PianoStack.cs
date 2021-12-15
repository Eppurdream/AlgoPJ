using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PianoStack : MonoBehaviour
{
    public List<Button> pianoKeys = new List<Button>();

    private Stack<int> passwordStack = new Stack<int>();

    private List<int> password = new List<int>() { 0, 1, 6, 5, 3, 2 };

    private void Start()
    {
        for(int i = 0; i < pianoKeys.Count; i++)
        {
            int value = i;
            pianoKeys[i].onClick.AddListener(() =>
            {
                StackPush(value);
            });
        }
    }

    void StackPush(int value)
    {
        if (passwordStack.Count == password.Count) passwordStack.Clear();

        passwordStack.Push(value);

        if(passwordStack.Peek() != password[passwordStack.Count - 1])
        {
            passwordStack.Clear();
        }
        else if(passwordStack.Count == 6)
        {
            UIManager.instance.ClosePanel();    
            Debug.Log("¼º°ø");
        }
    }
}
