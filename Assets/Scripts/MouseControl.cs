using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MouseControl : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public string text;
    public Texture2D cursor;
    public Texture2D cursorDefault;

    void Update() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            if (nameText) nameText.text = "";
            Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.Auto);    
        }
    }

    public void OnMouseOver() {
        if (!EventSystem.current.IsPointerOverGameObject()) {
            if (nameText) nameText.text = text;
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        }
    }

    private void OnMouseExit() {
        if (nameText && nameText.text != "continue") {
            nameText.text = "";
        }
        Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.Auto);    
    }

    void Start() {
        // Time.timeScale = 1;
        if (nameText && nameText.text != "continue")
            nameText.text = "";
        Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.Auto);    
    }
}
