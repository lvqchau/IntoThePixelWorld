using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MouseControl : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Texture2D cursor;
    public Texture2D cursorDefault;

    private void OnMouseOver() {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);    
    }

    private void OnMouseExit() {
        nameText.text = "";
        Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.Auto);    
    }

    void Start() {
        nameText.text = "";
        Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.Auto);    
    }
}
