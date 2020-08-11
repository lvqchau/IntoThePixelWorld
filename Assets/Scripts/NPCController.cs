using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCController : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public string name;
    
    public Texture2D cursor;
    public Texture2D cursorDefault;
    // public Dialogue dialogue;
    // public DialogueManager dialogueManager;
    
    // public void TriggerDialogue () {
        // DialogueManager.StartDialogue();
    // }

    void Start() {
        nameText.text = "";
        Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.Auto);    
    }

    public void OnMouseOver() {
        nameText.text = name;
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnMouseExit() {
        nameText.text = "";
        Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.Auto);
    }

    public void OnMouseDown() {
        // TriggerDialogue();
    }
}
