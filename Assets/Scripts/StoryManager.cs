using System.Collections;
using UnityEngine;
using TMPro;

public class StoryManager : MonoBehaviour
{
    public static StoryManager instance = null;

    [SerializeField] DialogueEvent Script;
    [SerializeField] SelectEvent Event;

    [SerializeField] TextMeshProUGUI ScriptText;

    [SerializeField] GameObject Choice;

    [SerializeField] int eventNum;
    int waitingTime;     // next line waitng

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        ScriptText = GameObject.Find("InfiltrationScreen").transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        Choice = GameObject.Find("InfiltrationScreen").transform.GetChild(0).transform.GetChild(1).transform.GetChild(2).gameObject;

        // load data
        UserDataManager.instance.LoadUserData();
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

    IEnumerator MainFile1()
    {
        Script.name = "MainFile1";
        Script.dialogues = GameObject.Find("DialogueManager").GetComponent<InteractionEvent>().GetDialogues();
        Event.name = "Event";
        Event.selecter = GameObject.Find("DialogueManager").GetComponent<InteractionEvent>().GetSelectes();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        UserDataManager.instance.LoadUserData();
        UserDataManager.instance.userData.f_val = 0.5f;
        UserDataManager.instance.userData.w_val = 0.5f;
        UserDataManager.instance.userData.currentLine = 0;
        UserDataManager.instance.SaveUserData();

        while (UserDataManager.instance.userData.currentLine < Script.dialogues.Length)
        {
            eventNum = int.Parse(Script.dialogues[UserDataManager.instance.userData.currentLine].eventid);
            Debug.Log(eventNum);

            if (eventNum > 0 && Event.selecter[eventNum - 1].note == "start")
            {
                int j = eventNum;

                while(Event.selecter[j++].note != "end")
                {
                    Choice.transform.GetChild(j - eventNum).gameObject.SetActive(true);
                    Choice.transform.GetChild(j - eventNum).GetChild(0).GetComponent<TextMeshProUGUI>().text = Event.selecter[j].contexts;
                }

                if (Physics.Raycast(ray, out hit))
                {
                    switch (hit.transform.gameObject.name)
                    {
                        case "Selection1":
                            UserDataManager.instance.userData.currentLine = int.Parse(Event.selecter[eventNum + 0].eventid); break;

                        case "Selection2":
                            UserDataManager.instance.userData.currentLine = int.Parse(Event.selecter[eventNum + 1].eventid); break;

                        case "Selection3":
                            UserDataManager.instance.userData.currentLine = int.Parse(Event.selecter[eventNum + 2].eventid); break;

                        case "Selection4":
                            UserDataManager.instance.userData.currentLine = int.Parse(Event.selecter[eventNum + 3].eventid); break;
                    }
                }
            }
            else if(eventNum > 0 && Script.dialogues[UserDataManager.instance.userData.currentLine].note == "minigame")
            {

            }

            UserDataManager.instance.LoadUserData();
            yield return StartCoroutine(click_initLine(Script.dialogues[UserDataManager.instance.userData.currentLine++].contexts));
            UserDataManager.instance.SaveUserData();
        }


        UserDataManager.instance.LoadUserData();
        UserDataManager.instance.userData.isClear = 1;
        UserDataManager.instance.userData.f_current_val = UserDataManager.instance.userData.f_val;
        UserDataManager.instance.userData.w_current_val = UserDataManager.instance.userData.w_val;
        UserDataManager.instance.SaveUserData();

        yield return StartCoroutine(wait_initLine("wait seconds. coming mainscreen soon"));
        SceneChanger.instance.InMainScreen();
    }

    IEnumerator click_initLine(string line)             // next line with click
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

    IEnumerator wait_initLine(string line)              // next line after few seconds
    {
        yield return new WaitForSeconds(waitingTime);
    }
}