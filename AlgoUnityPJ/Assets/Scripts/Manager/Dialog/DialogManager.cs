using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    public GameObject dialogPanel;
    public Text nameText;
    public Text dialogText;

    public float textDelay = 0.2f;

    private Queue<IEnumerator> waitCorutinesQueue = new Queue<IEnumerator>();
    private bool locking = false;
    private bool firstStart = true;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("DialogManager가 여러개 생성되었습니다");
        }
        instance = this;
    }

    public void StartDialogCorutine(List<Scenario> list)
    {
        StartCoroutine(StartDialog(list));
    }

    IEnumerator StartDialog(List<Scenario> list)
    {
        if(locking) // 이미 다른 코루틴이 실행 중일시 Queue에 넣고 나가기
        {
            waitCorutinesQueue.Enqueue(StartDialog(list));
            firstStart = false;
            yield break;
        }
        if(firstStart)
        {
            dialogPanel.transform.localScale = new Vector3(0.1f, 1, 1);
            dialogPanel.transform.DOScaleX(1, 0.2f).SetEase(Ease.OutCirc);
        }

        locking = true;
        dialogPanel.SetActive(true);
        InputManager.instance.bindingKeys = true;

        for(int i = 0; i < list.Count; i++)
        {
            dialogText.text = "";
            nameText.text = list[i].name;

            for (int n = 0; n < list[i].say.Length; n++)
            {
                dialogText.text += list[i].say[n];
                
                yield return new WaitForSeconds(textDelay);

                if(InputManager.instance.dialogSkipKey) // 대사도중 스킵키 누를시 반복문 탈출
                {
                    InputManager.instance.dialogSkipKey = false;
                    break;
                }
            }
            dialogText.text = list[i].say;

            yield return new WaitUntil(() => Input.anyKeyDown);
            InputManager.instance.dialogSkipKey = false;
        }

        dialogText.text = "";
        nameText.text = "";

        if(waitCorutinesQueue.Count != 0) // 모든 대사 출력이 끝나면 Queue에 들어가 있던 다음 코루틴 실행
        {
            locking = false;
            StartCoroutine(waitCorutinesQueue.Dequeue());
        }
        else // 모든 대사 출력이 끝났음에도 다음 Queue 가 없다면 그냥 나가기
        {
            firstStart = true;
            locking = false;
            dialogPanel.SetActive(false);
            InputManager.instance.bindingKeys = false;
        }
    }
}
