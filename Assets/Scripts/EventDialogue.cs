using UnityEngine;

[System.Serializable]
public class SelectDialogue
{
    [Tooltip("CharictorName")]
    public string name;

    [Tooltip("Context")]
    public string contexts;

    [Tooltip("MoveLine")]
    public string eventid;

    [Tooltip("Note")]
    public string note;
}

[System.Serializable]
public class SelectEvent
{
    public string name;

    public SelectDialogue[] selecter;
}
