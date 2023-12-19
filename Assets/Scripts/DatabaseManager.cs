using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance;

    [SerializeField] string csv_dialogueFile;
    [SerializeField] string csv_eventFile;

    Dictionary<int, Dialogue> dialogueDic = new Dictionary<int, Dialogue>();
    Dictionary<int, SelectDialogue> eventDic = new Dictionary<int, SelectDialogue>();

    public static bool isFinish = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DialogueParser dialogueParser = GetComponent<DialogueParser>();
            Dialogue[] dialogues = dialogueParser.Parse(csv_dialogueFile);
            EventDialogueParser eventdialogueParser = GetComponent<EventDialogueParser>();
            SelectDialogue[] selects = eventdialogueParser.Parse(csv_eventFile);

            for (int i = 0; i < dialogues.Length; i++)
                dialogueDic.Add(i + 1, dialogues[i]);

            for (int i = 0; i < selects.Length; i++)
                eventDic.Add(i + 1, selects[i]);

            isFinish = true;
        }
    }

    public Dialogue[] GetDialogue(int StartNum, int EndNum)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();

        for (int i = 0; i <= EndNum - StartNum; i++)
        {
            dialogueList.Add(dialogueDic[StartNum + i]);
        }

        return dialogueList.ToArray();
    }

    public SelectDialogue[] GetSelect(int StartNum, int EndNum)
    {
        List<SelectDialogue> selectList = new List<SelectDialogue>();

        for (int i = 0; i <= EndNum - StartNum; i++)
        {
            selectList.Add(eventDic[StartNum + i]);
        }

        return selectList.ToArray();
    }
}
