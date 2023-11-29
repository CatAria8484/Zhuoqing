using System.Collections;
using UnityEngine;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class StoryManager : MonoBehaviour
{
    public static StoryManager instance;

    UserData userData;

    TextMeshProUGUI ScriptText;

    public int waitingTime = 5;     // next line waitng

    public int isClear = 0;         // clear what kind of file

    void Start()
    {
        ScriptText = GameObject.Find("InfiltrationScreen").transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        // load data
        LoadUserData();
        isClear = userData.isClear;

        instance = this;
    }

    public void StartStory(int WhatFile)
    {
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

    public void z_Dead()
    {
        ScriptText.text = "z is dead.";
    }

    public void z_getHint()
    {
        ScriptText.text = "z got hint.";
    }

    IEnumerator MainFile1()
    {
        yield return StartCoroutine(wait_initLine("一个月前姐姐失踪，顾知易发现了姐姐留下的线索，\n他只身一人前去调查，目的只有一个，\n赢得毒枭王梓朔的信任，代替方信石的地位，成为一名“毒贩”。"));
        yield return StartCoroutine(click_initLine("就从方信石开始吧！"));
        waitingTime = 2;
        yield return StartCoroutine(wait_initLine("第一章   浊清"));

        isClear = 1;
        instance = this;
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

    void LoadUserData()
    {
        FileStream file;

        try
        {
            file = new FileStream(Application.persistentDataPath + "/userdata.dat", FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            userData = (UserData)binaryFormatter.Deserialize(file);
            file.Close();
        }
        catch (FileNotFoundException exception)
        {
            Debug.Log(exception.Message);
            userData = new UserData();
        }
    }
}