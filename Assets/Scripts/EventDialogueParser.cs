using System.Collections.Generic;
using UnityEngine;

public class EventDialogueParser : MonoBehaviour
{
    public SelectDialogue[] Parse(string CSVFileName)
    {
        List<SelectDialogue> eventdialogueList = new List<SelectDialogue>();                // Creat Dialogue List
        TextAsset csvData = Resources.Load<TextAsset>("csvFile/" + CSVFileName);            // Keep CSV data In
        if (csvData == null) Debug.Log("csvData is not loaded");

        string[] data = csvData.text.Split(new char[] { '\n' });        // split with '\n'

        for (int i = 1; i < data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' });           // split with ','

            SelectDialogue dialogue = new SelectDialogue();             // CharactorName
            string context;                                             // Lines
            string Event;                                               // EventNumber
            string Note;                                                // Note

            do
            {
                dialogue.name = row[1];
                context = row[2];
                Event = row[3];
                Note = row[4];

                if (++i < data.Length)
                    row = data[i].Split(new char[] { ',' });
                else break;
            } while (row[0].ToString() == "");                          // if ID is null, break from while

            dialogue.contexts = context;
            dialogue.eventid = Event;
            dialogue.note = Note;

            eventdialogueList.Add(dialogue);

            GameObject obj = GameObject.Find("DialogueManager");
            obj.GetComponent<InteractionEvent>().s_lineY = eventdialogueList.Count;
        }

        return eventdialogueList.ToArray();
    }
}
