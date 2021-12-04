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
            Debug.LogError("MessageManager�� ������ �����Ǿ����ϴ�");
        }
        instance = this;
    }

    private void Start()
    {
        loadTexts = Resources.LoadAll<TextAsset>("Scenarios");
        
        SetScenario();
    }

    public void SetScenario() // Resources���� �����°� ��ȯ
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

    public List<Scenario> GetScenario(string str) // �ٸ� Ŭ�������� ���ϴ°� �ִ°�
    {
        // ���������� cvs�� ��ȯ�ؼ� �����ö� ���ܸ��� "\r\n"�� �ٱ⿡
        // �̸� Ȯ���ϰ� ó���Ѵ�
        return messageDic[str + "\r"];
    }
}
