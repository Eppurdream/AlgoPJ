using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scenario
{
    public string name;
    public string say;

    public Scenario(string name, string sayScript)
    {
        this.name = name;
        this.say = sayScript;
    }
}

public class ScenarioManager : MonoBehaviour
{
    public static ScenarioManager instance;

    public Dictionary<string, List<Scenario>> messageDic = new Dictionary<string, List<Scenario>>();

    private TextAsset[] loadTexts;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("MessageManager가 여러개 생성되었습니다");
        }
        instance = this;
    }

    private void Start()
    {
        loadTexts = Resources.LoadAll<TextAsset>("Scenarios");
        
        SetScenario();
    }

    public void SetScenario() // Resources에서 가져온걸 변환
    {
        messageDic.Clear();

        string[] temp;
        string scriptName = "";
        int pointer = 0;

        for (int i = 0; i < loadTexts.Length; i++)
        {
            string[] strs = loadTexts[i].text.Split('\n');

            for (int n = 0; n < strs.Length; n++)
            {
                temp = strs[n].Split(',');

                if (temp[0] == "") break;

                if (n == pointer)
                {
                    pointer += int.Parse(temp[0]) + 1;
                    scriptName = temp[1];
                    messageDic[scriptName] = new List<Scenario>();
                }
                else
                {
                    messageDic[scriptName].Add(new Scenario(temp[0], temp[1]));
                }
            }
        }
    }

    public List<Scenario> GetScenario(string str) // 다른 클래스에서 원하는걸 주는거
    {
        // 엑셀파일을 cvs로 변환해서 가져올때 문단마다 "\r\n"이 붙기에
        // 이를 확실하게 처리한다
        return messageDic[str + "\r"];
    }
}
