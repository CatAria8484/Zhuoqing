using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [Tooltip("CharictorName")]
    public string name;

    [Tooltip("Context")]
    public string contexts;

    [Tooltip("EventID")]
    public string eventid;

    [Tooltip("Note")]
    public string note;
}

[System.Serializable]
public class DialogueEvent
{
    public string name;

    public Dialogue[] dialogues;
}