using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    [SerializeField] DialogueEvent dialogueEvent;

    public Dialogue[] GetDialogue()
    {
        DialogueEvent t_DialogueEvent = new DialogueEvent();
        t_DialogueEvent.dialogues = DatabaseManager.dbinstance.GetDialogue((int)dialogueEvent.line.x, (int)dialogueEvent.line.y);

        for (int i = 0; i < dialogueEvent.dialogues.Length; i++)
        {
            t_DialogueEvent.dialogues[i].tf_Target = dialogueEvent.dialogues[i].tf_Target;
            t_DialogueEvent.dialogues[i].cameraType = dialogueEvent.dialogues[i].cameraType;
        }
        dialogueEvent.dialogues = t_DialogueEvent.dialogues;
        return dialogueEvent.dialogues;
    }

    //Dialogue[] SettingDialogue(Dialogue[] p_Dialogue, int p_lineX, int p_lineY)
    //{
    //    Dialogue[] t_Dialogues = DatabaseManager.dbinstance.GetDialogue(p_lineX, p_lineY);

    //    for (int i = 0; i < dialogueEvent.dialogues.Length; i++)
    //    {
    //        t_Dialogues[i].tf_Target = p_Dialogue[i].tf_Target;
    //        t_Dialogues[i].cameraType = p_Dialogue[i].cameraType;
    //    }

    //    return t_Dialogues;
    //}
}
