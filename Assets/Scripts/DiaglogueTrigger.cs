using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Trigger new dialogue
public class DiaglogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public void TriggerDialogue() {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
