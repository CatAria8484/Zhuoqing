using System.Collections;
using UnityEngine;
using TMPro;

public class StoryManager : MonoBehaviour
{
    public static StoryManager instance = null;

    TextMeshProUGUI ScriptText;

    int waitingTime;     // next line waitng

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        ScriptText = GameObject.Find("InfiltrationScreen").transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        // load data
        DataManager.instance.LoadUserData();
    }

    public void StartStory(int WhatFile)
    {
        Debug.Log(WhatFile);
        if (WhatFile == 1)
        {
            StartCoroutine(MainFile1());
        }
        else if (WhatFile == 2)
        {
            ScriptText.text = "MainFile2 hasn't been developed yet.";
        }
        else if (WhatFile == 3)
        {
            ScriptText.text = "MainFile3 hasn't been developed yet.";
        }
    }

    IEnumerator MainFile1()
    {
        waitingTime = 5;
        yield return StartCoroutine(wait_initLine("一个月前姐姐失踪，顾知易发现了姐姐留下的线索，\n他只身一人前去调查，目的只有一个，\n赢得毒枭王梓朔的信任，代替方信石的地位，成为一名“毒贩”。"));
        yield return StartCoroutine(click_initLine("就从方信石开始吧！"));
        waitingTime = 2;
        yield return StartCoroutine(wait_initLine("浊清"));

        DataManager.instance.LoadUserData();
        DataManager.instance.userData.isClear = 1;
        DataManager.instance.SaveUserData();

        yield return StartCoroutine(wait_initLine("wait seconds. coming mainscreen soon"));
        SceneChanger.instance.InMainScreen();
    }

    IEnumerator click_initLine(string line)
    {
        ScriptText.text = line;
        yield return null;

        while (true)
        {
            if (Input.GetMouseButtonUp(0))
                break;
            yield return null;
        }
    }

    IEnumerator wait_initLine(string line)
    {
        ScriptText.text = line;
        yield return new WaitForSeconds(waitingTime);
    }
}