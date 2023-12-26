using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class StoryManager : MonoBehaviour
{
    public static StoryManager Instance = null;

    [SerializeField] DialogueEvent Script;
    [SerializeField] SelectEvent Event;

    [SerializeField] TextMeshProUGUI ScriptText;
    [SerializeField] GameObject[] Choice;
    [SerializeField] Sprite[] Sprites;
    Image image;

    bool isTalking = false;
    bool isNext = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        ScriptText = GameObject.Find("InfiltrationScreen").transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        image = GameObject.Find("InfiltrationScreen").transform.GetChild(0).GetComponent<Image>();
    }

    void Update()
    {
        if (isTalking && isNext)
        {
            if (Input.GetMouseButtonDown(0))                    // if clicked, writing next context
            {
                Debug.Log("clicked");

                if (EventSystem.current.currentSelectedGameObject == null || EventSystem.current.currentSelectedGameObject.name != "Back" || EventSystem.current.currentSelectedGameObject.name != "OpenBag")            // if clicked Back button or Bag button, don't write next context
                {
                    if (++UserDataManager.Instance.userData.currentLine < Script.dialogues.Length)
                    {
                        isNext = false;
                        flow_Story();
                    }
                    else                                           // if wrote all context, going main screen after clear dialogues and save user datas
                    {
                        Script = null;
                        Event = null;
                        isTalking = false;
                        isNext = false;

                        UserDataManager.Instance.LoadUserData();
                        if(UserDataManager.Instance.userData.isClear == 0) UserDataManager.Instance.userData.isClear = 1;
                        UserDataManager.Instance.userData.currentFile++;
                        UserDataManager.Instance.userData.currentLine = 0;
                        UserDataManager.Instance.userData.f_current_val = UserDataManager.Instance.userData.f_val;
                        UserDataManager.Instance.userData.w_current_val = UserDataManager.Instance.userData.w_val;
                        UserDataManager.Instance.SaveUserData();

                        SceneChanger.Instance.InMainScreen();
                    }
                }
            }
        }
    }

    public void StartStory()
    {
        UserDataManager.Instance.LoadUserData();

        switch (UserDataManager.Instance.userData.currentFile)
        {
            case 1: Debug.Log("startstory"); MainFile1(); break;
            case 2: ScriptText.text = "MainFile2 hasn't been developed yet."; break;
            case 3: ScriptText.text = "MainFile3 hasn't been developed yet."; break;
        }
    }

    void MainFile1()
    {
        image.sprite = Sprites[0];
        Script.name = "MainFile1";
        Script.dialogues = GameObject.Find("DialogueManager").GetComponent<InteractionEvent>().GetDialogues();
        Event.name = "EventFile1";
        Event.selecter = GameObject.Find("DialogueManager").GetComponent<InteractionEvent>().GetSelectes();

        UserDataManager.Instance.LoadUserData();
        UserDataManager.Instance.userData.f_val = 0.5f;
        UserDataManager.Instance.userData.w_val = 0.5f;
        UserDataManager.Instance.userData.currentLine = 0;
        UserDataManager.Instance.SaveUserData();

        isTalking = true;
        flow_Story();
    }

    void flow_Story()
    {
        ScriptText.text = Script.dialogues[UserDataManager.Instance.userData.currentLine].contexts;

        int num = int.Parse(Script.dialogues[UserDataManager.Instance.userData.currentLine].eventid);
        Debug.Log(num);
        if (num > 0)
        {
            SelectsActive(num - 1);
            return;
        }

        if (Script.dialogues[UserDataManager.Instance.userData.currentLine].note == Script.dialogues[9].note)
        {
            isTalking = false;
            SceneChanger.Instance.Invoke("InGameOverScreen", 1);
        }

        isNext = true;
        Debug.Log("flowstory");
    }

    void SelectsActive(int num)
    {
        for (int i = num; ; i++)
        {
            Debug.Log(Event.selecter[i].note);

            Choice[i - num].gameObject.SetActive(true);
            Choice[i - num].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Event.selecter[i].contexts;

            if (Event.selecter[i].note == Event.selecter[2].note)
                break;
        }
        Debug.Log("active");
    }

    void SelectsDeactive()
    {
        for(int i = 0; i < Choice.Length; i++)
            Choice[i].gameObject.SetActive(false);
        Debug.Log("deactive");
    }

    public void choice1()
    {
        UserDataManager.Instance.userData.currentLine = int.Parse(Event.selecter[int.Parse(Script.dialogues[UserDataManager.Instance.userData.currentLine].eventid) - 1].eventid) - 1;
        SelectsDeactive();
        flow_Story();
    }

    public void choice2()
    {
        UserDataManager.Instance.userData.currentLine = int.Parse(Event.selecter[int.Parse(Script.dialogues[UserDataManager.Instance.userData.currentLine].eventid)].eventid) - 1;
        SelectsDeactive();
        flow_Story();
    }

    public void choice3()
    {
        UserDataManager.Instance.userData.currentLine = int.Parse(Event.selecter[int.Parse(Script.dialogues[UserDataManager.Instance.userData.currentLine].eventid) + 1].eventid) - 1;
        SelectsDeactive();
        flow_Story();
    }

    public void choice4()
    {
        UserDataManager.Instance.userData.currentLine = int.Parse(Event.selecter[int.Parse(Script.dialogues[UserDataManager.Instance.userData.currentLine].eventid) + 2].eventid) - 1;
        SelectsDeactive();
        flow_Story();
    }
}