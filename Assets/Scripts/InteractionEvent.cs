using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    public int lineY;
    public int s_lineY;

    [SerializeField] DialogueEvent dialogue;
    [SerializeField] SelectEvent select;

    public Dialogue[] GetDialogues()
    {
        dialogue.dialogues = DatabaseManager.Instance.GetDialogue(1, lineY);
        return dialogue.dialogues;
    }

    public SelectDialogue[] GetSelectes()
    {
        select.selecter = DatabaseManager.Instance.GetSelect(1, s_lineY);
        return select.selecter;
    }
}